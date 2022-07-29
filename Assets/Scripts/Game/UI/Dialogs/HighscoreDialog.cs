using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class HighscoreDialog : Dialog
{
    private List<HighscoreItem> highscoreItems;
    public GameObject playerInTop3Parent;
    public GameObject playerNotInTop3Parent;
    public List<LeaderboardItem> playerNotInTop3leaderboardItems;
    public List<LeaderboardItem> playerInTop3leaderboardItems;
    public LeaderboardItem playerLeaderboardItem;
    private void OnEnable()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        playerInTop3Parent.SetActive(false);
        playerNotInTop3Parent.SetActive(false);
        playerNotInTop3leaderboardItems = new List<LeaderboardItem>();
        playerInTop3leaderboardItems = new List<LeaderboardItem>();
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
                SetupHighscoreIfPlayerNotTop3(userRank);
            }
        }));
    }

    private void SetupHighscoreIfPlayerTop3()
    {
        playerInTop3Parent.SetActive(true);
        for (int i = 0; i < playerInTop3leaderboardItems.Count; i++)
        {
            playerInTop3leaderboardItems[i].Setup(i,highscoreItems[i]);
        }
    }

    private void SetupHighscoreIfPlayerNotTop3(int userRank)
    {
        playerNotInTop3Parent.SetActive(true);
        for (int i = 0; i < playerNotInTop3leaderboardItems.Count; i++)
        {
            playerNotInTop3leaderboardItems[i].Setup(i,highscoreItems[i]);
        }
        
        playerLeaderboardItem.Setup(userRank, highscoreItems[userRank]);
        
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