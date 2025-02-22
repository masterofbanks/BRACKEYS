using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CCTVRippleEffect : MonoBehaviour
{
    public Material cctvMaterial;
    [Range(0, 1)] public float effectStrength = 1.0f;
    [Range(0, 1)] public float rippleProgress = 0.0f;
    public float rippleSpeed = 1.0f;
    public float rippleSize = 0.5f;
    private bool isRippling = false;
    private bool isRippleReversed = false;

    private const float maxRippleProgress = 0.9f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (cctvMaterial != null)
        {
            cctvMaterial.SetFloat("_EffectStrength", effectStrength);
            cctvMaterial.SetFloat("_RippleProgress", rippleProgress);
            cctvMaterial.SetFloat("_RippleSize", rippleSize);
            Graphics.Blit(src, dest, cctvMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    public void ToggleRipple(float duration, bool reverse)
    {
        if (isRippling)
        {
            StopAllCoroutines();
            StartCoroutine(RippleCoroutine(duration, reverse));
        }
        else
        {
            StartCoroutine(RippleCoroutine(duration, reverse));
        }
    }

    private IEnumerator RippleCoroutine(float duration, bool reverse)
    {
        isRippling = true;
        isRippleReversed = reverse;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            rippleProgress = reverse ? Mathf.Clamp(0.0f + (1.0f - (time / duration)) * (maxRippleProgress - 0.0f), 0.0f, maxRippleProgress) 
                                     : Mathf.Clamp(0.0f + (time / duration) * (maxRippleProgress - 0.0f), 0.0f, maxRippleProgress);
            yield return null;
        }

        rippleProgress = reverse ? 0.0f : maxRippleProgress;
        isRippling = false;
    }
}
