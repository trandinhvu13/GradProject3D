using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreItem
{
    private int levelID;
    public string username;
    public float time;

    public HighscoreItem(int levelID, string username, float time)
    {
        this.levelID = levelID;
        this.username = username;
        this.time = time;
    }
}
