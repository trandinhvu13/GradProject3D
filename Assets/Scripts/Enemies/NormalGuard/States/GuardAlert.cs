using Game;
using UnityEngine;

namespace Enemies.NormalGuard.States
{
    public class GuardAlert : BaseState
    {
        private NormalGuard normalGuard;
        private NormalGuardStateMachine normalGuardStateMachine;

        public GuardAlert(NormalGuardStateMachine stateMachine) : base("GuardAlert", stateMachine)
        {
            normalGuardStateMachine = stateMachine;
            normalGuard = normalGuardStateMachine.normalGuard;
        }

        public override void Enter()
        {
            base.Enter();
            GameEvent.instance.EnemyAlert(normalGuard.transform);
            normalGuard.canMove = true; 
            normalGuard.data.isMoving = true;
            normalGuard.data.isRunning = true;
            normalGuard.maxSpeed = normalGuard.data.alertSpeed;
            //normalGuard.seekerScript.StartPath(normalGuard.transform.position, LevelManager.instance.playerTransform.position);
            normalGuard.onSearchPath += UpdateLogic;
            Helper.SetTriggerAnimator(normalGuard.animator, "Run");
            normalGuard.playerLastPlaceIndicator.Hide();
        }

        public void Chase()
        {
            Transform player = LevelManager.instance.player.transform;
            if (player != null) normalGuard.destination = player.position;
            //if(normalGuard.suspectMeterAmount <= 0) normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            Chase();
        }

        public override void Exit()
        {
            base.Exit();
            normalGuard.onSearchPath -= UpdateLogic;
        }

        public void OnTargetReached()
        {
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
        }
    }
}