using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : BaseController
{
    private Camera cam;              //���� ī�޶� ���� ����(���콺 ��ǥ -> ���� ��ǥ�� ��ȯ�� �� ���)

    protected override void Start()
    {
        base.Start();                   //BaseController�� start() ȣ��(��� ������ ��� ���� ���� ����)
        cam = Camera.main;          //���� ī�޶� ã�Ƽ� ������ ����
    }


    //�Է��� �ް� movementDirection�� lookDirection�� �����ϴ� ����
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");      //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        float vertical = Input.GetAxisRaw("Vertical");          //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        movementDirection = new Vector2(horizontal, vertical).normalized;        //���� ���ͷ� ��ȯ�Ͽ� �̵� ���� ����(normalized������ ���⸸ ��������(ũ�� 1�ΰ���))
        if(Mathf.Abs(horizontal) > 0.01f)           //���� ���� Ű �Է��� ��������
        {
            lookDirection = new Vector2(horizontal, 0).normalized;
        }



        //���콺�� �¿츦 �ٶ󺼶�
        //Vector2 mounsePosition = Input.mousePosition;               //���콺 ��ġ�� �ȼ� ��ǥ�� ������
        //Vector2 worldPos = cam.ScreenToWorldPoint(mounsePosition);       //�ȼ� ��ǥ�� ���� �� ���� ��ǥ�� ��ȯ��
        //lookDirection = (worldPos - (Vector2)transform.position);       //ĳ���� ��ġ �������� ���콺 �ٶ󺸴� ���� ���

        ////���콺�� �ʹ� ������ ���� ���� ����
        //if (lookDirection.magnitude < .9f)              //manitude�� ������ ����(�Ÿ�)
        //{
        //    lookDirection = Vector2.zero;               // ���� ����
        //}
        //else
        //{
        //    lookDirection = lookDirection.normalized;       //���� ���͸� ����ȭ�Ͽ� ����(ũ�� ����)
        //}
    }
}
