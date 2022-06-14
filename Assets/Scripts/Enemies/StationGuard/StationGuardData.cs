using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StationGuardData : MonoBehaviour
{
    public Vector3 stationPos;
    public Vector3 stationRotation;
    
    public float suspectSpeed;
    public float alertSpeed;

    public bool isMoving;
    public bool isRunning;
    public bool isInStation;

    public float suspectMeterMax;

    //Idle
    public Ease rotationEase;
    public float rotationAmount = 0;
    public float rotationTime;
    public float rotationIntervalTime;
    
    public int currentRotationCount;
}