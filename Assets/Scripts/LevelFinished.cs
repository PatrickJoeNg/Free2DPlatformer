using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    //cached
    LevelManager levelManager;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool goToNextLevel = GameManager.instance.CheckIfAllCollected();

        if (goToNextLevel)
        {
            levelManager.LoadNextLevel();
        }

    }
}
