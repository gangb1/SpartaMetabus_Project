using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStatController : MonoBehaviour
{
    [Range(1f, 20f)]    //�����̴� ui�� ���� ����(���� ���ѱ��� ����)
    [SerializeField] private float speed = 6f;      //�ܺ� ���� ������ ���� ����Ƽ �ν����Ϳ��� Ȯ���ϰ� ������ �� �ְ� ����

    //������Ƽ
    public float Speed
    {
        get => speed;           //�ܺο��� ���� ���� �� �ְ� ��
        set => speed = Mathf.Clamp(value,0,20);     //�ܺο��� ���� ������ �� 0~20 ���̷� �ڵ� ����(value�� ������Ƽ(set����)���� �Ű����� ����)
    }
}
