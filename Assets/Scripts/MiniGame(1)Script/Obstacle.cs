using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;  //장애물 y축 위치 상한선
    public float lowPosY = -1f;  //장애물 y축 위치 하한선

    //장애물 위치 기준을 위한 구멍 크기
    public float holeSizeMin = 1f; 
    public float holeSizeMax = 3f;

    //장애물 위치 변수
    public Transform topObject;
    public Transform bottomObject;

    // 장애물 간격
    public float widthPadding = 4f;

    GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameManager.Instance;
    }
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        //구멍 크기를 랜덤으로 정함(형식적)
        float holeSize = Random.Range(holeSizeMin,holeSizeMax);
        //구멍 크기의 절반 값 
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3(0, halfHoleSize);  // y축에 구멍 크기 절반 값을 +
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // y축에 구멍 크기 절반 값을 -

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY); // y축 방향으로 랜덤한 높이를 부여

        transform.position = placePosition;      

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player playert = collision.GetComponent<Player>();
        if (playert != null)
            gamemanager.AddScore(1);
    }
}
