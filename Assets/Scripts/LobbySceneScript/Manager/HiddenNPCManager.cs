using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HiddenNPCManager : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(OnClickHiddenButton);
        noButton.onClick.AddListener(OnClickQuitButton);
    }

    public void OnClickHiddenButton()
    {
        SceneManager.LoadScene("HiddenGameScene");
    }

    public void OnClickQuitButton()
    {
        canvas.SetActive(false);
    }

    public void TalkNPC()
    {
        canvas.SetActive(true);
    }
}
