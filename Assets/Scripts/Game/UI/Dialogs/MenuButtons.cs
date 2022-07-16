using System;
using System.Collections;
using System.Collections.Generic;
using Main;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button shop;
    [SerializeField] private Button setting;
    [SerializeField] private Button exit;
    private void Start()
    {
        play.onClick.AddListener(() =>
            {
                SceneController.instance.Load("Main", null, () => { GameUIManager.instance.gameTimer.StartTime(); });
            });
    }
}
