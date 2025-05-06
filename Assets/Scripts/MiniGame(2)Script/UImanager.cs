using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


public enum UIState
{
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

    TheStack theStack = null;

    private void Awake()
    {
        instance = this;            //싱글톤 인스턴스 할당

        theStack = FindObjectOfType<TheStack>();

        // HomeUI, GameUI, ScroeUI 오브젝트를 계층에서 찾아서 연결하고 초기화
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);     // 여기서 HomeUI.Init 호출됨

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        //홈 UI로 초기상태 설정
        ChangeState(UIState.Home);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;

        //각 UI에 현재 상태 전달 -> 상태가 맞는 UI만 활성화
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(UIState.Game);
    }

    public void OncClickExit()
    {
        SceneManager.LoadScene("Mainscene");
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


}
