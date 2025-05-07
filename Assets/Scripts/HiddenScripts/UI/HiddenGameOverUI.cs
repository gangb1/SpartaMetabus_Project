using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenGameOverUI : HiddenBaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public override void Init(HiddenUIManager uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    protected override HiddenUIState GetUIState()
    {
        return HiddenUIState.GameOver;
    }
}
