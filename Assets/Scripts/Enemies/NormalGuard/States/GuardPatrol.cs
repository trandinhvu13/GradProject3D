using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GuardPatrol : BaseState
{
    private NormalGuard normalGuard;
    private NormalGuardStateMachine normalGuardStateMachine;
    
    public GuardPatrol(NormalGuardStateMachine stateMachine) : base("GuardPatrol", stateMachine)
    {
        normalGuardStateMachine = stateMachine;
        normalGuard = normalGuardStateMachine.normalGuard;
    }
    
    public override void Enter()
    {
        base.Enter();
        Helper.SetTriggerAnimator(normalGuard.animator, "Walk");
        normalGuard.canMove = true;
        normalGuard.data.isMoving = true;
        normalGuard.data.isRunning = false;
        normalGuard.maxSpeed = normalGuard.data.patrolSpeed;
        normalGuard.seekerScript.StartPath(normalGuard.transform.position, PickARandomPlace());
        Debug.Log("Guard in Patrol");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public Vector3 PickARandomPlace()
    {
        var constraint = NNConstraint.None;
        constraint.constrainWalkability = true;
        constraint.walkable = true;
        
        var startNode = AstarPath.active.GetNearest(normalGuard.transform.position, constraint).node;
        var nodes = PathUtilities.BFS(startNode, normalGuard.data.patrolRange);
        return PathUtilities.GetPointsOnNodes(nodes, 1)[0];
    }

    public void OnTargetReached()
    {
        int rand = Random.Range(0, 100);
        if (rand <= normalGuard.data.continueToPatrolChance)
        {
            normalGuard.seekerScript.StartPath(normalGuard.transform.position, PickARandomPlace());
        }
        else
        {
            normalGuardStateMachine.ChangeState(normalGuardStateMachine.idleState);
        }
    }
}