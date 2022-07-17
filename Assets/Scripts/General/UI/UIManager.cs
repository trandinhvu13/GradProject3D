using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public bool isDoneTransition;
    public TransitionScreen transitionScreen;
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
