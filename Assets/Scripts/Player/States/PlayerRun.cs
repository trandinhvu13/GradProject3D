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
        MakeSound();
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
        runningTween.Kill();
        player.soundRing
            .DOFade(0,
                player.data.runRingTweenTime)
            .SetEase(player.data.soundRingTweenType);
    }

    private void MakeSound()
    {
        //if (runningTween.IsActive() && runningTween != null && runningTween.IsPlaying()) return;
        player.soundRing.transform.localScale = new Vector3(player.data.runSoundRadius*2, player.data.runSoundRadius*2, 1);
        player.soundRing.color = new Color(255, 255, 255, 0);
        
        runningTween = player.soundRing
            .DOFade(1,
                player.data.runRingTweenTime).From(0)
            .SetEase(player.data.soundRingTweenType).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() =>
            {
                if (runningTween.CompletedLoops() % 2 != 0)
                {
                    GameEvent.instance.PLayerRun(player.transform, player.data.runSoundRadius);
                }
            });
    }
}