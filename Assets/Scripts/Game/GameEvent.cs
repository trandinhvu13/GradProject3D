using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoSingleton<GameEvent>
{
    #region Mono
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
    #endregion
    
    #region Enemy

    /*public event Action<Transform> OnDetectPlayer;

    public void DetectPlayer(Transform playerTransform)
    {
        OnDetectPlayer?.Invoke(playerTransform);
    }*/
    #endregion

    
}