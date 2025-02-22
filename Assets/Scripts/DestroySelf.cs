using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float timeAlive;

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < timeAlive)
        {
            t += Time.deltaTime;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
