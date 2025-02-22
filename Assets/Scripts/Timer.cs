using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public int Minutes = 1;
    public int Seconds = 30;
    public TextMeshProUGUI timerUI;
    public string EndGameScene;

    private float timerMinutes;
    private float timerSeconds;
    private float timeToDisplay;

    void Start()
    {
        timeToDisplay = ((Minutes * 60) + Seconds);
    }

    // Update is called once per frame
    void Update()
    {
        timerMinutes = Mathf.FloorToInt(timeToDisplay / 60);
        timerSeconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (timeToDisplay <= 0)
        {
            //Move to end game screen
        }
        //string timerInt = timerSeconds.ToString("0");
        // Mathf.Floor(timer);
        timerUI.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);
        timeToDisplay -= Time.deltaTime;
    }

    public float timeRemaingInSec()
    {
        return timeToDisplay;
    }

}
