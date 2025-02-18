using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Room Cameras")]
    public GameObject[] RoomCameras;
    public int currentCamIndex;

    [Header("Camera Room Cam")]
    public GameObject MainRoomCam;

    private void Start()
    {
        MainRoomCam.SetActive(true);
        currentCamIndex = 0;
    }

    public void activateCamera(int camIndex)
    {
        currentCamIndex = camIndex;
        disableAllCameras();
        MainRoomCam.SetActive(false);
        RoomCameras[camIndex].SetActive(true);
        RoomCameras[camIndex].GetComponent<RoomCameraFields>().player.GetComponent<PlayeyMovement>().enabled = true;
    }
    void disableAllCameras()
    {
        for (int i = 0; i < RoomCameras.Length; i++)
        {
            RoomCameras[i].SetActive(false);
            RoomCameras[i].GetComponent<RoomCameraFields>().player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
            RoomCameras[i].GetComponent<RoomCameraFields>().player.GetComponent<PlayeyMovement>().enabled = false;
        }
    }
    public void GoBackToMain()
    {
        disableAllCameras();
        MainRoomCam.SetActive(true);
        currentCamIndex = 0;
    }

    
}
