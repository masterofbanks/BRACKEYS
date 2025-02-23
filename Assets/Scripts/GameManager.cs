using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public GameObject CameraManager;
    public bool inMinigame;

    public GameObject[] nonActiveGames;

    public int Minutes;
    public int Seconds;
    public TextMeshProUGUI timerUI;
    public string EndGameScene;

    private float timerMinutes;
    private float timerSeconds;
    private float timeToDisplay;

    public int secBetweenMinigames = 10;
    private int nextMinigameCount;

    void Start()
    {
        timeToDisplay = ((Minutes * 60) + Seconds);
        nextMinigameCount = secBetweenMinigames;
        InvokeRepeating(nameof(TickMinigameCount), 0, 1f);
        nonActiveGames = GameObject.FindGameObjectsWithTag("Minigame");
    }

    // Update is called once per frame
    void Update()
    {
        timerMinutes = Mathf.FloorToInt(timeToDisplay / 60);
        timerSeconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        timerUI.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);
        timeToDisplay -= Time.deltaTime;

        //every 10 seconds 
        //find all minigame locators in the scene that have the non active tag
        //if there are none -> do nothing for now but this might be a losing condition
        //randomly pick one of those minigame locators and make it active plus change its tag
        //picking that minigame plays some sort of signal effect telling the player that that minigame is active
    }

    void TickMinigameCount()
    {
        nextMinigameCount++;

        if (nextMinigameCount >= secBetweenMinigames) // When threshold is reached
        {
            ActivateRandomMinigame();
            nextMinigameCount = 0; // Reset count
        }
    }

    void ActivateRandomMinigame()
    {
        nonActiveGames = GameObject.FindGameObjectsWithTag("Minigame");

        if (nonActiveGames.Length == 0)
        {
            Debug.Log("All Minigames activated; Lose??");
            SceneManager.LoadScene(1);
        }
        else
        {
            int randomIndex = Random.Range(0, nonActiveGames.Length);
            nonActiveGames[randomIndex].GetComponent<MinigameLocatorBehavior>().TurnOn();
        }
    }


    //create some sort of method that is the process for the succesfull completeion of a minigame

}
