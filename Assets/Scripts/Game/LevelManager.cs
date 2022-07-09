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
        if (GameEvent.instance) GameEvent.instance.OnPlayerLose -= Lose;
        if (GameEvent.instance) GameEvent.instance.OnEnemyAlert -= PlayerGetChased;
    }

    protected override void InternalOnEnable()
    {
        GameEvent.instance.OnPlayerLose += Lose;
        GameEvent.instance.OnEnemyAlert += PlayerGetChased;
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
    public Transform detectedEnemy;
    
    //Level
    public bool isLevelLoad;
    public Level levelToLoad;
    public int itemsToCollectNum;
    public GameObject playerPrefab;
    //Game State
    public LevelState state;
    public enum LevelState
    {
        Normal,
        Pause,
        Win,
        Lose,
    }
    public int numOfItemsToCollect;
    public int currentItemsAmount;

    protected override void Awake()
    {
        base.Awake();
        state = LevelState.Normal;
    }

    private void Update()
    {
        CheckWin();
    }

    private void Start()
    {
        isLevelLoad = false;
        LoadLevel(1);
    }

    public void CheckWin()
    {
        if (state==LevelState.Win) return;
        if (player && Vector3.Distance(player.transform.position, destinationTransform.position) < destinationRadius)
        {
            Win();
        }
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
                isLevelLoad = true;
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
        playerTarget.weight = 1.5f;
        playerTarget.radius = 2;
        cinemachineTargetGroup.m_Targets[1] = playerTarget;
    }

    private void SetupItemsToCollect()
    {
        numOfItemsToCollect = levelToLoad.itemsToCollect.Count;
        for (int i = 0; i < numOfItemsToCollect; i++)
        {
            levelToLoad.itemsToCollect[i].id = i;
            levelToLoad.itemsToCollect[i].isCollected = false;
        }

        itemsListHUD.LoadItemInLevel(levelToLoad.itemsToCollect);
    }

    private void SetupGame()
    {
        state = LevelState.Normal;
        destinationTransform = levelToLoad.destination;
        numOfItemsToCollect = levelToLoad.itemsToCollect.Count;
        currentItemsAmount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CollectItem(int id)
    {
        currentItemsAmount++;
        levelToLoad.itemsToCollect[id].isCollected = true;
        itemsListHUD.CollectItem(id);

        if (currentItemsAmount >= numOfItemsToCollect)
        {
            Debug.Log("Find all the items");
        }
    }

    private void PlayerGetChased(Transform enemyTransform)
    {
        Debug.Log("add player");
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3;
        
        CinemachineTargetGroup.Target enemyTarget = new CinemachineTargetGroup.Target();
        enemyTarget.target = enemyTransform;
        enemyTarget.weight = 1.5f;
        enemyTarget.radius = 2;

        for (int i = 0; i < cinemachineTargetGroup.m_Targets.Length; i++)
        {
            if (cinemachineTargetGroup.m_Targets[i].radius==0)
            {
                cinemachineTargetGroup.m_Targets[i] = enemyTarget;
                break;
            }
        }
    }
    public void Win()
    {
        state = LevelState.Win;
        GameEvent.instance.PlayerWin();
    }

    public void Lose()
    {
        state = LevelState.Lose;
    }
}