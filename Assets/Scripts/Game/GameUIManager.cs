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
    public List<Dialog> gameDialogs;
    public GameObject leftHUD;
    public GameObject rightHUD;
    public Button pauseButton;

    public WalkableIndicator walkableIndicator;
    public WalkableCursor walkableCursor;
    public TargetCursor targetCursor;
    
    public Dialog currentDialog;
    protected override void InternalInit()
    {
        pauseButton.onClick.AddListener(LevelManager.instance.PauseGame);
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
        
        walkableCursor.Show();
        walkableIndicator.gameObject.SetActive(true);
        targetCursor.Show();
        
        gameTimer.SetupNew();
        itemsListHUD.LoadItemInLevel(collectableItems);
    }

    public void FadeInBackground()
    {
        fadeBackground.gameObject.SetActive(true);
        fadeBackground.DOFade(0.75f, 0.2f).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void FadeOutBackground()
    {
        fadeBackground.DOFade(0, 0.2f).SetEase(Ease.InQuad).SetUpdate(true).OnComplete(() =>
        {
            fadeBackground.gameObject.SetActive(false);
        });
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

    public void CloseCurrentDialog()
    {
        if (currentDialog == null) return;
        currentDialog.Close();
    }
    
}
