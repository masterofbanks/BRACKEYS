using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialBehavior : MonoBehaviour
{
    private bool held = false;
    private Camera cam;

    public int spins = 0;
    private void Start()
    {
        //cam = GameObject.FindWithTag("dialCam").GetComponent<Camera>();
        cam = GameObject.FindWithTag("q").GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        if (held)
        {
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = direction;
        }
    }
    public void StartHold()
    {
        held = true;
    }
    public void StopHold()
    {
        held = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spins++;
    }
}
