using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI creds;
    public GameObject endpos;
    public float speed;
    private Vector3 startpos;
    void Start()
    {
        startpos = GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (creds.transform.position == endpos.transform.position)
        {
            GetComponent<RectTransform>().position = startpos;
        }
        GetComponent<RectTransform>().position = Vector3.MoveTowards(creds.transform.position, endpos.transform.position, speed);
    }
}

