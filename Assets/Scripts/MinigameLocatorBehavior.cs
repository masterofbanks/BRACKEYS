using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLocatorBehavior : MonoBehaviour
{

    public GameObject miniGameCamera;
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
        miniGameCamera.SetActive(true);
    }
}
