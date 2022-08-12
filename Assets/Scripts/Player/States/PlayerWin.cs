using System.Collections;
using System.Collections.Generic;
using Game;
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
        player.maxSpeed = player.data.walkSpeed;
        Helper.SetTriggerAnimator(player.animator, "Walk");
        player.seekerScript.StartPath(player.transform.position, LevelManager.instance.destinationTransform.position,
            (Path p) =>
            {
                
            });
    }

    public void OnTargetReached()
    {
        player.canMove=false;
        player.winGameCamera.gameObject.SetActive(true);
        player.winParticle.SetActive(true);
        Helper.SetTriggerAnimator(player.animator, "Win");
    }
}
