using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GreyScale : MonoBehaviour
{
    public Volume volume; // Assign this in the inspector

    void Update()
    {
        // Check if Ctrl key is pressed
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y pressed");
            GreyScene();
        }
    }

    void GreyScene()
    {
        if (volume == null)
        {
            Debug.LogError("Post Processing Volume is not assigned.");
            return;
        }

        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.Log("Adjusting saturation and contrast");
            colorAdjustments.saturation.value = -100; // Set saturation to -100
            colorAdjustments.contrast.value = 30;     // Set contrast to 30
        }
        else
        {
            Debug.LogError("ColorAdjustments component not found in the volume's profile.");
        }
    }
    void ColouredScene()
    {
        if (volume == null)
        {
            Debug.LogError("Post Processing Volume is not assigned.");
            return;
        }

        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.Log("Adjusting saturation and contrast");
            colorAdjustments.saturation.value = 0; // Set saturation to 0
            colorAdjustments.contrast.value = 0;     // Set contrast to 0
        }
        else
        {
            Debug.LogError("ColorAdjustments component not found in the volume's profile.");
        }
    }

}