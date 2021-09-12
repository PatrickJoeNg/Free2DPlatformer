using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // creating instance
    public static GameManager instance;

    // Logic
    public int itemsCollected;
    public List<ItemPickup> collectablePickups = new List<ItemPickup>();
    public PlayerController player;
    public GameObject spawnPoint;
    public int maxTimeRemaining;
    public float levelTimer;
    public bool  foundHiddenArea = false;

    // state
    private bool canEndLevel = false;

    //cached 
    [SerializeField] GameOverScreen gameOverScreen;
    

    private void Awake()
    {       
        //if (GameManager.instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        player.transform.position = spawnPoint.transform.position;      
        itemsCollected = 0;
        levelTimer = maxTimeRemaining;

        GetCollectablesInScene();
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        levelTimer -= Time.deltaTime;

        if (levelTimer <= 0)
        {
            levelTimer = 0;
            GameOver();
        }
    }

    void GetCollectablesInScene()
    {
        collectablePickups.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag(TagManager.COLLECTABLE_TAG);

        foreach (Transform child in parent.transform)
        {
            ItemPickup itemPickup = child.GetComponent<ItemPickup>();

            if (itemPickup != null)
            {
                collectablePickups.Add(itemPickup);
            }
        }        
    }

    public bool CheckIfAllCollected()
    {
        canEndLevel = itemsCollected == collectablePickups.Count;

        if (canEndLevel)
        {
            Debug.Log("Player can go to next level");
            return true;
        }
        if (!canEndLevel)
        {
            Debug.Log("can't not end yet");
        }
        return false;
    }

    public void GameOver()
    {
        gameOverScreen.SetupGameOverScreen();
        Debug.Log("End Game");
    }   
}
