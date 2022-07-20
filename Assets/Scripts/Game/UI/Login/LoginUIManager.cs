using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoSingleton<LoginUIManager>
{
    public Button loginWithMailButton;
    public Button registerWithMailButton;
    protected override void InternalInit()
    {
    }

    protected override void InternalOnDestroy()
    {
    }

    protected override void InternalOnDisable()
    {
    }

    protected override void InternalOnEnable()
    {
    }

    private void Start()
    {
        loginWithMailButton.onClick.AddListener(() =>
        {
            DialogSystem.instance.GetDialog("LoginDialog").Open(true);
        });
        
        registerWithMailButton.onClick.AddListener(() =>
        {
            DialogSystem.instance.GetDialog("RegisterDialog").Open(true);
        });
    }
}
