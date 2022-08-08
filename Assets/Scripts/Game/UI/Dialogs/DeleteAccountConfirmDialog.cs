using Firebase;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class DeleteAccountConfirmDialog : Dialog
    {
        [SerializeField] private Button yes;
        [SerializeField] private Button no;

        public override void Init()
        {
            base.Init();
            yes.onClick.AddListener(() =>
            {
                FirebaseManager.instance.DeleteAccount((() =>
                {
                    Close();
                    SceneController.instance.Load("Login", () => { DialogSystem.instance.CloseAllOpenedDialogs(); });
                }));
            });

            no.onClick.AddListener(Close);
        }
    }
}