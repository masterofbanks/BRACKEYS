using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchboardGameManager : MiniGameManager
{
    public List<GameObject> switchList;
    public List<GameObject> lightList;

    private Dictionary<GameObject, GameObject> switchLightMapping = new Dictionary<GameObject, GameObject>();

    
    private void Update()
    {
        if (checkWinState() == true)
        {
            this.gameObject.SetActive(false);
        }

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        AssignRandomSwitchesToLights();
        setupBoard(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        setupBoard(false);
    }

    private void AssignRandomSwitchesToLights()
    {
        List<GameObject> availableLights = new List<GameObject>(lightList);

        foreach (GameObject switchObj in switchList)
        {
            if (availableLights.Count == 0) break; // Avoid errors if more switches than lights

            int randomIndex = Random.Range(0, availableLights.Count);
            switchLightMapping[switchObj] = availableLights[randomIndex];
            availableLights.RemoveAt(randomIndex); // Ensure each light is assigned only once

            // Assign light reference to switch component (if applicable)
            SwitchBoardSwitch switchComponent = switchObj.GetComponent<SwitchBoardSwitch>();
            if (switchComponent != null)
            {
                switchComponent.SetControlledLight(switchLightMapping[switchObj]);
            }
        }
    }

    private void setupBoard(bool setup)
    {
        foreach (GameObject lightObj in lightList)
        {
            lightObj.SetActive(setup);
        }
        foreach (GameObject switchobj in switchList)
        {
            switchobj.SetActive(setup);
        }
    }

    public bool checkWinState()
    {
        foreach (GameObject lightObj in lightList)
        {
            if (lightObj.GetComponent<SwitchBoardLight>().getIsOn() == false)
                return false;
        }
        return true;
    }
}
