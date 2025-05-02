using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;
    public int numBgCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacle = GameObject.FindObjectsOfType<Obstacle>();   //obstacle이 달려있는 것을 전부 찾아와서 obstacle 배열에 넘겨줌
        obstacleLastPosition = obstacle[0].transform.position;             //마지막 위치는 obstacle의 첫번째위치
        obstacleCount = obstacle.Length;

        for(int i= 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacle[i].SetRandomPlace(obstacleLastPosition,obstacleCount);  //이전 장애물 위치를 기준으로 x축은 widthPadding만큼 밀고(4) y축은 랜덤하게 올리고 새로운 장애물위치를 return해서 다음 장애물의 기준점으로 obstacleLastPosition을 업데이트 한다.

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)   //실제 충돌이 아닌 충돌을 인지시켜줌(trigger)
    {
        Debug.Log("Triggerd: " + collision.name);

        if(collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;      //배경화면의 x축을 구했음
            Vector3 pos = collision.transform.position;                     //pos에 충돌한 오브젝트의 포지션값을 구했어(background)

            pos.x += widthOfBgObject*numBgCount;                            //pos의 x값에 배경화면 x축 값 * 이어놓은 배경화면 수
            collision.transform.position = pos;                             //부딫인 오브젝트의 현재 위치를 pos 값으로 바꿈
            return;
        }

        //트리거 충돌을 했을때 랜덤배치 해주는 명령어
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition,obstacleCount);  
        }

    }
}
