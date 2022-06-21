using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using Path = Pathfinding.Path;

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
        normalGuard.seekerScript.StartPath(normalGuard.transform.position, LevelManager.instance.player.transform.position,
            (Path p) =>
            {
                Helper.SetTriggerAnimator(normalGuard.animator, "Walk");
            });

        Debug.Log("Guard suspect");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (normalGuard.suspectMeterAmount >= normalGuard.data.suspectMeterMax)
        {
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.alertState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void OnTargetReached()
    {
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
    }

    public void OnHearPlayer()
    {
        normalGuard.seekerScript.StartPath(normalGuard.transform.position, LevelManager.instance.player.transform.position);
    }
}
