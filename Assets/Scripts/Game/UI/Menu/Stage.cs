using System;
using System.Collections;
using System.Collections.Generic;
using Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> levelTexts;
    private List<GameObject> stars;
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

    public void Setup(StageDialog stageDialog, int levelID, int currentLevel)
    {
        foreach (TextMeshProUGUI levelText in levelTexts)
        {
            levelText.text = levelID.ToString();
        }

        if (levelID < currentLevel)
        {
            completeState.SetActive(true);

            int star = PlayerDataManager.instance.GetStarByLevel(levelID);

            for (int i = 0; i < stars.Count; i++)
            {
                if (i > star)
                {
                    stars[i].SetActive(false);
                }
            }
        }
        else if (levelID == currentLevel)
        {
            currentState.SetActive(true);
        }
        else
        {
            lockedState.SetActive(true);
        }

        if (levelID <= currentLevel)
        {
            playButton.onClick.AddListener(() =>
            {
                PlayerDataManager.instance.levelIDToLoad = levelID;
                SceneController.instance.Load("Main", () =>
                {
                    stageDialog.Close();
                }, () => { GameUIManager.instance.gameTimer.StartTime(); });
            });
        }
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
    }
}