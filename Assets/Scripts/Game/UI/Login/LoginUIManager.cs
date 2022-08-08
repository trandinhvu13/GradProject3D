using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Login
{
    public class LoginUIManager : MonoSingleton<LoginUIManager>
    {
        [SerializeField] private Button loginWithMailButton;
        [SerializeField] private Button registerWithMailButton;
        [SerializeField] private Button exitButton;
        protected override void InternalInit()
        {
        }

        protected override void InternalOnDestroy()
        {
        }

        protected override void InternalOnDisable()
        {
        }

        protected override void InternalOnEnable()
        {
        }

        private void Start()
        {
            loginWithMailButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("LoginDialog").Open(true);
            });
        
            registerWithMailButton.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("RegisterDialog").Open(true);
            });
        
            exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
