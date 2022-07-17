using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image image;
    [SerializeField] private Shape circle;
    [SerializeField] private float transitionDuration;
    [SerializeField] private Ease ease;
    #endregion

    #region Method

    private void Start()
    {
        image.raycastTarget = false;
    }

    public void Intro(Action action = null)
    {
        image.raycastTarget = true;
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
            Debug.Log(action);
            image.raycastTarget = false;
            action?.Invoke();
        });
    }

    public float GetTweenTime()
    {
        return transitionDuration;
    }
    #endregion
}