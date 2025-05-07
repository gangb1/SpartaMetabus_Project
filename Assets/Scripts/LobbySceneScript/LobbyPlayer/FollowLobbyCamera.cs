using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLobbyCamera : MonoBehaviour
{
    public Transform player;        //카메라가 따라갈 플레이어의 transform
    public float smoothSpeed = 5f;  //카메라가 움직일 때 부드럽게 따라가는 정도(값이 클수록 빠르게 따라가고, 작으면 느리게 따라감)
    //카메라가 움직일 수 있는 최소/최대 위치 범위
    public Vector2 minBounds;       
    public Vector2 maxBounds;

    //카메라와 플레이어 사이의 거리만큼 떨어져 있게 유지하기 위한 변수
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        //플레이어 null 체크
        if(player == null)
        {
            return;
        }
        offset = transform.position - player.position;      //카메라와 플레이어 사이의 초기 거리를 offset으로 저장
    }


    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;             //카메라가 이동하고 싶은 위치 계산
        desiredPosition.z = transform.position.z;                       //카메라 z값 고정

        //카메라 맵 범위 안에서만 움직이게 함
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        //현재 위치에서 목표 위치까지 부드럽게 이동(프레임 시간 * 카메라 속도)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*smoothSpeed);

        //플레이어를 즉시 따라감
        //Vector3 pos = transform.position;
        //pos.x = target.position.x;
        //pos.y = target.position.y;
        //transform.position = pos;


    }
}
