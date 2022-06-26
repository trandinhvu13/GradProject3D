using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] private ItemsListHUD itemsListHUD;

    public Player player;
    public Transform destinationTransform;
    public Transform levelTransformPos;
    public float destinationRadius;

    public List<Transform> enemyTransforms = new List<Transform>();


    //Level
    public Level levelToLoad;
    public int itemsToCollectNum;
    public GameObject playerPrefab;
    public GameObject normalGuardPrefab;
    public GameObject stationGuardPrefab;

    //Game State
    public bool isWin = false;
    public int numOFItemsToCollect;
    public int currentItemsAmount;


    private void Update()
    {
        if (player && Vector3.Distance(player.transform.position, destinationTransform.position) < destinationRadius)
        {
            Debug.Log("Win");
        }
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
                SetupGame();
            });
    }

    private void SetupAstar()
    {
        astarPath.data.gridGraph.center = levelToLoad.gridGraphCenter;
        astarPath.data.gridGraph.SetDimensions(levelToLoad.gridGraphWidth, levelToLoad.gridGraphDepth,
            levelToLoad.gridGraphNodeSize);
        astarPath.Scan();
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

    private void SetupVirtualCam()
    {
        CinemachineTargetGroup.Target playerTarget = new CinemachineTargetGroup.Target();
        playerTarget.target = player.transform;
        playerTarget.weight = 2;
        playerTarget.radius = 2;
        cinemachineTargetGroup.m_Targets[1] = playerTarget;
    }

    private void SetupItemsToCollect()
    {
        for (int i = 0; i < levelToLoad.itemsToCollect.Count; i++)
        {
            levelToLoad.itemsToCollect[i].id = i;
            levelToLoad.itemsToCollect[i].isCollected = false;
        }

        itemsListHUD.LoadItemInLevel(levelToLoad.itemsToCollect);
    }

    private void SetupGame()
    {
        isWin = false;
        destinationTransform = levelToLoad.destination;
        numOFItemsToCollect = levelToLoad.itemsToCollect.Count;
        currentItemsAmount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CollectItem(int id)
    {
        currentItemsAmount++;
        levelToLoad.itemsToCollect[id].isCollected = true;
        itemsListHUD.CollectItem(id);
    }
}