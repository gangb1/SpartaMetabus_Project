using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    //���� ����ȭ�� ���� ���ڿ��� �̸� �ؽð����� ��ȯ
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsPush = Animator.StringToHash("IsPush");
    private static readonly int IsActive = Animator.StringToHash("IsActive");

    //�ִϸ����� ������Ʈ ���� ����
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();      //�ִϸ����� �Ҵ�
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);        //������ ���� ũ�Ⱑ 0.5�̻��̸� IsMove �Ķ���͸� true�� ����
    }

    //�������� Ȱ��ȭ/��Ȱ��ȭ �ִϸ��̼� bool��
    public void Active()
    {
        animator.SetBool(IsActive, true);
    }

    public void DeActive()
    {
        animator.SetBool(IsActive, false);
    }

}
