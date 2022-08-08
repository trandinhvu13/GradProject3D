using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class LoseDialog : Dialog
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button retryButton;
        public override void Init()
        {
            base.Init();
            retryButton.onClick.AddListener(()=>
            {
                Close();
                LevelManager.instance.Retry();
            });
        
            homeButton.onClick.AddListener(() =>
            {
                SceneController.instance.Load("Menu", Close, null);
            });
        }
    }
}
