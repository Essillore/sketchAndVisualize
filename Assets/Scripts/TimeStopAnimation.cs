using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopAnimation : MonoBehaviour
{
    public Sprite[] frames; // Array of your frames
    public float frameRate = 1.0f; // Frames per second

    private int currentFrame = 0;
    private float lastFrameTime;

    void Start()
    {
        lastFrameTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - lastFrameTime >= 1.0f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            GetComponent<SpriteRenderer>().sprite = frames[currentFrame];
            lastFrameTime = Time.realtimeSinceStartup;
        }
    }
}
