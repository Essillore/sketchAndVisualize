using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GreyScaleToggle : MonoBehaviour
{
    public Volume volume; // Assign this in the inspector

    private float interpolationDuration = 0.5f; // Duration of the interpolation
    private float timer = 0; // Timer to track interpolation progress
    private bool isInterpolating = false; // Flag to track if we're interpolating

    private float targetSaturation;
    private float targetContrast;
    private float startSaturation;
    private float startContrast;

    private void Awake()
    {
    }

    void Update()
    {
        // Check if Y key is pressed for greyscale
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y pressed - switching to greyscale");
            PrepareInterpolation(-100, 30); // Prepare to interpolate to greyscale
        }

        // Check if U key is pressed for colored
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U pressed - switching to colored");
            PrepareInterpolation(0, 0); // Prepare to interpolate to colored
        }

        if (isInterpolating)
        {
            PerformInterpolation();
        }
    }

    public void FindCamera()
    {
        volume = GameObject.FindWithTag("MainCamera").GetComponent<Volume>();

    }


    public void PrepareInterpolation(float saturation, float contrast)
    {
        if (volume == null || !volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
        {
            Debug.LogError("Post Processing Volume is not assigned or ColorAdjustments not found.");
            return;
        }

        startSaturation = colorAdjustments.saturation.value;
        startContrast = colorAdjustments.contrast.value;
        targetSaturation = saturation;
        targetContrast = contrast;

        timer = 0; // Reset the timer
        isInterpolating = true; // Start interpolating
    }

    public void PerformInterpolation()
    {
        if (volume == null || !volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
        {
            Debug.LogError("Post Processing Volume is not assigned or ColorAdjustments not found.");
            isInterpolating = false;
            return;
        }

        timer += Time.unscaledDeltaTime; // Increment the timer by the unscaled time elapsed since last frame
        float saturation = Mathf.Lerp(startSaturation, targetSaturation, timer / interpolationDuration);
        float contrast = Mathf.Lerp(startContrast, targetContrast, timer / interpolationDuration);

        colorAdjustments.saturation.value = saturation;
        colorAdjustments.contrast.value = contrast;

        // Check if the interpolation is complete
        if (timer >= interpolationDuration)
        {
            isInterpolating = false; // Stop interpolating
        }
    }
}

