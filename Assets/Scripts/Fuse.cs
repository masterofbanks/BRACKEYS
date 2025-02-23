using UnityEngine;

public class ClickToSpawnDraggable : MonoBehaviour {
    // Assign your draggable prefab in the Inspector
    public GameObject draggablePrefab;

    void OnMouseDown() {
        // Spawn the draggable object at the current sprite's position
        Instantiate(draggablePrefab, transform.position, Quaternion.identity);
        // Remove the original sprite
        Destroy(gameObject);
    }
}