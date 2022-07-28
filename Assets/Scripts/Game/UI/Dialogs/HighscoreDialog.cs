using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreDialog : Dialog
{
    public override void Init()
    {
        base.Init();
        //StartCoroutine(FirebaseManager.instance.GetLevelAllScore());
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
