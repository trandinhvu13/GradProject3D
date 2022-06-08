using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public bool isMoving;
    public bool isRunning;

    public LayerMask enemyLayerMask;
    
    public float whistleRange;
    public float whistleRingTweenTime;
    public float runSoundRange;
    public float runRingTweenTime;
    public Ease soundRingTweenType;
}