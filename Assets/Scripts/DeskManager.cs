using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeskManager : MonoBehaviour
{
    [Header("Camera Manager")]
    public GameObject cameraManager;

    [Header("Transition Manager")]
    public CameraTransitionManager cameraTransitionManager;

    public PIAs playerInputActions;
    private InputAction cam1;
    private InputAction cam2;
    private InputAction cam3;
    private InputAction cam4;
    private InputAction takeControl;

    [Header("In Control Boolean")]
    public bool inControl = false;

    void Start()
    {
        cam1 = playerInputActions.Desk.Cam1;
        cam2 = playerInputActions.Desk.Cam2;
        cam3 = playerInputActions.Desk.Cam3;
        cam4 = playerInputActions.Desk.Cam4;
        takeControl = playerInputActions.Desk.TakeControl;
        
        EnableCamSwitch();
        takeControl.Enable();

        cam1.performed += Cam1;
        cam2.performed += Cam2;
        cam3.performed += Cam3;
        cam4.performed += Cam4;
        takeControl.performed += TakeControl;

    }
    private void Awake()
    {
        playerInputActions = new PIAs();
    }
    private void EnableCamSwitch()
    {
        cam1.Enable(); cam2.Enable(); cam3.Enable(); cam4.Enable();
    }
    private void DisableCamSwitch()
    {
        cam1.Disable(); cam2.Disable(); cam3.Disable(); cam4.Disable();
    }
    private void Cam1(InputAction.CallbackContext context)
    {
        cameraManager.GetComponent<CameraManager>().SwitchCamTo(0);
    }
    private void Cam2(InputAction.CallbackContext context)
    {
        cameraManager.GetComponent<CameraManager>().SwitchCamTo(1);
    }
    private void Cam3(InputAction.CallbackContext context)
    {
        cameraManager.GetComponent<CameraManager>().SwitchCamTo(2);
    }
    private void Cam4(InputAction.CallbackContext context)
    {
        cameraManager.GetComponent<CameraManager>().SwitchCamTo(3);
    }
    private void TakeControl(InputAction.CallbackContext context)
    {
        if (inControl)
        {
            inControl = false;
            EnableCamSwitch();
        }
        else
        {
            inControl = true;
            DisableCamSwitch();
        }
        cameraTransitionManager.ToggleTransition();
    }
}
