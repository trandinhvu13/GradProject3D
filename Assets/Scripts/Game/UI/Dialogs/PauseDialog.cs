using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseDialog : Dialog
{
    [SerializeField] private Button resume;
    [SerializeField] private Button retry;
    [SerializeField] private Button exit;

    public override void Init()
    {
        base.Init();
        resume.onClick.AddListener(LevelManager.instance.ResumeGame);
    }
}
