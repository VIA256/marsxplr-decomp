using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class TankTrack : MonoBehaviour
{
	private float motorMinSpeed;

	private float motorMaxAccel;

	private float motorAccelTime;

	private float motorPower;

	private float motorSpeed;

	private float motorSpeedNew;

	private int sideSlipDragForce;

	private int linearDragForce;

	private ContactPoint hit;

	private Transform myTransform;

	private Texture myTexture;

	public Renderer TreadTex;

	public Tank vehicle;

	public bool rightSide;

	public ConfigurableJoint joint;

	public float offset;

	public Vector3 strtPos;

	public TankTrack()
	{
		motorMinSpeed = 100f;
		motorMaxAccel = 0f;
		motorAccelTime = 2.5f;
		motorPower = 0f;
		motorSpeed = 0f;
		motorSpeedNew = 0f;
		sideSlipDragForce = 150;
		linearDragForce = 50;
		rightSide = false;
	}

	public void Start()
	{
		myTransform = transform;
		vehicle = (Tank)transform.parent.transform.parent.gameObject.GetComponent(typeof(Tank));
		joint.connectedBody = vehicle.vehicle.rigidbody;
		strtPos = transform.localPosition;
	}

	public void OnEnable()
	{
		if ((bool)joint.connectedBody)
		{
			rigidbody.isKinematic = true;
			transform.localRotation = Quaternion.identity;
			transform.localPosition = strtPos;
			if (joint.anchor != Vector3.zero)
			{
				joint.anchor = Vector3.zero;
			}
			rigidbody.isKinematic = false;
			rigidbody.velocity = Vector3.zero;
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		if (vehicle.vehicle.zorbBall || RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(collision.collider, "transform"), "root"), transform.root))
		{
			return;
		}
		if ((bool)collision.transform.root && (bool)collision.transform.root.gameObject.rigidbody)
		{
			vehicle.vehicle.OnRam(collision.transform.root.gameObject);
			return;
		}
		if ((bool)collision.rigidbody)
		{
			vehicle.vehicle.OnRam((GameObject)RuntimeServices.Coerce(collision.gameObject, typeof(GameObject)));
			return;
		}
		int i = 0;
		ContactPoint[] contacts = collision.contacts;
		for (int length = contacts.Length; i < length; i = checked(i + 1))
		{
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(contacts[i].otherCollider, "gameObject"), "layer"), 0) || RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(contacts[i].otherCollider, "gameObject"), "layer"), 11))
			{
				gotDirt(contacts[i], collision);
				break;
			}
		}
	}

	public void gotDirt(ContactPoint hit, Collision collision)
	{
		if (transform.InverseTransformPoint(hit.point).y > 0f)
		{
			return;
		}
		Vector3 vector = vehicle.vehicle.myRigidbody.GetPointVelocity(hit.point);
		if (vector.magnitude > 1000f)
		{
			Debug.Log("Crazy Track LocVel: " + vector);
			vector = vector.normalized * 1000f;
		}
		vector = myTransform.InverseTransformDirection(vector);
		offset += Time.deltaTime * vector.z * (1.2f * -1f);
		if (offset > 1f)
		{
			offset -= 1f;
		}
		if (offset < 0f)
		{
			offset += 1f;
		}
		TreadTex.material.SetTextureOffset("_MainTex", new Vector2(0f, offset));
		TreadTex.material.SetTextureOffset("_BumpMap", new Vector2(0f, offset));
		if (vehicle.vehicle.input.y > 0f)
		{
			if (motorSpeed < 0f)
			{
				motorSpeed *= -1f;
			}
			if (motorSpeed < motorMinSpeed)
			{
				motorSpeed = motorMinSpeed;
			}
			motorSpeedNew = Game.Settings.tankPower * (Mathf.Max(0.1f, Game.Settings.tankSpeed - vector.z) / Game.Settings.tankSpeed);
			if (motorSpeedNew > motorSpeed)
			{
				motorSpeed = Mathf.SmoothDamp(motorSpeed, motorSpeedNew, ref motorMaxAccel, motorAccelTime);
			}
			else
			{
				motorSpeed = motorSpeedNew;
			}
			motorPower = motorSpeed;
			if (vehicle.vehicle.input.x != 0f)
			{
				if (rightSide && vehicle.vehicle.input.x > 0f)
				{
					motorPower = 0f;
				}
				else if (!rightSide && vehicle.vehicle.input.x < 0f)
				{
					motorPower = 0f;
				}
			}
		}
		else if (vehicle.vehicle.input.y < 0f)
		{
			if (motorSpeed > 0f)
			{
				motorSpeed *= -1f;
			}
			if (motorSpeed > motorMinSpeed * -1f)
			{
				motorSpeed = motorMinSpeed * -1f;
			}
			motorSpeedNew = Game.Settings.tankPower * -1f * (Mathf.Max(0.1f, Game.Settings.tankSpeed + vector.z) / Game.Settings.tankSpeed);
			if (motorSpeedNew < motorSpeed)
			{
				motorSpeed = Mathf.SmoothDamp(motorSpeed, motorSpeedNew, ref motorMaxAccel, motorAccelTime);
			}
			else
			{
				motorSpeed = motorSpeedNew;
			}
			motorPower = motorSpeed;
			if (vehicle.vehicle.input.x != 0f)
			{
				if (rightSide && vehicle.vehicle.input.x > 0f)
				{
					motorPower = 0f;
				}
				else if (!rightSide && vehicle.vehicle.input.x < 0f)
				{
					motorPower = 0f;
				}
			}
		}
		else if (vehicle.vehicle.input.x != 0f)
		{
			motorPower = Game.Settings.tankPower * vehicle.vehicle.input.x * (0.5f * -1f) * (float)(rightSide ? 1 : (-1));
		}
		else
		{
			motorPower = 0f;
		}
		Vector3 vector2 = new Vector3(vector.x * (float)(sideSlipDragForce * -1), 10f, (!(motorPower < 0.1f * -1f) && !(motorPower > 0.1f)) ? (vector.z * (float)(linearDragForce * -1)) : motorPower);
		rigidbody.AddForceAtPosition(Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)) * vector2, new Vector3(transform.position.x, hit.point.y, hit.point.z) + transform.TransformDirection(Vector3.up * Game.Settings.tankGrip));
	}

	public void Main()
	{
	}
}
