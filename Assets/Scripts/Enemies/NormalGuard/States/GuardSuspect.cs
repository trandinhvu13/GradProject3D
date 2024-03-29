using Game;
using Path = Pathfinding.Path;

namespace Enemies.NormalGuard.States
{
    public class GuardSuspect : BaseState
    {
        private NormalGuard normalGuard;
        private NormalGuardStateMachine normalGuardStateMachine;
    
        public GuardSuspect(NormalGuardStateMachine stateMachine) : base("GuardSuspect", stateMachine)
        {
            normalGuardStateMachine = stateMachine;
            normalGuard = normalGuardStateMachine.normalGuard;
        }
    
        public override void Enter()
        {
            base.Enter();
            normalGuard.canMove = true;
            normalGuard.data.isMoving = true;
            normalGuard.data.isRunning = false;
            normalGuard.maxSpeed = normalGuard.data.suspectSpeed;
        
            var position = LevelManager.instance.player.transform.position;
            normalGuard.seekerScript.StartPath(normalGuard.transform.position, position,
                (Path p) =>
                {
                    Helper.SetTriggerAnimator(normalGuard.animator, "Walk");
                });
            normalGuard.playerLastPlaceIndicator.Show(position);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            if (normalGuard.suspectMeterAmount >= normalGuard.data.suspectMeterMax)
            {
                normalGuardStateMachine.ChangeState(normalGuardStateMachine.alertState);
            }
        }

        public void OnTargetReached()
        {
            normalGuard.suspectMeterAmount = 0;
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
            normalGuard.playerLastPlaceIndicator.Hide();
        }

        public void OnHearPlayer()
        {
            var position = LevelManager.instance.player.transform.position;
            normalGuard.seekerScript.StartPath(normalGuard.transform.position, position);
            normalGuard.playerLastPlaceIndicator.Show(position);
        }
    }
}
