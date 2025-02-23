using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class CrankGameManager : MiniGameManager
{
    [Header("Game Settings")]
    public int spinGoal = 6;

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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void Click(InputAction.CallbackContext context)
    {
        Debug.Log("bruh");
        Ray ray = this.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);
        if (hit.collider != null)
        {
            Debug.Log("hold");
            wheelInstance.GetComponent<WheelBehavior>().StartHold();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("genBottom"))
        {
            
        }*/
        Debug.Log(collision.gameObject.name);
    }
}
