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
        transform.localScale = new Vector3(0,  0, 0);
    }

    public void Show(Vector3 pos)
    {
        Debug.Log("Show");
        if (isShow)
        {
            indicator.DOFade(0, 0);
            transform.localScale = new Vector3(0,  0, 0);
        }
        transform.parent = null;
        transform.position = pos;
        isShow = true;
        indicator.DOFade(1, showTime).SetEase(showTweenType);
        transform.DOScale(scaleUpAmount, showTime).SetEase(scaleUpTweenType);
    }

    public void Hide()
    {
        Debug.Log("Hide");
        isShow = false;
        indicator.DOFade(0, hideTime).SetEase(hideTweenType);
        transform.DOScale(Vector3.zero, hideTime).SetEase(hideTweenType).OnComplete(() =>
        {
            transform.parent = parent;
        });
    }
}