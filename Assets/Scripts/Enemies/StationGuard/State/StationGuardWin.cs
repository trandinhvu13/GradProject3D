using Game;
using UnityEngine;

namespace Enemies.StationGuard.State
{
    public class StationGuardWin : BaseState
    {
        private StationGuard stationGuard;
        private StationGuardStateMachine stationGuardStateMachine;
    
        public StationGuardWin(StationGuardStateMachine stateMachine) : base("StationGuardWin", stateMachine)
        {
            stationGuardStateMachine = stateMachine;
            stationGuard = stationGuardStateMachine.stationGuard;
        }
    
        public override void Enter()
        {
            base.Enter();
            LevelManager.instance.detectedEnemy = stationGuard.transform;
            Helper.SetTriggerAnimator(stationGuard.animator, "Win");
            stationGuard.data.isMoving = false;
            stationGuard.canMove = false;
            stationGuard.playerLastPlaceIndicator.Hide();
            LevelManager.instance.Lose();
        }
    }
}
