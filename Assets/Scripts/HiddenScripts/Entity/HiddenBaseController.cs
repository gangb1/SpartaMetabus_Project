using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;                                     //Ű����� �Էµ� �̵� ����

    public Vector2 MovementDirection { get { return movementDirection; } }             //public ������Ƽ ����(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)

    protected Vector2 lookDirection = Vector2.zero;                                     //���콺�� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }                      //public ������Ƽ ����(�ٸ� Ŭ�������� ���Ⱚ�� �о� �� �� �ֵ���)

    private Vector2 knockback = Vector2.zero;                                   //�˹�
    private float knockbackDuration = 0.0f;                                     //�˹� ���ӽð�

    protected HiddenAnimationHandler animationhendler;
    protected HiddenStatHandler statHandler;


    [SerializeField] public WeaponHandler WeaponPrefabs;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();                   //�ѹ� ã�ƿ��� _rigidbody ������ ����(���߿� �ٸ� ������ ������ �����ϱ� ����)
        animationhendler = GetComponent<HiddenAnimationHandler>();
        statHandler = GetComponent<HiddenStatHandler>();

        if(WeaponPrefabs != null )
        {
            weaponHandler = Instantiate(WeaponPrefabs, weaponPivot);
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }


    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();                     //�Է�ó��(��ӹ��� Ŭ�������� �������̵��ؾ� �۵�)
        Rotate(LookDirection);              //���콺�� ���� ĳ����/���� ���� ȸ��
        HandleAttackDelay();
    }

    //���� ������ �����Ǵ� FixedUpdate���� �̵� ó��
    protected virtual void FixedUpdate()
    {
        MoveMent(movementDirection);
        //�ð� ��� �˹� ���� ���
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;               //fixedDeltatime�� ������ �ð�(���� ������, �׻� ����)
        }
    }

    //�ڽ� Ŭ������ �Է� ó���� ���⼭ ������ �� �ֵ��� ����� �޼���
    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * statHandler.Speed;                      //�̵��ӵ� �⺻ ��              
        if(knockbackDuration > 0.0f)                //�˹� ��Ÿ���� �� ���ٸ�
        {
            direction *= 0.2f;                  //�̵��ӵ� 20%�� ���̰� �˹� �������� ƨ��
            direction += knockback;
        }

        _rigidbody.velocity = direction;        //���� ���� ����
        animationhendler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;       //���콺�� �ٶ󺸴� �������(atan2:���� ���͸� ������ ��ȯ , Rad2Deg:���� -> ��(degree)�� ��ȯ
        bool isLeft = Mathf.Abs(rotZ) > 90f;                                    //90���� ������ true

        characterRenderer.flipX = isLeft;                                       //90���� ������ �������� �ٶ�

        //���Ⱑ �ִٸ� ���⵵ ȸ��
        if (weaponPivot != null)                                           
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        weaponHandler?.Rotate(isLeft);
    }


    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;   //duration��ŭ �˹� ����
        knockback = -(other.position - transform.position).normalized * power;          //�˹� ��ɾ�( -((��.������)- (�÷��̾�.������)).normalized(���⸸ ������) * power(�˹� ũ��)
    }
    private void HandleAttackDelay()
    {
        if (weaponHandler == null) return;

        if(timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if(isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }
    protected virtual void Attack()
    {
        if(lookDirection != Vector2.zero)
        {
            weaponHandler.Attack();
        }
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach(SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;

            renderer.color = color;
        }
        foreach(Behaviour componet in transform.GetComponentsInChildren<Behaviour>())
        {
            componet.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
