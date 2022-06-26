using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class NormalGuard : AIPath
{
    public NormalGuardData data;
    public Seeker seekerScript;
    [SerializeField] private NormalGuardStateMachine normalGuardStateMachine;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Canvas canvas;

    [SerializeField] private SuspectMeter suspectMeter;
    public PlayerLastPlaceIndicator playerLastPlaceIndicator;
    
    public float suspectMeterAmount;

    public Animator animator;

    // Idle
    public Coroutine lookAroundCoroutine;

    protected override void Awake()
    {
        base.Awake();
        canvas.worldCamera = Camera.main;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvent.instance.OnPlayerWhistle += HearPlayer;
        GameEvent.instance.OnPlayerRun += HearPlayer;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (GameEvent.instance) GameEvent.instance.OnPlayerWhistle -= HearPlayer;
        if (GameEvent.instance) GameEvent.instance.OnPlayerRun -= HearPlayer;
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
            suspectMeterAmount += Time.deltaTime;

            if (suspectMeterAmount > data.suspectMeterMax)
            {
                suspectMeterAmount = data.suspectMeterMax;
            }
        }
        else
        {
            suspectMeterAmount -= Time.deltaTime / 1.5f;

            if (suspectMeterAmount < 0)
            {
                suspectMeterAmount = 0;
            }
        }
        
        suspectMeter.ChangeValueSuspectMeter(suspectMeterAmount/data.suspectMeterMax);
    }

    public void HearPlayer(Transform playerPosTransform, float radius)
    {
        if (Vector3.Distance(transform.position, playerPosTransform.position) <= radius)
        {
            suspectMeterAmount += data.suspectMeterMax / 4;
            OnHearPlayer();
        }
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

    public void OnHearPlayer()
    {
        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.patrolState)
        {
            normalGuardStateMachine.patrolState.OnHearPlayer();
            return;
        }

        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.idleState)
        {
            normalGuardStateMachine.idleState.OnHearPlayer();
            return;
        }
        
        if (normalGuardStateMachine.GetCurrentState() == normalGuardStateMachine.suspectState)
        {
            normalGuardStateMachine.suspectState.OnHearPlayer();
            return;
        }
    }
}