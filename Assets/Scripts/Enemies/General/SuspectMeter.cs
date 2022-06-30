using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class SuspectMeter : MonoBehaviour
{
    [SerializeField] private Shape suspectMeterShape;
    [SerializeField] private Transform parent;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        parent.transform.LookAt(cam.transform);
        parent.transform.rotation=cam.transform.rotation;
    }

    public void ChangeValueSuspectMeter(float amount)
    {
        if (amount == 0)
        {
            suspectMeterShape.settings.startAngle = 0.01f;
        }
        else
        {
            suspectMeterShape.settings.startAngle = 360 * amount;
        }

        
    }

    public void FadeIn()
    {
    }

    public void FadeOut()
    {
    }
}