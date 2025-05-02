using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI resultScore;

    // Start is called before the first frame update
    void Start()
    {
        if (GameOverPanel == null)
            Debug.Log("restart text is null");
        if (scoreTxt == null)
            Debug.Log("score text is null");

        GameOverPanel.gameObject.SetActive(false);
    }
    public void SetRestart()
    {
        GameOverPanel.gameObject.SetActive(true);
        scoreTxt.gameObject.SetActive(false);

    }
    public void UpdateScore(int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void ResultScore()
    {
        resultScore.text = scoreTxt.text;
    }
}
