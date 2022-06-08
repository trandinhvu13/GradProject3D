using DG.Tweening;
using Pathfinding;
using Shapes2D;
using UnityEngine;

public class Player : AIPath
{
    public PlayerData data;
    [SerializeField] private Seeker seekerScript;
    [SerializeField] private CharacterController characterController;
    public Animator animator;
    public Shape soundRing;

    // Input
    private Camera cam;

    // Tween
    private Tween whistleTween;

    // Sound Ring
    private Collider[] insideSoundRing;


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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Whistle();
        }

        ;
    }

    private void ReachTarget()
    {
        data.isMoving = false;
    }

    private void Whistle()
    {
        if (whistleTween.IsActive() && whistleTween != null && whistleTween.IsPlaying()) return;

        whistleTween = soundRing.transform
            .DOScale(new Vector3(data.whistleRange*2, data.whistleRange*2, 1), data.whistleRingTweenTime)
            .SetEase(data.soundRingTweenType).SetLoops(2, LoopType.Yoyo).From(Vector3.zero).OnStepComplete(() =>
            {
                if (whistleTween.CompletedLoops() == 1)
                {
                    // Or using event? with parameter is radius and transform pos
                    insideSoundRing = Physics.OverlapSphere(transform.position, data.whistleRange, data.enemyLayerMask);

                    if (insideSoundRing.Length > 0)
                    {
                        
                    }
                }
            });
    }
}