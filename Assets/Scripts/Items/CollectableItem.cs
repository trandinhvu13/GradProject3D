using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private LayerMask targetMask;

    [SerializeField] private LayerMask obstacleMask;
    public Sprite hudImage;

    [Header("Properties")] public int id;
    [SerializeField] private float delayTime;
    public bool isCollected = false;
    public float checkRadius;

    [Header("Tween")] public float moveUpAmount;
    public float moveUpTime;
    public Ease moveUpTweenType;
    public Vector3 scaleUpAmount;
    public float scaleUpTime;
    public Ease scaleUpTweenType;
    public Vector3 scaleDownAmount;
    public float scaleDownTime;
    public Ease scaleDownTweenType;
    public float moveDownTime;
    public Ease moveDownTweenType;


    private void Awake()
    {
        isCollected = false;
    }

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
                    //Animation
                    transform.DOMoveY(moveUpAmount, moveUpTime).SetEase(moveUpTweenType);
                    transform.DOScale(scaleUpAmount, scaleUpTime).SetEase(scaleUpTweenType).OnComplete(() =>
                    {
                        transform.parent = LevelManager.instance.player.transform;
                    });
                    transform.DOScale(scaleDownAmount, scaleDownTime).SetEase(scaleDownTweenType)
                        .SetDelay(scaleUpTime);
                    transform.DOLocalMoveZ(0, moveDownTime).SetEase(moveDownTweenType).SetDelay(scaleUpTime);
                    transform.DOLocalMoveY(0, moveDownTime).SetEase(moveDownTweenType).SetDelay(scaleUpTime);
                    transform.DOLocalMoveX(0, moveDownTime).SetEase(moveDownTweenType).SetDelay(scaleUpTime).OnComplete(
                        () =>
                        {
                            //add particle
                            transform.localScale = Vector3.zero;
                            LevelManager.instance.CollectItem(id);
                            transform.parent = null;
                        });
                }
            }
        }
    }
}