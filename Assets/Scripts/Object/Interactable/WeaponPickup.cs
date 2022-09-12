using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public Sprite haveWeapon, noWeapon;

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        InitData();
    }

    public void InitData() {
        // check whether the player has the weapon
        Data<bool> data = (Data<bool>)DataManager.Instance.GetData(DataConst.IsHaveWeapon);
        if (data != null && data.value1)
        {
            spriteRenderer.sprite = noWeapon;
        }
        else
        {
            spriteRenderer.sprite = haveWeapon;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TagConst.Player))
        {
            spriteRenderer.sprite = noWeapon;

            // save the data of isHaveWeapon
            Data<bool> data = new Data<bool>();
            data.value1 = true;
            DataManager.Instance.SaveData(DataConst.IsHaveWeapon, data);

            TipMessagePanel.Instance.ShowTip("You Got a weapon! You can press K or O to attack!", TipStyle.Style1);
            Invoke(nameof(HideTip), 2);
            //the box collider will be useless after the player get the weapon
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    public void HideTip() {
        TipMessagePanel.Instance.HideTip(TipStyle.Style1);
    }

}
