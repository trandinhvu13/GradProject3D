using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, layerMask) &&
            hitInfo.transform.gameObject.CompareTag("CursorDetect"))
        {
            if (!LevelManager.instance.player) return;
            transform.position = hitInfo.point;
        }
    }
}