using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
    public int levelIDToLoad;
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

    public int GetCurrentStar()
    {
        return 100;
    }

    public int GetTotalStar()
    {
        return 1000;
    }

    public int GetStarByLevel(int levelID)
    {
        return 1;
    }

    public int GetCurrentLevel()
    {
        return 0;
    }
}
