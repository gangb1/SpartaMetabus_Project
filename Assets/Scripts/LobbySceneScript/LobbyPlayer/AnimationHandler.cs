using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    //성능 최적화를 위해 문자열을 미리 해시값으로 변환
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsPush = Animator.StringToHash("IsPush");
    private static readonly int IsActive = Animator.StringToHash("IsActive");

    //애니메이터 컴포넌트 보관 변수
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();      //애니메이터 할당
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);        //움직임 벡터 크기가 0.5이상이면 IsMove 파라미터를 true로 설정
    }

    //리더보드 활성화/비활성화 애니메이션 bool값
    public void Active()
    {
        animator.SetBool(IsActive, true);
    }

    public void DeActive()
    {
        animator.SetBool(IsActive, false);
    }

}
