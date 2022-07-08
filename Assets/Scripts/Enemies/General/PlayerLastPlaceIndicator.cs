using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerLastPlaceIndicator : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private Vector3 scaleUpAmount;
    [SerializeField] private bool isShow = false;


    private void Start()
    {
        indicator.DOFade(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnEnable()
    {
        GameEvent.instance.OnPlayerLose += Disable;
        GameEvent.instance.OnPlayerWin += Disable;
    }

    private void OnDisable()
    {
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Disable;
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= Disable;
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

    public void Disable()
    {
        transform.gameObject.SetActive(false);
    }
}