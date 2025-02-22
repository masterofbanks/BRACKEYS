using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RodBehavior : MonoBehaviour
{
    private InputAction move;
    private InputAction rotate;
    public PIAs inputs;

    private Vector2 rawDirectionInputs;

    public Vector2 DirectionalInput;

    public float moveSpeed;
    public bool startRotating;
    public float rotateUnit;
    public bool horizontal_rod;
    private Rigidbody2D rb;
    private float z_coord;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startRotating = false;
        if (horizontal_rod)
        {
            z_coord = 90;
        }
        else
        {
            z_coord = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rawDirectionInputs = move.ReadValue<Vector2>();
        DirectionalInput = new Vector2(System.Math.Sign(rawDirectionInputs.x), System.Math.Sign(rawDirectionInputs.y));
        rb.velocity = rawDirectionInputs * moveSpeed;

        
    }

    private void Awake()
    {
        inputs = new PIAs();
    }

    private void Rotate(InputAction.CallbackContext context)
    {
        z_coord += rotateUnit;
        transform.rotation = Quaternion.Euler(0, 0, z_coord);
    }

    

    private void OnEnable()
    {
        move = inputs.Player.Move;
        move.Enable();

        rotate = inputs.Player.Rotate;
        rotate.Enable();
        rotate.performed += Rotate;
    }


    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RodGoal") && horizontal_rod)
        {
            Debug.Log("Goal Reached");
            GetComponentInParent<RodGameManager>().enabled = false;
        }
    }


}
