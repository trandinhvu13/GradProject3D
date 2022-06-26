using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using Path = Pathfinding.Path;

public class StationGuardSuspect : BaseState
{
    private StationGuard stationGuard;
    private StationGuardStateMachine stationGuardStateMachine;
    
    public StationGuardSuspect(StationGuardStateMachine stateMachine) : base("StationGuardSuspect", stateMachine)
    {
        stationGuardStateMachine = stateMachine;
        stationGuard = stationGuardStateMachine.stationGuard;
    }
    
    public override void Enter()
    {
        base.Enter();
        stationGuard.canMove = true;
        stationGuard.data.isMoving = true;
        stationGuard.data.isRunning = false;
        stationGuard.maxSpeed = stationGuard.data.suspectSpeed;
        stationGuard.data.isInStation = false;
        stationGuard.seekerScript.StartPath(stationGuard.transform.position, LevelManager.instance.player.transform.position,
            (Path p) =>
            {
                Helper.SetTriggerAnimator(stationGuard.animator, "Walk");
                stationGuard.playerLastPlaceIndicator.Show(LevelManager.instance.player.transform.position);
            });
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (stationGuard.suspectMeterAmount >= stationGuard.data.suspectMeterMax)
        {
            stationGuardStateMachine.ChangeState(stationGuardStateMachine.alertState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void OnTargetReached()
    {
        stationGuardStateMachine.ChangeState(stationGuardStateMachine.idleState);
    }

    public void OnHearPlayer()
    {
        stationGuard.seekerScript.StartPath(stationGuard.transform.position, LevelManager.instance.player.transform.position,(Path p) =>
        {
            stationGuard.playerLastPlaceIndicator.Show(LevelManager.instance.player.transform.position);
        });
    }
}