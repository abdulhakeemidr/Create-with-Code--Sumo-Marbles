using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleEffect : MonoBehaviour
{
    private ParticleSystem particleEffect;

    void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!particleEffect.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
