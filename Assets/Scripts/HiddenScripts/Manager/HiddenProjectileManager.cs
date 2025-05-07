using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenProjectileManager : MonoBehaviour
{
    //�̱��� ����
    private static HiddenProjectileManager instance;
    public static HiddenProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;            //���� ������ �������� �迭�� ����(bulletIndext�� ���� �� �迭���� � �Ѿ��� ���� ����)

    [SerializeField] private ParticleSystem impactParticleSystem;       //�浹 �� ������ ����Ʈ(��ƼŬ �ý���)

    //���� ���� �� �� ��ü�� �ڱ� �ڽ��� instance�� ���
    private void Awake()
    {
        instance = this;
    }

    //����ü ���� �޼���
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];              //bulletIndex�� ���� ����� �Ѿ� �������� ����
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);           //�ش� �������� startPosition ��ġ�� ����

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();       //������ �Ѿ˿��� ProjectileController ��ũ��Ʈ�� ������
        projectileController.Init(direction, rangeWeaponHandler,this);                          //�ʱ�ȭ


    }

    public void CreatImpactPaticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;                     //����Ʈ ������ ��ġ ����(�浹 ��ġ)
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;               //��ƼŬ�� �� �� ������ ����
        em.SetBurst(0, new ParticleSystem.Burst(0,Mathf.Ceil(weaponHandler.BulletSize*5)));     //�Ѿ��� Ŭ���� ��ƼŬ�� ���� ����(burst(0,n) == 0�� ������ n�� �߻�)

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;           //��ƼŬ�ý����� ���θ�⿡ �����ؼ� �ٽ� ������(�ӵ�, ũ��, ���� ��)�� ������ �� �ְ� ����
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;           //�Ѿ��� Ŭ���� ����Ʈ�� ���ϰ� ����
        impactParticleSystem.Play();                //��ƼŬ ���
    }




}
