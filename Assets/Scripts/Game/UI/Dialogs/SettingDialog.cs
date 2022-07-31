using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingDialog : Dialog
{
    public Button backButton;
    public Button ChangeUsernameButton;
    public Button deleteAccountButton;
    public Button signOutButton;
    
    public override void Init()
    {
        base.Init();
    }

    public override void Outro()
    {
        base.Outro();
        
    }
}
