using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI time;
    public TextMeshProUGUI rankAbove3Text;
    public List<GameObject> rankImages;

    public void Setup(int rank, HighscoreItem highscoreItem)
    {
        int finalRank = rank;
        foreach (var image in rankImages)
        {
            image.SetActive(false);
        }
        
        rankImages[Mathf.Clamp(finalRank,0,2)].SetActive(true);

        rankAbove3Text.text = $"{rank + 1}";
        name.text = highscoreItem.username;
        time.text = Helper.ChangeTimeToTextString(highscoreItem.time);
    }
    
}
