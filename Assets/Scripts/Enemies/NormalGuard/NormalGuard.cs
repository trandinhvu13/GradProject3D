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

    [SerializeField] private SuspectMeter suspectMeter;
    public PlayerLastPlaceIndicator playerLastPlaceIndicator;

    public float suspectMeterAmount;

    public Animator animator;

    // Idle
    public Coroutine lookAroundCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvent.instance.OnPlayerWhistle += HearPlayer;
        GameEvent.instance.OnPlayerRun += HearPlayer;
        GameEvent.instance.OnPlayerWin += GameEnd;
        GameEvent.instance.OnPlayerLose += GameEnd;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (GameEvent.instance) GameEvent.instance.OnPlayerWhistle -= HearPlayer;
        if (GameEvent.instance) GameEvent.instance.OnPlayerRun -= HearPlayer;
        if (GameEvent.instance) GameEvent.instance.OnPlayerWin -= GameEnd;
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= GameEnd;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CheckDistancePlayer());
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

        suspectMeter.ChangeValueSuspectMeter(suspectMeterAmount / data.suspectMeterMax);
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
        Debug.Log("Still hear player");
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

    IEnumerator CheckDistancePlayer()
    {
        while (true)
        {
            if (LevelManager.instance.isLevelLoad && LevelManager.instance.state == LevelManager.LevelState.Normal &&
                Vector3.Distance(transform.position, LevelManager.instance.player.transform.position) < 1.75f)
            {
                Debug.Log("Lose");
                normalGuardStateMachine.ChangeState(normalGuardStateMachine.winState);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void GameEnd()
    {
        canMove = false;
        if (transform != LevelManager.instance.detectedEnemy) transform.gameObject.SetActive(false);
    }
}