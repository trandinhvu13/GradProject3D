using System.Collections.Generic;
using Enemies.NormalGuard;
using Enemies.StationGuard;
using Pathfinding;
using UnityEngine;


public class Level : MonoBehaviour
{
    public int id;

    public Transform player;
    public Transform destination;
    [SerializeField] private List<NormalGuard> normalGuards;
    [SerializeField] private List<StationGuard> stationGuards;
    public List<CollectableItem> itemsToCollect;
    public List<float> milestoneTimes;

    public Transform playerParent;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform itemsParent; 
    
    //AstarPathSettings 
    public Vector3 gridGraphCenter;
    public int gridGraphWidth;
    public int gridGraphDepth;
    public float gridGraphNodeSize;

    public List<GameObject> disableEndgameGameObjects;

}