using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class MenuUIManager : MonoSingleton<MenuUIManager>
{
    protected override void InternalInit()
    {
        ResourceItem items = ResourceDB.GetFolder("Levels");
        Debug.Log(items.childs.Count);
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

    public void TransitionIn()
    {
        
    }

    public void TransitionOut()
    {
        
    }
}
