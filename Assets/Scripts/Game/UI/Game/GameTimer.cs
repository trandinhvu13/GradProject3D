using System;
using Game;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public float currentTime = 0;
    [SerializeField] private bool isTimeStart = false;

    private void OnEnable()
    {
        GameEvent.instance.OnPlayerWin += StopTime;
        GameEvent.instance.OnPlayerLose += StopTime;
    }

    private void OnDisable()
    {
       if(GameEvent.instance) GameEvent.instance.OnPlayerWin -= StopTime;
       if(GameEvent.instance) GameEvent.instance.OnPlayerLose -= StopTime;
    }

    private void Start()
    {
        SetupNew();
    }

    private void Update()
    {
        if (!isTimeStart) return;
        currentTime += Time.deltaTime;
        SetTextTime();
    }

    public void SetupNew()
    {
        text.text = "00:00.000";
        currentTime = 0;
    }
    
    public void StartTime()
    {
        isTimeStart = true;
    }

    private void StopTime()
    {
        isTimeStart = false;
    }

    private void SetTextTime()
    {
        float time = currentTime;

        text.text = Helper.ChangeTimeToTextString(time);
    }
}
