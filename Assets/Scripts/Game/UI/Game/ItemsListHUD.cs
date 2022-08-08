using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class ItemsListHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemsText;

    public void LoadItemInLevel(List<CollectableItem> collectableItems)
    {
        itemsText.text = $"0/{collectableItems.Count} ITEMS";
    }

    public void CollectItem()
    {
        itemsText.text = $"{LevelManager.instance.currentItemsAmount}/{LevelManager.instance.numOfItemsToCollect} ITEMS";
    }

    public void DoneCollectItem()
    {
        itemsText.text = $"DONE!";
    }
}
