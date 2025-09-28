using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EntryPoint : MonoBehaviour
{
    public IEnumerator Start() 
    {
        yield return new WaitForSeconds(15.0f);

        ParticleEmitter pe = (ParticleEmitter)GetComponent(typeof(ParticleEmitter));
        pe.emit = true;
    }
}
