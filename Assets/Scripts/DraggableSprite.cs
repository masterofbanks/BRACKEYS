using UnityEngine;

public class DraggableSprite : MonoBehaviour {
    private Vector3 offset;
    private bool dragging = false;

    void OnMouseDown() {
        // Convert the mouse position from screen to world coordinates.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Calculate offset between sprite position and mouse position.
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        dragging = true;
    }

    void OnMouseDrag() {
        if (dragging) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            // Update sprite position while preserving depth.
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        }
    }

    void OnMouseUp() {
        dragging = false;
    }
}
