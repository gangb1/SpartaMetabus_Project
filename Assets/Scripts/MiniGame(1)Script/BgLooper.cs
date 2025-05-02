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
        Obstacle[] obstacle = GameObject.FindObjectsOfType<Obstacle>();   //obstacle�� �޷��ִ� ���� ���� ã�ƿͼ� obstacle �迭�� �Ѱ���
        obstacleLastPosition = obstacle[0].transform.position;             //������ ��ġ�� obstacle�� ù��°��ġ
        obstacleCount = obstacle.Length;

        for(int i= 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacle[i].SetRandomPlace(obstacleLastPosition,obstacleCount);  //���� ��ֹ� ��ġ�� �������� x���� widthPadding��ŭ �а�(4) y���� �����ϰ� �ø��� ���ο� ��ֹ���ġ�� return�ؼ� ���� ��ֹ��� ���������� obstacleLastPosition�� ������Ʈ �Ѵ�.

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)   //���� �浹�� �ƴ� �浹�� ����������(trigger)
    {
        Debug.Log("Triggerd: " + collision.name);

        if(collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;      //���ȭ���� x���� ������
            Vector3 pos = collision.transform.position;                     //pos�� �浹�� ������Ʈ�� �����ǰ��� ���߾�(background)

            pos.x += widthOfBgObject*numBgCount;                            //pos�� x���� ���ȭ�� x�� �� * �̾���� ���ȭ�� ��
            collision.transform.position = pos;                             //�΋H�� ������Ʈ�� ���� ��ġ�� pos ������ �ٲ�
            return;
        }

        //Ʈ���� �浹�� ������ ������ġ ���ִ� ��ɾ�
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition,obstacleCount);  
        }

    }
}
