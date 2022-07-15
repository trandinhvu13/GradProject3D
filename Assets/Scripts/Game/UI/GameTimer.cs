using System;
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
        text.text = "00:00";
        currentTime = 0;
        StartTime();
    }
    
    public void StartTime()
    {
        isTimeStart = true;
    }

    public void StopTime()
    {
        isTimeStart = false;
    }

    private void SetTextTime()
    {
        float time = currentTime;
        int min = (int)time / 60;
        int sec = (int)time % 60;

        string minString = min.ToString();
        string secString = sec.ToString();
        
        if (min < 10)
        {
            minString = $"0{min}";
        }

        if (sec < 10)
        {
            secString = $"0{sec}";
        }

        text.text = $"{minString}:{secString}";
    }
}
