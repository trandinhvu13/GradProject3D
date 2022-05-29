using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Player : VersionedMonoBehaviour
{
    [SerializeField] private Seeker seeker;
    [SerializeField] private AIPath aiPath;

    // Input
    private Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
       CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.CompareTag("Ground"))
            {
                seeker.StartPath(transform.position, hitInfo.point);
            }
        }
    }
}