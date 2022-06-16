using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float delayTime;
    [SerializeField] private bool isCollected = false;
    public float checkRadius;


    private void Start()
    {
        StartCoroutine(CheckForPlayer(delayTime));
    }

    IEnumerator CheckForPlayer(float delay)
    {
        while (!isCollected)
        {
            yield return new WaitForSeconds(delay);

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, checkRadius, targetMask);

            if (targetsInViewRadius.Length > 0)
            {
                Transform target = targetsInViewRadius[0].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    isCollected = true;
                    transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                    
                    //Game Event
                }
            }
        }
    }
}