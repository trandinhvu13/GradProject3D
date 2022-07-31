using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Firebase.Database;
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
    public int currentLevelID;
    public bool isLevelLoad;
    public Level levelToLoad;
    public GameObject playerPrefab;
    public float oldFinishTime;
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
        isLevelLoad = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == LevelState.Normal) PauseGame();
            else if (state == LevelState.Pause)
            {
                DialogSystem.instance.CloseTopDialog();
                ResumeGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            DialogSystem.instance.GetDialog("HighscoreDialog").Open(true);
        }

        CheckWin();
    }

    private void Start()
    {
        LoadLevel(PlayerDataManager.instance.levelIDToLoad);
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
                if (!DialogSystem.instance.GetDialog("NotEnoughItemDialog").isOpen)
                    DialogSystem.instance.GetDialog("NotEnoughItemDialog").Open();
            }
        }
        else
        {
            if (DialogSystem.instance.GetDialog("NotEnoughItemDialog").isOpen)
                DialogSystem.instance.GetDialog("NotEnoughItemDialog").Close();
        }
    }

    private void LoadLevel(int levelId)
    {
        currentLevelID = levelId;
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

    public void ReloadLevel(int levelID)
    {
        LoadingResource loadedLevel = GroupLoader.Instance.GetResource($"Levels/Level{levelID}") as LoadingResource;
        GameObject gameObjectToLoad = loadedLevel.Result as GameObject;
        levelToLoad = Instantiate(gameObjectToLoad, levelTransformPos).GetComponent<Level>();
        SetupAstar();
        SetupPlayer();
        SetupVirtualCam();
        SetupItemsToCollect();
        SetupGame();
        SetupUI();
        GameUIManager.instance.gameTimer.StartTime();
        isLevelLoad = true;
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

        StartCoroutine(FirebaseManager.instance.GetUserLevelDataByLevel(levelToLoad.id, (snapshot) =>
        {
            oldFinishTime = 0;
            if (snapshot != null)
            {
                oldFinishTime = float.Parse(snapshot.Child("score").Value.ToString());
            }
            
        }));
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
        AudioManager.instance.PlayEffect("CollectItem");
        levelToLoad.itemsToCollect[id].isCollected = true;
        if (currentItemsAmount >= numOfItemsToCollect)
        {
            GameUIManager.instance.itemsListHUD.DoneCollectItem();
        }
        else
        {
            GameUIManager.instance.itemsListHUD.CollectItem();
        }
    }

    private void PlayerGetChased(Transform enemyTransform)
    {
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
        AudioManager.instance.FadeOutMusic();
        AudioManager.instance.PlayEffect("Win");
        finishTime = GameUIManager.instance.gameTimer.currentTime;
        finishMilestone = Helper.CalculateMilestone(finishTime, milestoneTimes);
        if (oldFinishTime == 0 || oldFinishTime > finishTime)
        {
            Score levelScore = new Score(FirebaseManager.instance.user.UserId, levelToLoad.id, finishTime,
                finishMilestone);
            StartCoroutine(FirebaseManager.instance.SaveUserScoreLevel(levelScore));
        }

        GameEvent.instance.PlayerWin();
    }

    public void Lose()
    {
        if (state == LevelState.Lose) return;
        state = LevelState.Lose;
        AudioManager.instance.FadeOutMusic();
        AudioManager.instance.PlayEffect("Lose");
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

    public void ResetGame()
    {
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
        AudioManager.instance.FadeInMusic();
        GameEvent.instance.HideIndicator();
    }

    public void PauseGame()
    {
        if (state == LevelState.Pause) return;
        state = LevelState.Pause;
        AudioManager.instance.FadeOutMusic();
        Time.timeScale = 0;
        Cursor.visible = true;
        cinemachineTargetGroup.gameObject.SetActive(false);
        DialogSystem.instance.GetDialog("PauseDialog").Open(true);
    }

    public void ResumeGame()
    {
        state = LevelState.Normal;
        AudioManager.instance.FadeInMusic();
        Cursor.visible = false;
        Time.timeScale = 1;
        cinemachineTargetGroup.gameObject.SetActive(true);
    }

    public void Retry()
    {
        int levelID = levelToLoad.id;
        //GroupLoader.Instance.Cleanup();
        Destroy(levelToLoad.gameObject);
        ResetGame();
        if (state == LevelState.Pause) ResumeGame();
        //LoadLevel(currentLevelID);
        ReloadLevel(levelID);
    }
}

public class Score
{
    public string userKey;
    public int levelID;
    public float score;
    public int star;

    public Score(string userKey = null, int levelID = 0, float score = 0, int star = 0)
    {
        this.userKey = userKey;
        this.levelID = levelID;
        this.score = score;
        this.star = star;
    }
}