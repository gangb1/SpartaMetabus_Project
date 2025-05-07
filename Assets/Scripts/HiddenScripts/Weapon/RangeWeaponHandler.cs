using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    //원거리 공격 데이터
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;     //총알이 발사되는 위치

    [SerializeField] private int bulletIndex;       //총알 종류 구분용
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1f; //총알 크기
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;        //총알 지속 시간
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;      //탄퍼짐 각도
    public float Spread {  get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;        //발사한 총알 개수
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle;         //총알들 사이의 각도 간격
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor;         //총알 색상
    public Color ProjectileColor { get { return projectileColor; } }

    private HiddenProjectileManager projectileManager;        //총알 생성/관리 역할을 하는 싱글톤 매니저

    protected override void Start()
    {
        base.Start();               //부모 무기 초기화(애니메이션, 넉백 등)
        projectileManager = HiddenProjectileManager.Instance;     //싱글톤 참조
    }

    public override void Attack()
    {
        base.Attack();              //부모(baseController) 무기 공격 로직 

        //SerializeField로 선언된 필드를 지역 변수로 꺼냄
        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberofProjectilesPerShot;

        //중앙을 기준으로 대칭적인 각도로 퍼뜨리는 기초 계산
        float minAngle = -(numberOfProjectilePerShot / 2f) *projectileAngleSpace;

        //총알마다 i번째에 해당하는 각도 계산
        for(int i= 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);             //spread값에 따라 각도를 살짝 랜덤하게 퍼뜨림
            angle += randomSpread;
            CreatProjectile(Controller.LookDirection, angle);       //방향과 계산된 각도를 기반으로 투사체 발사
        }
    }

    //방향과 위치를 계산해서 총알을 만듦/쏨
    private void CreatProjectile(Vector2 _lookDirection , float angle)
    {
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection,angle));
    }

    //
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0,0,degree)*v;
    }
}
