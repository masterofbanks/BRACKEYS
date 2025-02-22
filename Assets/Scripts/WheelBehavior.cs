using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : MonoBehaviour
{
    private bool held = false;
    void Update()
    {
        if (held)
        {
            Vector2 direction = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
            transform.up = direction;
        }
    }
    public void StartHold()
    {
        held = true;
    }
}
