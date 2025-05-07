using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenResouceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;         //체력이 바뀐 뒤 일정 시간 동안 다시 못 바꾸게 하는 쿨타임 설정

    private HiddenBaseController baseController;              //기본 컨트롤러 클래스 참조
    private HiddenStatHandler statHandler;                    //스탯 클래스 참조
    private HiddenAnimationHandler animationHandler;          //애니메이션 재생용 클래스 참조

    private float timeSinceLastChange = float.MaxValue;    //체력 변화 후 일정 시간동안 무적 상태를 유지하는 쿨타임

    public AudioClip damageClip;
    public float CurrentHealth {  get; private set; }       //현재 체력(외부에서는 읽기만 가능)
    public float MaxHealth => statHandler.Health;           //최대 체력을 statHandler의 Health에서 가져옴 **

    private Action<float,float> OnChangeHealth;

    //컴포넌트 참조 캐싱
    private void Awake()
    {
        baseController = GetComponent<HiddenBaseController>();
        statHandler = GetComponent<HiddenStatHandler>();
        animationHandler = GetComponent<HiddenAnimationHandler>();   
    }

    private void Start()
    {
       CurrentHealth = statHandler.Health;      //게임 시작 시 현재 체력을 최대 체력으로 초기화
    }

    private void Update()
    {
        if(timeSinceLastChange < healthChangeDelay)             //프레임 마다 무적 시간 경과 체크
        {
            timeSinceLastChange += Time.deltaTime;              //무적 시간 쿨타임을 프레임단위로 체크하여 올림
            if(timeSinceLastChange >= healthChangeDelay)        //무적시간이 끝나면  animationHandler.InvincibilityEnd(); 호출
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }
    //체력 변화 처리
    //아주 큰 값으로 초기화하여 아래에 있는 changeHealth 메서드를 사용치 못하게 함(처음부터 대미지를 입을 수 있음)
    //대미지를 입으면 timeSinceLastChange를 0으로 초기화함(무적시간 발동)
    //timeSinceLastChange이 0일때는 healthChangeDelay보다 낮으므로 changeHealgh를 사용하지 않음
    //update문에서 healthchangeDelay보다 커지면 다시 hit당할 수 있는 상태가 됨
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;               //변화 없음 또는 아직 무적 시간이라 무시
        }
        timeSinceLastChange = 0f;       //무적시간 초기화
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;      //너무 높으면 maxHealth로 잘라주고 너무 낮으면 0으로 고정
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth,MaxHealth);

        //데미지를 받으면 애니메이션.damage 출력
        if (change < 0)
        {
            animationHandler.Damage();

            if(damageClip != null)
            {
                HiddenSoundManager.PlayClip(damageClip);
            }
        }

        //체력이 0 이하가 되면 사망
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

