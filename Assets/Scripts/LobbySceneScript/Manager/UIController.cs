using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    //일반적인 알림 메시지를 보여주는 패널
    [Header("default tooltip Panel")]
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private TextMeshProUGUI defualtText;

    //점수 관련 메시지 패널
    [Header("Button tooltip Panel")]
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private TextMeshProUGUI ScoreText;

    //싱글톤 인스턴스
    public static UIController instance { get; private set; }

    //싱글톤 패턴
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //메시지 표시 함수
    public void ShowText(string message, bool useScorePanel = false)
    {
        HideText();     //모든 UI 비우고 시작

        if(useScorePanel)           //useScorePanel이 true이면 scorePanel에 표시
        {
            ScorePanel.SetActive(true);             
            ScoreText.text = message;
        }
        else                        //false이면 defaultPanel에 표시
        {
            defaultPanel.SetActive(true);
            defualtText.text = message;
        }
    }

    //모든 텍스트 숨기기
    public void HideText()
    {
        defaultPanel.SetActive(false);
        ScorePanel.SetActive(false);
        if (defaultPanel != null) defaultPanel.SetActive(false);
        if (ScorePanel != null) ScorePanel.SetActive(false);
    }

    //ui가 켜져있는지 여부 확인
    public bool IsActive()
    {
        return defaultPanel.activeSelf || ScorePanel.activeSelf;
    }
}
