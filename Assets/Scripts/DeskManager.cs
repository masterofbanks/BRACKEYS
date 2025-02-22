using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Experimental.GraphView.GraphView;

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
    private InputAction click;

    [Header("In Control Boolean")]
    public bool inControl = false;

    [Header("Desk Layer")]
    public LayerMask deskLayer;
    void Start()
    {
        cam1 = playerInputActions.Desk.Cam1;
        cam2 = playerInputActions.Desk.Cam2;
        cam3 = playerInputActions.Desk.Cam3;
        cam4 = playerInputActions.Desk.Cam4;
        takeControl = playerInputActions.Desk.TakeControl;
        click = playerInputActions.Desk.Click;

        EnableCamSwitch();
        takeControl.Enable();
        click.Enable();

        cam1.performed += Cam1;
        cam2.performed += Cam2;
        cam3.performed += Cam3;
        cam4.performed += Cam4;
        takeControl.performed += TakeControl;
        click.performed += Click;

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
        SwitchCamTo(0);
    }
    private void Cam2(InputAction.CallbackContext context)
    {
        SwitchCamTo(1);
    }
    private void Cam3(InputAction.CallbackContext context)
    {
        SwitchCamTo(2);
    }
    private void Cam4(InputAction.CallbackContext context)
    {
        SwitchCamTo(3);
    }
    private void SwitchCamTo(int cam)
    {
        DisableCamSwitch();
        if(!cameraTransitionManager.isZoomedIn) cameraManager.GetComponent<CameraManager>().SwitchCamTo(cam);
        EnableCamSwitch();
    }
    private void TakeControl(InputAction.CallbackContext context)
    {
        takeControl.Disable();
        if (!cameraTransitionManager.isTransitioning)
        {
            DisableCamSwitch();
            if (inControl)
            {
                inControl = false;
                EnableCamSwitch();
            }
            else
            {
                inControl = true;
            }
            cameraTransitionManager.ToggleTransition();
        }
        takeControl.Enable();

    }
    private void Click(InputAction.CallbackContext context)
    {
        Ray ray = GameObject.FindWithTag("DeskCam").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            StartCoroutine(PlayAnimation(hit));
        }
    }
    IEnumerator PlayAnimation(RaycastHit2D hit)
    {
        hit.collider.gameObject.GetComponent<Animator>().SetBool("clicked", true);
        yield return new WaitForSeconds(0.20f);
        hit.collider.gameObject.GetComponent<Animator>().SetBool("clicked", false);
    }
}