using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    public int id;
    public List<CollectableItem> itemsInLevel;
    public Transform playerTransform;
    public List<Transform> normalGuardsTransforms;
    public List<Transform> stationGuardsTransforms;
}