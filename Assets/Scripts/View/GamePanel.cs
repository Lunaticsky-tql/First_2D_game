using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : SingletonView<GamePanel>
{
    public GameObject hpItemPrefab;
    private Transform hpParent;
    GameObject[] hpItems;

    protected override void Awake()
    {
        base.Awake();
        hpParent = transform.Find("HP");
    }
    public void InitHp(int hp)
    {
        hpItems = new GameObject[hp];
        for (int i = 0; i < hp; i++)
        {
            hpItems[i] = GameObject.Instantiate(hpItemPrefab, hpParent);
        }
    }

    public void UpdateHp(int hp)
    {
        for (int i = hp; i < hpItems.Length; i++)
        {
            if (hpItems[i].GetComponent<Toggle>().isOn)
            {
                hpItems[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }
    
    public void ResetHP() {

        for (int i = 0; i < hpItems.Length; i++)
        {
            hpItems[i].GetComponent<Toggle>().isOn = true;
        }

    }
}