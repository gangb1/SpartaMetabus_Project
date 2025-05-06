using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenResouceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;         //ü���� �ٲ� �� ���� �ð� ���� �ٽ� �� �ٲٰ� �ϴ� ��Ÿ�� ����

    private HiddenBaseController baseController;              //�⺻ ��Ʈ�ѷ� Ŭ���� ����
    private HiddenStatHandler statHandler;                    //���� Ŭ���� ����
    private HiddenAnimationHandler animationHandler;          //�ִϸ��̼� ����� Ŭ���� ����

    private float timeSinceLastChange = float.MaxValue;    //ü�� ��ȭ �� ���� �ð����� ���� ���¸� �����ϴ� ��Ÿ��

    public AudioClip damageClip;
    public float CurrentHealth {  get; private set; }       //���� ü��(�ܺο����� �б⸸ ����)
    public float MaxHealth => statHandler.Health;           //�ִ� ü���� statHandler�� Health���� ������ **

    private Action<float,float> OnChangeHealth;

    //������Ʈ ���� ĳ��
    private void Awake()
    {
        baseController = GetComponent<HiddenBaseController>();
        statHandler = GetComponent<HiddenStatHandler>();
        animationHandler = GetComponent<HiddenAnimationHandler>();   
    }

    private void Start()
    {
       CurrentHealth = statHandler.Health;      //���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
    }

    private void Update()
    {
        if(timeSinceLastChange < healthChangeDelay)             //������ ���� ���� �ð� ��� üũ
        {
            timeSinceLastChange += Time.deltaTime;              //���� �ð� ��Ÿ���� �����Ӵ����� üũ�Ͽ� �ø�
            if(timeSinceLastChange >= healthChangeDelay)        //�����ð��� ������  animationHandler.InvincibilityEnd(); ȣ��
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }
    //ü�� ��ȭ ó��
    //���� ū ������ �ʱ�ȭ�Ͽ� �Ʒ��� �ִ� changeHealth �޼��带 ���ġ ���ϰ� ��(ó������ ������� ���� �� ����)
    //������� ������ timeSinceLastChange�� 0���� �ʱ�ȭ��(�����ð� �ߵ�)
    //timeSinceLastChange�� 0�϶��� healthChangeDelay���� �����Ƿ� changeHealgh�� ������� ����
    //update������ healthchangeDelay���� Ŀ���� �ٽ� hit���� �� �ִ� ���°� ��
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;               //��ȭ ���� �Ǵ� ���� ���� �ð��̶� ����
        }
        timeSinceLastChange = 0f;       //�����ð� �ʱ�ȭ
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;      //�ʹ� ������ maxHealth�� �߶��ְ� �ʹ� ������ 0���� ����
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth,MaxHealth);

        //�������� ������ �ִϸ��̼�.damage ���
        if (change < 0)
        {
            animationHandler.Damage();

            if(damageClip != null)
            {
                HiddenSoundManager.PlayClip(damageClip);
            }
        }

        //ü���� 0 ���ϰ� �Ǹ� ���
        if (CurrentHealth <= 0f)
        {
            Death();
        }
        return true;
    }

        private void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}

