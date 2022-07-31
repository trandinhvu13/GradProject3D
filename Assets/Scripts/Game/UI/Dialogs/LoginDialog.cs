using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginDialog : Dialog
{
    public Button close;
    public Button login;
    public Button signUp;
    public Button forgotPassword;

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public GameObject errorMessage;
    public TextMeshProUGUI errorText;

    public GameObject message;
    public TextMeshProUGUI messageText;

    public override void Init()
    {
        base.Init();
        close.onClick.AddListener(Close);
        login.onClick.AddListener(Login);
        signUp.onClick.AddListener(SignUp);
        forgotPassword.onClick.AddListener(ForgotPassword);
        
        errorMessage.SetActive(false);
        message.SetActive(false);
    }

    public override void Outro()
    {
        base.Outro();
        ClearInput();
        errorMessage.SetActive(false);
        message.SetActive(false);
    }

    private void Login()
    {
        StartCoroutine(FirebaseManager.instance.Login(emailInput.text, passwordInput.text, this));
    }

    private void SignUp()
    {
        Close();
        DialogSystem.instance.GetDialog("RegisterDialog").Open(true);
    }

    private void ForgotPassword()
    {
        Close();
        DialogSystem.instance.GetDialog("ResetPasswordDialog").Open(true);
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

        SceneController.instance.Load("Menu", Close, null);
        
    }
    
    private void ClearInput()
    {
        emailInput.text = "";
        passwordInput.text = "";
    }
}