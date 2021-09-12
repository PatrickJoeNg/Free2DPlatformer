using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HeadsUpDisplay : MonoBehaviour
{
    public TextMeshProUGUI collectableText;
    public TextMeshProUGUI timerText;
    [SerializeField] Animator finishedAnimator;
    [SerializeField] GameObject finishedPopup;
    
    private void Update()
    {
        UpdateHeadsUpDisplay();
    }

    private void UpdateHeadsUpDisplay()
    {
        int itemsCollected = GameManager.instance.itemsCollected;
        int totalCollected = GameManager.instance.collectablePickups.Count;

        float levelTimer = GameManager.instance.levelTimer;

        // check for collectables found
        collectableText.text = $"{itemsCollected.ToString()} " +
            $"/ {totalCollected}";

        // Set timer text
        timerText.text = $"Time: {Mathf.RoundToInt(levelTimer).ToString()}";

        if (levelTimer <=0)
        {
            timerText.color = Color.red;
        }

        //  if all collectabled the
        //  text will change color and play animation
        if (itemsCollected == totalCollected)
        {
            finishedPopup.SetActive(true);
            collectableText.color = Color.green;
            PlayLevelFinishedPopUp();
        }
        
    }

    private void PlayLevelFinishedPopUp()
    {
        finishedAnimator.SetTrigger(TagManager.LEVELFINISHED_ANIMATION_NAME);
    }
}
