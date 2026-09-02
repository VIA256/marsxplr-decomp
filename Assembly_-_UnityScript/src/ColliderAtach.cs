using System;
using UnityEngine;

[Serializable]
public class ColliderAtach : MonoBehaviour
{
	public void Update()
    {
        Debug.Log(collider.attachedRigidbody.gameObject.name);
	}
}
