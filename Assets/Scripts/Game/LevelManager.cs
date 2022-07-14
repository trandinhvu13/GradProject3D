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
        if (GameEvent.instance) GameEvent.instance.OnEnemyAlert -= PlayerGetChased;
    }

    protected override void InternalOnEnable()
    {
        GameEvent.instance.OnEnemyAlert += PlayerGetChased;
    }

    #endregion

    [SerializeField] private AstarPath astarPath;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;


    public Player player;
    public Transform destinationTransform;
    public Transform levelTransformPos;
    public float destinationRadius;
    public Transform detectedEnemy;

    //Level
    public bool isLevelLoad;
    public Level levelToLoad;
    public GameObject playerPrefab;
    public float finishTime;
    public List<float> milestoneTimes;
    public int finishMilestone;

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
        if (state == LevelState.Win) return;
        if (!isLevelLoad) return;
        if (player && Vector3.Distance(player.transform.position, destinationTransform.position) < destinationRadius)
        {
            if (currentItemsAmount >= numOfItemsToCollect)
            {
                Win();
            }
            else
            {
                if (!GameUIManager.instance.GetDialog("NotEnoughItemDialog").isOpen)
                    GameUIManager.instance.GetDialog("NotEnoughItemDialog").Open();
            }
        }
        else
        {
            if (GameUIManager.instance.GetDialog("NotEnoughItemDialog").isOpen)
                GameUIManager.instance.GetDialog("NotEnoughItemDialog").Close();
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
                SetupAstar();
                SetupPlayer();
                SetupVirtualCam();
                SetupItemsToCollect();
                SetupGame();
                SetupUI();
                isLevelLoad = true;
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
    }

    private void SetupGame()
    {
        state = LevelState.Normal;
        milestoneTimes = levelToLoad.milestoneTimes;
        destinationTransform = levelToLoad.destination;
        numOfItemsToCollect = levelToLoad.itemsToCollect.Count;
        currentItemsAmount = 0;
        //TEST Dialog
    }

    private void SetupUI()
    {
        GameUIManager.instance.SetupNewGame(levelToLoad.itemsToCollect);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CollectItem(int id)
    {
        currentItemsAmount++;
        levelToLoad.itemsToCollect[id].isCollected = true;
        GameUIManager.instance.itemsListHUD.CollectItem();

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
            if (cinemachineTargetGroup.m_Targets[i].radius == 0)
            {
                cinemachineTargetGroup.m_Targets[i] = enemyTarget;
                break;
            }
        }
    }

    private void Win()
    {
        if (state == LevelState.Win) return;
        state = LevelState.Win;
        DisableEndgameGameObjects();
        finishTime = GameUIManager.instance.gameTimer.currentTime;
        finishMilestone = CalculateMilestone();
        GameEvent.instance.PlayerWin();
    }

    public void Lose()
    {
        if (state == LevelState.Lose) return;
        state = LevelState.Lose;
        DisableEndgameGameObjects();
        GameEvent.instance.PlayerLose();
    }

    private void DisableEndgameGameObjects()
    {
        foreach (GameObject disableEndgameGameObject in levelToLoad.disableEndgameGameObjects)
        {
            disableEndgameGameObject.SetActive(false);
        }
    }

    private int CalculateMilestone()
    {
        for (int i = 2; i >= 0; i--)
        {
            if (finishTime <= milestoneTimes[i])
            {
                return i;
            }
        }

        return 0;
    }

    public void ResetGame()
    {
        levelToLoad = null;
        isLevelLoad = false;

        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.5f;

        CinemachineTargetGroup.Target blank = new CinemachineTargetGroup.Target();
        blank.target = null;
        blank.weight = 1.5f;
        blank.radius = 2;

        for (int i = 0; i < cinemachineTargetGroup.m_Targets.Length; i++)
        {
            if (i > 1)
            {
                cinemachineTargetGroup.m_Targets[i] = blank;
            }
        }
        
    }

    public void Retry()
    {
        Debug.Log("Click retry");
        GroupLoader.Instance.Cleanup();
        Destroy(levelToLoad.gameObject);
        ResetGame();
        LoadLevel(1);
    }
}