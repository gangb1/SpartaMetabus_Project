using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;  //��ֹ� y�� ��ġ ���Ѽ�
    public float lowPosY = -1f;  //��ֹ� y�� ��ġ ���Ѽ�

    //��ֹ� ��ġ ������ ���� ���� ũ��
    public float holeSizeMin = 1f; 
    public float holeSizeMax = 3f;

    //��ֹ� ��ġ ����
    public Transform topObject;
    public Transform bottomObject;

    // ��ֹ� ����
    public float widthPadding = 4f;

    GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameManager.Instance;
    }
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        //���� ũ�⸦ �������� ����(������)
        float holeSize = Random.Range(holeSizeMin,holeSizeMax);
        //���� ũ���� ���� �� 
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3(0, halfHoleSize);  // y�࿡ ���� ũ�� ���� ���� +
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // y�࿡ ���� ũ�� ���� ���� -

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY); // y�� �������� ������ ���̸� �ο�

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
