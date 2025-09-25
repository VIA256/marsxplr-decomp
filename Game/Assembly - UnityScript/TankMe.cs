using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class TankMe : MonoBehaviour
{
	public Vehicle vehicle;

	public bool bellyup;

	public TankMe()
	{
		bellyup = false;
	}

	public void FixedUpdate()
	{
		if (vehicle.myRigidbody.isKinematic)
		{
			return;
		}
		if (vehicle.brakes && vehicle.myRigidbody.velocity.magnitude < 3f)
		{
			vehicle.myRigidbody.velocity = Vector3.zero;
			vehicle.myRigidbody.angularVelocity = Vector3.zero;
			if (vehicle.input.y != 0f)
			{
				vehicle.myRigidbody.drag = 5f;
			}
			else
			{
				vehicle.myRigidbody.drag = 10000f;
			}
		}
		else
		{
			vehicle.myRigidbody.angularDrag = 2f;
			vehicle.myRigidbody.drag = 0.01f;
		}
		if (bellyup && Vector3.Angle(transform.up, Vector3.up) < 35f)
		{
			bellyup = false;
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		if (vehicle.zorbBall)
		{
			return;
		}
		int i = 0;
		ContactPoint[] contacts = collision.contacts;
		for (int length = contacts.Length; i < length; i = checked(i + 1))
		{
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(contacts[i].otherCollider, "gameObject"), "layer"), 0))
			{
				if (!bellyup && vehicle.myRigidbody.velocity.sqrMagnitude < 20f && vehicle.myRigidbody.angularVelocity.sqrMagnitude < 5f && Vector3.Angle(transform.up, contacts[i].normal) > 130f)
				{
					bellyup = true;
				}
				if (bellyup)
				{
					vehicle.myRigidbody.AddForce(Vector3.up * 5000f);
					vehicle.myRigidbody.AddTorque(Vector3.Cross(transform.up, Vector3.up) * 100000f, ForceMode.Acceleration);
				}
			}
		}
	}

	public void OnSetSpecialInput()
	{
	}

	public void Main()
	{
	}
}
