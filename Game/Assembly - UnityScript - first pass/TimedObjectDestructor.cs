using System;
using UnityEngine;

[Serializable]
public class TimedObjectDestructor : MonoBehaviour
{
	public float timeOut;

	public bool detachChildren;

	public TimedObjectDestructor()
	{
		timeOut = 1f;
		detachChildren = false;
	}

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
		UnityEngine.Object.DestroyObject(gameObject);
	}

	public void Main()
	{
	}
}
