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

   //애니메이션 제어 클래스 
    protected AnimationHandler animationHandler;
    //실제 저장된 최고 점수를 로드 후에 저장해둘 변수들
    private int PlainBestScore;
    private int StackBestScore;
    //리더보드가 열려있는지 확인용 bool값
    private bool isActive;

    private void Awake()
    {
        //컴포넌트 참조
        animationHandler = GetComponent<AnimationHandler>();


    }
    public void ShowLeaderBoard()
    {
        //이미 열려있으면 닫기
        if(isActive)
        {
            CloseLeaderBoard();
        }
        //열려있지 않으면
        else
        {   //최고점수 불러옴
            PlainBestScore = PlayerPrefs.GetInt("HighScore");
            StackBestScore = PlayerPrefs.GetInt("BestScore");

            //텍스트 ui에 점수 표시
            PlainBestScoreTxt.text = PlainBestScore.ToString();
            StackBestScoreTxt.text = StackBestScore.ToString();

            //canvas 활성화
            canvas.gameObject.SetActive(true);
            animationHandler.Active();      //애니메이션 재생
            isActive = true;                //상태 불값 true 변경
        }
    }

    //리더보드 닫기
    public void CloseLeaderBoard()
    {
        canvas.gameObject.SetActive(false);         //canvas 비활성화
        animationHandler.DeActive();                //애니메이션 실행(열려진 것 닫는 애니메이션)
        isActive = false;                           //상태 불값 리셋
    }





}
