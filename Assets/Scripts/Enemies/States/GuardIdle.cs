using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GuardIdle : BaseState
{
    private NormalGuard normalGuard;
    private NormalGuardStateMachine normalGuardStateMachine;

    public GuardIdle(NormalGuardStateMachine stateMachine) : base("GuardIdle", stateMachine)
    {
        normalGuardStateMachine = stateMachine;
        normalGuard = normalGuardStateMachine.normalGuard;
    }

    public override void Enter()
    {
        base.Enter();
        normalGuard.canMove = false;
        normalGuard.lookAroundCoroutine = normalGuard.StartCoroutine(LookAround());
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

    IEnumerator LookAround()
    {
        normalGuard.currentRotationCount = 0;

        Vector3 currentRotation = normalGuard.transform.eulerAngles;
        Vector3 targetRotationRight = new Vector3(0, currentRotation.y + normalGuard.rotationAmount, 0);
        Vector3 targetRotationLeft = new Vector3(0, currentRotation.y - normalGuard.rotationAmount, 0);

        bool isRotateRight = false;

        while (normalGuard.currentRotationCount < normalGuard.rotationTime)
        {
            normalGuard.currentRotationCount++;

            float rotationTime = normalGuard.currentRotationCount == 1 ? normalGuard.rotationTime / 2 : normalGuard.rotationTime;
            
            if (isRotateRight)
            {
                normalGuard.transform
                    .DORotate(targetRotationRight,
                        rotationTime)
                    .SetEase(normalGuard.rotationEase);
            }
            else
            {
                normalGuard.transform.DORotate(targetRotationLeft,
                       rotationTime)
                    .SetEase(normalGuard.rotationEase);
            }

            isRotateRight = !isRotateRight;

            yield return new WaitForSeconds(rotationTime);
            yield return new WaitForSeconds(normalGuard.rotationIntervalTime);
        }
    }
}