using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class CameraManager : MonoBehaviour
{
    [Header("Room Cameras")]
    public GameObject[] RoomCameras;
    public int currentCamIndex;

    [Header("Camera Room Cam")]
    public GameObject MainRoomCam;

    [Header("Transition Manager")]
    public CameraTransitionManager cameraTransitionManager;

    [Header("Static Prefabs")]
    public GameObject StaticEffectPrefab;
    private GameObject staticEffectInstance;
    private VideoPlayer staticVideoPlayer;

    private void Start()
    {
        MainRoomCam.SetActive(true);
        currentCamIndex = 0;
        InstantiateStatic();
    }
    private void InstantiateStatic()
    {
        // Pre-instantiate the StaticEffectPrefab and get the VideoPlayer component
        staticEffectInstance = Instantiate(StaticEffectPrefab);
        staticVideoPlayer = staticEffectInstance.GetComponent<VideoPlayer>();

        // Set the VideoPlayer's render mode to CameraNearPlane and assign the main camera
        staticVideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        staticVideoPlayer.targetCamera = Camera.main;

        // Disable the static effect instance initially
        staticEffectInstance.SetActive(false);
    }
    private void PlayStaticOnSwitch(int newCamIndex)
    {
        // Get the new room's position
        GameObject newRoom = RoomCameras[newCamIndex].GetComponent<RoomCameraFields>().room;

        // Set the position and rotation of the static effect instance
        staticEffectInstance.transform.position = newRoom.transform.position;
        staticEffectInstance.transform.rotation = newRoom.transform.rotation;

        // Enable the static effect instance and play the video
        staticEffectInstance.SetActive(true);
        staticVideoPlayer.Play();
    }
    private IEnumerator DisableStaticEffectAndSwitchCamera(int newCamIndex)
    {
        // Wait for the duration of the video
        yield return new WaitForSeconds((float)staticVideoPlayer.clip.length);

        // Disable the static effect instance
        staticEffectInstance.SetActive(false);

        // Activate the new cam via the index
        ActivateCamera(newCamIndex);
    }
    public void SwitchCamTo(int camIndex)
    {
        PlayStaticOnSwitch(camIndex);
        StartCoroutine(DisableStaticEffectAndSwitchCamera(camIndex));
    }
    public void ActivateCamera(int camIndex)
    {
        currentCamIndex = camIndex;
        disableAllCameras();
        MainRoomCam.SetActive(false);
        RoomCameras[camIndex].SetActive(true);
        RoomCameras[camIndex].GetComponent<RoomCameraFields>().player.GetComponent<PlayerMovement>().enabled = true;
    }

    

    public void disableAllCameras()
    {
        for (int i = 0; i < RoomCameras.Length; i++)
        {
            RoomCameras[i].SetActive(false);
            RoomCameras[i].GetComponent<RoomCameraFields>().player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
            RoomCameras[i].GetComponent<RoomCameraFields>().player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void GoBackToMain()
    {
        disableAllCameras();
        MainRoomCam.SetActive(true);
    }
}
