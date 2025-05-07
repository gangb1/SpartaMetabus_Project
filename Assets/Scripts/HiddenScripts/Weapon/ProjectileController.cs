using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //���̳� ���� �浹 ������ ���� ���̾�
    [SerializeField] private LayerMask levelCollsionLayer;

    private RangeWeaponHandler rangeWeaponHandler;          //�Ѿ��� �߻��� ������ ������ ����

    private float currentDuration;               //����ִ� �ð�(�� ����)       
    private Vector2 direction;                  //���󰡴� ����
    private bool isReady;                       //true�� ���� Update()�۵�(�ʱ�ȭ �Ϸ� �÷���)
    private Transform pivot;                    //�Ѿ��� �ð��� ȸ�� ��(��������Ʈ ������)

    private Rigidbody2D _rigidbody;             //���� ���� �̵� �� �� ���
    private SpriteRenderer spriteRenderer;      //�����̳� ���� ������ ���

    public bool fxOnDestroy = true;                //�ε��� �� ����Ʈ ���� ����

    HiddenProjectileManager projectileManager;        //�ı� �� ����Ʈ�� �����ϱ� ���� �ʿ�


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();          //�ڽ� ������Ʈ �ȿ��� SpriteRenderer������
        _rigidbody = GetComponent<Rigidbody2D>();                       //Rigidbody2d ���� ����
        pivot = transform.GetChild(0);                                  //�ڽ�(�Ѿ� ���־��� �ǹ�)������
    }
    private void Update()
    {
        if (!isReady) return;               //�ʱ�ȭ(Init)���̸� �ƹ��͵� ����

        //������ �� �Ǹ� �ı�
        currentDuration += Time.deltaTime;      

        if(currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;             //���� �ӵ��� ��� ���ư�
    }

    //�浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //������ �浹�� ���
        if (levelCollsionLayer.value == (levelCollsionLayer.value | (1 << collision.gameObject.layer)))                 //�ش� ���̾ ���ԵǾ� �ִ��� Ȯ��
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);           //�Ѿ��� ���� �浹 ���� �ٷ� �� ��ġ���� ����
        }
        //������ ���� ���
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))          //���� ������ ��� LayerMask�� ��(layermask�� weaponHandler�� ����)
        {
            HiddenResouceController resouceController = collision.GetComponent<HiddenResouceController>();              //ChangeHealth() �޼���(ü�� ���� �ý���)�� ����ִ� ���� ResouceController�� ã��
            if (resouceController != null)                                                      
                {
                resouceController.ChangeHealth(-rangeWeaponHandler.Power);                              //���� power�� ���� ��ŭ ü�� ����
                if(rangeWeaponHandler.IsOnKnockback)                                    //���⿡ �˹��� �߰���������
                {
                    HiddenBaseController controller = collision.GetComponent<HiddenBaseController>();           //���� baseController ������ ������
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);      //�浹�� ������ �˹� ����
                    }
                }
                }


            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);       //�Ѿ��� ���� �浹 �������� �ణ �з��� ��ġ���� ����Ʈ�� ����� ����
        }
    }

    //������ �������� ������� �Ѿ� ��ü�� ���¸� ����
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, HiddenProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;             //����Ʈ ���� �� Ǯ�� �Ŵ������� ��ȯ

        rangeWeaponHandler = weaponHandler;                     //�߻��� ���� ���� ����

        this.direction = direction;                     //�Ѿ��� ���ư� ���� ���� ����
        currentDuration = 0;                            //����ü ���� �ð� �ʱ�ȭ
        transform.localScale = Vector3.one * weaponHandler.BulletSize;              //�Ѿ� ũ�� ����
        spriteRenderer.color = weaponHandler.ProjectileColor;                   //���⿡�� ������ �Ѿ� ���� ����

        transform.right = this.direction;                   //�Ѿ��� �÷��̾ �ٶ󺸴� ���� ������ ȸ����Ŵ

        //sprite(�Ǵ� �ڽ� ������Ʈ�� pivot)�� �ð��� ������ ������Ű�� �ڵ�(���� ���¿����� skull�� ���� �ڵ�)
        if (direction.x < 0)                        //direction�� ����ü�� ���ư� ���� , direction.x < 0 �̸� �������� ���ư��ٴ� ��
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);                  //�ǹ��� �θ�������� x���� 180�� ȸ����Ŵ

        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
        isReady = true;             //�ʱ�ȭ ���� ����
    }
    //����ü ����
    private void DestroyProjectile(Vector3 position, bool creatFx)
    {
        if(creatFx)
        {
            projectileManager.CreatImpactPaticlesAtPosition(position, rangeWeaponHandler);      //�浹 ������ ��ƼŬ ����Ʈ ����
        }

        Destroy(this.gameObject);           //���� ������Ʈ�� ������ ����
    }
}
