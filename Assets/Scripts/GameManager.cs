using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("Managers")]
    public GameObject CameraManager;

    //test variable
    public int activeCamera = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //this is not permanent, just to test moving screen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space was pressed");
            if (activeCamera == 0) activeCamera = 1;
            //else if (activeCamera == 1) activeCamera = 2;
            else activeCamera = 0;
            CameraManager.GetComponent<CameraManager>().activateCamera(activeCamera);
        }
    }

}
