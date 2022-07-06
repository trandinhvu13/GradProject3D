using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class StationGuard : AIPath
{
    public StationGuardData data;
    public Seeker seekerScript;
    [SerializeField] private StationGuardStateMachine stationGuardStateMachine;
    public FieldOfView fieldOfView;

    [SerializeField] public SuspectMeter suspectMeter;
    public float suspectMeterAmount;
    public PlayerLastPlaceIndicator playerLastPlaceIndicator;

    public Animator animator;

    // Idle
    public Coroutine lookAroundCoroutine;

    protected override void Awake()
    {
        base.Awake();
        data.isInStation = true;
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
        data.stationPos = transform.position;
        data.stationRotation = transform.eulerAngles;
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
            suspectMeterAmount -= Time.deltaTime / 5f;

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
            suspectMeterAmount += data.suspectMeterMax / 3;
            OnHearPlayer();
        }
    }

    public override void OnTargetReached()
    {
        if (stationGuardStateMachine.GetCurrentState() == stationGuardStateMachine.idleState)
        {
            stationGuardStateMachine.idleState.OnTargetReached();
            return;
        }

        if (stationGuardStateMachine.GetCurrentState() == stationGuardStateMachine.suspectState)
        {
            stationGuardStateMachine.suspectState.OnTargetReached();
            return;
        }

        if (stationGuardStateMachine.GetCurrentState() == stationGuardStateMachine.alertState)
        {
            stationGuardStateMachine.alertState.OnTargetReached();
            return;
        }
    }

    public void OnHearPlayer()
    {
        if (stationGuardStateMachine.GetCurrentState() == stationGuardStateMachine.idleState)
        {
            stationGuardStateMachine.idleState.OnHearPlayer();
            return;
        }

        if (stationGuardStateMachine.GetCurrentState() == stationGuardStateMachine.suspectState)
        {
            stationGuardStateMachine.suspectState.OnHearPlayer();
            return;
        }
    }

    
}