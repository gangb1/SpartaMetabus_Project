using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{

    [SerializeField] private float interactDistance = 1.5f;             //상호작용 거리 제한 값
    [SerializeField] private string message = "Press [E]";              //ui에 표시할 기본 안내 메시지

    [SerializeField] private UnityEvent onInteract;                     //버튼을 눌렀을 때 실행할 UnityEvent(문 열기, 씬 전환 등)

    //점수 표시 기능 켜기/끄기 옵션
    [Header("Score On/Off")]
    [SerializeField] private bool showScore = false;
    [SerializeField] private string scoreKey = "HighScore";         //playerPrefs에서 저장된 키값을 inspector에서 조정 가능


    private Transform player;               //플레이어 오브젝트 위치 참조
    private bool playerInRange = false;     //현재 트리거 영역 안에 있는지 여부
    private UIController ui;                //ui표시를 위한 컨트롤러 참조

    private void Start()
    {
        ui = UIController.instance;             //uicontroller 싱글턴 인스턴스 저장
    }
    private void Update()
    {
        //플레이어가 범위 안에 있고 E키를 눌렀을 때 
        if(playerInRange && Input.GetKeyDown(KeyCode.E))        
        {

            float distance = Vector2.Distance(transform.position, player.position);         //트리거 오브젝트와 플레이어가 얼마나 떨어져 있는지 계산
            //상호작용 거리보다 실제거리가 더 가까우면
            if(distance <= interactDistance)
            {
                onInteract.Invoke();            //이벤트 실행
                ui.HideText();                  //ui텍스트 숨김
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //트리거에 태그가 player인 오브젝트가 들어오면 실행
        if(collision.CompareTag("Player"))
        {
            player = collision.transform;             //상호작용 거리 계산을 위해 player 위치 저장
            playerInRange = true;                   //상호작용 허용 조건

            string finalMessage = message;          //안내 메시지 구성

            //showScore가 true이고 scoreKey가 null/빈 문자열이 아니라면
            if(showScore && !string.IsNullOrEmpty(scoreKey))
            {
                //playerPrefs에서 점수값을 가져와 메시지에 추가
                int score = PlayerPrefs.GetInt(scoreKey);
                finalMessage += $"\nBestScore: {score}";
            }
            ui.ShowText(finalMessage,showScore);        //ui컨트롤러를 통해 메시지 표시
        }
    }

    //플레이어가 트리거 범위에서 나갔을 때
    private void OnTriggerExit2D(Collider2D other)
    {
            playerInRange = false;  //상호작용 허용 조건 변경
            player = null;      //플레이어 참조 제거
        if(ui != null)      
        ui.HideText();      //메시지 숨김
    }


}
