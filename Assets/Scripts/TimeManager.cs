using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public Text timeText;
    public float Timer { get; private set; }

    private bool timerStarted;

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }

    public void ResetTimer()
    {
        Timer = 0;
        timeText.text = "00:00";
    }

    void Update()
    {
        if (timerStarted)
        {
            Timer += Time.deltaTime;

            string minutes = Mathf.Floor(Timer / 60).ToString("00");
            string seconds = Mathf.Floor(Timer % 60).ToString("00");

            timeText.text = $"{minutes}:{seconds}";
        }
    }
}