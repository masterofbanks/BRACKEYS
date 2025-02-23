using UnityEngine;

public class DraggableObject : MonoBehaviour {
    private Rigidbody2D rb;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 lastMouseWorldPos;
    private Vector2 throwVelocity;
    
    // A multiplier to control the throw's strength.
    public float throwMultiplier = 5f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        dragging = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Calculate the offset between the object's position and the mouse's position
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        lastMouseWorldPos = mouseWorldPos;
        // Optionally disable gravity so the object doesn't fall during dragging
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    void OnMouseDrag() {
        if (dragging) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            // Determine the target position and update using MovePosition for physics interaction
            Vector3 targetPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;
            rb.MovePosition(targetPos);
            // Calculate the velocity based on the movement since last frame
            throwVelocity = (mouseWorldPos - lastMouseWorldPos) / Time.deltaTime;
            lastMouseWorldPos = mouseWorldPos;
        }
    }

    void OnMouseUp() {
        dragging = false;
        // Re-enable gravity after releasing
        rb.gravityScale = 1;
        // Apply the computed velocity (scaled by the multiplier) to "throw" the object
        rb.velocity = throwVelocity * throwMultiplier;
    }
}
