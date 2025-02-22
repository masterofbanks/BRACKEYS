using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public GameObject CameraManager;
    public bool inMinigame;
    
    public List<GameObject> MiniGameLocators;
    public List<GameObject> ActiveMiniGames;

    void Start()
    {

    }

    void Update()
    {
        //every 10 seconds 
            //find all minigame locators in the scene that have the non active tag
                //if there are none -> do nothing for now but this might be a losing condition
            //randomly pick one of those minigame locators and make it active plus change its tag
                //picking that minigame plays some sort of signal effect telling the player that that minigame is active

    }

    //create some sort of method that is the process for the succesfull completeion of a minigame
    
    
}
