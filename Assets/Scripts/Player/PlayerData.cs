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
    
    public float whistleRadius;
    public float whistleRingTweenTime;
    public float runSoundRadius;
    public float runRingTweenTime;
    
    public Ease soundRingTweenType;
}