using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinDialog : Dialog
{
    [SerializeField] private TextMeshProUGUI finishTime;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button highScoreButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private List<Star> stars = new List<Star>();
    [SerializeField] private float middleTime;

    public override void Init()
    {
        base.Init();
        SetupText();
        BindButton();
    }

    public override void Intro()
    {
        base.Intro();
        StartCoroutine(ShowAnimation());
    }

    public override void Outro()
    {
        base.Outro();
    }

    private void SetupText()
    {
        finishTime.text = $"FINISH TIME: {ChangeToTimeString(LevelManager.instance.finishTime)}";
        //Get personal best
        bestTime.text = $"PERSONAL BEST: {ChangeToTimeString(LevelManager.instance.finishTime)}";
        
        finishTime.transform.localScale = Vector3.zero;
        bestTime.transform.localScale = Vector3.zero;
        retryButton.transform.localScale = Vector3.zero;
        highScoreButton.transform.localScale = Vector3.zero;
        nextButton.transform.localScale = Vector3.zero;
    }

    private void BindButton()
    {
    }

    IEnumerator ShowAnimation()
    {
        yield return new WaitForSeconds(middleTime);
        finishTime.transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(middleTime);
        bestTime.transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(middleTime);
        
        for (int i = 0; i < stars.Count; i++)
        {
            if (i <= LevelManager.instance.finishMilestone)
            {
                stars[i].Show(true);
            }
            else
            {
                stars[i].Show(false);
            }

            yield return new WaitForSeconds(stars[i].showTime);
        }
        yield return new WaitForSeconds(middleTime);

        retryButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        highScoreButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        nextButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
    }

    private string ChangeToTimeString(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;

        string minString = min.ToString();
        string secString = sec.ToString();

        if (min < 10)
        {
            minString = $"0{min}";
        }

        if (sec < 10)
        {
            secString = $"0{sec}";
        }

        return $"{minString}:{secString}";
    }
    
}