using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : ViewBase
{
    public OptionPanel optionPanel;

    #region Action Listeners

    public void OnStartGameClick()
    {
       SceneController.Instance.LoadScene(1);
    }

    public void OnOptionClick()
    {
        this.Hide();
        optionPanel.Show();
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}