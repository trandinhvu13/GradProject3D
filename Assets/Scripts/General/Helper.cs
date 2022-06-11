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
}