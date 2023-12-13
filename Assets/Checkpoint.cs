using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 spawnPoint;

    void Start()
    {
        // Initialize the spawn point to the initial position of the checkpoint
        spawnPoint = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the spawn point to the position of the checkpoint
            spawnPoint = transform.position;
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        // Reset the player to the stored spawn point
        player.transform.position = spawnPoint;
    }
}
