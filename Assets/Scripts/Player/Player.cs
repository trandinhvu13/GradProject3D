using System;
using Cinemachine;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class Player : AIPath
{
    public PlayerData data;
    public Seeker seekerScript;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    public Animator animator;
    public SpriteRenderer soundRing;
    public ParticleSystem smokeTrail;
    public CinemachineVirtualCamera winGameCamera;
    public CinemachineVirtualCamera loseGameCamera;
    public GameObject winParticle;
    public GameObject loseParticle;

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

        SetUpSoundRing();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvent.instance.OnPlayerWin += Win;
        GameEvent.instance.OnPlayerLose += Lose;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= Win;
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Lose;
    }

    private void SetUpSoundRing()
    {
        soundRing.transform.localScale = new Vector3(data.whistleRadius * 2, data.whistleRadius * 2, 1);
        soundRing.color = new Color(255, 255, 255, 0);
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
    }

    public override void OnTargetReached()
    {
        GameEvent.instance.HideIndicator();
        if (playerStateMachine.GetCurrentState() == playerStateMachine.winState)
        {
            playerStateMachine.winState.OnTargetReached();
        }
        else
        {
            data.isMoving = false;
        }
    }

    private void CheckInput()
    {
        if (LevelManager.instance.state == LevelManager.LevelState.Win ||
            LevelManager.instance.state == LevelManager.LevelState.Lose) return;
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
                seekerScript.StartPath(transform.position, hitInfo.point, (Path p) =>
                {
                    data.isMoving = true;
                    GameEvent.instance.ShowIndicator(hitInfo.point);
                    GameEvent.instance.ClickOnGround();
                });
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Whistle();
        }

        ;
    }

    private void Whistle()
    {
        if (whistleTween.IsActive() && whistleTween != null && whistleTween.IsPlaying()) return;

        soundRing.transform.localScale = new Vector3(data.whistleRadius * 2, data.whistleRadius * 2, 1);
        soundRing.color = new Color(255, 255, 255, 0);

        whistleTween = soundRing
            .DOFade(1, data.whistleRingTweenTime)
            .SetEase(data.soundRingTweenType).SetLoops(2, LoopType.Yoyo).From(0).OnStepComplete(() =>
            {
                if (whistleTween.CompletedLoops() == 1)
                {
                    GameEvent.instance.PlayerWhistle(transform, data.whistleRadius);
                }
            });
    }

    public void EnableSmokeTrail(bool isEnabled)
    {
        var emission = smokeTrail.emission;
        emission.enabled = isEnabled;
    }

    public void Win()
    {
        if (LevelManager.instance.state == LevelManager.LevelState.Lose) return;
        playerStateMachine.ChangeState(playerStateMachine.winState);
    }

    public void Lose()
    {
        if (LevelManager.instance.state == LevelManager.LevelState.Win) return;
        playerStateMachine.ChangeState(playerStateMachine.loseState);
    }
}