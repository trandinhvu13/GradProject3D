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
    
    #region Player

    public event Action<Transform,float> OnPlayerWhistle;

    public void PlayerWhistle(Transform playerTransform, float soundRingRadius)
    {
        OnPlayerWhistle?.Invoke(playerTransform, soundRingRadius);
    }
    
    public event Action<Transform,float> OnPlayerRun;

    public void PlayerRun(Transform playerTransform, float soundRingRadius)
    {
        OnPlayerRun?.Invoke(playerTransform, soundRingRadius);
    }
    #endregion

    #region Enemy

   

    #endregion


    #region Itemn

    

    #endregion

    #region Game UI
    
    public event Action<Vector3> OnShowIndicator;

    public void ShowIndicator(Vector3 pos)
    {
        OnShowIndicator?.Invoke(pos);
    }
    
    public event Action OnHideIndicator;

    public void HideIndicator()
    {
        OnHideIndicator?.Invoke();
    }
    
    public event Action OnClickOnGround;

    public void ClickOnGround()
    {
        OnClickOnGround?.Invoke();
    }

    #endregion

    
}