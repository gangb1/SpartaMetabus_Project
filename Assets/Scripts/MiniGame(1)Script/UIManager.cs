using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI restartTxt;
    // Start is called before the first frame update
    void Start()
    {
        if (restartTxt == null)
            Debug.Log("restart text is null");
        if (scoreTxt == null)
            Debug.Log("score text is null");

        restartTxt.gameObject.SetActive(false);
    }
    public void SetRestart()
    {
        restartTxt.gameObject.SetActive(true);

    }
    public void UpdateScore(int score)
    {
        scoreTxt.text = score.ToString();
    }
}
