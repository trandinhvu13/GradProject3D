using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Guard Win");
        LevelManager.instance.detectedEnemy = stationGuard.transform;
        Helper.SetTriggerAnimator(stationGuard.animator, "Win");
        stationGuard.data.isMoving = false;
        stationGuard.canMove = false;
        stationGuard.playerLastPlaceIndicator.Hide();
        GameEvent.instance.PlayerLose();
    }
}
