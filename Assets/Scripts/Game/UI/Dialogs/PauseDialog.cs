using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class PauseDialog : Dialog
    {
        [SerializeField] private Button resume;
        [SerializeField] private Button retry;
        [SerializeField] private Button setting;
        [SerializeField] private Button exit;

        public override void Init()
        {
            base.Init();
            resume.onClick.AddListener(()=>
            {
                Close();
                LevelManager.instance.ResumeGame();
            });
            retry.onClick.AddListener(()=>
            {
                Close();
                LevelManager.instance.Retry();
            });
            exit.onClick.AddListener(() =>
            {
                SceneController.instance.Load("Menu", () =>
                {
                    Close();
                }, null);
            });
            setting.onClick.AddListener(() =>
            {
                DialogSystem.instance.GetDialog("SettingDialog").Open();
            });
        }

        public override void Outro()
        {
            base.Outro();
            Time.timeScale = 1;
        }
    }
}
