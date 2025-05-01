using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;                                     //키보드로 입력된 이동 방향

    public Vector2 MovementDirection { get { return movementDirection; } }             //public 프로퍼티 제공(다른 클래스에서 방향값을 읽어 올 수 있도록)

    protected Vector2 lookDirection = Vector2.zero;                                     //마우스를 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }                      //public 프로퍼티 제공(다른 클래스에서 방향값을 읽어 올 수 있도록)

    //protected AnimationHandler animationhendler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();                   //한번 찾아오고 _rigidbody 변수에 저장(나중에 다른 곳에서 빠르게 접근하기 위해)
        //animationhendler = GetComponent<AnimationHandler>();
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();                     //입력처리(상속받은 클래스에서 오버라이드해야 작동)
        Rotate(LookDirection);              //마우스를 향해 캐릭터/무기 방향 회전
    }

    //물리 엔진과 연동되는 FixedUpdate에서 이동 처리
    protected virtual void FixedUpdate()
    {
        MoveMent(movementDirection);
        //시간 기반 넉백 종료 기능
    }

    //자식 클래스가 입력 처리를 여기서 구현할 수 있도록 열어둔 메서드
    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * 3f;                     //이동속도 기본 값              


        _rigidbody.velocity = direction;        //방향 벡터 적용
        //animationhendler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       //마우스를 바라보는 각도계산(atan2:방향 벡터를 각도로 변환 , Rad2Deg:라디안 -> 도(degree)로 변환
        bool isLeft = Mathf.Abs(rotZ) > 90f;                                    //90도가 넘으면 true

        characterRenderer.flipX = isLeft;                                       //90도가 넘으면 왼쪽으로 바라봄

    }
}
