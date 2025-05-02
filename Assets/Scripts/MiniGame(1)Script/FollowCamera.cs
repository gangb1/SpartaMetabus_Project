using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform target;
    float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)              //target(player)가 없으면 다시 반환
        {
            return;
        }
        offsetX = transform.position.x - target.position.x;  //카메라가 타겟을 일정 거리를 유지하며 따라가게끔 하기 위한 값
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 pos = transform.position;               //포지션을 가져올땐 변수에 저장 후 가져오기(그래야만 좌표하나의 값만을 변동시킬 수 있음)
        pos.x = target.position.x + offsetX;            //타겟 위치에서 offsetx 만큼 더해서 새로운 x 위치를 계산함
        transform.position = pos;                       //계산된 위치로 이동
    }   
}
