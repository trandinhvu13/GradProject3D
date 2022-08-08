using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class UpdateEmailDialog : Dialog
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private TextMeshProUGUI currentEmail;
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
}
