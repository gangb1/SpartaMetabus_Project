using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HiddenStatHandler : MonoBehaviour
{
    [Range(1, 100)]     //�����̴� ui�� inspector�� �����Ǿ� ���� ���� ����(1~100) Ư��(Attribute)��� �θ��� c#������ �Ϻζ�� ��
    [SerializeField] private int health = 10;                       //private �������� [SerializeField] ���п� inspector�� ���� ����(SerializeField)


    //Health ������Ƽ�� �ܺο��� ���� ������ �� �ֵ��� ����
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100); //������ 0~100���̷θ� �����ǰ� clampó��
    
    }

    [Range(1f, 20f)]                                   //�����̴�ui�� inspector���� ���� ����
    [SerializeField] private float speed = 3;       //�ӵ��� ���������� inspector���� ���� ����(float ��)

    //�ܺο��� ���� �����ϵ� �׻� 0~20���̷� ����
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
