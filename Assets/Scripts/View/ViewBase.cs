using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ViewBase : MonoBehaviour
{
    public virtual void Show()
    {
        transform.gameObject.SetActive(true);
    }
    
    public virtual void Hide()
    {
        transform.gameObject.SetActive(false);
    }
    public bool IsShow()
    {
        return transform.gameObject.activeSelf;
    }
}

