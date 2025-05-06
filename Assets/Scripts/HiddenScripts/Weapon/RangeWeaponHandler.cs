using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    //���Ÿ� ���� ������
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;     //�Ѿ��� �߻�Ǵ� ��ġ

    [SerializeField] private int bulletIndex;       //�Ѿ� ���� ���п�
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1f; //�Ѿ� ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;        //�Ѿ� ���� �ð�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;      //ź���� ����
    public float Spread {  get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;        //�߻��� �Ѿ� ����
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle;         //�Ѿ˵� ������ ���� ����
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor;         //�Ѿ� ����
    public Color ProjectileColor { get { return projectileColor; } }

    private HiddenProjectileManager projectileManager;        //�Ѿ� ����/���� ������ �ϴ� �̱��� �Ŵ���

    protected override void Start()
    {
        base.Start();               //�θ� ���� �ʱ�ȭ(�ִϸ��̼�, �˹� ��)
        projectileManager = HiddenProjectileManager.Instance;     //�̱��� ����
    }

    public override void Attack()
    {
        base.Attack();              //�θ�(baseController) ���� ���� ���� 

        //SerializeField�� ����� �ʵ带 ���� ������ ����
        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberofProjectilesPerShot;

        //�߾��� �������� ��Ī���� ������ �۶߸��� ���� ���
        float minAngle = -(numberOfProjectilePerShot / 2f) *projectileAngleSpace;

        //�Ѿ˸��� i��°�� �ش��ϴ� ���� ���
        for(int i= 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);             //spread���� ���� ������ ��¦ �����ϰ� �۶߸�
            angle += randomSpread;
            CreatProjectile(Controller.LookDirection, angle);       //����� ���� ������ ������� ����ü �߻�
        }
    }

    //����� ��ġ�� ����ؼ� �Ѿ��� ����/��
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
