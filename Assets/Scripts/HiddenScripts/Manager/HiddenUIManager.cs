using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HiddenUIState
{ 
    Home,
    Game,
    GameOver
}


public class HiddenUIManager : MonoBehaviour
{
    HiddenHomeUI homeUI;
    HiddenGameUI gameUI;
    HiddenGameOverUI gameOverUI;

    private HiddenUIState currentState;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HiddenHomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<HiddenGameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<HiddenGameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(HiddenUIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(HiddenUIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(HiddenUIState.GameOver);
    }

    public void ChangeWave(int waveIndex)
    {
        gameUI.UpdateWaveText(waveIndex);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP/maxHP);
    }

    public void ChangeState(HiddenUIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}
