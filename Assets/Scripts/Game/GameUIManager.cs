using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    public ItemsListHUD itemsListHUD;
    public GameTimer gameTimer;
    public List<Dialog> gameDialogs;
    public GameObject leftHUD;
    public GameObject rightHUD;

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

    public Dialog GetDialog(string id)
    {
        return gameDialogs.Find(x => x.id == id);
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
        
        gameTimer.SetupNew();
        itemsListHUD.LoadItemInLevel(collectableItems);
    }

    private void Win()
    {
        MoveOutStatsHUD();
        GetDialog("WinDialog").Open();
    }

    private void Lose()
    {
        MoveOutStatsHUD();
        GetDialog("LoseDialog").Open();
    }
    
}
