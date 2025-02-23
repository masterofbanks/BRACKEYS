using UnityEngine;

public class DraggableSprite : MonoBehaviour {
    private Vector3 offset;
    private bool dragging = false;
    private float zDistance;

    void OnMouseDown() {
        // Calculate the distance from the camera to the object
        zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
        // Calculate offset between sprite position and mouse position
        offset = transform.position - mousePos;
        dragging = true;
    }

    void OnMouseDrag() {
        if (dragging) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
            // Update sprite position while preserving depth
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp() {
        dragging = false;
    }
}
