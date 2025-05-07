using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //벽이나 지형 충돌 판정을 위한 레이어
    [SerializeField] private LayerMask levelCollsionLayer;

    private RangeWeaponHandler rangeWeaponHandler;          //총알을 발사한 무기의 정보를 참조

    private float currentDuration;               //살아있는 시간(초 단위)       
    private Vector2 direction;                  //날라가는 방향
    private bool isReady;                       //true일 때만 Update()작동(초기화 완료 플래그)
    private Transform pivot;                    //총알의 시각적 회전 축(스프라이트 반전용)

    private Rigidbody2D _rigidbody;             //실제 물리 이동 할 때 사용
    private SpriteRenderer spriteRenderer;      //색상이나 투명도 설정에 사용

    public bool fxOnDestroy = true;                //부딪힐 때 이펙트 생성 여부

    HiddenProjectileManager projectileManager;        //파괴 시 이펙트를 생성하기 위해 필요


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();          //자식 오브젝트 안에서 SpriteRenderer가져옴
        _rigidbody = GetComponent<Rigidbody2D>();                       //Rigidbody2d 참조 저장
        pivot = transform.GetChild(0);                                  //자식(총알 비주얼의 피벗)가져옴
    }
    private void Update()
    {
        if (!isReady) return;               //초기화(Init)전이면 아무것도 안함

        //수명이 다 되면 파괴
        currentDuration += Time.deltaTime;      

        if(currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;             //물리 속도로 계속 날아감
    }

    //충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //지형에 충돌한 경우
        if (levelCollsionLayer.value == (levelCollsionLayer.value | (1 << collision.gameObject.layer)))                 //해당 레이어가 포함되어 있는지 확인
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);           //총알을 현재 충돌 지점 바로 전 위치에서 제거
        }
        //적에게 맞은 경우
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))          //공격 가능한 대상 LayerMask와 비교(layermask는 weaponHandler에 있음)
        {
            HiddenResouceController resouceController = collision.GetComponent<HiddenResouceController>();              //ChangeHealth() 메서드(체력 감소 시스템)이 들어있는 적의 ResouceController를 찾음
            if (resouceController != null)                                                      
                {
                resouceController.ChangeHealth(-rangeWeaponHandler.Power);                              //무기 power의 음수 만큼 체력 감소
                if(rangeWeaponHandler.IsOnKnockback)                                    //무기에 넉백이 추가되있으면
                {
                    HiddenBaseController controller = collision.GetComponent<HiddenBaseController>();           //적의 baseController 정보를 가져옴
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);      //충돌한 적에게 넉백 적용
                    }
                }
                }


            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);       //총알을 현재 충돌 지점에서 약간 밀려난 위치에서 이펙트를 만들고 제거
        }
    }

    //무기의 설정값을 기반으로 총알 자체의 상태를 세팅
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, HiddenProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;             //이펙트 생성 및 풀링 매니저에게 반환

        rangeWeaponHandler = weaponHandler;                     //발사한 무기 정보 저장

        this.direction = direction;                     //총알이 날아갈 방향 벡터 저장
        currentDuration = 0;                            //투사체 생존 시간 초기화
        transform.localScale = Vector3.one * weaponHandler.BulletSize;              //총알 크기 설정
        spriteRenderer.color = weaponHandler.ProjectileColor;                   //무기에서 설정한 총알 색상 적용

        transform.right = this.direction;                   //총알을 플레이어가 바라보는 방향 쪽으로 회전시킴

        //sprite(또는 자식 오브젝트인 pivot)의 시각적 방향을 반전시키는 코드(현재 상태에서는 skull을 위한 코드)
        if (direction.x < 0)                        //direction은 투사체가 날아갈 방향 , direction.x < 0 이면 왼쪽으로 날아간다는 뜻
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);                  //피벗을 부모기준으로 x축을 180도 회전시킴

        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
        isReady = true;             //초기화 이후 시작
    }
    //투사체 제거
    private void DestroyProjectile(Vector3 position, bool creatFx)
    {
        if(creatFx)
        {
            projectileManager.CreatImpactPaticlesAtPosition(position, rangeWeaponHandler);      //충돌 지점에 파티클 이펙트 생성
        }

        Destroy(this.gameObject);           //현재 오브젝트를 씬에서 제거
    }
}
