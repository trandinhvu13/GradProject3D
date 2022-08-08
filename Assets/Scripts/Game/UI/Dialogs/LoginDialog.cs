using System.Collections;
using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class LoginDialog : Dialog
    {
        [SerializeField] private Button close;
        [SerializeField] private Button login;
        [SerializeField] private Button signUp;
        [SerializeField] private Button forgotPassword;

        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField] private GameObject errorMessage;
        [SerializeField] private TextMeshProUGUI errorText;

        [SerializeField] private GameObject message;
        [SerializeField] private TextMeshProUGUI messageText;

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

            SceneController.instance.Load("Menu", Close);
        
        }
    
        private void ClearInput()
        {
            emailInput.text = "";
            passwordInput.text = "";
        }
    }
}