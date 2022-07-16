using System.Collections;
using System.Collections.Generic;
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
    }

    public override void Intro()
    {
        base.Intro();
    }

    public override void Outro()
    {
        base.Outro();
        retryButton.onClick.RemoveAllListeners();
    }
}
