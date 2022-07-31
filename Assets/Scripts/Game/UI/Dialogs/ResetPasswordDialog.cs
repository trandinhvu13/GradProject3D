using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordDialog : Dialog
{
    public TMP_InputField email;
    public Button closeButton;
    public Button sendReset;
    
    public GameObject errorMessage;
    public TextMeshProUGUI errorText;

    public GameObject message;
    public TextMeshProUGUI messageText;

    public override void Init()
    {
        base.Init();
        closeButton.onClick.AddListener((() =>
        {
            Close();
            DialogSystem.instance.GetDialog("LoginDialog").Open(true);
        }));
        
        sendReset.onClick.AddListener(() =>
        {
            FirebaseManager.instance.SendResetPassword(email.text,this);
        });
    }

    public override void Outro()
    {
        base.Outro();
        email.text = "";
    }

    public IEnumerator ShowError(string text)
    {
        errorText.text = text;
        errorMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        errorMessage.SetActive(false);
    }

    public IEnumerator ShowMessage(string text)
    {
        UnbindAllButtons();
        messageText.text = text;
        message.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        Close();
        DialogSystem.instance.GetDialog("LoginDialog").Open(true);
        
    }
}
