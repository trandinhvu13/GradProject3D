using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shapes2D;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Shape circle;
    [SerializeField] private float transitionDuration;
    [SerializeField] private Ease ease;
    #endregion

    #region Method

    public void Intro(Action action = null)
    {
        float value = 1;
        DOTween.To(() => value, x => value = x, 0, transitionDuration).SetUpdate(UpdateType.Normal,true).SetEase(ease).OnUpdate(() =>
        {
            circle.settings.innerCutout = new Vector2(value, value);
        }).OnComplete(() =>
        {
            action?.Invoke();
        });
        
    }

    public void Outro(Action action = null)
    {
        float value = circle.settings.innerCutout.x;
        DOTween.To(() => value, x => value = x, 1, transitionDuration).SetUpdate(UpdateType.Normal,true).SetEase(ease).OnUpdate(() =>
        {
            circle.settings.innerCutout = new Vector2(value, value);
        }).OnComplete(() =>
        {
            action?.Invoke();
        });
    }

    public float GetTweenTime()
    {
        return transitionDuration;
    }
    #endregion
}