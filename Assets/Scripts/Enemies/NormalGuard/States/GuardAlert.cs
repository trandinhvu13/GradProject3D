using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Helper.SetTriggerAnimator(normalGuard.animator, "Run");
        normalGuard.canMove = true;
        normalGuard.data.isMoving = true;
        normalGuard.data.isRunning = true;
        normalGuard.maxSpeed = normalGuard.data.alertSpeed;
        //normalGuard.seekerScript.StartPath(normalGuard.transform.position, LevelManager.instance.playerTransform.position);
        normalGuard.onSearchPath += UpdateLogic;
    }

    public void Chase()
    {
        Transform player = LevelManager.instance.playerTransform;
        if (player != null) normalGuard.destination = player.position;
        if(normalGuard.suspectMeter <= 0) normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Chase();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
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