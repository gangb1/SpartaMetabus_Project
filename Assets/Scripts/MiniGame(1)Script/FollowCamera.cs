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
        if(target == null)              //target(player)�� ������ �ٽ� ��ȯ
        {
            return;
        }
        offsetX = transform.position.x - target.position.x;  //ī�޶� Ÿ���� ���� �Ÿ��� �����ϸ� ���󰡰Բ� �ϱ� ���� ��
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 pos = transform.position;               //�������� �����ö� ������ ���� �� ��������(�׷��߸� ��ǥ�ϳ��� ������ ������ų �� ����)
        pos.x = target.position.x + offsetX;            //Ÿ�� ��ġ���� offsetx ��ŭ ���ؼ� ���ο� x ��ġ�� �����
        transform.position = pos;                       //���� ��ġ�� �̵�
    }   
}
