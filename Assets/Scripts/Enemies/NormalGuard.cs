using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class NormalGuard : AIPath
{
    [SerializeField] private Seeker seekerScript;
    [SerializeField] private CharacterController characterController;
    public Animator animator;
    
    // Input
    private Camera cam;
    
    // Enemy
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float suspectSpeed;
    [SerializeField] private float alertSpeed;
    
    public bool isMoving = false;
    public bool isRunning = false;
    
    // Idle
    public Coroutine lookAroundCoroutine;
    public Ease rotationEase;
    public float rotationAmount = 0;
    public float rotationTime;
    public float rotationIntervalTime;
    
    public int rotationCount;
    public int currentRotationCount;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void HearSound()
    {
        
    }

    public void Suspect()
    {
        
    }

    public void Chase()
    {
        
    }
    
    
}
