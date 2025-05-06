using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HiddenPlayerController : HiddenBaseController
{
    private Camera cam;              //���� ī�޶� ���� ����(���콺 ��ǥ -> ���� ��ǥ�� ��ȯ�� �� ���)
    private HiddenGameManager gameManager;

    public void Init(HiddenGameManager gameManager)
    {
        this.gameManager = gameManager;
        cam = Camera.main;
    }


    //�Է��� �ް� movementDirection�� lookDirection�� �����ϴ� ����
    protected override void HandleAction()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");      //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        //float vertical = Input.GetAxisRaw("Vertical");          //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        //movementDirection = new Vector2(horizontal,vertical).normalized;        //�̵� ���� ����(normalized������ ���⸸ ��������)

        //Vector2 mounsePosition = Input.mousePosition;               //���콺 ��ġ�� �ȼ� ��ǥ�� ��
        //Vector2 worldPos = cam.ScreenToWorldPoint(mounsePosition);       //�ȼ� ��ǥ�� ���� �� ���� ��ǥ�� ��ȯ��
        //lookDirection = (worldPos - (Vector2)transform.position);       //���콺 �ٶ󺸴� ���� ���

        ////�ʹ� ������ ����
        //if (lookDirection.magnitude < .9f)              //manitude�� ������ ����(�Ÿ�)
        //{
        //    lookDirection = Vector2.zero;               // ���� ����
        //}
        //else
        //{
        //    lookDirection = lookDirection.normalized;       //���⸸ ����
        //}

        //isAttacking = Input.GetMouseButton(0);
    }
    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    void OnMove(InputValue inputValue)
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");      //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        //float vertical = Input.GetAxisRaw("Vertical");          //���� ���� Ű �Է��� �ǽð����� ���ڷ� �����´�.
        //movementDirection = new Vector2(horizontal, vertical).normalized;        //�̵� ���� ����(normalized������ ���⸸ ��������)
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
    void OnLook(InputValue inputValue)
    {
        Vector2 mounsePosition = inputValue.Get<Vector2>();               //���콺 ��ġ�� �ȼ� ��ǥ�� ��
        Vector2 worldPos = cam.ScreenToWorldPoint(mounsePosition);       //�ȼ� ��ǥ�� ���� �� ���� ��ǥ�� ��ȯ��
        lookDirection = (worldPos - (Vector2)transform.position);       //���콺 �ٶ󺸴� ���� ���

        //�ʹ� ������ ����
        if (lookDirection.magnitude < .9f)              //manitude�� ������ ����(�Ÿ�)
        {
            lookDirection = Vector2.zero;               // ���� ����
        }
        else
        {
            lookDirection = lookDirection.normalized;       //���⸸ ����
        }
    }

    void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        isAttacking = inputValue.isPressed;
    }

}
