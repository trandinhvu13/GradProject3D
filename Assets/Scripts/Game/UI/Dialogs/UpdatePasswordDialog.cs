using System.Collections;
using System.Collections.Generic;
using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePasswordDialog : Dialog
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button updateButton;
    [SerializeField] private Button closeButton;
    
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
