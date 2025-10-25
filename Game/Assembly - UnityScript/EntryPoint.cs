using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EntryPoint : MonoBehaviour
{
    public IEnumerator Start() 
    {
        yield return new WaitForSeconds(15f);

        ParticleEmitter pe = this.GetComponent<ParticleEmitter>();
        pe.emit = true;
    }
}
