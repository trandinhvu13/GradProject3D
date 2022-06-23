using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class Level : MonoBehaviour
{
    public int id;
    public List<CollectableItem> itemsInLevel;
    
    public Transform player;
    public Transform destination;
    public List<NormalGuard> normalGuards;
    public List<StationGuard> stationGuards;
    public List<CollectableItem> itemsToCollect;

    public Transform playerParent;
    public Transform enemyParent;
    public Transform itemsParent; 
    
    //AstarPathSettings
    public Vector3 gridGraphCenter;
    public int gridGraphWidth;
    public int gridGraphDepth;
    public float gridGraphNodeSize;


}