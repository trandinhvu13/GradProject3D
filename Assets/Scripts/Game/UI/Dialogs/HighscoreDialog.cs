using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class HighscoreDialog : Dialog
    {
        [SerializeField] private Button close;
        private List<HighscoreItem> highscoreItems;
        [SerializeField] private GameObject playerInTop3Parent;
        [SerializeField] private GameObject playerNotInTop3Parent;
        [SerializeField] private List<LeaderboardItem> playerNotInTop3leaderboardItems;
        [SerializeField] private List<LeaderboardItem> playerInTop3leaderboardItems;
        [SerializeField] private LeaderboardItem playerLeaderboardItem;

        public override void Init()
        {
            base.Init();
            close.onClick.AddListener(Close);
            playerInTop3Parent.SetActive(false);
            playerNotInTop3Parent.SetActive(false);
            highscoreItems = new List<HighscoreItem>();
            StartCoroutine(FirebaseManager.instance.GetLevelAllScore(LevelManager.instance.levelToLoad.id, (snapshot) =>
            {
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    HighscoreItem highscoreItem = new HighscoreItem(LevelManager.instance.levelToLoad.id,
                        childSnapshot.Key, float.Parse(childSnapshot.Value.ToString()));

                    highscoreItems.Add(highscoreItem);
                }

                int userRank = highscoreItems.FindIndex(x => x.username == FirebaseManager.instance.user.DisplayName);

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
    }
}