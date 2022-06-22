using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.SceneTemplate;
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
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public Player player;
    public Transform destinationTransform;
    public Transform levelTransformPos;

    public List<Transform> enemyTransforms = new List<Transform>();


    //Level
    public Level levelToLoad;
    public int itemsToCollectNum;
    public GameObject playerPrefab;
    public GameObject normalGuardPrefab;
    public GameObject stationGuardPrefab;

    private void Update()
    {
        /*if (Vector3.Distance(playerTransform.position, destinationTransform.position)<0.5f)
        {
            Debug.Log("Win");
        }*/
    }

    private void Start()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int levelId)
    {
        GroupLoader.Instance.LoadResources(new List<Resource>
            {
                new Resource
                {
                    Name = $"Levels/Level{levelId}",
                    CacheResult = true,
                    FinishCallback = (object go) => { levelToLoad = (go as GameObject).GetComponent<Level>(); },
                    InstantiateResult = true,
                    Transform = levelTransformPos
                }
            },
            () =>
            {
                Debug.Log("All prefabs loaded!");
                SetupAstar();
                SetupPlayer();
                SetupEnemy();
                SetupVirtualCam();
                SetupItemsToCollect();
            });
    }

    private void SetupPlayer()
    {
        player = Instantiate(playerPrefab, levelToLoad.player.position,
            levelToLoad.player.rotation,
            levelToLoad.playerParent).GetComponent<Player>();
    }

    private void SetupEnemy()
    {
    }

    private void SetupItemsToCollect()
    {
        for (int i = 0; i < levelToLoad.itemsToCollect.Count; i++)
        {
            levelToLoad.itemsToCollect[i].id = i;
            levelToLoad.itemsToCollect[i].isCollected = false;
            // Set up hud
        }
    }

    private void SetupVirtualCam()
    {
        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
    }

    private void SetupAstar()
    {
        astarPath.data.gridGraph.center = levelToLoad.gridGraphCenter;
        astarPath.data.gridGraph.SetDimensions(levelToLoad.gridGraphWidth, levelToLoad.gridGraphDepth,
            levelToLoad.gridGraphNodeSize);
        astarPath.Scan();
    }

    public void CollectItem(int id)
    {
        levelToLoad.itemsToCollect[id].isCollected = true;
    }
}