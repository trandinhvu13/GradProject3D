using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public static class Helper
{
    public static void SetTriggerAnimator(this Animator animator, string triggerName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(triggerName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return;
        
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