using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class ColliderAtach : MonoBehaviour
{
	public void Update()
	{
		Debug.Log(RuntimeServices.GetProperty(RuntimeServices.GetProperty(RuntimeServices.GetProperty(collider, "attachedRigidbody"), "gameObject"), "name"));
	}

	public void Main()
	{
	}
}
