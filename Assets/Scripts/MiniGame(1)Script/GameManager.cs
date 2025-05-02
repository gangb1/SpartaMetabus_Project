using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }  // ΩÃ±€≈Ê ∆–≈œ ±‚∫ª

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
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        uimanager.SetRestart();
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
}
