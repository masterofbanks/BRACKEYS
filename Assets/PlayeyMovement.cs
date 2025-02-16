using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayeyMovement : MonoBehaviour
{
    public PIAs player_input_actions;

    private InputAction move;

    private Rigidbody2D rb;

    private Vector2 rawDirectionInputs;

    public Vector2 DirectionalInput;

    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        DirectionalInput = Vector2.zero;
        rawDirectionInputs = Vector2.zero;
    }

    private void Awake()
    {
        player_input_actions = new PIAs();
    }

    // Update is called once per frame
    void Update()
    {
        rawDirectionInputs = move.ReadValue<Vector2>();
        DirectionalInput = new Vector2(System.Math.Sign(rawDirectionInputs.x), System.Math.Sign(rawDirectionInputs.y));
        rb.velocity = rawDirectionInputs * moveSpeed;
    }

    private void OnEnable()
    {
        move = player_input_actions.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
}
