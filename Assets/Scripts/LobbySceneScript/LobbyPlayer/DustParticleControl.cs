using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;          //�ȴ� ���� ���� ����Ʈ�� ������ ���θ� �����ϴ� bool��(�⺻������ ������ ����/�ν����Ϳ��� ���� ����)
    [SerializeField] private ParticleSystem dustParticleSystem;     //��ƼŬ �ý��� ������Ʈ ���� ����

    public void CreateDustParticles()
    {
        if(createDustOnWalk)
        {
            dustParticleSystem.Stop();      //������̴� ��ƼŬ �ߴ�
            dustParticleSystem.Play();      //��ƼŬ �ٽ� ���(����Ʈ ����)
        }
    }
}
