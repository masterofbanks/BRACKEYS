using System;
using System.Collections;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetTransform; // The transform of the UI Image or Sprite Renderer
    public float transitionDuration = 4.0f; // Duration of the transition
    public CCTVRippleEffect cctvEffect;
    private Vector3 originalPosition;
    private float originalSize;
    public bool isTransitioning = false; // Flag to track the transition state
    public bool isZoomedIn = false; // Flag to track if the camera is zoomed in

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

    // Record starting values.
    Vector3 startPosition = mainCamera.transform.position;
    float startSize = mainCamera.orthographicSize;

    bool rippleTriggered = false;
    float alpha = 3f; // Adjust to control the exponential curve.
    float timer = 0f;
    while (timer < transitionDuration)
    {
        // Compute a normalized exponential ease-out factor.
        float factor = (1 - Mathf.Exp(-alpha * timer)) / (1 - Mathf.Exp(-alpha * transitionDuration));

        // Interpolate using our factor.
        mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, factor);
        mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, factor);

        float tNormalized = timer / transitionDuration;
        if (!rippleTriggered && tNormalized >= 0.5f)
        {
            cctvEffect.ToggleRipple(transitionDuration, false);
            rippleTriggered = true;
        }

        timer += Time.deltaTime;
        yield return null;
    }

    // Snap to target values at the end.
    mainCamera.transform.position = targetPosition;
    mainCamera.orthographicSize = targetSize;
    isTransitioning = false;
    isZoomedIn = true;
}

private IEnumerator TransitionToOriginal()
{
    isTransitioning = true;

    // Record starting values.
    Vector3 startPosition = mainCamera.transform.position;
    float startSize = mainCamera.orthographicSize;
    Vector3 targetPosition = originalPosition;
    float targetSize = originalSize;

    bool rippleTriggered = false;
    float alpha = 3f; // Use same exponent factor.
    float timer = 0f;
    while (timer < transitionDuration)
    {
        float factor = (1 - Mathf.Exp(-alpha * timer)) / (1 - Mathf.Exp(-alpha * transitionDuration));
        mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, factor);
        mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, factor);

        float tNormalized = timer / transitionDuration;
        if (!rippleTriggered && tNormalized >= 0.5f)
        {
            cctvEffect.ToggleRipple(transitionDuration, true);
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


}
