using System.Collections.Generic;
using Game.UI.Dialogs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> levelTexts;
        [SerializeField] private List<GameObject> stars;
        [SerializeField] private GameObject completeState;
        [SerializeField] private GameObject currentState;
        [SerializeField] private GameObject lockedState;
        [SerializeField] private Button playButton;

        private void Awake()
        {
            completeState.SetActive(false);
            currentState.SetActive(false);
            lockedState.SetActive(false);
        }

        public void Setup(StageDialog stageDialog, StageData stageData, int currentLevel)
        {
            int id = stageData.levelID;
            foreach (TextMeshProUGUI levelText in levelTexts)
            {
                levelText.text = (id + 1).ToString();
            }

            if (id < currentLevel)
            {
                completeState.SetActive(true);

                int star = stageData.star-1;

                for (int i = 0; i < stars.Count; i++)
                {
                    if (i > star)
                    {
                        stars[i].SetActive(false);
                    }
                }
            }
            else if (id == currentLevel)
            {
                currentState.SetActive(true);
            }
            else
            {
                lockedState.SetActive(true);
            }

            if (id <= currentLevel)
            {
                playButton.onClick.AddListener(() =>
                {
                    PlayerDataManager.instance.levelIDToLoad = id;
                    SceneController.instance.Load("Main", () => { stageDialog.Close(); },
                        () => { GameUIManager.instance.gameTimer.StartTime(); });
                });
            }
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
            completeState.SetActive(false);
            currentState.SetActive(false);
            lockedState.SetActive(false);
        }
    }
}