using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    public GameObject hiddenArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            hiddenArea.SetActive(false);
        }
        GameManager.instance.foundHiddenArea = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hiddenArea.SetActive(true);

    }
}
