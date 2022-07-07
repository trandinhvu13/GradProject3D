using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGuardAlert : BaseState
{
    private StationGuard stationGuard;
    private StationGuardStateMachine stationGuardStateMachine;

    public StationGuardAlert(StationGuardStateMachine stateMachine) : base("StationGuardAlert", stateMachine)
    {
        stationGuardStateMachine = stateMachine;
        stationGuard = stationGuardStateMachine.stationGuard;
    }

    public override void Enter()
    {
        base.Enter();
        stationGuard.playerLastPlaceIndicator.Hide();
        stationGuard.canMove = true; 
        stationGuard.data.isMoving = true;
        stationGuard.data.isRunning = true;
        stationGuard.data.isInStation = false;
        stationGuard.maxSpeed = stationGuard.data.alertSpeed;
        stationGuard.onSearchPath += UpdateLogic;
        Helper.SetTriggerAnimator(stationGuard.animator, "Run");
        Debug.Log("Alert");
    }

    public void Chase()
    {
        stationGuard.playerLastPlaceIndicator.Hide();
        stationGuard.destination = LevelManager.instance.player.transform.position;
        //if(stationGuard.suspectMeterAmount <= 0) stationGuardStateMachine.ChangeState(stationGuardStateMachine.idleState);
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
        stationGuard.onSearchPath -= UpdateLogic;
    }

    public void OnTargetReached()
    {
        stationGuard.playerLastPlaceIndicator.Hide();
    }
}