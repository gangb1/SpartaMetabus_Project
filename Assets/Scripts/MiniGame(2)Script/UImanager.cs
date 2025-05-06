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
        instance = this;            //�̱��� �ν��Ͻ� �Ҵ�

        theStack = FindObjectOfType<TheStack>();

        // HomeUI, GameUI, ScroeUI ������Ʈ�� �������� ã�Ƽ� �����ϰ� �ʱ�ȭ
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);     // ���⼭ HomeUI.Init ȣ���

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        //Ȩ UI�� �ʱ���� ����
        ChangeState(UIState.Home);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;

        //�� UI�� ���� ���� ���� -> ���°� �´� UI�� Ȱ��ȭ
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
