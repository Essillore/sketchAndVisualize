using UnityEngine;
using System.Collections.Generic; // For using List

public class IndependentParticleSystem : MonoBehaviour
{
    private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    private float deltaTime;

    void Start()
    {
        // Find all child ParticleSystem components
        particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    void Update()
    {
        deltaTime += Time.unscaledDeltaTime;

        // Manually simulate each particle system
        foreach (var ps in particleSystems)
        {
            ps.Simulate(deltaTime, withChildren: true, restart: false, fixedTimeStep: true);
        }
        deltaTime = 0;

        // Update position during drag
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = GetMousePositionInWorld();
            transform.position = mousePos;
        }
        // Stop and clear particles when drag ends or timescale is 1
        else if (Input.GetMouseButtonUp(0) || Time.timeScale == 1)
        {
            StopAndClearParticles();
        }
    }

    Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Assuming you are in 2D space
        return mousePos;
    }

    void StopAndClearParticles()
    {
        // Stop emitting particles and clear existing ones from each particle system
        foreach (var ps in particleSystems)
        {
            ps.Pause(); // Pause the particle system
            var emission = ps.emission;
            emission.enabled = false;
            ps.Clear(); // Clear existing particles
        }
    }
}