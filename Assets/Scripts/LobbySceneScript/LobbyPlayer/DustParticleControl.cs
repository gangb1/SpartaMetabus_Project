using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;          //걷는 동안 먼지 이펙트를 만들지 여부를 설정하는 bool값(기본적으로 먼지를 생성/인스펙터에서 조정 가능)
    [SerializeField] private ParticleSystem dustParticleSystem;     //파티클 시스템 컴포넌트 연결 변수

    public void CreateDustParticles()
    {
        if(createDustOnWalk)
        {
            dustParticleSystem.Stop();      //재생중이던 파티클 중단
            dustParticleSystem.Play();      //파티클 다시 재생(이펙트 실행)
        }
    }
}
