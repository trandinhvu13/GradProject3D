using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class GameUIManager : MonoSingleton<GameUIManager>
    {
        public ItemsListHUD itemsListHUD;
        public GameTimer gameTimer;
        [SerializeField] private GameObject leftHUD;
        [SerializeField] private GameObject rightHUD;

        [SerializeField] private WalkableIndicator walkableIndicator;
        [SerializeField] private WalkableCursor walkableCursor;
        [SerializeField] private TargetCursor targetCursor;
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

        private void MoveOutStatsHUD()
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
}
