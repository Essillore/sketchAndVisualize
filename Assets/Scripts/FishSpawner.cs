using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Vector3 spawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FishSpawn()
    {
        spawnDistance.x = Random.Range(0, 8);
        spawnDistance.y = Random.Range(0, 8);
        Instantiate(fishPrefab, (transform.position-spawnDistance), Quaternion.identity);
    }


}
