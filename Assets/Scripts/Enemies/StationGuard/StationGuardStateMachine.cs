using UnityEngine;

public class StationGuardStateMachine : StateMachine
{
    public StationGuard stationGuard;
    
    [HideInInspector] public StationGuardIdle idleState;
    [HideInInspector] public StationGuardSuspect suspectState;
    [HideInInspector] public StationGuardAlert alertState;
    [HideInInspector] public StationGuardWin winState;


    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    private void Awake()
    {
        idleState = new StationGuardIdle(this);
        alertState = new StationGuardAlert(this);
        suspectState = new StationGuardSuspect(this);
        winState = new StationGuardWin(this);
    }
}