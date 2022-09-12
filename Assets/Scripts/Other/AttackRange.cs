using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> hurtables = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtable hurtable = collision.transform.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            hurtables.Add(hurtable.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Hurtable hurtable = collision.transform.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            if (!hurtables.Contains(hurtable.gameObject))
            {
                hurtables.Add(hurtable.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Hurtable hurtable = collision.transform.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            hurtables.Remove(hurtable.gameObject);
        }
    }

    public GameObject[] GetHurtableGameObjects()
    {
        return hurtables.ToArray();
    }
}