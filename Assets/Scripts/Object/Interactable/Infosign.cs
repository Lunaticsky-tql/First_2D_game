using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infosign : MonoBehaviour
{
    public Sprite normalSprite,lightedSprite;
    SpriteRenderer spriteRenderer;
    public string tipContent;
    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ( collision.CompareTag("Player") )
        {
            spriteRenderer.sprite = lightedSprite;
            // show tips
            TipMessagePanel.Instance.ShowTip(tipContent, TipStyle.Style1);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = normalSprite;
            TipMessagePanel.Instance.HideTip(TipStyle.Style1);
        }
    }
}
