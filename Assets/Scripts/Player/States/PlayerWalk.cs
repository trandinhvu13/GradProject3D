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
        player.maxSpeed = player.data.walkSpeed;
        Helper.SetTriggerAnimator(player.animator, "Walk");

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!player.data.isMoving)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }else if (player.data.isRunning)
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
