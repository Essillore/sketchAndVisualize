using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here
    public float initialPlatformHeight = 2.0f;
    public float maxPlatformWidth = 5.0f;

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

        if (Input.GetMouseButtonDown(1))
        {
            ErasePlatform();
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
        currentPlatform = Instantiate(platformPrefab, new Vector3(platformStartPosition.x, platformStartPosition.y, initialPlatformHeight), Quaternion.identity);
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
            float platformWidth = Mathf.Clamp(Mathf.Abs(currentMousePosition.x - platformStartPosition.x), 0f, maxPlatformWidth);
            float platformHeight = initialPlatformHeight; //keep the height constant and modify in the inspector

            currentPlatform.transform.localScale = new Vector3(platformWidth, platformHeight, 1f);
        }
    }

    void ErasePlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("PaintedIce"))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
