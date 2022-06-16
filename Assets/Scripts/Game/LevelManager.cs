using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
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

    public Transform playerTransform;
    public List<Transform> enemyTransforms = new List<Transform>();
    
    //Level
    public int itemsToCollectNum;

}
