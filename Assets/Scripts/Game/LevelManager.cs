using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    #region Mono

    protected override void InternalInit()
    {
    }

    protected override void InternalOnDestroy()
    {
    }

    protected override void InternalOnDisable()
    {
    }

    protected override void InternalOnEnable()
    {
    }

    #endregion

    [SerializeField] private AstarPath astarPath;
    public Transform playerTransform;
    public Transform destinationTransform;
    public Transform levelTransformPos;

    public List<Transform> enemyTransforms = new List<Transform>();


    //Level
    public Level levelToLoad;
    public int itemsToCollectNum;
    public GameObject player;
    public GameObject normalGuard;
    public GameObject stationGuard;

    private void Update()
    {
        /*if (Vector3.Distance(playerTransform.position, destinationTransform.position)<0.5f)
        {
            Debug.Log("Win");
        }*/
    }

    private void Start()
    {
        LoadLevel(levelToLoad.id);
    }

    public void LoadLevel(int levelId)
    {
        GroupLoader.Instance.LoadResources(new List<Resource>
            {
                new Resource
                {
                    Name = $"Levels/{levelId}",
                    CacheResult = true,
                    InstantiateResult = true,
                    Transform = levelTransformPos
                }
            },
            () =>
            {
                Debug.Log("All prefabs loaded!");
                astarPath.Scan();
                
                SetupPlayer();
                SetupEnemy();
                SetupItemsToCollect();
            });
    }

    private void SetupPlayer()
    {
        
    }
    
    private void SetupEnemy()
    {
        
    }
    
    private void SetupItemsToCollect()
    {
        
    }
}