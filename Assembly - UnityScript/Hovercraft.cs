using System;
using UnityEngine;

[Serializable]
public class Hovercraft : MonoBehaviour
{
	public LayerMask thrustMask;

	public Vehicle vehicle;

	private RaycastHit hitDown;

	private float thrustLast;

	private float hoverHeight;

	public Hovercraft()
	{
		thrustMask = -1;
	}

	public void InitVehicle(Vehicle veh)
	{
		vehicle = veh;
	}

	public void FixedUpdate()
	{
		if (vehicle.myRigidbody.isKinematic)
		{
			return;
		}
		int num = 0;
		Vector3 centerOfMass = vehicle.myRigidbody.centerOfMass;
		float num2 = (centerOfMass.x = num);
		Vector3 vector = (vehicle.myRigidbody.centerOfMass = centerOfMass);
		int num3 = 0;
		Vector3 centerOfMass2 = vehicle.myRigidbody.centerOfMass;
		float num4 = (centerOfMass2.z = num3);
		Vector3 vector3 = (vehicle.myRigidbody.centerOfMass = centerOfMass2);
		float y = 0.3f * -1f;
		Vector3 centerOfMass3 = vehicle.myRigidbody.centerOfMass;
		float num5 = (centerOfMass3.y = y);
		Vector3 vector5 = (vehicle.myRigidbody.centerOfMass = centerOfMass3);
		vehicle.myRigidbody.inertiaTensor = new Vector3(15f, 15f, 15f);
		vehicle.myRigidbody.mass = 50f;
		Vector3 vector7 = vehicle.myRigidbody.transform.InverseTransformDirection(vehicle.myRigidbody.velocity);
		if (!vehicle.brakes)
		{
			vehicle.myRigidbody.drag = 0f;
			vehicle.myRigidbody.angularDrag = 4f;
			vehicle.myRigidbody.AddRelativeForce(new Vector3(vector7.x * -10f, 0f, (!(vehicle.input.y > 0f)) ? 0f : (vehicle.input.y * Game.Settings.hoverThrust)));
		}
		else
		{
			if (vehicle.myRigidbody.velocity.magnitude > 5f)
			{
				vehicle.myRigidbody.drag = 1.5f;
			}
			else
			{
				vehicle.myRigidbody.drag = 100f;
			}
			vehicle.myRigidbody.angularDrag = 20f;
		}
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(transform.position, Vector3.up * -1f, out hitInfo, 30f, thrustMask) || transform.position.y < Game.Settings.lavaAlt + 30f)
		{
			if (hitInfo.distance == 0f || hitInfo.distance > Mathf.Max(0f, transform.position.y - Game.Settings.lavaAlt) || transform.position.y < Game.Settings.lavaAlt)
			{
				hitInfo.distance = Mathf.Max(0f, transform.position.y - Game.Settings.lavaAlt);
				hitInfo.normal = Vector3.up;
			}
			hoverHeight = (Physics.Raycast(transform.position, Vector3.up, 5f, thrustMask) ? ((!(hoverHeight > 5f)) ? 5f : (hoverHeight - Time.deltaTime * 3f)) : ((!(hoverHeight < Game.Settings.hoverHeight)) ? Game.Settings.hoverHeight : (hoverHeight + Time.deltaTime * 3f)));
			if (hitInfo.distance < hoverHeight)
			{
				vehicle.myRigidbody.AddForce(transform.up * (hoverHeight - hitInfo.distance) * Game.Settings.hoverHover);
				if (thrustLast > hitInfo.distance)
				{
					vehicle.myRigidbody.AddForce(hitInfo.normal * Mathf.Min((thrustLast - hitInfo.distance) * Game.Settings.hoverRepel, 10f), ForceMode.VelocityChange);
				}
			}
			vehicle.myRigidbody.AddTorque(Vector3.Cross(transform.up, hitInfo.normal) * Vector3.Angle(transform.up, hitInfo.normal) * 0.2f * (40f - Mathf.Min(40f, hitInfo.distance)));
			thrustLast = hitInfo.distance;
		}
		else
		{
			vehicle.myRigidbody.angularDrag = 0.5f;
		}
		vehicle.myRigidbody.AddRelativeTorque(new Vector3(vehicle.input.y * 30f, vehicle.input.x * 100f, ((vehicle.input.x > 0f) ? ((!(((!(transform.eulerAngles.z > 180f)) ? transform.eulerAngles.z : (transform.eulerAngles.z - 360f)) < 40f)) ? 0f : vehicle.input.x) : ((!(((!(transform.eulerAngles.z > 180f)) ? transform.eulerAngles.z : (transform.eulerAngles.z - 360f)) > -40f)) ? 0f : vehicle.input.x)) * -200f));
	}

	public void Main()
	{
	}
}
