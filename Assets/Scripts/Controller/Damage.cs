using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HurtType {
    Normal,
    Fatal
}

public class Damage : MonoBehaviour
{

    public int damage; // damage amount
    public HurtType hurtType;
    public string ResetPos;

    // damage single object
    public void OnDamage(GameObject gameObject) {
        Hurtable hurtable = gameObject.GetComponent<Hurtable>();
        if ( hurtable == null )
        {
            return;
        }
        hurtable.TakeDamage(this.damage, hurtType, ResetPos);
    }

    //damage multiple objects
    public void OnDamage(GameObject[] gameObjects)
    {
        for ( int i=0;i<gameObjects.Length;i++ )
        {
            OnDamage(gameObjects[i]);
        }
    }


}
