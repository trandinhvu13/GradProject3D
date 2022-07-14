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
        Debug.Log("Init lose");
        retryButton.onClick.AddListener(LevelManager.instance.Retry);
    }

    public override void Intro()
    {
        base.Intro();
        Debug.Log("Intro lose");
    }

    public override void Outro()
    {
        base.Outro();
        Debug.Log("Outro lose");
    }
}
