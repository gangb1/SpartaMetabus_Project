using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }  // ΩÃ±€≈Ê ∆–≈œ ±‚∫ª
    [SerializeField] private GameObject introPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button LobbyButton;


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
        uimanager.UpdateScore(0);
        uimanager.scoreTxt.gameObject.SetActive(false);
        Time.timeScale = 0f;
        introPanel.SetActive(true);
        startButton.onClick.AddListener(StartMiniGame);
        
    }

    public void StartMiniGame()
    {
        introPanel.SetActive(false);
        uimanager.scoreTxt.gameObject.SetActive(true);
        Time.timeScale = 1f;

    }
    public void GameOver()
    {
        Debug.Log("Game Over");
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
    }
}
