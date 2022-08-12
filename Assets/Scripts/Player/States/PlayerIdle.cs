using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;

    public PlayerIdle(PlayerStateMachine stateMachine) : base("PlayerIdle", stateMachine)
    {
        playerStateMachine = stateMachine;
        player = playerStateMachine.player;
    }

    public override void Enter()
    {
        base.Enter();
        player.EnableSmokeTrail(false);
        Helper.SetTriggerAnimator(player.animator, "Idle");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (player.data.isMoving)
        {
            if (player.data.isRunning) playerStateMachine.ChangeState(playerStateMachine.runState);
            else playerStateMachine.ChangeState(playerStateMachine.walkState);
        }
    }
}