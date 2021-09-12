using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //cached
    BoxCollider2D itemCollider;
    [SerializeField] bool collected;

    private void Start()
    {
        itemCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            // if item hasn't been collected it
            // will let the player collect it
            if (!collected)
            {
                OnCollect();
            }      
        }
    }

    public void OnCollect()
    {
        int itemCollected = GameManager.instance.itemsCollected++;
        int totalCollected = GameManager.instance.collectablePickups.Count;

        collected = true;
        gameObject.SetActive(false);
    }
}
