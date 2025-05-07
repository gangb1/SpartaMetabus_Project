using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLobbyCamera : MonoBehaviour
{
    public Transform player;        //ī�޶� ���� �÷��̾��� transform
    public float smoothSpeed = 5f;  //ī�޶� ������ �� �ε巴�� ���󰡴� ����(���� Ŭ���� ������ ���󰡰�, ������ ������ ����)
    //ī�޶� ������ �� �ִ� �ּ�/�ִ� ��ġ ����
    public Vector2 minBounds;       
    public Vector2 maxBounds;

    //ī�޶�� �÷��̾� ������ �Ÿ���ŭ ������ �ְ� �����ϱ� ���� ����
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        //�÷��̾� null üũ
        if(player == null)
        {
            return;
        }
        offset = transform.position - player.position;      //ī�޶�� �÷��̾� ������ �ʱ� �Ÿ��� offset���� ����
    }


    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;             //ī�޶� �̵��ϰ� ���� ��ġ ���
        desiredPosition.z = transform.position.z;                       //ī�޶� z�� ����

        //ī�޶� �� ���� �ȿ����� �����̰� ��
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        //���� ��ġ���� ��ǥ ��ġ���� �ε巴�� �̵�(������ �ð� * ī�޶� �ӵ�)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*smoothSpeed);

        //�÷��̾ ��� ����
        //Vector3 pos = transform.position;
        //pos.x = target.position.x;
        //pos.y = target.position.y;
        //transform.position = pos;


    }
}
