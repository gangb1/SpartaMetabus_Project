using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiddenHomeUI : HiddenBaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button ExitButton;

    public override void Init(HiddenUIManager uiManager)
    {
        base.Init(uiManager);

        startButton.onClick.AddListener(OnClickStartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        HiddenGameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    protected override HiddenUIState GetUIState()
    {
        return HiddenUIState.Home;
    }
}
