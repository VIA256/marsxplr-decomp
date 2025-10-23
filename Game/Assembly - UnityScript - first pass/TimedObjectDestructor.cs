using System;
using UnityEngine;

[Serializable]
public class TimedObjectDestructor : MonoBehaviour
{
	public float timeOut = 1.0f;
	public bool detachChildren = false;

	public void Awake()
	{
		Invoke("DestroyNow", timeOut);
	}

	public void DestroyNow()
	{
		if (detachChildren)
		{
			transform.DetachChildren();
		}
		DestroyObject(gameObject);
	}
}
