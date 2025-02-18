using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public GameObject CameraManager;
    public GameObject StaticEffectPrefab;

    private GameObject staticEffectInstance;
    private VideoPlayer staticVideoPlayer;

    void Start()
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

    void Update()
    {
        // This is not permanent, just to test moving screen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space was pressed");
            // Get the number of cameras
            int numCams = CameraManager.GetComponent<CameraManager>().RoomCameras.Length;
            // Increment the current camera index to the next to get the index of the next room's cam
            int currentIndex = CameraManager.GetComponent<CameraManager>().currentCamIndex;
            if (currentIndex == numCams - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            // Get the new room's position
            GameObject newRoom = CameraManager.GetComponent<CameraManager>().RoomCameras[currentIndex].GetComponent<RoomCameraFields>().room;

            // Set the position and rotation of the static effect instance
            staticEffectInstance.transform.position = newRoom.transform.position;
            staticEffectInstance.transform.rotation = newRoom.transform.rotation;

            // Enable the static effect instance and play the video
            staticEffectInstance.SetActive(true);
            staticVideoPlayer.Play();

            // Start coroutine to disable the static effect instance and switch camera after the video finishes playing
            StartCoroutine(DisableStaticEffectAndSwitchCamera(currentIndex));
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            CameraManager.GetComponent<CameraManager>().GoBackToMain();
        }
    }

    private IEnumerator DisableStaticEffectAndSwitchCamera(int newCamIndex)
    {
        // Wait for the duration of the video
        yield return new WaitForSeconds((float)staticVideoPlayer.clip.length);

        // Disable the static effect instance
        staticEffectInstance.SetActive(false);

        // Activate the new cam via the index
        CameraManager.GetComponent<CameraManager>().activateCamera(newCamIndex);
    }
}
