using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAnimationHandler : MonoBehaviour
{
    //bool 값 파라미터를 해시 값(int)로 미리 계산하여 저장(문자열로 매번 접근하는 것보다)성능이 더 좋고 안전함, readonly라 초기화 후 바뀌지 않고, static이라 클래스 전체에서 공유
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;        //animator 컴포넌트 참조 변수

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();          //자기 자신이나 자식 오브젝트 중에 있는 Animator 컴포넌트를 찾음
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);    // obj.magnitude > .5f == 움직임의 세기(벡터 크기)가 0.5 이상이면 이동 중으로 판단
    }

    // 데미지 상태 진입 애니메이션 실행
    public void Damage()
    {
        animator.SetBool(IsDamage, true);       
    }

    //무적상태 종료/ 데미지 상태 해제
    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}

