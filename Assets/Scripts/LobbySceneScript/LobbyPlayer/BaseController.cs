using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;                                     //Ű����� �Էµ� �̵� ����

    public Vector2 MovementDirection { get { return movementDirection; } }             //(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)public ������Ƽ ����

    protected Vector2 lookDirection = Vector2.zero;                                     //���콺�� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }                      //(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)public ������Ƽ ����

    protected AnimationHandler animationhandler;
    protected LobbyStatController lobbyStatController;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();                   //�ѹ� ã�ƿ��� _rigidbody ������ ����(���߿� �ٸ� ������ ������ �����ϱ� ����)
       animationhandler = GetComponent<AnimationHandler>();
        lobbyStatController = GetComponent<LobbyStatController>();
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
        Movement(movementDirection);
    }

    //�ڽ� Ŭ������ �Է� ó���� ���⼭ ������ �� �ֵ��� ����� �޼���
    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * lobbyStatController.Speed;                     //�̵��ӵ��� ���� ���� ���� ����             


        _rigidbody.velocity = direction;        //rigidbody�� �̵� ���� ����
        animationhandler.Move(direction);      //�ִϸ��̼� �̵� ���� ����
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       //���콺�� �ٶ󺸴� �������(atan2:���� ���͸� ������ ��ȯ , Rad2Deg:���� -> ��(degree)�� ��ȯ
        bool isLeft = Mathf.Abs(rotZ) > 90f;                                    //90���� ������ true(������ �ٶ󺸴� ���·� �Ǵ�)

        characterRenderer.flipX = isLeft;                                       //�¿� ���� ó��(������ ��� flipX ����)

    }
}
