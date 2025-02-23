using UnityEngine;

public class DraggableFuse : MonoBehaviour {
    public Vector2 targetPosition; // Set this to the broken fuse's last known location.
    public float placementThreshold = 0.5f; // How close it must be to count as "placed"

    private Vector3 offset;
    private bool dragging = false;
    private float zDistance;

    void OnMouseDown() {
        dragging = true;
        // Calculate the correct distance from the camera to the object
        zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseDrag() {
        if (dragging) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
            transform.position = mouseWorldPos + offset;
        }
    }

    void OnMouseUp() {
        dragging = false;
        // Check if the current position is close enough to the target position.
        if (Vector2.Distance(transform.position, targetPosition) <= placementThreshold) {
            Debug.Log("Fuse placed correctly!");
            // Optionally snap it to the target location:
            transform.position = targetPosition;
            // Trigger further game events here
        } else {
            Debug.Log("Fuse not placed correctly.");
        }
    }
}
