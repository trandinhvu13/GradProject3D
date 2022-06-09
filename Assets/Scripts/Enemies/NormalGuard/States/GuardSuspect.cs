using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Helper.SetTriggerAnimator(normalGuard.animator, "Walk");
        normalGuard.canMove = true;
        normalGuard.data.isMoving = true;
        normalGuard.data.isRunning = false;
        normalGuard.maxSpeed = normalGuard.data.suspectSpeed;
        normalGuard.seekerScript.StartPath(normalGuard.transform.position, LevelManager.instance.playerTransform.position);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void OnTargetReached()
    {
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
    }
}
