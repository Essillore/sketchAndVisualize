using UnityEngine;
using System.Collections;

public class PlatformMelt : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float meltDelay = 5.0f; // Duration for the platform to be destroyed
    public float opacityFadeDuration = 1.9f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeToTransparent(meltDelay - opacityFadeDuration)); // Start fading 1.9 seconds before destruction
        Destroy(gameObject, meltDelay); // Destroy the object after the meltDelay
    }

    private IEnumerator FadeToTransparent(float fadeStartDelay)
    {
        yield return new WaitForSeconds(fadeStartDelay); // Wait for the fade to start

        float fadeDuration = meltDelay - fadeStartDelay; // Calculate remaining time for fade
        float currentTime = 0;
        Color startColor = spriteRenderer.color;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, currentTime / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0); // Ensure it ends fully transparent
    }
}
