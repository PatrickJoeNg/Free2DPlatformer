using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI collectableText;

    public void SetupGameOverScreen()
    {
        int remaining = GameManager.instance.collectablePickups.Count - GameManager.instance.itemsCollected;

        gameObject.SetActive(true);
        collectableText.text = $"{remaining.ToString()} Collectables remaining";
    }
}
