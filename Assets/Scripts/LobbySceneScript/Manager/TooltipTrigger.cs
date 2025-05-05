using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI scoreText;
    public string scoreKey = "MiniGame1_HighScore";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            tooltipPanel.SetActive(true);
            int highScore = PlayerPrefs.GetInt("HighScore");
            scoreText.text = $"BestScore: {highScore}";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            tooltipPanel.SetActive(false);
        }
    }
}
