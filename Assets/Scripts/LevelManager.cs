using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Starts game based on next scene
    // in scene index
    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Quits entire application
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadNextLevel()
    {
        Scene currentSceneIndex = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentSceneIndex.buildIndex + 1);
    }

    public void LoadSelectedLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
