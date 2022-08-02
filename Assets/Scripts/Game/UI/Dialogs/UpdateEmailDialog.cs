using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEmailDialog : Dialog
{
    public Button closeButton;
    public Button updateButton;
    public TMP_InputField input;
    public TextMeshProUGUI currentEmail;
    public override void Init()
    {
        base.Init();
        currentEmail.text = $"Current email: {FirebaseManager.instance.user.Email}";
        closeButton.onClick.AddListener(Close);
        updateButton.onClick.AddListener(() =>
        {
            FirebaseManager.instance.UpdateEmail(input.text, () =>
            {
                Close();
            });
            StartCoroutine(FirebaseManager.instance.UpdateEmailDatabase(input.text));
        });
    }

    public override void Outro()
    {
        base.Outro();
        input.text = "";
    }
}
