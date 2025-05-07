using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    [SerializeField] private Sprite openDoorSprite;     //문이 열렸을 때 보여줄 스프라이트 이미지
    [SerializeField] private Sprite closeDoorSprite;    //문이 닫혔을 때 보여줄 스프라이트 이미지
    [SerializeField] private bool isOpen = false;       //초기 상태에서 열려 있는지 여부
    [SerializeField] private Transform player;          //플레이어 위치 참조

    private BoxCollider2D boxCollider;                  //충돌 처리(닫힌 상태일때만 충돌 활성화)
    private SpriteRenderer spriteRenderer;              //문 스프라이트를 표시


    //컴포넌트 참조 저장
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();                
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 null 방어(인스펙터에 할당 안되면 출력)
        if (player == null)
        {
            Debug.LogWarning("Player가 할당되지 않아 자동으로 태그로 검색합니다.");
            player = GameObject.FindWithTag("Player")?.transform;
        }

        SetDoorState(isOpen);           //isOpen에 따라 문 상태 결정
        UpdateSortingOrder();           //정렬 순서 설정
    }

    //public void Interact()
    //{
    //    ToggleDoor();
    //}

    //문 열기/닫기 토글
    public void ToggleDoor()
    {
        isOpen = !isOpen;           //현재 상태 반전
        SetDoorState(isOpen);       //변경된 상태를 기준으로 문 설정 반영
    }

    //문 상태 설정
    private void SetDoorState(bool open)
    {   
        //문이 열릴때 문 열림 스프라이트를 띄우고 콜라이더를 끔
        boxCollider.enabled = !open;
        spriteRenderer.sprite = open ? openDoorSprite: closeDoorSprite;

        //문이 열릴때 sortingOrder 0 고정(order in layer를 가장 낮게 함)
        if(open)
        {
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            UpdateSortingOrder();       //닫힐 땐 플레이어와 위치 비교 후 정렬 순서 결정
        }
    }

    private void UpdateSortingOrder()
    {

        if (player == null) return;
        //문이 플레이어보다 아래에 있으면 플레이어 뒤로, 위에 있으면 앞으로 감
        spriteRenderer.sortingOrder = transform.position.y < player.position.y ? 200:0;
    }

}
