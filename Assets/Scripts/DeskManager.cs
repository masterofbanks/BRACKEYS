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

    [Header("Buttons")]
    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject redButton;
    public GameObject yellowButton;
    public GameObject blackButton;
    public GameObject q_button;

    public PIAs playerInputActions;
    private InputAction cam1;
    private InputAction cam2;
    private InputAction cam3;
    private InputAction cam4;
    private InputAction takeControl;
    private InputAction click;
    private InputAction GoToMain;

    [Header("In Control Boolean")]
    public bool inControl = false;

    [Header("Desk Layer")]
    public LayerMask deskLayer;

    [Header("Player To Control")]
    public GameObject player;
    void Start()
    {
        cam1 = playerInputActions.Desk.Cam1;
        cam2 = playerInputActions.Desk.Cam2;
        cam3 = playerInputActions.Desk.Cam3;
        cam4 = playerInputActions.Desk.Cam4;
        takeControl = playerInputActions.Desk.TakeControl;
        click = playerInputActions.Desk.Click;
        GoToMain = playerInputActions.Desk.Main;

        EnableCamSwitch();
        takeControl.Enable();
        click.Enable();
        GoToMain.Enable();

        cam1.performed += Cam1;
        cam2.performed += Cam2;
        cam3.performed += Cam3;
        cam4.performed += Cam4;
        takeControl.performed += TakeControl;
        click.performed += Click;
        GoToMain.performed += MainCam;

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
        if (!GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            StartCoroutine(PlayAnimation(greenButton));
            SwitchCamTo(0);
        }
        
    }
    private void Cam2(InputAction.CallbackContext context)
    {
        if (!GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            StartCoroutine(PlayAnimation(redButton));
            SwitchCamTo(1);
        }
        
    }
    private void Cam3(InputAction.CallbackContext context)
    {
        if (!GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            StartCoroutine(PlayAnimation(yellowButton));
            SwitchCamTo(2);
        }
        
    }
    private void Cam4(InputAction.CallbackContext context)
    {
        if (!GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            StartCoroutine(PlayAnimation(blueButton));
            SwitchCamTo(3);
        }
        
    }

    private void MainCam(InputAction.CallbackContext context)
    {
        if (!GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            StartCoroutine(PlayAnimation(q_button));
            SwitchCamTo(4);
        }
    }
    private void SwitchCamTo(int cam)
    {
        DisableCamSwitch();
        if(!cameraTransitionManager.isZoomedIn) cameraManager.GetComponent<CameraManager>().SwitchCamTo(cam);
        EnableCamSwitch();
    }
    private void TakeControl(InputAction.CallbackContext context)
    {
        if (!GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().MainRoomCam.activeSelf)
        {
           
            StartCoroutine(PlayAnimation(blackButton));
            takeControl.Disable();
            if (!cameraTransitionManager.isTransitioning)
            {
                DisableCamSwitch();
                if (inControl)
                {
                    player.GetComponent<PlayerMovement>().enabled = false;
                    inControl = false;
                    EnableCamSwitch();
                }
                else
                {
                    player.GetComponent<PlayerMovement>().enabled = true;
                    inControl = true;
                }
                cameraTransitionManager.ToggleTransition();
            }
            takeControl.Enable();
        }
        
    }
    private void Click(InputAction.CallbackContext context)
    {
        Ray ray = GameObject.FindWithTag("DeskCam").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);
        if(hit.collider != null)
        {
            //StartCoroutine(PlayAnimation(hit.collider.gameObject));
            switch (hit.collider.gameObject.tag)
            {
                case "green":
                    StartCoroutine(PlayAnimation(greenButton));
                    SwitchCamTo(0); break;
                case "red":
                    StartCoroutine(PlayAnimation(redButton));
                    SwitchCamTo(1); break;
                case "yellow":
                    StartCoroutine(PlayAnimation(yellowButton));
                    SwitchCamTo(2); break;
                case "blue":
                    StartCoroutine(PlayAnimation(blueButton));
                    SwitchCamTo(3); break;
                case "q":
                    StartCoroutine(PlayAnimation(q_button));
                    SwitchCamTo(4);  break;
                case "black":
                    StartCoroutine(PlayAnimation(blackButton));
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
                    takeControl.Enable(); break;
                default: Debug.Log("no button"); break;
            }
        }
    }
    IEnumerator PlayAnimation(GameObject button)
    {
        if (!button.GetComponent<Animator>().GetBool(0))
        {
            button.GetComponent<Animator>().SetBool("clicked", true);
            yield return new WaitForSeconds(0.20f);
            button.GetComponent<Animator>().SetBool("clicked", false);
        }
    }

    private void OnEnable()
    {
        
    }

}