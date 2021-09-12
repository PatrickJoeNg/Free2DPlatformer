using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzoneController : MonoBehaviour
{
    //cached
    LevelManager levelManager;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        GameManager.instance.GameOver();
    }
}
