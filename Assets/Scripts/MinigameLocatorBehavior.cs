using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLocatorBehavior : MonoBehaviour
{

    public GameObject miniGameRoom;
    public string MiniGameName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamsToMinigame()
    {
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().disableAllCameras();
        string name = MiniGameName;
        switch (name)
        {
            case "Rod":
                miniGameRoom.GetComponent<RodGameManager>().StartUp();
                break;
            default:
                Debug.Log("Missing Game Name");
                break;

        }
    }
}
