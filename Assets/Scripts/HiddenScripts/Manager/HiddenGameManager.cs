using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGameManager : MonoBehaviour
{
    public static HiddenGameManager Instance;

    public HiddenPlayerController  player {  get; private set; }
    private HiddenResouceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private HiddenEnemyManager enemyManager;

    private HiddenUIManager uiManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<HiddenPlayerController>();
        player.Init(this);

       enemyManager = GetComponentInChildren<HiddenEnemyManager>();
        enemyManager.Init(this);

        uiManager = FindObjectOfType<HiddenUIManager>();


        _playerResourceController = player.GetComponent<HiddenResouceController>();
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

    }

    private void Start()
    {
        if(!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }
    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }
}
