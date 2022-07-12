using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static void SetTriggerAnimator(this Animator animator, string triggerName)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }

        animator.SetTrigger(triggerName);
    }

    public static void WaitThenDo(MonoBehaviour monoBehaviour, float waitTime, bool isRealTime, Action method)
    {
        monoBehaviour.StartCoroutine(CoroutineWaitThenDo(waitTime, isRealTime, method));
    }

    static IEnumerator CoroutineWaitThenDo(float waitTime, bool isRealTime, Action method)
    {
        if (isRealTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }

        method();
    }
}