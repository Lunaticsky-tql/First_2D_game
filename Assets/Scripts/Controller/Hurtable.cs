using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{

    public int health;    // HP
    int defaultHealth;    // default HP
    public Action<HurtType,string> OnHurt; // 
    public Action<string> OnDead; // 

    public bool isEnable = true;

    private void Start()
    {
        defaultHealth = health;
    }

    public void Enable()
    {
        isEnable = true;
    }
    public void Disable()
    {
        isEnable = false;
    }

    
    public void TakeDamage( int damage , HurtType hurtType ,string ResetPos )
    {

        if (isEnable == false) { return; } // if isEnable is false, return
        if ( health <= 0  ) { return; }     // if it is dead, return

     
        health--;
        if (health == 0)
        {
            // trigger dead action
            if (OnDead != null)
            {
                OnDead(ResetPos);
            }
        }
        else {
            // trigger hurt action
            if (OnHurt!=null)
            {
                OnHurt(hurtType, ResetPos);
            }
        }
    }

     
    public void ResetHealth( )
    {
        this.health = defaultHealth;
        Enable();
    }

}
