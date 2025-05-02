using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text interactionText;
    public GameObject interactionPanel;


    public void ShowText(string message)
    {
        interactionText.text = message;
        interactionPanel.SetActive(true);
    }

    public void HideText()
    {
        interactionPanel.SetActive(false);
    }
}
