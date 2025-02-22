using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MouseBehavior : MonoBehaviour
{
    public Camera thisCamera;

    public bool rodGrabbed;


    // Start is called before the first frame update
    void Start()
    {
        rodGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.CompareTag("Rod"))
            {
                GameObject[] Rods = GameObject.FindGameObjectsWithTag("Rod");
                for(int i = 0; i < Rods.Length; i++)
                {
                    Rods[i].GetComponent<RodBehavior>().enabled = false;
                    Rods[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                hit.collider.gameObject.GetComponent<RodBehavior>().enabled = true;
            }

        }
    }
}
