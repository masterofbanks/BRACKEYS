using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : MonoBehaviour
{
    private bool held = false;
    private Camera cam;


    public int spins = 0;

    private void Start()
    {
        cam = GameObject.FindWithTag("crankCam").GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        if (held)
        {
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.right = direction;
        }
    }
    public void StartHold()
    {
        held = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spins++;
    }
}
