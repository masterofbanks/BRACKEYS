using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class CrankGameManager : MiniGameManager
{
    [Header("Game Settings")]
    public int spinGoal = 8;

    [Header("Crank Collisions")]
    public GameObject[] gameObjects;

    [Header("Wheel Prefab")]
    public GameObject wheelPrefab;
    private GameObject wheelInstance;

    [Header("Input")]
    public PIAs playerInputActions;
    private InputAction click;

    [Header("Desk Layer")]
    public LayerMask deskLayer;
    void Update()
    {
        if (wheelInstance.GetComponent<WheelBehavior>().spins >= spinGoal*4)
        {
            this.GetComponent<CrankGameManager>().enabled = false;
        }
    }
    private void Awake()
    {
        playerInputActions = new PIAs();
    }
    private void OnEnable()
    {
        //activate camera
        cam.SetActive(true);
        //activate clicking
        click = playerInputActions.Desk.Click;
        click.Enable();
        click.performed += Click;
        //instantiate wheel at center of room
        wheelInstance = Instantiate(wheelPrefab, transform);
    }
    private void OnDisable()
    {
        //locator remover .GetComponent<MinigameLocatorBehavior>().TurnOff();
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().ActivateCamera(GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().currentCamIndex);
        GameObject.FindWithTag("Desk").GetComponent<DeskManager>().player.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame = false;
        Destroy(wheelInstance);

    }
    private void Click(InputAction.CallbackContext context)
    {
        Ray ray = this.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);
        if (hit.collider != null)
        {
            wheelInstance.GetComponent<WheelBehavior>().StartHold();
        }
    }
}
