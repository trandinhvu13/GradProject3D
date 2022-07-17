using System.Collections;
using System.Collections.Generic;
using Main;
using UnityEngine;
using UnityEngine.UI;

public class LoseDialog : Dialog
{
    public Button homeButton;
    public Button retryButton;
    public override void Init()
    {
        base.Init();
        retryButton.onClick.AddListener(()=>
        {
            Close();
            LevelManager.instance.Retry();
        });
        
        homeButton.onClick.AddListener(() =>
        {
            SceneController.instance.Load("Menu", () =>
            {
                Close();
            }, null);
        });
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
