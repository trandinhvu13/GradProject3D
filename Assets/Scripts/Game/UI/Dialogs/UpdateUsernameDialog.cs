using Firebase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class UpdateUsernameDialog : Dialog
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private TextMeshProUGUI currentUsername;

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
}