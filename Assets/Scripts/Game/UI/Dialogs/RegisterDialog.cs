using System.Collections;
using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class RegisterDialog : Dialog
    {
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField] private Button closeButton;
        [SerializeField] private Button signUpButton;
        [SerializeField] private Button logInButton;

        [SerializeField] private GameObject errorMessage;
        [SerializeField] private TextMeshProUGUI errorText;

        [SerializeField] private GameObject message;
        [SerializeField] private TextMeshProUGUI messageText;

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
}