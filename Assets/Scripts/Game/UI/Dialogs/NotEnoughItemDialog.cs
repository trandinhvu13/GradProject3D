using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughItemDialog : Dialog
{
    [SerializeField] private TextMeshProUGUI text;
    public override void Init()
    {
        base.Init();
        text.text = $"You need to collect all {LevelManager.instance.numOfItemsToCollect} items to unlock";
    }
}
