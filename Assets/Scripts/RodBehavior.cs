using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RodBehavior : MonoBehaviour
{
    public bool moveWithMouse;
    public Camera thisCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveWithMouse = true;
        }

        if (moveWithMouse)
        {
            Vector3 mousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3 (mousePos.x, mousePos.y, transform.position.z);
        }
    }

    

    
}
