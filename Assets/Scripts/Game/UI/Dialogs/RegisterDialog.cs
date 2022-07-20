using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterDialog : Dialog
{
    public TMP_InputField emailInput;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button closeButton;
    public Button signUpButton;
    public Button logInButton;

    public GameObject errorMessage;
    public TextMeshProUGUI errorText;

    public GameObject message;
    public TextMeshProUGUI messageText;

    public override void Init()
    {
        base.Init();
        closeButton.onClick.AddListener(Close);
        signUpButton.onClick.AddListener(SignUp);
        logInButton.onClick.AddListener(Login);
    }
    
    public override void Outro()
    {
        base.Outro();
        ClearInput();
        errorMessage.SetActive(false);
        message.SetActive(false);
    }

    private void SignUp()
    {
        StartCoroutine(FirebaseManager.instance.Register(usernameInput.text, emailInput.text, passwordInput.text,
            this));
    }

    private void Login()
    {
        Close();
        DialogSystem.instance.GetDialog("LoginDialog").Open(true);
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
        yield return new WaitForSeconds(2);
        Close();
        DialogSystem.instance.GetDialog("LoginDialog").Open(true);
    }

    private void ClearInput()
    {
        emailInput.text = "";
        passwordInput.text = "";
        usernameInput.text = "";
    }
}