using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    Idle = 0,
    Jump = 1,
    Run = 2,
    Crouch = 3
}

public enum AttackType
{
    Attack = 0,
    Shoot = 1
}

public class PlayerCharcacter : MonoBehaviour
{
    #region Fields

    //physics
    Rigidbody2D playerRigidbody2D;
    CapsuleCollider2D capsuleCollider;

    //movement
    SpriteRenderer spriteRenderer;
    public float speedX;
    public float speedY;
    public float jumpingTime;

    Animator animator;

    //timer for jump
    private float timerY;

    //timer for dash attack
    float setSpeedXTime;
    float setSpeedXTimer;
    float setSpeedX;

    //status
    public bool isGround;
    public bool isJumping;

    public PlayerStatus currentStatus = PlayerStatus.Idle;

    //animation status
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int SpeedY = Animator.StringToHash("speedY");
    private static readonly int TriggerHurt = Animator.StringToHash("triggerHurt");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsInvincible = Animator.StringToHash("isInvincible");
    private static readonly int TriggerDead = Animator.StringToHash("triggerDead");
    private static readonly int TriggerAttack = Animator.StringToHash("triggerAttack");
    private static readonly int AttackType = Animator.StringToHash("attackType");
    private static readonly int TrrigerAttackTempt = Animator.StringToHash("triggerAttackTempt");
    private static readonly int TriggerFalling = Animator.StringToHash("triggerFalling");
    private static readonly int IsShoot = Animator.StringToHash("isShoot");//0:normal 1:shooting
    
    //camera followings
    Transform followTarget;
    public Vector3 followTargetOffset;
    public Transform startCheckGroundPos;

    //interaction
    PassPlatform currentPlatform;
    Hurtable playerHurtable;
    Damage playerDamage;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    string RebornPos; //reborn position
    float attackTime = 0.4f; // Attack CD
    bool attckIsReady = true; // Attack is ready or not
    AttackRange attackRange;
    Transform bulletSpawnPos;    
    GameObject bulletPrefab;     


    #endregion

    #region Unity Events

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = transform.GetComponent<Rigidbody2D>();
        capsuleCollider = transform.GetComponent<CapsuleCollider2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();
        followTarget = GameObject.Find("FollowTarget").transform;
        followTarget.position = transform.position + followTargetOffset;
        startCheckGroundPos = transform.Find("StartCheckGroundPos");
        playerDamage = transform.GetComponent<Damage>();
        playerHurtable = transform.GetComponent<Hurtable>();
        playerHurtable.OnHurt += this.OnHurt; //register hurt event
        playerHurtable.OnDead += this.OnDead; //register dead event
        GamePanel.Instance.InitHp(playerHurtable.health); //init hp bar
        attackRange = transform.Find("AttackRange").GetComponent<AttackRange>();
        bulletSpawnPos = transform.Find("BulletSpawnPos");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();
        UpdateSetSpeedXWithTime();
        CheckGround();
        UpdateStatus();
        UpdateAnimator();
        UpdateFollowTargetPos();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentPlatform = collision.gameObject.GetComponent<PassPlatform>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagConst.PassPlatform))
        {
            currentPlatform = null;
        }
    }
    
    #endregion

    #region Methods

    public void SetSpeedX(float speed)
    {
        animator.SetBool(IsRunning, speed != 0);
        var attackRangeTransform = attackRange.transform;
        if (speed < 0)
        {
            spriteRenderer.flipX = true;
            
            attackRangeTransform.localPosition = new Vector3(-1.1f, attackRangeTransform.localPosition.y, 0);
        }
        else if (speed > 0)
        {
            spriteRenderer.flipX = false;
            attackRangeTransform.localPosition = new Vector3(1.1f, attackRange.transform.localPosition.y, 0);
        }

        if (currentStatus == PlayerStatus.Crouch)
        {
            speed = 0;
        }

        playerRigidbody2D.velocity = new Vector2(speed, playerRigidbody2D.velocity.y);
    }

    public void SetSpeedXWithTime(float x, float time)
    {
        setSpeedXTime = time;
        setSpeedXTimer = 0;
        setSpeedX = x;
    }
    
    public void UpdateSetSpeedXWithTime() {
        setSpeedXTimer += Time.deltaTime;
        if (setSpeedXTimer < setSpeedXTime)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(setSpeedX, GetComponent<Rigidbody2D>().velocity.y);
        }
    }


    public void SetSpeedY(float speed)
    {
        if (currentStatus == PlayerStatus.Crouch)
        {
            speed = 0;
        }

        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, speed);
    }

    public void CheckGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(startCheckGroundPos.position, Vector3.down, 0.3f, 1 << 8);
        isGround = raycastHit2D;

        if (raycastHit2D)
        {
            if (raycastHit2D.collider.tag == TagConst.SkyGround)
            {
                if (raycastHit2D.point.y < raycastHit2D.transform.position.y +
                    raycastHit2D.transform.GetComponent<BoxCollider2D>().offset.y)
                {
                    isGround = false;
                }
                else
                {
                    isGround = true;
                }
            }
            else
            {
                isGround = true;
            }
        }
        else
        {
            isGround = false;
            // resetPos = true;
        }
    }

    public bool CheckJump()
    {
        if (PlayerInput.instance.Jump.Down && isGround)
        {
            timerY = 0;
            isJumping = true;
        }

        if (PlayerInput.instance.Jump.Held && isJumping)
        {
            timerY += Time.deltaTime;
            if (timerY < jumpingTime)
            {
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }

        if (PlayerInput.instance.Jump.Up)
        {
            isJumping = false;
        }

        return isJumping;
    }

    public void UpdateStatus()
    {
        currentStatus = PlayerStatus.Idle;
        if (playerRigidbody2D.velocity.x != 0)
        {
            currentStatus = PlayerStatus.Run;
        }

        if (!isGround)
        {
            currentStatus = PlayerStatus.Jump;
        }

        if (PlayerInput.instance.Vertical.value == -1 && isGround)
        {
            currentStatus = PlayerStatus.Crouch;
        }

        // When player is on the passable platform
        if (PlayerInput.instance.Vertical.value == -1 && isGround && PlayerInput.instance.Jump.Down)
        {
            // fall down
            if (currentPlatform != null)
            {
                currentPlatform.Fall(gameObject);
                animator.SetTrigger(TriggerFalling);
            }
        }

        // attack
        if (PlayerInput.instance.Attack.Down || PlayerInput.instance.Attack.Held)
        {
            Attack(global::AttackType.Attack);
        }

        // shoot
        if (PlayerInput.instance.Shoot.Down || PlayerInput.instance.Shoot.Held)
        {
            Attack(global::AttackType.Shoot);
        }
    }

    public void UpdateAnimator()
    {
        animator.SetBool(IsJumping, !isGround);
        animator.SetFloat(SpeedY, this.playerRigidbody2D.velocity.y);
        animator.SetBool(IsCrouching, PlayerInput.instance.Vertical.value == -1);
    }

    public void UpdateVelocity()
    {
        //update speed of x axis
        SetSpeedX(PlayerInput.instance.Horizontal.value * speedX);

        //update speed of y axis
        if (CheckJump())
        {
            SetSpeedY(speedY);
        }
    }

    public void UpdateFollowTargetPos()
    {
        //change follow target position when player move
        if (spriteRenderer.flipX)
        {
            // followTarget.position = transform.position - followTargetOffset;
            followTarget.position =
                Vector3.MoveTowards(followTarget.position, transform.position - followTargetOffset, 0.1f);
        }
        else
        {
            followTarget.position =
                Vector3.MoveTowards(followTarget.position, transform.position + followTargetOffset, 0.1f);
        }
    }

    public void UpdateHpData(int hp)
    {
        Data data = DataManager.Instance.GetData(DataConst.hp);
        if (data == null)
        {
            data = new Data<int>();
            ((Data<int>)data).value1 = hp;
            DataManager.Instance.SaveData(DataConst.hp, data);
        }
        else
        {
            ((Data<int>)data).value1 = hp;
        }
    }

    public int GetHpFromData()
    {
        Data<int> data = (Data<int>)DataManager.Instance.GetData(DataConst.hp);

        return data.value1;
    }

    #region Attack Methods

    public void Attack(AttackType attackType)
    {
        if (!IsHaveWeapon()) return; // if player don't have weapon, don't attack
        if (!attckIsReady)
        {
            return;
        } // if attack is not ready, return

        animator.SetTrigger(TriggerAttack);
        animator.SetTrigger(TrrigerAttackTempt);
        animator.SetInteger(AttackType, (int)attackType);

        
        if (attackType == global::AttackType.Attack)
        {
            //if the the attack type is close combat(AttackType.Attack), set the dash speed
            SetSpeedXWithTime(spriteRenderer.flipX ? -7 : 7, 0.2f);
        }
        else
        {
            animator.SetFloat(IsShoot, 1);
            // create bullet
            Invoke(nameof(CreateBullet), 0.02f);
        }

        attckIsReady = false;
        Invoke(nameof(ResetAttackIsReady), attackTime);
    }
    
    public void ResetAttackIsReady() {
        attckIsReady = true;
        animator.SetFloat(IsShoot, 0);
    }

    public bool IsHaveWeapon()
    {
        Data data = DataManager.Instance.GetData(DataConst.IsHaveWeapon);
        if (data != null && ((Data<bool>)data).value1)
        {
            return true;
        }

        return false;
    }
    
    public void AttackDamage() {
        // get the object that player attacks
        GameObject[] hurtableGameObjects = attackRange.GetHurtableGameObjects();
        if (hurtableGameObjects != null && hurtableGameObjects.Length != 0)
        {
            
            playerDamage.OnDamage(hurtableGameObjects);
        }
    }
    
    void CreateBullet() {
        if (bulletPrefab == null) {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Interactable/Bullet");
        }
        GameObject bullet = GameObject.Instantiate(bulletPrefab);

        if (spriteRenderer.flipX)
        {
            var localPosition = bulletSpawnPos.localPosition;
            localPosition = new Vector3(-localPosition.x, localPosition.y, localPosition.z);
            bulletSpawnPos.localPosition = localPosition;
        }

        bullet.transform.position = bulletSpawnPos.position;
        bullet.GetComponent<Bullet>().SetDirection(!spriteRenderer.flipX);
    }

    #endregion


    #region Damage Events

    public void OnHurt(HurtType hurtType, string rebornPos)
    {
        // update hp
        UpdateHpData(playerHurtable.health);
        this.RebornPos = rebornPos;
        switch (hurtType)
        {
            case HurtType.Normal:
                // play hurt animation
                animator.SetTrigger(TriggerHurt);
                // set invincible
                SetInvincible(1);
                break;
            case HurtType.Fatal:
                SetFatal();
                // set reborn status
                Invoke(nameof(Reborn), 1);
                break;
        }

        // Update hp panel
        GamePanel.Instance.UpdateHp(GetHpFromData());
    }

    public void Reborn()
    {
        animator.SetBool(IsDead, false);
        playerRigidbody2D.gravityScale = 5;
        PlayerInput.instance.SetEnable(true);
        // set invincible status
        SetInvincible(1);
        // set reborn position
        GameObject targetPos = GameObject.Find(RebornPos);
        if (targetPos != null)
        {
            transform.position = targetPos.transform.position;
        }
    }

    public void SetInvincible(int time)
    {
        animator.SetBool(IsInvincible, true);
        playerHurtable.Disable();
        Invoke(nameof(ResetDamageable), time);

        // avoid collision with enemy
        // Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("player"), ~LayerMask.GetMask("enemy", "IgnorePlayer"));
    }

    public void ResetDamageable()
    {
        playerHurtable.Enable();
        animator.SetBool(IsInvincible, false);
        // restore collision with enemy
        // Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("player"), ~LayerMask.GetMask("IgnorePlayer"));
    }

    public void SetFatal()
    {
        animator.SetBool(IsDead, true);
        animator.SetBool(TriggerDead, true);
        playerRigidbody2D.gravityScale = 0;
        playerRigidbody2D.velocity = Vector2.zero;
        // disable input
        PlayerInput.instance.SetEnable(false);

        // show fatal hurt panel
        TipMessagePanel.Instance.ShowTip(null, TipStyle.Style2);
    }

    //dead means that the hp is 0
    public void OnDead(string resetPos)
    {
        // update hp
        UpdateHpData(playerHurtable.health);
        //set reborn position
        RebornPos = resetPos;
        //set dead status
        SetFatal();
        //restart game
        Invoke(nameof(RestartGame), 1);
        // update hp panel
        GamePanel.Instance.UpdateHp(GetHpFromData());
    }

    public void RestartGame()
    {
        // show game over panel
        TipMessagePanel.Instance.ShowTip(null, TipStyle.Style3);
        // set reborn status
        RebornPos = "Pos1";
        Reborn();
        // reset hp
        GamePanel.Instance.ResetHP();
        playerHurtable.ResetHealth();
    }

    #endregion

    #endregion
}