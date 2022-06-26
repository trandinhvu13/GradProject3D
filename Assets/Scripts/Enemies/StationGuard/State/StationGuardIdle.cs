using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class StationGuardIdle : BaseState
{
    private StationGuard stationGuard;
    private StationGuardStateMachine stationGuardStateMachine;
    [SerializeField] private bool isPause = false;

    private Tween rotateTween;

    public StationGuardIdle(StationGuardStateMachine stateMachine) : base("StationGuardIdle", stateMachine)
    {
        stationGuardStateMachine = stateMachine;
        stationGuard = stationGuardStateMachine.stationGuard;
    }

    public override void Enter()
    {
        base.Enter();

        if (!stationGuard.data.isInStation)
        {
            stationGuard.seekerScript.StartPath(stationGuard.transform.position, stationGuard.data.stationPos,
                (Path p) => { Helper.SetTriggerAnimator(stationGuard.animator, "Walk"); });
        }
        else
        {
            SetupIdle();
            stationGuard.lookAroundCoroutine = stationGuard.StartCoroutine(LookAround());
        }
    }

    private void SetupIdle()
    {
        Helper.SetTriggerAnimator(stationGuard.animator, "Idle");
        rotateTween = null;
        isPause = false;
        stationGuard.data.isMoving = false;
        stationGuard.canMove = false;
        stationGuard.data.isInStation = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SightOfPlayer();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        if (stationGuard.lookAroundCoroutine != null) stationGuard.StopCoroutine(stationGuard.lookAroundCoroutine);
        stationGuard.lookAroundCoroutine = null;
    }

    private void SightOfPlayer()
    {
        if (stationGuard.suspectMeterAmount > 0 && stationGuard.suspectMeterAmount < stationGuard.data.suspectMeterMax)
        {
            stationGuardStateMachine.ChangeState(stationGuardStateMachine.suspectState);
            return;
        }

        if (stationGuard.suspectMeterAmount >= stationGuard.data.suspectMeterMax)
        {
            stationGuardStateMachine.ChangeState(stationGuardStateMachine.alertState);
        }
    }

    IEnumerator LookAround()
    {
        stationGuard.data.currentRotationCount = 0;

        Vector3 currentRotation = stationGuard.transform.eulerAngles;
        Vector3 targetRotationRight = new Vector3(0, currentRotation.y + stationGuard.data.rotationAmount, 0);
        Vector3 targetRotationLeft = new Vector3(0, currentRotation.y - stationGuard.data.rotationAmount, 0);

        bool isRotateRight = false;

        while (true)
        {
            bool isDoneAll = false;
            stationGuard.data.currentRotationCount++;

            float rotationTime = stationGuard.data.currentRotationCount == 1
                ? stationGuard.data.rotationTime / 2
                : stationGuard.data.rotationTime;

            Vector3 targetRotation = isRotateRight ? targetRotationRight : targetRotationLeft;

            rotateTween = stationGuard.transform
                .DORotate(targetRotation,
                    rotationTime)
                .SetEase(stationGuard.data.rotationEase).OnComplete(() => { isDoneAll = true; });

            isRotateRight = !isRotateRight;

            yield return new WaitUntil(() => isDoneAll);
            yield return new WaitForSeconds(Random.Range(0, stationGuard.data.rotationIntervalTime));
        }

        //stateMachine.ChangeState(normalGuardStateMachine.patrolState);
    }

    public void OnHearPlayer()
    {
        stationGuardStateMachine.ChangeState(stationGuardStateMachine.suspectState);
    }

    public void OnTargetReached()
    {
        stationGuard.transform
            .DORotate(stationGuard.data.stationRotation,
                stationGuard.data.rotationTime / 2)
            .SetEase(stationGuard.data.rotationEase).OnComplete(() =>
            {
                SetupIdle();
                stationGuard.lookAroundCoroutine = stationGuard.StartCoroutine(LookAround());
            });
    }
}