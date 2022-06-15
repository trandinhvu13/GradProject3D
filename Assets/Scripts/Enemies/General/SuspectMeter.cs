using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class SuspectMeter : MonoBehaviour
{
    [SerializeField] private Image suspectMeterImage;
    [SerializeField] private Shape suspectMeterShape;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(cam.transform);
        transform.rotation = cam.transform.rotation;
    }

    public void ChangeValueSuspectMeter(float amount)
    {
        suspectMeterImage.fillAmount = amount;
    }

    public void FadeIn()
    {
        
    }

    public void FadeOut()
    {
        
    }
}
