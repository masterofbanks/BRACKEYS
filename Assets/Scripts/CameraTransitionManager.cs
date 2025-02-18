using System;
using System.Collections;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetTransform; // The transform of the UI Image or Sprite Renderer
    public float transitionDuration = 1.0f; // Duration of the transition
    public CCTVRippleEffect cctvEffect;
    private Vector3 originalPosition;
    private float originalSize;
    private bool isTransitioning = false; // Flag to track the transition state
    private bool isZoomedIn = false; // Flag to track if the camera is zoomed in

    void Start()
    {
        originalPosition = mainCamera.transform.position;
        originalSize = mainCamera.orthographicSize;
    }

    public void ToggleTransition()
    {
        if (!isTransitioning)
        {
            if (!isZoomedIn)
            {
                StartCoroutine(TransitionToTarget());
            }
            else
            {
                StartCoroutine(TransitionToOriginal());
            }
        }
    }
    private IEnumerator TransitionToTarget()
    {
        isTransitioning = true;

        SpriteRenderer spriteRenderer = targetTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Target transform does not have a SpriteRenderer component.");
            yield break;
        }

        float spriteHeight = spriteRenderer.bounds.size.y;
        float spriteWidth = spriteRenderer.bounds.size.x;
        float targetSize = Mathf.Max(spriteHeight, spriteWidth * mainCamera.pixelHeight / mainCamera.pixelWidth) / 2f;

        Vector3 targetPosition = targetTransform.position;
        targetPosition.z = mainCamera.transform.position.z;

        bool rippleTriggered = false;
        float smoothing = 5f; // Adjust this factor to change the decay rate

        float timer = 0f;
        while (timer < transitionDuration ||
            Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.001f ||
            Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.001f)
        {
            // Exponential decay behavior: lerp from the current state toward the target
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * smoothing);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * smoothing);

            float t = timer / transitionDuration;
            if (!rippleTriggered && t >= 0.3f)
            {
                cctvEffect.ToggleRipple(transitionDuration / 2.0f, false);
                rippleTriggered = true;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = targetSize;

        isTransitioning = false;
        isZoomedIn = true;
    }


    private IEnumerator TransitionToOriginal()
    {
        isTransitioning = true;

        Vector3 targetPosition = originalPosition;
        float targetSize = originalSize;
        bool rippleTriggered = false;
        float smoothing = 5f; // Same smoothing factor as above

        float timer = 0f;
        while (timer < transitionDuration ||
            Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.001f ||
            Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.001f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * smoothing);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * smoothing);

            float t = timer / transitionDuration;
            if (!rippleTriggered && t >= 0f)
            {
                cctvEffect.ToggleRipple(transitionDuration / 4.0f, true);
                rippleTriggered = true;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = targetSize;

        isTransitioning = false;
        isZoomedIn = false;
    }


    private void StopTransition()
    {
        isTransitioning = false;

        // Reset the camera to the original position and size
        mainCamera.transform.position = originalPosition;
        mainCamera.orthographicSize = originalSize;
        isZoomedIn = false;
    }

    public void ResetCamera()
    {
        mainCamera.transform.position = originalPosition;
        mainCamera.orthographicSize = originalSize;
        isTransitioning = false;
        isZoomedIn = false;
    }
}