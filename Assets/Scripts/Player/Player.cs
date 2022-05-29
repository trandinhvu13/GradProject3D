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
    [SerializeField] private Animator animator;

    // Input
    private Camera cam;
    
    // Animation
    private string animationState = "Idle";

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    protected override void Start()
    {
        base.Start();
        Helper.SetTriggerAnimator(animator, "Idle");
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        CheckAnimation();
    }

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        ReachTarget();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.CompareTag("Ground"))
            {
                seekerScript.StartPath(transform.position, hitInfo.point);
                Helper.SetTriggerAnimator(animator, "Run");
            }
        }
    }

    private void CheckAnimation()
    {

    }

    private void ReachTarget()
    {
        Helper.SetTriggerAnimator(animator, "Idle");
    }
}