using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class SuspectMeter : MonoBehaviour
{
    [SerializeField] private Shape suspectMeterShape;
    [SerializeField] private Transform parent;
    private Camera cam;
    [SerializeField] private bool isShow;
    [SerializeField] private Ease showEase;
    [SerializeField] private float showTweenTime;
    [SerializeField] private ParticleSystem angryEmoji;
    [SerializeField] private ParticleSystem suspectEmoji;
    [SerializeField] private MeterState state;

    private enum MeterState
    {
        Normal,
        Suspect,
        Angry,
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        isShow = false;
        transform.localScale = Vector3.zero;
        state = MeterState.Normal;
    }

    private void OnEnable()
    {
        GameEvent.instance.OnPlayerLose += FadeOut;
        GameEvent.instance.OnPlayerWin += FadeOut;
    }

    private void OnDisable()
    {
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= FadeOut;
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= FadeOut;
    }

    private void LateUpdate()
    {
        parent.transform.LookAt(cam.transform);
        parent.transform.rotation = cam.transform.rotation;
    }

    public void ChangeValueSuspectMeter(float amount)
    {
        if (LevelManager.instance.state == LevelManager.LevelState.Win ||
            LevelManager.instance.state == LevelManager.LevelState.Lose) return;
        if (state == MeterState.Angry) return;
        if (amount <= 0)
        {
            if (isShow)
            {
                FadeOut();
            }

            suspectMeterShape.settings.startAngle = 0.01f;
        }
        else if (amount < 1)
        {
            if (!isShow)
            {
                state = MeterState.Suspect;
                suspectEmoji.Play(true);
                FadeIn();
            }

            suspectMeterShape.settings.startAngle = 360 * amount;
        }
        else
        {
            if (isShow)
            {
                state = MeterState.Angry;
                angryEmoji.Play(true);
                FadeOut();
            }
        }
    }

    public void FadeIn()
    {
        isShow = true;
        transform.DOScale(new Vector3(1, 1, 1), showTweenTime * 2).SetEase(showEase);
    }

    public void FadeOut()
    {
        isShow = false;
        transform.DOScale(Vector3.zero, showTweenTime).SetEase(showEase);
    }
}