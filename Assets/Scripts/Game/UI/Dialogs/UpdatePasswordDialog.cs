using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePasswordDialog : Dialog
{
    public TMP_InputField input;
    public Button updateButton;
    public Button closeButton;
    
    public override void Init()
    {
        base.Init();
        updateButton.onClick.AddListener((() =>
        {
            FirebaseManager.instance.UpdatePassword(input.text, () =>
            {
                Close();
            });
        }));
        
        closeButton.onClick.AddListener(Close);
    }

    public override void Outro()
    {
        base.Outro();
        input.text = "";
    }
}
