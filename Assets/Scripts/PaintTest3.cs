using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTest3 : MonoBehaviour
{
    public GameObject platformTilePrefab; // Drag your platform tile prefab here in the Unity Editor
    public float tileSpacing = 0.1f; // Adjust this value to set the spacing between tiles

    private bool isPainting;
    private GameObject lastTile;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPainting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopPainting();
        }

        if (isPainting)
        {
            PaintPlatform();
        }
    }

    void StartPainting()
    {
        isPainting = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastTile = Instantiate(platformTilePrefab, mousePosition, Quaternion.identity);
    }

    void StopPainting()
    {
        isPainting = false;
        lastTile = null;
    }

    void PaintPlatform()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mousePosition, lastTile.transform.position) > tileSpacing)
        {
            lastTile = Instantiate(platformTilePrefab, mousePosition, Quaternion.identity);
        }
    }
}
