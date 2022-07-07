using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLose : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    public PlayerLose(PlayerStateMachine stateMachine) : base("PlayerLose", stateMachine)
    {
        playerStateMachine = stateMachine;
        player = playerStateMachine.player;
    }
    
    public override void UpdateLogic()
    {
        base.UpdateLogic();
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
