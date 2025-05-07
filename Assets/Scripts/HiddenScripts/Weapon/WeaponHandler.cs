using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    //���� ���� ���� ���� Inspector���� �����ִ� ����
    //�ø���������ʵ�� private���� Inspector���� ������ �� �ְ� ��
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;                  //���� ����
    public float Delay { get => delay; set => delay = value; }     //������Ƽ�� ���� �ܺο��� ���� �ٲٰ�, ���� �ʵ带 �����ϰ� ������  

    [SerializeField] private float weaponSize = 1f;             //���� ������

    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] public float power = 1f;               //������ ���ݷ�

    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;          //�߻�ü �ӵ�

    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;       //���� ����
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;                        //���� ��� ���̾� ����

    //�˹� ��� ���� ����
    [Header("knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;        //�˹� Ȱ��ȭ ����

    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value;}

    [SerializeField] private float knockbackPower = 0.1f;       //�˹��� ����

    public float KnockbackPower {  get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;        //�˹� ���� �ð�
        public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");           //�ִϸ������� IsAttack �Ķ���͸� ������ �����ϱ� ���� �ؽð�

    public HiddenBaseController Controller {  get; private set; }         //(���⸦ ��� �ִ�)ĳ������ baseController ������

    //���� ���� �ִϸ��̼ǰ� �ð� �������� ������
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    public AudioClip attackSoundClip;


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<HiddenBaseController>();            //�θ𿡼� basecontroller ã��

        //�ڽĿ��� animator,spriteRenderer ��������
        animator = GetComponentInChildren<Animator>();          
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f /delay;                       //�ִϸ��̼� �ӵ� = ���� �����̿� �°� ����
        transform.localScale = Vector3.one * weaponSize;        //���� ũ�� = weaponSize�� ���� ������ ����
    }

    protected virtual void Start()
    {

    }
    //���� ���� �� ȣ��
    public virtual void Attack()
    {
        AttackAnimation();

        if(attackSoundClip != null)
        {
            HiddenSoundManager.PlayClip(attackSoundClip);
        }
    }

    //�ִϸ��̼� ����
    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    //�÷��̾ �¿� ��ȯ �� �� ���⵵ ������Ű�� ���� ���
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
