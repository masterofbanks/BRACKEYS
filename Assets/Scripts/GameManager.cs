using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("Managers")]
    public GameObject CameraManager;

    //test variable
    //public int activeCamera = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //this is not permanent, just to test moving screen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*Debug.Log("space was pressed");
            if (activeCamera == 0) activeCamera = 1;
            //else if (activeCamera == 1) activeCamera = 2;
            else activeCamera = 0;
            CameraManager.GetComponent<CameraManager>().activateCamera(activeCamera);*/
            Debug.Log("space was pressed");
            //get the number of cameras
            int numCams = CameraManager.GetComponent<CameraManager>().RoomCameras.Length;
            //incrament the current camera index to the next to get the index of the next room's cam
            int currentIndex = CameraManager.GetComponent<CameraManager>().currentCamIndex;
            if(currentIndex == numCams - 1)
            {
                currentIndex = 0;
            }

            else
            {
                currentIndex++;

            }

            //activate the new cam via the index
            CameraManager.GetComponent<CameraManager>().activateCamera(currentIndex);

        }

        else if (Input.GetKeyDown(KeyCode.V))
        {
            CameraManager.GetComponent<CameraManager>().GoBackToMain();
        }
    }

}
