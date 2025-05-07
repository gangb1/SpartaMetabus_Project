using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStatController : MonoBehaviour
{
    [Range(1f, 20f)]    //슬라이더 ui로 조절 가능(범위 제한까지 가능)
    [SerializeField] private float speed = 6f;      //외부 직접 접근은 막되 유니티 인스펙터에서 확인하고 조절할 수 있게 설정

    //프로퍼티
    public float Speed
    {
        get => speed;           //외부에서 값을 읽을 수 있게 함
        set => speed = Mathf.Clamp(value,0,20);     //외부에서 값을 설정할 때 0~20 사이로 자동 제한(value는 프로퍼티(set한정)만의 매개변수 느낌)
    }
}
