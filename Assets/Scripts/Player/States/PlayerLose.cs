using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using Pathfinding;
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

    public override void Enter()
    {
        base.Enter();
        Vector3 enemyPos = LevelManager.instance.detectedEnemy.position;

        player.canMove = false;
        player.transform.DOLookAt(enemyPos, 0.15f);
        player.loseGameCamera.gameObject.SetActive(true);
        player.loseParticle.SetActive(true);
        Helper.SetTriggerAnimator(player.animator, "Lose");
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        player.loseParticle.transform.position = (player.transform.position+LevelManager.instance.detectedEnemy.position)/2f;
    }
    
}