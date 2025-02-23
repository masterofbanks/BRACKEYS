using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoardLight : MonoBehaviour
{
    private Renderer rend;
    private bool isOn = false;

    public void Awake()
    {
        rend = this.GetComponent<Renderer>();
        rend.material.color = isOn ? Color.green : Color.red;
    }
    public void ToggleLight()
    {
        isOn = !isOn;
        rend.material.color = isOn ? Color.green : Color.red;
    }

    public bool getIsOn()
    {
        return isOn;
    }

}
