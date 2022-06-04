using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGuardStateMachine : StateMachine
{
    public NormalGuard normalGuard;
    
    [HideInInspector] public GuardIdle idleState;
    [HideInInspector] public GuardPatrol patrolState;
    [HideInInspector] public GuardSuspect suspectState;
    [HideInInspector] public GuardAlert alertState;


    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    private void Awake()
    {
        idleState = new GuardIdle(this);
        patrolState = new GuardPatrol(this);
        alertState = new GuardAlert(this);
        suspectState = new GuardSuspect(this);
    }
}
