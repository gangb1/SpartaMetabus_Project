using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("default tooltip Panel")]
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private TextMeshProUGUI defualtText;

    [Header("Button tooltip Panel")]
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private TextMeshProUGUI ScoreText;

    public static UIController instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowText(string message, bool useScorePanel = false)
    {
        HideText();

        if(useScorePanel )
        {
            ScorePanel.SetActive(true);
            ScoreText.text = message;
        }
        else
        {
            defaultPanel.SetActive(true);
            defualtText.text = message;
        }
    }

    public void HideText()
    {
        defaultPanel.SetActive(false);
        ScorePanel.SetActive(false);
    }

    public bool IsActive()
    {
        return defaultPanel.activeSelf || ScorePanel.activeSelf;
    }
}
