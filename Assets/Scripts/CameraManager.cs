using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Sub Cameras")]
    public GameObject[] SubCameras;

    private void Start()
    {
        SubCameras[0].SetActive(true);
    }

    public void activateCamera(int camIndex)
    {
        disableAllCameras();
        SubCameras[camIndex].SetActive(true);
    }
    void disableAllCameras()
    {
        for(int i = 0; i< SubCameras.Length; i++) SubCameras[i].SetActive(false);
    }

    
}
