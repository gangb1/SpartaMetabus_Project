using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenProjectileManager : MonoBehaviour
{
    //싱글톤 패턴
    private static HiddenProjectileManager instance;
    public static HiddenProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;            //여러 종류의 프리팹을 배열로 보관(bulletIndext를 통해 이 배열에서 어떤 총알을 쓸지 선택)

    [SerializeField] private ParticleSystem impactParticleSystem;       //충돌 시 터지는 이펙트(파티클 시스템)

    //게임 시작 시 이 객체가 자기 자신을 instance로 등록
    private void Awake()
    {
        instance = this;
    }

    //투사체 생성 메서드
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];              //bulletIndex에 따라서 사용할 총알 프리팹을 결정
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);           //해당 프리팹을 startPosition 위치에 생성

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();       //생성한 총알에서 ProjectileController 스크립트를 가져옴
        projectileController.Init(direction, rangeWeaponHandler,this);                          //초기화


    }

    public void CreatImpactPaticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;                     //이펙트 생성할 위치 설정(충돌 위치)
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;               //파티클이 몇 개 터질지 설정
        em.SetBurst(0, new ParticleSystem.Burst(0,Mathf.Ceil(weaponHandler.BulletSize*5)));     //총알이 클수록 파티클도 많이 터짐(burst(0,n) == 0초 시점에 n개 발사)

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;           //파티클시스템의 메인모듈에 점근해서 핵심 설정들(속도, 크기, 수명 등)을 수정할 수 있게 만듬
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;           //총알이 클수록 이펙트도 강하게 퍼짐
        impactParticleSystem.Play();                //파티클 재생
    }




}
