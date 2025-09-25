using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class JumpPoint : MonoBehaviour
{
	public WhirldObject whirldObject;

	private int time;

	private int randMin;

	private int randMax;

	private int velocity;

	private float lastBlast;

	public JumpPoint()
	{
		time = 1;
		randMin = 0;
		randMax = 0;
		velocity = 50;
	}

	public void Start()
	{
		if ((bool)whirldObject && whirldObject.@params != null)
		{
			if (RuntimeServices.ToBool(whirldObject.@params["JumpTime"]))
			{
				time = RuntimeServices.UnboxInt32(UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { whirldObject.@params["JumpTime"] }, typeof(MonoBehaviour)));
			}
			if (RuntimeServices.ToBool(whirldObject.@params["JumpRandMin"]))
			{
				randMin = RuntimeServices.UnboxInt32(UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { whirldObject.@params["JumpRandMin"] }, typeof(MonoBehaviour)));
			}
			if (RuntimeServices.ToBool(whirldObject.@params["JumpRandMax"]))
			{
				randMax = RuntimeServices.UnboxInt32(UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { whirldObject.@params["JumpRandMax"] }, typeof(MonoBehaviour)));
			}
			if (RuntimeServices.ToBool(whirldObject.@params["JumpVelocity"]))
			{
				velocity = RuntimeServices.UnboxInt32(UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { whirldObject.@params["JumpVelocity"] }, typeof(MonoBehaviour)));
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != 14)
		{
			lastBlast = Time.time + (float)time;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer != 14 && !(Time.time - 0.1f < lastBlast))
		{
			lastBlast = Time.time;
			if (randMin != 0 && randMax != 0)
			{
				other.attachedRigidbody.AddForce(transform.up * UnityEngine.Random.Range(randMin, randMax), ForceMode.VelocityChange);
			}
			else
			{
				other.attachedRigidbody.AddForce(transform.up * velocity, ForceMode.VelocityChange);
			}
		}
	}

	public void Main()
	{
	}
}
