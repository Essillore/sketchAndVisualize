using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public LevelManager levelManager;
    private int currentLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentLevel = levelManager.CurrentLevel();
            levelManager.ChangeLevel(currentLevel + 1);
        }
    }
}
