using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here

    private bool isCreatingPlatform;
    private Vector2 platformStartPosition;
    private GameObject currentPlatform;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCreatingPlatform();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCreatingPlatform();
        }

        if (isCreatingPlatform)
        {
            UpdatePlatformSize();
        }
    }

    void StartCreatingPlatform()
    {
        isCreatingPlatform = true;
        platformStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlatform = Instantiate(platformPrefab, platformStartPosition, Quaternion.identity);
    }

    void StopCreatingPlatform()
    {
        isCreatingPlatform = false;
    }

    void UpdatePlatformSize()
    {
        if (currentPlatform != null)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float platformWidth = Mathf.Abs(currentMousePosition.x - platformStartPosition.x);
            float platformHeight = Mathf.Abs(currentMousePosition.y - platformStartPosition.y);

            currentPlatform.transform.localScale = new Vector3(platformWidth, platformHeight, 1f);
        }
    }
}