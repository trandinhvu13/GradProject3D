using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerLastPlaceIndicator : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private float showTime;
    [SerializeField] private Ease showTweenType;
    [SerializeField] private Vector3 scaleUpAmount;
    [SerializeField] private Ease scaleUpTweenType;
    [SerializeField] private float hideTime;
    [SerializeField] private Ease hideTweenType;
    [SerializeField] private bool isShow = false;


    private void Start()
    {
        indicator.DOFade(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Show(Vector3 pos)
    {
        isShow = true;
        transform.parent = null;
        transform.position = pos;
        indicator.DOFade(1, 0);
        transform.DOScale(scaleUpAmount, 0);
    }

    public void Hide()
    {
        if (!isShow) return;
        isShow = false;
        indicator.DOFade(0, 0);
        transform.DOScale(Vector3.zero, 0);
        transform.parent = parent;
    }
}