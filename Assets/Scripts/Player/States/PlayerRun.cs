using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;

    public PlayerRun(PlayerStateMachine stateMachine) : base("PlayerRun", stateMachine)
    {
        playerStateMachine = stateMachine;
        player = playerStateMachine.player;
    }

    public override void Enter()
    {
        base.Enter();
        player.maxSpeed = player.data.runSpeed;
        Helper.SetTriggerAnimator(player.animator, "Run");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!player.data.isMoving)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }else if (!player.data.isRunning)
        {
            playerStateMachine.ChangeState(playerStateMachine.walkState);
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