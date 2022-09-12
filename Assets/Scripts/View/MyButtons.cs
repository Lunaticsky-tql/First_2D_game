
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MyButtons : MonoBehaviour
{

    #region fields

    Transform btnLogo;
    Image btnBg;
    Sprite btnNormalSprite;
    public Sprite btnHightedSprite;

    #endregion




    #region Unity Events

    void Start()
    {
        btnLogo = transform.Find("btn_logo");
        btnLogo.gameObject.SetActive(false);
        btnBg = transform.GetComponent<Image>();
        btnNormalSprite = btnBg.sprite;
    }

    #endregion


    #region Action Listeners

    public void OnPointerEnter()
    {
        SetHighlight(true);
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit()
    {
        SetHighlight(false);
        Debug.Log("OnPointerExit");
    }

    public void OnPointerUp()
    {
        SetHighlight(false);
        Debug.Log("OnPointerUp");
    }

    #endregion

    #region Methods

    public void SetHighlight(bool needToLight) {
        btnLogo.gameObject.SetActive(needToLight);
        btnBg.sprite = needToLight ? btnHightedSprite : btnNormalSprite;
    }

    #endregion
}
