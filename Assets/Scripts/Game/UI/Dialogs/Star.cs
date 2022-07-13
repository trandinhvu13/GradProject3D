using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject starOn;
    [SerializeField] private GameObject starOff;

    public float showTime;
    [SerializeField] private Ease showEase;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show(bool isOn)
    {
        starOn.SetActive(isOn);

        transform.DOScale(new Vector3(1, 1, 1), showTime).SetEase(showEase);
    }
}
