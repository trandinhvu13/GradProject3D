using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Login
{
    public class LoginUIManager : MonoBehaviour
    {
        [SerializeField] private Button loginWithMailButton;
        [SerializeField] private Button registerWithMailButton;
        [SerializeField] private Button exitButton;

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
