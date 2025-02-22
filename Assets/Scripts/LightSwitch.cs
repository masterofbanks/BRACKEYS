using UnityEngine;

public class LightSwitchToggle : MonoBehaviour {
    public Sprite offSprite; // Assign in Inspector
    public Sprite onSprite;  // Assign in Inspector

    private SpriteRenderer spriteRenderer;
    private bool isOn = false;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = offSprite;
    }

    void OnMouseDown() {
        isOn = !isOn;
        spriteRenderer.sprite = isOn ? onSprite : offSprite;
        // Optionally, add logic here to handle turning lights on or off in your scene.
    }
}
