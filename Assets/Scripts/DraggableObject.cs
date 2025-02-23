using UnityEngine;

public class DraggableObject : MonoBehaviour {
    private Rigidbody2D rb;
    private bool dragging;
    private Vector3 offset;
    private float distanceFromCamera;
    private Vector3 targetPos;
    private Vector3 lastMouseWorldPos;
    private Vector2 throwVelocity;
    
    public float throwMultiplier = 5f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void OnMouseDown() {
        // Start dragging
        dragging = true;
        // Compute the distance between camera and object (object should be on the same plane)
        distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        // Convert mouse position to world coordinates using the stored distance
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
        // Calculate the offset between the object’s position and where you clicked
        offset = transform.position - mouseWorldPos;
        // Set the initial target position
        targetPos = transform.position;
        lastMouseWorldPos = mouseWorldPos;
        // Disable gravity during drag
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }
    
    void OnMouseDrag() {
        if (dragging) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
            // Update the target position so the object maintains the offset
            targetPos = mouseWorldPos + offset;
            // Calculate instantaneous throw velocity for later use
            throwVelocity = (mouseWorldPos - lastMouseWorldPos) / Time.deltaTime;
            lastMouseWorldPos = mouseWorldPos;
        }
    }
    
    void FixedUpdate() {
        if (dragging) {
            // Use MovePosition in FixedUpdate for smooth physics-based movement
            rb.MovePosition(targetPos);
        }
    }
    
    void OnMouseUp() {
        // Stop dragging
        dragging = false;
        // Re-enable gravity
        rb.gravityScale = 1;
        // Apply the stored throw velocity multiplied by your throw factor
        rb.velocity = throwVelocity * throwMultiplier;
    }
}
