using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAnimationHandler : MonoBehaviour
{
    //bool �� �Ķ���͸� �ؽ� ��(int)�� �̸� ����Ͽ� ����(���ڿ��� �Ź� �����ϴ� �ͺ���)������ �� ���� ������, readonly�� �ʱ�ȭ �� �ٲ��� �ʰ�, static�̶� Ŭ���� ��ü���� ����
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;        //animator ������Ʈ ���� ����

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();          //�ڱ� �ڽ��̳� �ڽ� ������Ʈ �߿� �ִ� Animator ������Ʈ�� ã��
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);    // obj.magnitude > .5f == �������� ����(���� ũ��)�� 0.5 �̻��̸� �̵� ������ �Ǵ�
    }

    // ������ ���� ���� �ִϸ��̼� ����
    public void Damage()
    {
        animator.SetBool(IsDamage, true);       
    }

    //�������� ����/ ������ ���� ����
    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}

