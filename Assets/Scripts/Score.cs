using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public Score resourcesCollected;
    public int hamCollectedNumber;
    public TMP_Text resourcesCollectedUI;
    // Start is called before the first frame update
    void Start()
    {
        FindResourceUI();
        resourcesCollected = this.GetComponent<Score>();
    }

    public void FindResourceUI()
    {
        resourcesCollectedUI = GameObject.Find("ResourceUI").GetComponent<TMP_Text>();
    }

    public void HamIsCollected()
    {
        hamCollectedNumber++;
        HamCollectedUIUpdate();
    }

    private void HamCollectedUIUpdate()
    {
        if (resourcesCollectedUI != null)
        {
        resourcesCollectedUI.text = " " + hamCollectedNumber;
        }
    }

    public void ResetHamCollected()
    {
        hamCollectedNumber = 0;
    }

}
