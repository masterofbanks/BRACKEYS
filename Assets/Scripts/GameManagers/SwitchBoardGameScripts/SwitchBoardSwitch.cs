using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoardSwitch : MonoBehaviour
{
    private GameObject controlledLight;
    private bool isOn; // Track switch state
    private SpriteRenderer spriteRenderer;

    private float onY = 0.525f ;
    private float offY = -0.1f;

    public void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        isOn = Random.value > 0.5f;
        UpdateSwitchState();
    }
    public void SetControlledLight(GameObject light)
    {
        controlledLight = light;
    }

    private void OnMouseDown() // Click to toggle switch
    {
        isOn = !isOn;
        UpdateSwitchState();

        if (controlledLight != null)
        {
            SwitchBoardLight lightController = controlledLight.GetComponent<SwitchBoardLight>();
            if (lightController != null)
            {
                lightController.ToggleLight();
            }
        }
        
    }

    private void UpdateSwitchState()
    {
        float currentX = transform.localPosition.x; // Get current X position
        float currentZ = transform.localPosition.z; // Keep Z the same

        transform.localPosition = new Vector3(currentX, isOn ? onY : offY, currentZ);
        spriteRenderer.flipY = !isOn;
    }
}
