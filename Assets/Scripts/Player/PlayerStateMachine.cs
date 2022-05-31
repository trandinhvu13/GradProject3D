using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player;

    [HideInInspector] public PlayerIdle idleState;
    [HideInInspector] public PlayerWalk walkState;
    [HideInInspector] public PlayerRun runState;

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    private void Awake()
    {
        idleState = new PlayerIdle(this);
        walkState = new PlayerWalk(this);
        runState = new PlayerRun(this);
    }
}