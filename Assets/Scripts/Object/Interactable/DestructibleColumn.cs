using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleColumn : MonoBehaviour
{

    Hurtable hurtable;
    GameObject destoryObj;

    private void Start()
    {

        destoryObj = transform.Find("Destructible").gameObject;

        hurtable = transform.GetComponent<Hurtable>();
        hurtable.OnDead += this.OnBroken;
    }

    public void OnBroken(string ResetPos) {

        destoryObj.SetActive(true);
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject,3);
    }

}
