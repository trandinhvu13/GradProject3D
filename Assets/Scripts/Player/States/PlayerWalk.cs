using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    
    public PlayerWalk(PlayerStateMachine stateMachine) : base("PlayerWalk", stateMachine)
    {
        playerStateMachine = stateMachine;
        player = playerStateMachine.player;
    }

    public override void Enter()
    {
        base.Enter();
        player.ChangeToRunMode(false);
        Helper.SetTriggerAnimator(player.animator, "Walk");

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!player.isMoving)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }else if (player.isRunning)
        {
            playerStateMachine.ChangeState(playerStateMachine.runState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
