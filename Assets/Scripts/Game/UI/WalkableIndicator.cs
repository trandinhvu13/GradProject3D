using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WalkableIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleTime;
    [SerializeField] private Ease tween;

    private void Awake()
    {
        indicator.DOFade(0, 0);
        transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        GameEvent.instance.OnShowIndicator += Show;
        GameEvent.instance.OnHideIndicator += Hide;
        GameEvent.instance.OnPlayerWin += Hide;
        GameEvent.instance.OnPlayerLose += Hide;
    }

    private void OnDisable()
    {
        if (GameEvent.instance) GameEvent.instance.OnShowIndicator -= Show;
        if (GameEvent.instance) GameEvent.instance.OnHideIndicator -= Hide;
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= Hide;
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Hide;
    }

    private void Show(Vector3 pos)
    {
        if (LevelManager.instance.state is LevelManager.LevelState.Win or LevelManager.LevelState.Lose)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.DOScale(Vector3.zero, 0);
        //Fade in
        indicator.DOFade(0, 0);
        //Change the pos
        transform.position = pos;
        //Scale up
        transform.DOScale(new Vector3(1, 1, 1), scaleTime).SetEase(tween);
        //Fade in
        indicator.DOFade(1, fadeTime).SetEase(tween);
    }

    private void Hide()
    {
        transform.DOScale(Vector3.zero, scaleTime).SetEase(tween);

        indicator.DOFade(0, fadeTime).SetEase(tween);
    }
}