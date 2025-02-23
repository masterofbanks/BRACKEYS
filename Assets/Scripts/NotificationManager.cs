using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Time between blinks
    public float duration = 3f; // How long the notification stays active
    public GameObject[] roomSpecificNotifications;
    public GameObject[] genericNotifications;

    public CameraManager cameraManager;

    private List<GameObject> activeNotifications = new List<GameObject>();
    private List<GameObject> persistentNotifications = new List<GameObject>();

    void Start()
    {
        // Start all notifications as inactive
        foreach (GameObject notif in genericNotifications)
        {
            notif.SetActive(false);
        }
        foreach (GameObject notif in roomSpecificNotifications)
        {
            notif.SetActive(false);
        }
        StartCoroutine(BlinkNotificationsRepeat());
    }

    public void Notify(string name = null)
    {
        activeNotifications.Clear(); // Reset active notifications
        if (cameraManager.currentCamIndex == 0)
        {
            foreach (GameObject notif in roomSpecificNotifications)
            {
                if (notif.name == name)
                {
                    notif.SetActive(true);
                    persistentNotifications.Add(notif);
                }
            }
        }
        else
        {
            foreach (GameObject notif in genericNotifications)
            {
                notif.SetActive(true);
                activeNotifications.Add(notif);
            }
            StartCoroutine(BlinkNotificationsShort());
            StartCoroutine(HideNotificationsAfterDelay());
        }
    }

    public void HideNotification(string name)
    {
        GameObject notif = null;
        foreach (GameObject dignus in roomSpecificNotifications)
        {
            if (dignus.name == name)
            {
               notif = dignus;
            }
        }
        if (persistentNotifications.Contains(notif))
        {
            persistentNotifications.Remove(notif);
            notif.SetActive(false);
        }
       
    }

    private IEnumerator BlinkNotificationsShort()
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            isVisible = !isVisible;

            foreach (GameObject notif in activeNotifications)
            {
                Image notifImage = notif.GetComponent<Image>();
                if (notifImage != null)
                {
                    // If using an Image, modify alpha
                    Color color = notifImage.color;
                    color.a = isVisible ? 1f : 0.3f; // Full opacity or dim
                    notifImage.color = color;
                }
                else
                {
                    // If no image, just enable/disable the GameObject
                    notif.SetActive(isVisible);
                }
            }
            

            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
    }

    private IEnumerator BlinkNotificationsRepeat()
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (true)
        {
            isVisible = !isVisible;

            foreach (GameObject notif in persistentNotifications)
            {
                Image notifImage = notif.GetComponent<Image>();
                if (notifImage != null)
                {
                    // If using an Image, modify alpha
                    Color color = notifImage.color;
                    color.a = isVisible ? 1f : 0.3f; // Full opacity or dim
                    notifImage.color = color;
                }
                else
                {
                    // If no image, just enable/disable the GameObject
                    notif.SetActive(isVisible);
                }
            }


            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
    }

    private IEnumerator HideNotificationsAfterDelay()
    {
        yield return new WaitForSeconds(duration);

        foreach (GameObject notif in activeNotifications)
        {
            notif.SetActive(false);
        }

        activeNotifications.Clear();
    }
}
