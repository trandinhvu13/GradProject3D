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

    private void OnEnable()
    {
        GameEvent.instance.OnPlayerLose += Hide;
        GameEvent.instance.OnPlayerWin += Hide;
    }

    private void OnDisable()
    {
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Hide;
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= Hide;
    }

    private void LateUpdate()
    {
        if (LevelManager.instance.state is LevelManager.LevelState.Win or LevelManager.LevelState.Lose) return;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, layerMask) &&
            hitInfo.transform.gameObject.CompareTag("CursorDetect"))
        {
            if (!LevelManager.instance.player) return;
            transform.position = hitInfo.point;
        }
    }

    private void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}