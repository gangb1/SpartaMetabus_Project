using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardNpcManager : MonoBehaviour
{
    //점수 텍스트를 표시하는 UI 텍스트 필드
    [SerializeField] private TextMeshProUGUI PlainBestScoreTxt;         
    [SerializeField] private TextMeshProUGUI StackBestScoreTxt;
    //점수 UI 전체를 감싸는 CANVAS
    [SerializeField] private Canvas canvas;

    protected AnimationHandler animationHandler;
    private int PlainBestScore;
    private int StackBestScore;

    private bool isActive;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();


    }
    public void ShowLeaderBoard()
    {
        
        if(isActive)
        {
            CloseLeaderBoard();
        }
        else
        {
            PlainBestScore = PlayerPrefs.GetInt("HighScore");
            StackBestScore = PlayerPrefs.GetInt("BestScore");

            PlainBestScoreTxt.text = PlainBestScore.ToString();
            StackBestScoreTxt.text = StackBestScore.ToString();

            canvas.gameObject.SetActive(true);
            animationHandler.Active();
            isActive = true;
        }
    }

    public void CloseLeaderBoard()
    {
        canvas.gameObject.SetActive(false);
        animationHandler.DeActive();
        isActive = false;
    }





}
