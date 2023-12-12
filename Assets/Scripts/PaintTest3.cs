using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTest3 : MonoBehaviour
{
    public GameObject platformTilePrefab; // Drag your Prefab here
    public float tileSpacing = 0.1f; // Adjust this value to set the spacing between tiles

    private bool isPainting;
    private bool isErasing;
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

        if (Input.GetMouseButtonDown(1))
        {
            StartErasing();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopErasing();
        }

        if (isPainting)
        {
            PaintPlatform();
        }

        else if (isErasing)
        {
            ErasePlatform();
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

    void StartErasing()
    {
        isErasing = true;
    }

    void StopErasing()
    {
        isErasing = false;
    }

    void PaintPlatform()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mousePosition, lastTile.transform.position) > tileSpacing)
        {
            lastTile = Instantiate(platformTilePrefab, mousePosition, Quaternion.identity);
        }
    }

    void ErasePlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
