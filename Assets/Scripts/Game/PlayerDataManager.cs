using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
    public int playerID;
    public int levelIDToLoad;
    public int numberOfLevel;

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
        int currentStar = 0;

        for (int i = 0; i < numberOfLevel; i++)
        {
            currentStar += GetStarByLevel(i);
        }

        return currentStar;
    }

    public int GetTotalStar()
    {
        return 3 * numberOfLevel;
    }

    public int GetStarByLevel(int levelID)
    {
        string key = $"{playerID}/Score/{levelID}";
        string score = SPrefs.GetString(key, "");

        if (score == "")
        {
            return 0;
        }
        else
        {
            return int.Parse(score.Split("/")[1]);
        }
    }

    public int GetCurrentLevel()
    {
        return SPrefs.GetInt($"{playerID}/Data/Progress/CurrentLevel", 0);
    }

    public void SaveScore(int levelID, float time, int star)
    {
        //to local
        string key = $"{playerID}/Score/{levelID}";
        string value = $"{time}/{star}";
        SPrefs.SetString(key, value);

        //to firebase
    }

    public void SaveProgress(int currentLevel)
    {
        //to local
        string keyCurrentLevel = $"{playerID}/Data/Progress/CurrentLevel";
        SPrefs.SetInt(keyCurrentLevel, currentLevel);

        //to firebase
    }
}