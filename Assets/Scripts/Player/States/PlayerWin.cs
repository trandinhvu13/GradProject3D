using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlayerWin : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;
    public PlayerWin(PlayerStateMachine stateMachine) : base("PlayerWin", stateMachine)
    {
        playerStateMachine = stateMachine;
        player = playerStateMachine.player;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player Win");
        player.maxSpeed = player.data.walkSpeed;
        Helper.SetTriggerAnimator(player.animator, "Walk");
        player.seekerScript.StartPath(player.transform.position, LevelManager.instance.destinationTransform.position,
            (Path p) =>
            {
                
            });
    }

    public void OnTargetReached()
    {
        player.endGameCamera.gameObject.SetActive(true);
        Helper.SetTriggerAnimator(player.animator, "Win");
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
