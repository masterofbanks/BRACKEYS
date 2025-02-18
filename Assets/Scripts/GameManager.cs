using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject CameraManager;
    public GameObject Static;

    

    void Start()
    {
        
    }

    void Update()
    {
        //this is not permanent, just to test moving screen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
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

            //instantiate static effect at the position of the new camera
            GameObject newRoom = CameraManager.GetComponent<CameraManager>().RoomCameras[currentIndex].GetComponent<RoomCameraFields>().room;
            Instantiate(Static, newRoom.transform.position, newRoom.transform.rotation);
            //activate the new cam via the index

            CameraManager.GetComponent<CameraManager>().activateCamera(currentIndex);

        }

        else if (Input.GetKeyDown(KeyCode.V))
        {
            CameraManager.GetComponent<CameraManager>().GoBackToMain();
        }
    }

}
