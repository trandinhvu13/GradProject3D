using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button shop;
        [SerializeField] private Button setting;
        [SerializeField] private Button exit;
        private void Start()
        {
            /*play.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                SceneController.instance.Load("Main", null, () => { GameUIManager.instance.gameTimer.StartTime(); });
            });*/

            play.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("StageDialog").Open();
            });
        
            setting.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("SettingDialog").Open();
            });
        
            exit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
