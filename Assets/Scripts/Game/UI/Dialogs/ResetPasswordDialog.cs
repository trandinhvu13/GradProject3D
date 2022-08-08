using System.Collections;
using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class ResetPasswordDialog : Dialog
    {
        [SerializeField] private TMP_InputField email;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button sendReset;
    
        [SerializeField] private GameObject errorMessage;
        [SerializeField] private TextMeshProUGUI errorText;

        [SerializeField] private GameObject message;
        [SerializeField] private TextMeshProUGUI messageText;

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
}
