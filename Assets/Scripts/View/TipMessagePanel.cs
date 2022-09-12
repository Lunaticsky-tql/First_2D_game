using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TipStyle{
    Style1, // tip which is shown in the bottom of the screen
    Style2, // black screen with text
    Style3, // tip which is shown when the game is over
}

public class TipMessagePanel : SingletonView<TipMessagePanel>
{


    #region Fields

    GameObject style1Obj;
    GameObject style2Obj;
    GameObject style3Obj;
    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        style1Obj = transform.Find("Style1").gameObject;
        style1Obj.SetActive(false);
        style2Obj = transform.Find("Style2").gameObject;
        style2Obj.SetActive(false);
        style3Obj = transform.Find("Style3").gameObject;
        style3Obj.SetActive(false);

    }
    

    #endregion 

    #region Methods

    public void ShowTip(string content, TipStyle tipStyle) {
        switch (tipStyle)
        {
            case TipStyle.Style1:
                style1Obj.SetActive(true);
                style1Obj.transform.Find("Content").GetComponent<Text>().text = content;
                break;
            case TipStyle.Style2:
                style2Obj.SetActive(true);
                // hide after 1.5 seconds
                Invoke(nameof(HideTipStyle2), 1.5f);
                break;
            case TipStyle.Style3:
                style3Obj.SetActive(true);
                // hide after 2 seconds
                Invoke(nameof(HideTipStyle3), 2);
                break;

        }
    }

    public void HideTipStyle2() {
        HideTip(TipStyle.Style2);
    }

    public void HideTipStyle3() {
        HideTip(TipStyle.Style3);
    }

    public void HideTip(TipStyle tipStyle) {
        switch (tipStyle)
        {
            case TipStyle.Style1:
                style1Obj.SetActive(false);
                break;
            case TipStyle.Style2:
                style2Obj.SetActive(false);
                break;
            case TipStyle.Style3:
                style3Obj.SetActive(false);
                break;
        }
    }

    #endregion



}
