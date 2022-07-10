using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    public ItemsListHUD itemsListHUD;

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
    
    
    public void SetupItemsToCollect(List<CollectableItem> collectableItems)
    {
        itemsListHUD.LoadItemInLevel(collectableItems);
    }
}
