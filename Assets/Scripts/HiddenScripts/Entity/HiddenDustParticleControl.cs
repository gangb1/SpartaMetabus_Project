using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDustParticleControl : MonoBehaviour
{
    [SerializeField] private bool creatDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticleSystem;

    public void CreateDustParticles()
    {
        if(creatDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}
