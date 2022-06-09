using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerRun : BaseState
{
    private Player player;
    private PlayerStateMachine playerStateMachine;

    private Coroutine makeRunSoundCoroutine;
    private Tween runningTween;

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
        }
        else if (!player.data.isRunning)
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

    private void MakeSound()
    {
        //if (runningTween.IsActive() && runningTween != null && runningTween.IsPlaying()) return;

        runningTween = player.soundRing.transform
            .DOScale(new Vector3(player.data.runSoundRadius * 2, player.data.runSoundRadius * 2, 1),
                player.data.whistleRingTweenTime)
            .SetEase(player.data.soundRingTweenType).OnStepComplete(() => { });
    }
}