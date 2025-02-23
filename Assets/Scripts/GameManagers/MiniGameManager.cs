using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject cam;
    public GameObject gameLocator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        cam.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        gameLocator.GetComponent<MinigameLocatorBehavior>().TurnOff();
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().ActivateCamera(GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().currentCamIndex);
        GameObject.FindWithTag("Desk").GetComponent<DeskManager>().player.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame = false;
    }
}
