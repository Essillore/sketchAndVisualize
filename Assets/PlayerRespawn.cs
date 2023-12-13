using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private GameObject player;
    private Checkpoint lastCheckpoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.GetComponent<Checkpoint>();
        }
        else if (other.CompareTag("Deathzone"))
        {
            RespawnPlayer();
        }
    }

    void Update()
    {
        // For testing purposes, respawn the player when the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        if (lastCheckpoint != null && player != null)
        {
            // Respawn the player at the last checkpoint
            lastCheckpoint.RespawnPlayer(player);
        }
        else
        {
            Debug.LogWarning("No checkpoint set. Respawning at the initial spawn point.");
            // You may want to respawn the player at a default spawn point if no checkpoint is set
            player.transform.position = Vector3.zero;
        }
    }
}
