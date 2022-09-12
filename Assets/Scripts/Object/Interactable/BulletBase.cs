using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    #region Fields

    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;

    protected Damage damage;

    protected Animator animator;
    private static readonly int IsBomb = Animator.StringToHash("isBomb");

    #endregion

    #region Unity Events

    public virtual void Awake()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        damage = transform.GetComponent<Damage>();
        animator = transform.GetComponent<Animator>();
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
        damage.OnDamage(collision.gameObject);
        if (animator!=null)
        {
            animator.SetBool(IsBomb, true);
        }

        rigidbody2D.velocity = Vector2.zero;
        transform.GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 0.15f);

    }

    #endregion
}
