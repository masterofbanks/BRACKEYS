using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class LightGameManager : MiniGameManager
{
    public Transform[] boxBounds;
    private Vector2 boxCenter;
    private float boxLength;
    private float boxWidth;
    private SpriteRenderer rend;
    public Sprite breakerOn;
    public Sprite breakerOff;
    public GameObject flashlight;
    public GameObject switchObj;

    [Header("Input")]
    public PIAs playerInputActions;
    private InputAction click;

    [Header("Desk Layer")]
    public LayerMask deskLayer;


    private bool dark = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dark)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.GetComponent<Camera>().ScreenToWorldPoint(mousePosition);
            flashlight.transform.position = mousePosition;
            flashlight.transform.position = new Vector3 (flashlight.transform.position.x, flashlight.transform.position.y, -4f);
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
        //spawn switch
        flashlight.GetComponent<SpriteRenderer>().enabled = true;
        dark = true;
        boxCenter = new Vector2((boxBounds[0].position.x + boxBounds[1].position.x) / 2.0f, (boxBounds[0].position.y + boxBounds[2].position.y) / 2.0f);
        boxLength = Mathf.Abs(boxBounds[1].position.x - boxBounds[0].position.x) / 2.0f;
        boxWidth = Mathf.Abs(boxBounds[2].position.y - boxBounds[0].position.y) / 2.0f;
        switchObj.GetComponent<Transform>().position = boxCenter + new Vector2(Random.insideUnitCircle.x * boxLength, Random.insideUnitCircle.y * boxWidth);
        switchObj.GetComponent<SpriteRenderer>().sprite = breakerOff;
        switchObj.GetComponent<SpriteRenderer>().enabled = true;
        switchObj.transform.position = new Vector3(switchObj.transform.position.x, flashlight.transform.position.y, -4f);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        flashlight.GetComponent<SpriteRenderer>().enabled = false; dark = false;
        switchObj.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Click(InputAction.CallbackContext context)
    {
        Ray ray = this.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, deskLayer);

        if (hit.collider != null)
        {
            StartCoroutine(SwitchOnEnum());
        }
    }
    IEnumerator SwitchOnEnum()
    {
        switchObj.GetComponent<SpriteRenderer>().sprite = breakerOn;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<LightGameManager>().enabled = false;
    }
}
