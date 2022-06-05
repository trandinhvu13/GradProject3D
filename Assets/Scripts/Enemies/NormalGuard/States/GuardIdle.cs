using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GuardIdle : BaseState
{
    private NormalGuard normalGuard;
    private NormalGuardStateMachine normalGuardStateMachine;
    [SerializeField] private bool isPause = false;

    public GuardIdle(NormalGuardStateMachine stateMachine) : base("GuardIdle", stateMachine)
    {
        normalGuardStateMachine = stateMachine;
        normalGuard = normalGuardStateMachine.normalGuard;
    }

    public override void Enter()
    {
        base.Enter();
        Helper.SetTriggerAnimator(normalGuard.animator, "Idle");
        isPause = false;
        normalGuard.data.isMoving = false;
        normalGuard.canMove = false;
        normalGuard.lookAroundCoroutine = normalGuard.StartCoroutine(LookAround());
        Debug.Log("Guard in Idle");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (normalGuard.suspectMeter > 0 && normalGuard.suspectMeter < normalGuard.data.suspectMeterMax && !isPause)
        {
            Debug.Log("Pause");
            isPause = true;
            normalGuard.transform.DOPause();
        }
        else if (normalGuard.suspectMeter <= 0 && isPause)
        {
            isPause = false;
            Debug.Log("Resume");
            normalGuard.transform.DOTogglePause();
        }
        
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
            normalGuard.data.currentRotationCount++;

            float rotationTime = normalGuard.data.currentRotationCount == 1
                ? normalGuard.data.rotationTime / 2
                : normalGuard.data.rotationTime;

            if (isRotateRight)
            {
                normalGuard.transform
                    .DORotate(targetRotationRight,
                        rotationTime)
                    .SetEase(normalGuard.data.rotationEase);
            }
            else
            {
                normalGuard.transform.DORotate(targetRotationLeft,
                        rotationTime)
                    .SetEase(normalGuard.data.rotationEase);
            }

            isRotateRight = !isRotateRight;

            yield return new WaitForSeconds(rotationTime);
            yield return new WaitForSeconds(Random.Range(0, normalGuard.data.rotationIntervalTime));
        }

        stateMachine.ChangeState(normalGuardStateMachine.patrolState);
    }


}