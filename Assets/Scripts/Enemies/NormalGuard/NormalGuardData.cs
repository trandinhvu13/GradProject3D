using DG.Tweening;
using UnityEngine;

namespace Enemies.NormalGuard
{
    public class NormalGuardData : MonoBehaviour
    {
        public float patrolSpeed;
        public float suspectSpeed;
        public float alertSpeed;

        public bool isMoving;
        public bool isRunning;

        public float suspectMeterMax;

        //Idle
        public Ease rotationEase;
        public float rotationAmount = 0;
        public float rotationTime;
        public float rotationIntervalTime;

        public int rotationCount;
        public int currentRotationCount;
    
        //Patrol
        public int continueToPatrolChance;
        public int patrolRange;
    
        //Chase


    }
}
