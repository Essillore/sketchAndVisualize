using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMelt : MonoBehaviour
{
    public float meltDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, meltDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
