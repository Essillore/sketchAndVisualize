using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Score resourceCollector;

    // Start is called before the first frame update
    void Start()
    {
        resourceCollector = GameObject.Find("ScoreObject").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected()
    {
        resourceCollector.HamIsCollected();
    }
}
