using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class DialGameManager : MiniGameManager
{
  
    [Header("Dial Prefab")]
    public GameObject dialPrefab;
    public GameObject goalPrefab;
    private GameObject dialInstance1;
    private GameObject dialInstance2;
    private GameObject dialInstance3;
    private GameObject goalInstance1;
    private GameObject goalInstance2;
    private GameObject goalInstance3;
    private GameObject heldDial;


    [Header("Dial Positions")]
    public Transform dialtran1;
    public Transform dialtran2;
    public Transform dialtran3;

    [Header("Input")]
    public PIAs playerInputActions;
    private InputAction click;

    [Header("Desk Layer")]
    public LayerMask deskLayer;
    void Update()
    {
        if (dialInstance1.GetComponent<Collider2D>().IsTouching(goalInstance1.GetComponent<Collider2D>()))
        {
            if (dialInstance2.GetComponent<Collider2D>().IsTouching(goalInstance2.GetComponent<Collider2D>()))
            {
                if (dialInstance3.GetComponent<Collider2D>().IsTouching(goalInstance3.GetComponent<Collider2D>()))
                {
                    this.GetComponent<DialGameManager>().enabled = false;
                }
            }
        }
    }
    private void Awake()
    {
        playerInputActions = new PIAs();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        //activate clicking
        click = playerInputActions.Desk.Click;
        click.Enable();
        click.performed += Click;
        click.canceled += Release;

        //instantiate wheel at center of room
        dialInstance1 = Instantiate(dialPrefab, dialtran1);
        dialInstance2 = Instantiate(dialPrefab, dialtran2);
        dialInstance3 = Instantiate(dialPrefab, dialtran3);

        goalInstance1 = Instantiate(goalPrefab, dialtran1); 
        goalInstance1.transform.Rotate(0, 0, Random.Range(0f, 360f));
        goalInstance2 = Instantiate(goalPrefab, dialtran2);
        goalInstance2.transform.Rotate(0, 0, Random.Range(0f, 360f));
        goalInstance3 = Instantiate(goalPrefab, dialtran3);
        goalInstance3.transform.Rotate(0, 0, Random.Range(0f, 360f));
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Destroy(dialInstance1);
        Destroy(dialInstance2);
        Destroy(dialInstance3);
        Destroy(goalInstance1);
        Destroy(goalInstance2);
        Destroy(goalInstance3);
    }
    private void Click(InputAction.CallbackContext context)
    {
        Ray ray = this.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);
        if (hit.collider != null)
        {
           hit.collider.gameObject.GetComponent<DialBehavior>().StartHold();
            heldDial = hit.collider.gameObject;
        }
    }
    private void Release(InputAction.CallbackContext context)
    {
        if (heldDial != null)
        {
            heldDial.GetComponent<DialBehavior>().StopHold();
            heldDial = null;
        }
    }
}
