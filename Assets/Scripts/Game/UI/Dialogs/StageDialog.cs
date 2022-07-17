using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDialog : Dialog
{
    [SerializeField] private List<Stage> stages;
    [SerializeField] private Button home;
    [SerializeField] private TextMeshProUGUI currentStar;

    public override void Init()
    {
        base.Init();

        home.onClick.AddListener(() => { Close(); });

        currentStar.text = $"{PlayerDataManager.instance.GetCurrentStar()}/{PlayerDataManager.instance.GetTotalStar()}";

        int currentLevel = PlayerDataManager.instance.GetCurrentLevel();

        for (int i = 0; i < stages.Count; i++)
        {
            if (i <= PlayerDataManager.instance.numberOfLevel - 1)
                stages[i].Setup(this, i, currentLevel);
            else
            {
                stages[i].gameObject.SetActive(false);
            }
        }
    }

    public override void Outro()
    {
        base.Outro();
    }
}