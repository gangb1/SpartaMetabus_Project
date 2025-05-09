using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }  // �̱��� ���� �⺻
    [SerializeField] private GameObject introPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button LobbyButton;
    public static bool isFirstLoading = true;

    private int currentScore = 0;
    UIManager uimanager;
    public UIManager UIManager { get { return uimanager; } }

    private void Awake()
    {
        gameManager = this;
        uimanager= FindObjectOfType<UIManager>();

    }

    private void Start()
    {
        if(!isFirstLoading)
        {
            StartMiniGameSkip();
        }
        else
        {
            isFirstLoading = false;

            uimanager.UpdateScore(0);
            uimanager.scoreTxt.gameObject.SetActive(false);
            Time.timeScale = 0f;

            introPanel.SetActive(true);
            startButton.onClick.AddListener(StartMiniGame);
        }

    }
    

    public void StartMiniGame()
    {
        introPanel.SetActive(false);
        uimanager.scoreTxt.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
    public void StartMiniGameSkip()
    {
        introPanel.SetActive(false);
        uimanager.scoreTxt.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        SaveHighScore();
        PlayerPrefs.SetInt("LastScore",currentScore);
        PlayerPrefs.Save();
        uimanager.SetRestart();
        uimanager.ResultScore();
        restartButton.onClick.AddListener(RestartGame);
        LobbyButton.onClick.AddListener(GoLobby);


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log($"Score: {currentScore}");
        uimanager.UpdateScore(currentScore);
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("MainScene");
        isFirstLoading = true;
    }

    public void SaveHighScore()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if(currentScore > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();   
        }
    }


}
