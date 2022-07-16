using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    public Image fadeBackground;
    
    public ItemsListHUD itemsListHUD;
    public GameTimer gameTimer;
    public GameObject leftHUD;
    public GameObject rightHUD;

    public WalkableIndicator walkableIndicator;
    public WalkableCursor walkableCursor;
    public TargetCursor targetCursor;
    protected override void InternalInit()
    {
    }

    protected override void InternalOnDestroy()
    {
        
    }

    protected override void InternalOnEnable()
    {
        GameEvent.instance.OnPlayerWin += Win;
        GameEvent.instance.OnPlayerLose += Lose;
    }

    protected override void InternalOnDisable()
    {
        if(GameEvent.instance) GameEvent.instance.OnPlayerWin -= Win;
        if(GameEvent.instance) GameEvent.instance.OnPlayerLose -= Lose;
    }

    public void MoveOutStatsHUD()
    {
        leftHUD.transform.DOScale(Vector3.zero, 0.2f);
        rightHUD.transform.DOScale(Vector3.zero, 0.2f);
    }
    
    private void MoveInStatsHUD()
    {
        leftHUD.transform.DOScale(new Vector3(1,1,1), 0.2f);
        rightHUD.transform.DOScale(new Vector3(1,1,1), 0.2f);
    }

    public void SetupNewGame(List<CollectableItem> collectableItems)
    {
        MoveInStatsHUD();
        
        walkableCursor.Show();
        walkableIndicator.gameObject.SetActive(true);
        targetCursor.Show();
        
        gameTimer.SetupNew();
        itemsListHUD.LoadItemInLevel(collectableItems);
    }

    private void Win()
    {
        MoveOutStatsHUD();
        DialogSystem.instance.GetDialog("WinDialog").Open();
    }

    private void Lose()
    {
        MoveOutStatsHUD();
        DialogSystem.instance.GetDialog("LoseDialog").Open();
    }

}
