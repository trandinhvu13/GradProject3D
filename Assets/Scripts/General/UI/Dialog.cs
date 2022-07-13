using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public string id;
    public float openTime;
    public Ease openEase;
    public float closeTime;
    public Ease closeEase;
    public bool isOpen = false;

    public virtual void Init()
    {
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(true);
    }
    
    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        Init();
        transform.DOScale(new Vector3(1, 1, 1), openTime).SetEase(openEase).OnComplete(Intro);
    }

    public virtual void Intro()
    {
    }

    public void Close()
    {
        isOpen = false;
        transform.DOScale(Vector3.zero, closeTime).SetEase(closeEase).OnStart(Outro).OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
        });
    }
    
    public virtual void Outro()
    {
    }
}
