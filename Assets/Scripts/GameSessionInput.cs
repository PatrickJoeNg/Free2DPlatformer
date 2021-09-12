using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSessionInput : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] LevelManager levelManager;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.PausePressed();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            levelManager.ReloadScene();
        }
    }
}
