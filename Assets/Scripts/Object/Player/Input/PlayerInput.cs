using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    bool isEnabled = true;

    #region Input Variables

    public InputButton Pause = new InputButton(KeyCode.Escape); //Pause button
    public InputButton Jump = new InputButton(KeyCode.Space); //Jump button
    public InputButton Attack = new InputButton(KeyCode.K); //Attack button
    public InputButton Shoot = new InputButton(KeyCode.O); //Shoot button
    public InputAxis Horizontal = new InputAxis(KeyCode.A, KeyCode.D); //Horizontal movement
    public InputAxis Vertical = new InputAxis(KeyCode.S, KeyCode.W); //Vertical movement

    #endregion

    public void Awake()
    {
        if (instance != null)
        {
            throw new SystemException("There can only be one instance of PlayerInput");
        }

        instance = this;
    }

    public void Update()
    {
        if(isEnabled)
        {
            Pause.GetInput();
            Attack.GetInput();
            Shoot.GetInput();
            Jump.GetInput();
            Horizontal.GetInput();
            Vertical.GetInput();
        }
        else
        {
            Horizontal.value = 0;
            Vertical.value = 0;
        }

    }
    
    public void SetEnable( bool isCanUse ) {
        this.isEnabled = isCanUse;
    }
}