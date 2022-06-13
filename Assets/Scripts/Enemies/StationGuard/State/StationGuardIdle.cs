using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Helper.SetTriggerAnimator(stationGuard.animator, "Idle");
        rotateTween = null;
        isPause = false;
        stationGuard.data.isMoving = false;
        stationGuard.canMove = false;
        stationGuard.lookAroundCoroutine = stationGuard.StartCoroutine(LookAround());
        Debug.Log("Guard idle");
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
        stationGuard.StopCoroutine(stationGuard.lookAroundCoroutine);
        stationGuard.lookAroundCoroutine = null;
    }

    private void SightOfPlayer()
    {
        if (stationGuard.suspectMeter > 0 && stationGuard.suspectMeter < stationGuard.data.suspectMeterMax)
        {
            stationGuardStateMachine.ChangeState(stationGuardStateMachine.suspectState);
            return;
        }

        if (stationGuard.suspectMeter >= stationGuard.data.suspectMeterMax)
        {
            stationGuardStateMachine.ChangeState(stationGuardStateMachine.alertState);
        }
    }
    //TODO: NEED RE CODE TO REMOVE PATROL
    IEnumerator LookAround()
    {
        stationGuard.data.currentRotationCount = 0;
        int rotationCount = stationGuard.data.rotationCount;

        Vector3 currentRotation = stationGuard.transform.eulerAngles;
        Vector3 targetRotationRight = new Vector3(0, currentRotation.y + stationGuard.data.rotationAmount, 0);
        Vector3 targetRotationLeft = new Vector3(0, currentRotation.y - stationGuard.data.rotationAmount, 0);

        bool isRotateRight = false;

        while (stationGuard.data.currentRotationCount < rotationCount)
        {   
            bool isDoneAll = false;
            stationGuard.data.currentRotationCount++;

            float rotationTime = stationGuard.data.currentRotationCount == 1
                ? stationGuard.data.rotationTime / 2
                : stationGuard.data.rotationTime;

            if (isRotateRight)
            {
              rotateTween =  stationGuard.transform
                    .DORotate(targetRotationRight,
                        rotationTime)
                    .SetEase(stationGuard.data.rotationEase).OnComplete(() =>
                    {
                        isDoneAll = true;
                    });
            }
            else
            {
                rotateTween = stationGuard.transform.DORotate(targetRotationLeft,
                        rotationTime)
                    .SetEase(stationGuard.data.rotationEase).OnComplete(() =>
                    {
                        isDoneAll = true;
                    });
            }

            isRotateRight = !isRotateRight;
            
            yield return new WaitUntil(() => isDoneAll);
            //yield return new WaitForSeconds(rotationTime);
            yield return new WaitForSeconds(Random.Range(0, stationGuard.data.rotationIntervalTime));
        }
        
        //stateMachine.ChangeState(normalGuardStateMachine.patrolState);
    }

    public void OnHearPlayer()
    {
        stationGuardStateMachine.ChangeState(stationGuardStateMachine.suspectState);
    }

}