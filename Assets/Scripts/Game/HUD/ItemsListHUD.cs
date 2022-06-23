using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsListHUD : MonoBehaviour
{
    [SerializeField] private List<ItemsHUD> itemsHuds;

    public void LoadItemInLevel(List<CollectableItem> collectableItems)
    {
        foreach (var itemsHud in itemsHuds)
        {
            itemsHud.gameObject.SetActive(false);
        }

        for (int i = 0; i < collectableItems.Count; i++)
        {
            itemsHuds[i].gameObject.SetActive(true);
            itemsHuds[i].Setup(collectableItems[i].hudImage);
        }
    }

    public void CollectItem(int id)
    {
        itemsHuds[id].GetCollected();
    }
}
