using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public enum UIState
{ 
    Start,
    Main,
    Home,
    Game,
    Score,

}


public class UImanager : MonoBehaviour
{
   static UImanager instance;

    public static UImanager Instance
    {
        get { return instance; }
    }

    
    UIState currentState = UIState.Home;
    HomeUI homeUI = null;
    GameUI gameUI = null;
    ScoreUI scoreUI = null;
    StartSceneStartUI startUI = null;
    //MainUI mainUI = null;

    TheStack theStack = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;

        theStack =FindObjectOfType<TheStack>();

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        startUI = GetComponentInChildren<StartSceneStartUI>(true);
        startUI?.Init(this);

        //MainUI mainUI = GetComponentInChildrenS<MainSceneUI>(true);
        //mainUI?.Init(this);
        //ChangeState(UIState.Home);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
        startUI?.SetActive(currentState);
        //mainUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(UIState.Game);
    }

    public void OncClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.combo, theStack.MaxCombo);
    }

    public void SetScoreUI()
    {
        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);
        ChangeState(UIState.Score);
    }

    public void OnClickMainGame()
    {
        Debug.Log("눌리고 있어");
        startUI?.gameObject.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    
}
