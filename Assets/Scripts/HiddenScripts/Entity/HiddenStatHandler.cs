using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HiddenStatHandler : MonoBehaviour
{
    [Range(1, 100)]     //슬라이더 ui가 inspector에 생성되어 범위 제한 가능(1~100) 특성(Attribute)라고 부르는 c#문법의 일부라고 함
    [SerializeField] private int health = 10;                       //private 변수지만 [SerializeField] 덕분에 inspector로 조절 가능(SerializeField)


    //Health 프로퍼티는 외부에서 값을 조절할 수 있도록 공개
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100); //무조건 0~100사이로만 설정되게 clamp처리
    
    }

    [Range(1f, 20f)]                                   //슬라이더ui로 inspector에서 조절 가능
    [SerializeField] private float speed = 3;       //속도도 마찬가지로 inspector에서 조절 가능(float 값)

    //외부에서 조절 가능하되 항상 0~20사이로 유지
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
