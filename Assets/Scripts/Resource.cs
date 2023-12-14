using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Score resourceCollector;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        resourceCollector = GameObject.Find("GM").GetComponent<Score>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected()
    {
        resourceCollector.HamIsCollected();
        audioManager.Play("hamCollectedSound", audioManager.sounds);
    }
}
