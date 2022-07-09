using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GuardIdle : BaseState
{
    private NormalGuard normalGuard;
    private NormalGuardStateMachine normalGuardStateMachine;
    private bool isPause = false;

    private Tween rotateTween;

    public GuardIdle(NormalGuardStateMachine stateMachine) : base("GuardIdle", stateMachine)
    {
        normalGuardStateMachine = stateMachine;
        normalGuard = normalGuardStateMachine.normalGuard;
    }

    public override void Enter()
    {
        base.Enter();
        Helper.SetTriggerAnimator(normalGuard.animator, "Idle");
        rotateTween = null;
        isPause = false;
        normalGuard.data.isMoving = false;
        normalGuard.canMove = false;
        normalGuard.lookAroundCoroutine = normalGuard.StartCoroutine(LookAround());
        normalGuard.playerLastPlaceIndicator.Hide();
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
        normalGuard.StopCoroutine(normalGuard.lookAroundCoroutine);
        normalGuard.lookAroundCoroutine = null;
    }

    private void SightOfPlayer()
    {
        if (normalGuard.suspectMeterAmount > 0 && normalGuard.suspectMeterAmount < normalGuard.data.suspectMeterMax)
        {
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.suspectState);
            return;
        }

        if (normalGuard.suspectMeterAmount >= normalGuard.data.suspectMeterMax)
        {
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.alertState);
        }
    }
    IEnumerator LookAround()
    {
        normalGuard.data.currentRotationCount = 0;
        int rotationCount = normalGuard.data.rotationCount;

        Vector3 currentRotation = normalGuard.transform.eulerAngles;
        Vector3 targetRotationRight = new Vector3(0, currentRotation.y + normalGuard.data.rotationAmount, 0);
        Vector3 targetRotationLeft = new Vector3(0, currentRotation.y - normalGuard.data.rotationAmount, 0);

        bool isRotateRight = false;

        while (normalGuard.data.currentRotationCount < rotationCount)
        {   
            bool isDoneAll = false;
            normalGuard.data.currentRotationCount++;

            float rotationTime = normalGuard.data.currentRotationCount == 1
                ? normalGuard.data.rotationTime / 2
                : normalGuard.data.rotationTime;

            if (isRotateRight)
            {
              rotateTween =  normalGuard.transform
                    .DORotate(targetRotationRight,
                        rotationTime)
                    .SetEase(normalGuard.data.rotationEase).OnComplete(() =>
                    {
                        isDoneAll = true;
                    });
            }
            else
            {
                rotateTween = normalGuard.transform.DORotate(targetRotationLeft,
                        rotationTime)
                    .SetEase(normalGuard.data.rotationEase).OnComplete(() =>
                    {
                        isDoneAll = true;
                    });
            }

            isRotateRight = !isRotateRight;
            
            yield return new WaitUntil(() => isDoneAll);
            //yield return new WaitForSeconds(rotationTime);
            yield return new WaitForSeconds(Random.Range(0, normalGuard.data.rotationIntervalTime));
        }
        
        stateMachine.ChangeState(normalGuardStateMachine.patrolState);
    }

    public void OnHearPlayer()
    {
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.suspectState);
    }

}