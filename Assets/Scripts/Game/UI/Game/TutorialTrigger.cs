using UnityEngine;

namespace Game.UI.Game
{
    public class TutorialTrigger : MonoBehaviour
    {
        [SerializeField] private string dialogId;
        [SerializeField] private float distance;

        private void Update()
        {
            if (LevelManager.instance.player &&
                Vector3.Distance(LevelManager.instance.player.transform.position, transform.position) < distance)
            {
                if (!DialogSystem.instance.GetDialog(dialogId).isOpen)
                    DialogSystem.instance.GetDialog(dialogId).Open();
            }
            else
            {
                if (DialogSystem.instance.GetDialog(dialogId).isOpen)
                    DialogSystem.instance.GetDialog(dialogId).Close();
            }
        }
    }
}