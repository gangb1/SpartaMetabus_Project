using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    //무기 공격 관련 설정 Inspector에서 묶어주는 역할
    //시리얼라이즈필드로 private여도 Inspector에서 조절할 수 있게 함
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;                  //공격 간격
    public float Delay { get => delay; set => delay = value; }     //프로퍼티를 통해 외부에서 값을 바꾸고, 내부 필드를 안전하게 감싸줌  

    [SerializeField] private float weaponSize = 1f;             //무기 사이즈

    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] public float power = 1f;               //무가ㅣ 공격력

    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;          //발사체 속도

    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;       //공격 범위
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;                        //공격 대상 레이어 설정

    //넉백 기능 관련 설정
    [Header("knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;        //넉백 활성화 유무

    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value;}

    [SerializeField] private float knockbackPower = 0.1f;       //넉백의 세기

    public float KnockbackPower {  get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;        //넉백 지속 시간
        public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");           //애니메이터의 IsAttack 파라미터를 빠르게 접근하기 위한 해시값

    public HiddenBaseController Controller {  get; private set; }         //(무기를 들고 있는)캐릭터의 baseController 가져옴

    //무기 안의 애니메이션과 시각 렌더러를 가져옴
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    public AudioClip attackSoundClip;


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<HiddenBaseController>();            //부모에서 basecontroller 찾기

        //자식에서 animator,spriteRenderer 가져오기
        animator = GetComponentInChildren<Animator>();          
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f /delay;                       //애니메이션 속도 = 공격 딜레이에 맞게 조정
        transform.localScale = Vector3.one * weaponSize;        //무기 크기 = weaponSize에 따라 스케일 조정
    }

    protected virtual void Start()
    {

    }
    //공격 시작 시 호출
    public virtual void Attack()
    {
        AttackAnimation();

        if(attackSoundClip != null)
        {
            HiddenSoundManager.PlayClip(attackSoundClip);
        }
    }

    //애니메이션 실행
    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    //플레이어가 좌우 전환 할 때 무기도 반전시키기 위해 사용
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
