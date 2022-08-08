using Firebase;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class LogoutConfirmDialog : Dialog
    {
        [SerializeField] private Button yes;
        [SerializeField] private Button no;

        public override void Init()
        {
            base.Init();
        
            yes.onClick.AddListener(() =>
            {
                FirebaseManager.instance.SignOut();
                SceneController.instance.Load("Login", () =>
                {
                    DialogSystem.instance.CloseAllOpenedDialogs();
                }, null);
            });
        
            no.onClick.AddListener(Close);
        }
    }
}
