using System;
using UnityEngine;
using System.Threading;

[Serializable]
public class EntryPoint : MonoBehaviour
{
    public void Start() 
    {
        Thread.Sleep(15 * 1000);

        ParticleEmitter pe = (ParticleEmitter)GetComponent(typeof(ParticleEmitter));
        pe.emit = true;
    }
}
