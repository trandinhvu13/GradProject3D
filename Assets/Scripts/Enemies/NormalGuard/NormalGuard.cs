using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class NormalGuard : AIPath
{
    public NormalGuardData data;
    public Seeker seekerScript;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private AIPath aiPath;
    [SerializeField] private NormalGuardStateMachine normalGuardStateMachine;
    [SerializeField] private FieldOfView fieldOfView;
    public float suspectMeter;

    public Animator animator;

    // Idle
    public Coroutine lookAroundCoroutine;
    public Coroutine checkForPlayerCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //GameEvent.instance.OnDetectPlayer += DetectPlayer;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //if (GameEvent.instance) GameEvent.instance.OnDetectPlayer -= DetectPlayer;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckForPlayer();
    }

    public void CheckForPlayer()
    {
        if (fieldOfView.isSeeingPlayer)
        {
            //normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
            suspectMeter += Time.deltaTime;

            if (suspectMeter > data.suspectMeterMax)
            {
                suspectMeter = data.suspectMeterMax;
            }
        }
        else
        {
            suspectMeter -= Time.deltaTime / 1.5f;

            if (suspectMeter < 0)
            {
                suspectMeter = 0;
            }
        }
    }

    public void DetectPlayer(Transform playerPosTransform)
    {
        data.playerLastSeenPos = playerPosTransform;
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.alertState);
    }

    public override void OnTargetReached()
    {
        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.patrolState)
        {
            normalGuardStateMachine.patrolState.OnTargetReached();
            return;
        }

        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.suspectState)
        {
            normalGuardStateMachine.suspectState.OnTargetReached();
            return;
        }

        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.alertState)
        {
            normalGuardStateMachine.alertState.OnTargetReached();
            return;
        }
    }

    public void OnStartDetectingPlayer()
    {
    }

    public void OnDetectPlayer()
    {
    }
}