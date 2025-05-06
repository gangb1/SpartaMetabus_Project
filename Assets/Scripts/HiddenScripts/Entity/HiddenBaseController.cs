using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;                                     //키보드로 입력된 이동 방향

    public Vector2 MovementDirection { get { return movementDirection; } }             //public 프로퍼티 제공(다른 클래스에서 방향값을 읽어 올 수 있도록)

    protected Vector2 lookDirection = Vector2.zero;                                     //마우스를 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }                      //public 프로퍼티 제공(다른 클래스에서 방향값을 읽어 올 수 있도록)

    private Vector2 knockback = Vector2.zero;                                   //넉백
    private float knockbackDuration = 0.0f;                                     //넉백 지속시간

    protected HiddenAnimationHandler animationhendler;
    protected HiddenStatHandler statHandler;


    [SerializeField] public WeaponHandler WeaponPrefabs;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();                   //한번 찾아오고 _rigidbody 변수에 저장(나중에 다른 곳에서 빠르게 접근하기 위해)
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
        HandleAction();                     //입력처리(상속받은 클래스에서 오버라이드해야 작동)
        Rotate(LookDirection);              //마우스를 향해 캐릭터/무기 방향 회전
        HandleAttackDelay();
    }

    //물리 엔진과 연동되는 FixedUpdate에서 이동 처리
    protected virtual void FixedUpdate()
    {
        MoveMent(movementDirection);
        //시간 기반 넉백 종료 기능
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;               //fixedDeltatime은 고정된 시간(물리 엔진용, 항상 통일)
        }
    }

    //자식 클래스가 입력 처리를 여기서 구현할 수 있도록 열어둔 메서드
    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * statHandler.Speed;                      //이동속도 기본 값              
        if(knockbackDuration > 0.0f)                //넉백 쿨타임이 더 높다면
        {
            direction *= 0.2f;                  //이동속도 20%로 줄이고 넉백 방향으로 튕김
            direction += knockback;
        }

        _rigidbody.velocity = direction;        //방향 벡터 적용
        animationhendler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;       //마우스를 바라보는 각도계산(atan2:방향 벡터를 각도로 변환 , Rad2Deg:라디안 -> 도(degree)로 변환
        bool isLeft = Mathf.Abs(rotZ) > 90f;                                    //90도가 넘으면 true

        characterRenderer.flipX = isLeft;                                       //90도가 넘으면 왼쪽으로 바라봄

        //무기가 있다면 무기도 회전
        if (weaponPivot != null)                                           
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        weaponHandler?.Rotate(isLeft);
    }


    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;   //duration만큼 넉백 유지
        knockback = -(other.position - transform.position).normalized * power;          //넉백 명령어( -((적.포지션)- (플레이어.포지션)).normalized(방향만 가져옴) * power(넉백 크기)
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
