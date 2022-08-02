using System.Collections;
using System.Collections.Generic;
using Main;
using UnityEngine;
using UnityEngine.UI;

public class LogoutConfirmDialog : Dialog
{
    public Button yes;
    public Button no;

    public override void Init()
    {
        base.Init();
        
        yes.onClick.AddListener(() =>
        {
            FirebaseManager.instance.SignOut();
            SceneController.instance.Load("Login", () =>
            {
                DialogSystem.instance.CloseAllOpenedDialogs();
            }, null);
        });
        
        no.onClick.AddListener(Close);
    }
}
