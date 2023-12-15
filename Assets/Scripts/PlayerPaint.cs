using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here
    public GameObject magicianPainty;
    public GameObject movingPainty;

    public float initialPlatformHeight = 2.0f;
    public float maxPlatformWidth = 5.0f;

    private bool isCreatingPlatform;
    private Vector2 platformStartPosition;
    private GameObject currentPlatform;
    public AudioManager audioManager;

    private bool isDrawModeActive = false;

    public GreyScaleToggle greyScaleToggle;

    public Camera cam;

    //public ParticleSystem makeIce;

    private void Start()
    {
        greyScaleToggle = GameObject.Find("GreyScaleShift").GetComponent<GreyScaleToggle>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Debug.Log("Found greyscalestoggle");

        // Initial setup: Hide one object and show the other
        if (magicianPainty != null) magicianPainty.SetActive(false);
        if (movingPainty != null) movingPainty.SetActive(true);
    }

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

  


    void Update()
    {
        HandleInput();
        //makeIce.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            magicianPainty.SetActive(Time.timeScale == 0);
            movingPainty.SetActive(Time.timeScale != 0);
            ToggleDrawMode();
        }

        if (isDrawModeActive)
        {
            
            // Handle input related to drawing platforms
            if (Input.GetMouseButtonDown(0))
            {
                StartCreatingPlatform();
                //makeIce.Play();
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCreatingPlatform();
                //makeIce.Stop();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Clicked on mousebutton 1");
                ErasePlatform();
            }

            if (isCreatingPlatform)
            {
                UpdatePlatformSize();
            }
        }
    }

    void ToggleDrawMode()
    {
        isDrawModeActive = !isDrawModeActive;

        // Set the time scale to 0 when draw mode is active
        Time.timeScale = isDrawModeActive ? 0 : 1;

        // Additional logic when draw mode is toggled
        if (isDrawModeActive)
        {
            // For example, you can show UI or perform other actions
            greyScaleToggle.PrepareInterpolation(-100, 30);
            audioManager.Play("paintmodeTransitionSound", audioManager.sounds);
            if (magicianPainty != null) magicianPainty.SetActive(Time.timeScale == 0);
            if (movingPainty != null) movingPainty.SetActive(Time.timeScale != 0);
        }
        else
        {
            // Reset any state or perform actions when draw mode is turned off
            greyScaleToggle.PrepareInterpolation(0, 0);
            if (magicianPainty != null) magicianPainty.SetActive(Time.timeScale == 0);
            if (movingPainty != null) movingPainty.SetActive(Time.timeScale != 0);

        }
    }

    void StartCreatingPlatform()
    {
        isCreatingPlatform = true;
        platformStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlatform = Instantiate(platformPrefab, new Vector3(platformStartPosition.x, platformStartPosition.y, initialPlatformHeight), Quaternion.identity);
        audioManager.Play("drawingSound", audioManager.sounds);

    }

    void StopCreatingPlatform()
    {
        isCreatingPlatform = false;
    }

    void UpdatePlatformSize()
    {
        if (currentPlatform != null)
        {
            //test1
            /*Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float platformWidth = Mathf.Clamp(Mathf.Abs(currentMousePosition.x - platformStartPosition.x), 0f, maxPlatformWidth);
            float platformHeight = initialPlatformHeight; //keep the height constant and modify in the inspector

            currentPlatform.transform.localScale = new Vector3(platformWidth, platformHeight, 1f);*/

            //working
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float platformWidth = Mathf.Clamp(Mathf.Abs(currentMousePosition.x - platformStartPosition.x), 0f, maxPlatformWidth);

            // Determine the direction of mouse movement
            float direction = currentMousePosition.x > platformStartPosition.x ? 1 : -1;

            // Set the new position and scale of the platform
            currentPlatform.transform.position = new Vector3(platformStartPosition.x + (platformWidth / 2 * direction), platformStartPosition.y, initialPlatformHeight);
            currentPlatform.transform.localScale = new Vector3(platformWidth, initialPlatformHeight, 1f);

        }
    }

    void ErasePlatform()
    {
        audioManager.Play("eraseSound", audioManager.sounds);
        Debug.Log("Erasing platform");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("PaintedIce"))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
