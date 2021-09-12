using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PauseMenu : MonoBehaviour
{

    enum HiddenState { Found, NotFound };

    // Parameters
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI collectableText;
    public GameObject headsUpDisplay;
    public TextMeshProUGUI hiddenAreaText;
    [SerializeField] LevelManager levelManager;
    public void PausePressed()
    {
        UpdateMenu();

        if (GameIsPaused)
        {        
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        headsUpDisplay.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        headsUpDisplay.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {      
        Time.timeScale = 1f;
        GameIsPaused = false;
        levelManager.LoadMainMenu();
    }

    public void UpdateMenu()
    { 
      // Meta
        collectableText.text = $"{GameManager.instance.itemsCollected.ToString()} / " +
            $"{GameManager.instance.collectablePickups.Count}";

        // HiddenhiddenAreaText\
        switch (GameManager.instance.foundHiddenArea)
        {
            case true:
                hiddenAreaText.color = Color.green;
                hiddenAreaText.text = $"Found";
                break;
            case false:
            default:
                hiddenAreaText.color = Color.red;
                hiddenAreaText.text = $"Not Found";
                break;
        }


    }
}
