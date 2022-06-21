using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class Level : MonoBehaviour
{
    public int id;
    public List<CollectableItem> itemsInLevel;
    
    public Transform playerTransform;
    public List<Transform> normalGuardsTransforms;
    public List<Transform> stationGuardsTransforms;

    public Transform playerParent;
    public Transform enemyParent;
    
    //AstarPathSettings
    public Vector3 gridGraphCenter;
    public int gridGraphWidth;
    public int gridGraphDepth;
    public float gridGraphNodeSize;


}