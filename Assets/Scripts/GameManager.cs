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

    public float initMiniGameDelayInSec;

    public int Minutes;
    public int Seconds;
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
            nonActiveGames = GameObject.FindGameObjectsWithTag("Minigame");
            if(nonActiveGames.Length == 0)
            {
                Debug.Log("All Minigames activated; Lose??");
                SceneManager.LoadScene(1);
            }

            else
            {
                System.Random rand = new System.Random();
                int randomIndex = rand.Next(0, nonActiveGames.Length);
                nonActiveGames[randomIndex].GetComponent<MinigameLocatorBehavior>().TurnOn();
            }
            timeToDisplay = Seconds;
        }
        
        timerUI.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);
        timeToDisplay -= Time.deltaTime;

        //every 10 seconds 
        //find all minigame locators in the scene that have the non active tag
        //if there are none -> do nothing for now but this might be a losing condition
        //randomly pick one of those minigame locators and make it active plus change its tag
        //picking that minigame plays some sort of signal effect telling the player that that minigame is active
    }

   

    //create some sort of method that is the process for the succesfull completeion of a minigame

}
