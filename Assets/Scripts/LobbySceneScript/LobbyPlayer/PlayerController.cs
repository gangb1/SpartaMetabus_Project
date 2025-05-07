using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : BaseController
{
    private Camera cam;              //메인 카메라 참조 변수(마우스 좌표 -> 월드 좌표로 변환할 때 사용)

    protected override void Start()
    {
        base.Start();                   //BaseController의 start() 호출(비어 있지만 상속 구조 유지 목적)
        cam = Camera.main;          //메인 카메라를 찾아서 변수에 저장
    }


    //입력을 받고 movementDirection과 lookDirection을 설정하는 역할
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");      //수평 방향 키 입력을 실시간으로 숫자로 가져온다.
        float vertical = Input.GetAxisRaw("Vertical");          //수직 방향 키 입력을 실시간으로 숫자로 가져온다.
        movementDirection = new Vector2(horizontal, vertical).normalized;        //방향 벡터로 변환하여 이동 방향 저장(normalized때문에 방향만 가져와짐(크기 1로고정))
        if(Mathf.Abs(horizontal) > 0.01f)           //수평 방향 키 입력이 들어왔을때
        {
            lookDirection = new Vector2(horizontal, 0).normalized;
        }



        //마우스로 좌우를 바라볼때
        //Vector2 mounsePosition = Input.mousePosition;               //마우스 위치를 픽셀 좌표로 가져옴
        //Vector2 worldPos = cam.ScreenToWorldPoint(mounsePosition);       //픽셀 좌표를 게임 내 월드 좌표로 변환함
        //lookDirection = (worldPos - (Vector2)transform.position);       //캐릭터 위치 기준으로 마우스 바라보는 방향 계산

        ////마우스가 너무 가까우면 방향 설정 무시
        //if (lookDirection.magnitude < .9f)              //manitude는 벡터의 길이(거리)
        //{
        //    lookDirection = Vector2.zero;               // 방향 무시
        //}
        //else
        //{
        //    lookDirection = lookDirection.normalized;       //방향 벡터를 정규화하여 저장(크기 제거)
        //}
    }
}
