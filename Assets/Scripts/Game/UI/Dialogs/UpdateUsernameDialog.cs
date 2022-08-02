using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUsernameDialog : Dialog
{
    public Button closeButton;
    public Button updateButton;
    public TMP_InputField input;
    public TextMeshProUGUI currentUsername;

    public override void Init()
    {
        base.Init();

        StartCoroutine(FirebaseManager.instance.GetUserInfoData((snapshot) =>
        {
            currentUsername.text = $"Current username: {snapshot.Child("username").Value}";
        }));
        closeButton.onClick.AddListener(Close);
        updateButton.onClick.AddListener(() =>
        {
            StartCoroutine(FirebaseManager.instance.UpdateUsernameAuth(input.text));
            StartCoroutine(FirebaseManager.instance.UpdateUsername(input.text, () => { Close(); }));
        });
    }

    public override void Outro()
    {
        base.Outro();
        input.text = "";
    }
}