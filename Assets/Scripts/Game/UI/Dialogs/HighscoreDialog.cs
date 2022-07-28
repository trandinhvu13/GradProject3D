using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class HighscoreDialog : Dialog
{
    private List<HighscoreItem> highscoreItems;
    private void OnEnable()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        highscoreItems = new List<HighscoreItem>();
        StartCoroutine(FirebaseManager.instance.GetLevelAllScore(LevelManager.instance.levelToLoad.id, (snapshot) =>
        {
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                HighscoreItem highscoreItem = new HighscoreItem(LevelManager.instance.levelToLoad.id,
                    childSnapshot.Key, float.Parse(childSnapshot.Value.ToString()));
                Debug.Log(
                    $"Score of player {highscoreItem.username} in level {LevelManager.instance.levelToLoad.id} is {highscoreItem.time}");
                
                highscoreItems.Add(highscoreItem);
            }

            int userRank = highscoreItems.FindIndex(x => x.username == FirebaseManager.instance.user.DisplayName);
            
            Debug.Log($"User rank is at  {userRank}");

            if (userRank <= 2)
            {
                SetupHighscoreIfPlayerTop3();
            }
            else
            {
                SetupHighscoreIfPlayerNotTop3();
            }
        }));
    }

    private void SetupHighscoreIfPlayerTop3()
    {
        
    }

    private void SetupHighscoreIfPlayerNotTop3()
    {
        
    }
    public override void Intro()
    {
        base.Intro();
    }

    public override void Outro()
    {
        base.Outro();
    }
}