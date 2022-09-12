using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : ViewBase
{
    #region Fields

    public MenuPanel menuPanel;                         
    public OptionAudioSetPanel optionAudioSetPanel;     
    public OptionControlPanel optionControlPanel;     

    GameObject btnAudio;                               
    GameObject btnControl;                            
    GameObject messagePanel;                            

    #endregion

    #region Action Listeners
    
    
    public void OnAudioClick() {

        HideOrShowOptionPanel(false);
        optionAudioSetPanel.Show();
    }
    public void OnBackClick() {

        if (optionAudioSetPanel.IsShow() || optionControlPanel.IsShow())
        {
            optionAudioSetPanel.Hide();
            optionControlPanel.Hide();

            
            HideOrShowOptionPanel(true);

        }
        else {
            this.Hide();
            menuPanel.Show();
        }

    }
    
    public void OnControlClick() {
        HideOrShowOptionPanel(false);
        optionControlPanel.Show();
    }
    
    void HideOrShowOptionPanel(bool isShow) {
        btnAudio.SetActive(isShow);
        btnControl.SetActive(isShow);
        messagePanel.SetActive(isShow);
    }

    #endregion

    #region Unity Events

    public void Start()
    {
        btnAudio = transform.Find("bg/btn_audio").gameObject;
        btnControl= transform.Find("bg/btn_control").gameObject;
        messagePanel = transform.Find("bg/MessagePanel").gameObject;
    }

    #endregion
}
