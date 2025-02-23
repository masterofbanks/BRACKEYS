using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLocatorBehavior : MonoBehaviour
{
    public MiniGameManager minigameManager;
    private CircleCollider2D nonTrigger;
    private CircleCollider2D trigger;
    private SpriteRenderer spriteRenderer;

    private NotificationManager notificationManager;


    void Start()
    {
        notificationManager = GameObject.Find("GameManager").GetComponent<NotificationManager>();
        trigger = GetComponents<CircleCollider2D>()[0];
        nonTrigger = GetComponents<CircleCollider2D>()[1];
        spriteRenderer = GetComponent<SpriteRenderer>();

        trigger.enabled = false;
        nonTrigger.enabled = false;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        gameObject.tag = "MinigameOn";
        trigger.enabled = true;
        nonTrigger.enabled = true;
        spriteRenderer.enabled = true;
        notificationManager.Notify(transform.parent.gameObject.name);
    }

    public void TurnOff()
    {
        gameObject.tag = "Minigame";
        trigger.enabled = false;
        nonTrigger.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void ChangeCamsToMinigame()
    {
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().disableAllCameras();
        minigameManager.cam.SetActive(true);
        minigameManager.enabled = true;
    }
}
