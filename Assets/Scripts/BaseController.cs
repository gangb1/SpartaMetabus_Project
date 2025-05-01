using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;                                     //Ű����� �Էµ� �̵� ����

    public Vector2 MovementDirection { get { return movementDirection; } }             //public ������Ƽ ����(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)

    protected Vector2 lookDirection = Vector2.zero;                                     //���콺�� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }                      //public ������Ƽ ����(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)

    //protected AnimationHandler animationhendler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();                   //�ѹ� ã�ƿ��� _rigidbody ������ ����(���߿� �ٸ� ������ ������ �����ϱ� ����)
        //animationhendler = GetComponent<AnimationHandler>();
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();                     //�Է�ó��(��ӹ��� Ŭ�������� �������̵��ؾ� �۵�)
        Rotate(LookDirection);              //���콺�� ���� ĳ����/���� ���� ȸ��
    }

    //���� ������ �����Ǵ� FixedUpdate���� �̵� ó��
    protected virtual void FixedUpdate()
    {
        MoveMent(movementDirection);
        //�ð� ��� �˹� ���� ���
    }

    //�ڽ� Ŭ������ �Է� ó���� ���⼭ ������ �� �ֵ��� ����� �޼���
    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * 3f;                     //�̵��ӵ� �⺻ ��              


        _rigidbody.velocity = direction;        //���� ���� ����
        //animationhendler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       //���콺�� �ٶ󺸴� �������(atan2:���� ���͸� ������ ��ȯ , Rad2Deg:���� -> ��(degree)�� ��ȯ
        bool isLeft = Mathf.Abs(rotZ) > 90f;                                    //90���� ������ true

        characterRenderer.flipX = isLeft;                                       //90���� ������ �������� �ٶ�

    }
}
