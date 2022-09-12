using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    

    public float speed;
    

    public void SetDirection( bool isRight )
    {
        spriteRenderer.flipX = !isRight;
        rigidbody2D.velocity = new Vector2 ( isRight?speed:-speed ,0);
    }


}
