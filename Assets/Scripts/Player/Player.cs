using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Player : AIPath
{
    [SerializeField] private Seeker seekerScript;
    [SerializeField] private CharacterController characterController;
    public Animator animator;

    // Input
    private Camera cam;
    
    // Player
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    public bool isMoving = false;
    public bool isRunning = false;

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
        CheckInput();
    }

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        ReachTarget();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
            }
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.CompareTag("Ground"))
            {
                seekerScript.StartPath(transform.position, hitInfo.point, (Path p) =>
                {
                    
                    isMoving = true;
                });

            }
        }
    }

    private void ReachTarget()
    {
        isMoving = false;
    }

    public void ChangeToRunMode(bool isRun)
    {
        if (isRun) maxSpeed = runSpeed;
        else maxSpeed = walkSpeed;
    }
}