using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Main;
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

    private void SaveNewScore()
    {
        PlayerDataManager.instance.SaveScore(LevelManager.instance.currentLevelID, LevelManager.instance.finishTime,
            LevelManager.instance.finishMilestone);
        PlayerDataManager.instance.SaveProgress(PlayerDataManager.instance.GetCurrentLevel() + 1);
    }

    private void SetupText()
    {
        float currentFinishTime = LevelManager.instance.finishTime;

        string key = $"{PlayerDataManager.instance.username}/Score/{currentFinishTime}";
        string oldScore = SPrefs.GetString(key, "");

        if (oldScore == "")
        {
            finishTime.text = $"FINISH TIME: {Helper.ChangeTimeToTextString(currentFinishTime)} (NEW)";
            bestTime.text = $"PERSONAL BEST: {Helper.ChangeTimeToTextString(currentFinishTime)}";
            SaveNewScore();
        }
        else
        {
            float oldTime = float.Parse(oldScore.Split("/")[0]);
            if (currentFinishTime > oldTime)
            {
                finishTime.text = $"FINISH TIME: {Helper.ChangeTimeToTextString(currentFinishTime)} (NEW)";
                bestTime.text = $"PERSONAL BEST: {Helper.ChangeTimeToTextString(currentFinishTime)}";
                SaveNewScore();
            }
            else
            {
                finishTime.text = $"FINISH TIME: {Helper.ChangeTimeToTextString(currentFinishTime)}";
                bestTime.text = $"PERSONAL BEST: {Helper.ChangeTimeToTextString(oldTime)}";
            }
        }

        finishTime.transform.localScale = Vector3.zero;
        bestTime.transform.localScale = Vector3.zero;
        retryButton.transform.localScale = Vector3.zero;
        highScoreButton.transform.localScale = Vector3.zero;
        nextButton.transform.localScale = Vector3.zero;
    }

    private void BindButton()
    {
        retryButton.onClick.AddListener(() =>
        {
            Close();
            LevelManager.instance.Retry();
        });

        nextButton.onClick.AddListener(() =>
        {
            PlayerDataManager.instance.levelIDToLoad = LevelManager.instance.currentLevelID + 1;
            SceneController.instance.Load("Main", () => { Close(); },
                () => { GameUIManager.instance.gameTimer.StartTime(); });
        });
    }

    IEnumerator ShowAnimation()
    {
        yield return new WaitForSeconds(middleTime);
        finishTime.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(middleTime);
        bestTime.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(middleTime);

        for (int i = 1; i <= stars.Count; i++)
        {
            if (i <= LevelManager.instance.finishMilestone-1)
            {
                stars[i-1].Show(true);
            }
            else
            {
                stars[i-1].Show(false);
            }

            yield return new WaitForSeconds(stars[i-1].showTime);
        }

        yield return new WaitForSeconds(middleTime);

        retryButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        highScoreButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        nextButton.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
    }
}