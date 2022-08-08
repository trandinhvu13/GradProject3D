using TMPro;
using UnityEngine;

namespace Game.UI.Dialogs
{
    public class NotEnoughItemDialog : Dialog
    {
        [SerializeField] private TextMeshProUGUI text;
        public override void Init()
        {
            base.Init();
            text.text = $"You need to collect all {LevelManager.instance.numOfItemsToCollect} items to unlock";
        }
    }
}
