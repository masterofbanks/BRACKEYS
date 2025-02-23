using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PIAs player_input_actions;
    public GameObject miniGameObject;

    private InputAction move;
    private InputAction interact;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MinigameOn"))
        {
            Debug.Log("Witih Minigame circle");
            miniGameObject = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Minigame"))
        {
            Debug.Log("Witih Minigame circle");
            miniGameObject = null;

        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(miniGameObject != null && !GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame)
        {
            miniGameObject.GetComponent<MinigameLocatorBehavior>().ChangeCamsToMinigame();

            GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame = true;
        }
    }

    private void OnEnable()
    {
        move = player_input_actions.Player.Move;
        interact = player_input_actions.Player.Interact;
        move.Enable();
        interact.Enable();

        interact.performed += Interact;
    }

    private void OnDisable()
    {
        move.Disable();
    }
}
