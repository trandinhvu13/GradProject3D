using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Player : AIPath
{
    public PlayerData data;
    [SerializeField] private Seeker seekerScript;
    [SerializeField] private CharacterController characterController;
    public Animator animator;

    // Input
    private Camera cam;


    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
    }

    public override void OnTargetReached()
    {
        ReachTarget();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                data.isRunning = false;
            }
            else
            {
                data.isRunning = true;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.CompareTag("Ground"))
            {
                seekerScript.StartPath(transform.position, hitInfo.point, (Path p) => { data.isMoving = true; });
            }
        }
    }

    private void ReachTarget()
    {
        data.isMoving = false;
    }
}