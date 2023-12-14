using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public LevelManager levelManager;
    public GreyScaleToggle greyToggle;
    private int currentLevel;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Awake()
    {
        greyToggle = GameObject.Find("GreyScaleShift").GetComponent<GreyScaleToggle>();
        greyToggle.FindCamera();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentLevel = levelManager.CurrentLevel();
            levelManager.ChangeLevel(currentLevel + 1);
        }
    }


}
