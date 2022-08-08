using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using Game;
using Game.UI.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogs
{
    public class StageDialog : Dialog
    {
        [SerializeField] private List<Stage> stages;
        [SerializeField] private Button home;
        [SerializeField] private TextMeshProUGUI currentStar;
        private List<StageData> stageDatas;

        public override void Init()
        {
            base.Init();
            stageDatas = new List<StageData>();
            home.onClick.AddListener(() => { Close(); });

            currentStar.text = $"{PlayerDataManager.instance.GetCurrentStar()}/{PlayerDataManager.instance.GetTotalStar()}";

            int currentLevel = PlayerDataManager.instance.GetCurrentLevel();
            StartCoroutine(FirebaseManager.instance.GetUserLevelData((snapshot) =>
            {
                int level = 0;
                if (snapshot != null)
                {
                    foreach (DataSnapshot childSnapshot in snapshot.Children)
                    {
                        int star = int.Parse(childSnapshot.Child("star").Value.ToString());
                        stageDatas.Add(new StageData(level, star));
                        level++;
                    }
                    for (int i = 0; i < stages.Count; i++)
                    {
                        if (i <= PlayerDataManager.instance.numberOfLevel - 1)
                        {
                            if (i < stageDatas.Count)
                            {
                                stages[i].Setup(this, stageDatas[i], currentLevel);
                            }
                            else
                            {
                                stages[i].Setup(this, new StageData(i, 0), currentLevel);
                            }
                        }
                        else
                        {
                            stages[i].gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < stages.Count; i++)
                    {
                        if (i <= PlayerDataManager.instance.numberOfLevel - 1)
                        {
                            stages[i].Setup(this, new StageData(i, 0), currentLevel);
                        }
                        else
                        {
                            stages[i].gameObject.SetActive(false);
                        }
                    }
                }
            }));
        }
    }

    public class StageData
    {
        public int levelID;
        public int star;

        public StageData(int levelID, int star)
        {
            this.levelID = levelID;
            this.star = star;
        }
    }
}