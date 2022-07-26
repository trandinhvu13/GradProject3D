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

    public static void WaitThenDo(this MonoBehaviour monoBehaviour, float waitTime, bool isRealTime, Action method)
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
    
    public static void DoThenWait(this MonoBehaviour monoBehaviour, float waitTime, bool isRealTime, Action method)
    {
        monoBehaviour.StartCoroutine(CoroutineWaitThenDo(waitTime, isRealTime, method));
    }

    static IEnumerator CoroutineDoThenWait(float waitTime, bool isRealTime, Action method)
    {
        method();
        
        if (isRealTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    public static void Wait(this MonoBehaviour monoBehaviour, float waitTime, bool isRealTime=false)
    {
        monoBehaviour.StartCoroutine(Wait(waitTime, isRealTime));
    }

    static IEnumerator Wait(float waitTime, bool isRealTime)
    {

        if (isRealTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }
    }

    public static string ChangeTimeToTextString(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;

        string minString = min.ToString();
        string secString = sec.ToString();
        string milisecString = (time - (int)time).ToString();
        if (milisecString.Length >= 2)
        {
            milisecString=milisecString.Substring(2, 3);
        }
        else
        {
            milisecString = "000";
        }

        if (min < 10)
        {
            minString = $"0{min}";
        }

        if (sec < 10)
        {
            secString = $"0{sec}";
        }

        return $"{minString}:{secString}.{milisecString}";
    }
    
    public static int CalculateMilestone(float time, List<float> milestoneTimes)
    {
        for (int i = 3; i > 0; i--)
        {
            if (time <= milestoneTimes[i-1])
            {
                return i;
            }
        }

        return 0;
    }
}