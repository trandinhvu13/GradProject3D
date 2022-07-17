using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> levelTexts;
    private List<GameObject> stars;
    [SerializeField] private GameObject completeState;
    [SerializeField] private GameObject currentState;
    [SerializeField] private GameObject lockedState;

    private void Awake()
    {
        completeState.SetActive(false);
        currentState.SetActive(false);
        lockedState.SetActive(false);
    }

    public void Setup(int levelID)
    {
        foreach (TextMeshProUGUI levelText in levelTexts)
        {
            levelText.text = levelID.ToString();
        }

        //get current level from player prefs
        int currentLevel = 3;

        if (levelID < currentLevel)
        {
            completeState.SetActive(true);
            //get star from player prefs from level id
            int star = 1;

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
    }
}