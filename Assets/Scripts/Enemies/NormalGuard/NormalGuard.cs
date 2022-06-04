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

    public Animator animator;

    // Idle
    public Coroutine lookAroundCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Suspect()
    {
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.suspectState);
    }

    public void Alert()
    {
        normalGuardStateMachine.ChangeState(normalGuardStateMachine.alertState);
    }

    public void Chase()
    {
        
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
}