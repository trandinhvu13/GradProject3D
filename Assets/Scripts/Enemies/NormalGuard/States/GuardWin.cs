using Game;
using UnityEngine;

namespace Enemies.NormalGuard.States
{
    public class GuardWin : BaseState
    {
        private NormalGuard normalGuard;
        private NormalGuardStateMachine normalGuardStateMachine;
    
        public GuardWin(NormalGuardStateMachine stateMachine) : base("GuardWin", stateMachine)
        {
            normalGuardStateMachine = stateMachine;
            normalGuard = normalGuardStateMachine.normalGuard;
        }
    
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Guard Win");
            LevelManager.instance.detectedEnemy = normalGuard.transform;
            Helper.SetTriggerAnimator(normalGuard.animator, "Win");
            normalGuard.data.isMoving = false;
            normalGuard.canMove = false;
            normalGuard.playerLastPlaceIndicator.Hide();
            LevelManager.instance.Lose();
        }
    }
}
