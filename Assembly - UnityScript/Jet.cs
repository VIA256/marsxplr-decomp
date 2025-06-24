using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class Jet : MonoBehaviour
{
	public Vehicle vehicle;

	public GameObject[] landingGear;

	public float landingGearScale;

	public Transform[] hoverThrusters;

	public ParticleRenderer mainThrusterParticles;

	public ThrustCone mainThrusterCone;

	public Transform[] mainThruster;

	public LayerMask thrustMask;

	public MeshCollider bodyCollider;

	public WhirldLOD lod;

	public int hoverThrustFactor;

	public float hoverSteerFactor;

	public float hoverAngDrag;

	public float hoverLevelForceFactor;

	public int flightThrustFactor;

	public float flightAngDrag;

	public float atmosDensity;

	public Vector3 locvel;

	public float speed;

	public float pitch;

	public float roll;

	public float angleOfAttack;

	public float stallFactor;

	private float grav;

	private float mass;

	private Vector3 inertiaTensor;

	private Quaternion inertiaTensorRotation;

	public float lavaFloat;

	public RaycastHit hit;

	public Jet()
	{
		thrustMask = -1;
		hoverThrustFactor = 1;
		hoverSteerFactor = 0.1f;
		hoverAngDrag = 0.1f;
		hoverLevelForceFactor = 0.2f;
		flightThrustFactor = 5;
		flightAngDrag = 0.01f;
		lavaFloat = 0.1f;
	}

	public void InitVehicle(Vehicle veh)
	{
		vehicle = veh;
		vehicle.specialInput = true;
		mass = vehicle.myRigidbody.mass;
		grav = Physics.gravity.y * -1f * mass;
		inertiaTensor = vehicle.myRigidbody.inertiaTensor;
		inertiaTensorRotation = vehicle.myRigidbody.inertiaTensorRotation;
	}

	public void Update()
	{
		if (!vehicle)
		{
			return;
		}
		checked
		{
			if (vehicle.specialInput)
			{
				int i = 0;
				Transform[] array = hoverThrusters;
				for (int length = array.Length; i < length; i++)
				{
					array[i].gameObject.particleEmitter.emit = true;
					float y = -1f * vehicle.input.z;
					Vector3 localVelocity = array[i].gameObject.particleEmitter.localVelocity;
					float num = (localVelocity.y = y);
					Vector3 vector = (array[i].gameObject.particleEmitter.localVelocity = localVelocity);
					ParticleEmitter obj = array[i].gameObject.particleEmitter;
					float minSize = (array[i].gameObject.particleEmitter.maxSize = Mathf.Max(0.1f, vehicle.input.z * 0.3f));
					obj.minSize = minSize;
				}
				if (lod.level == 0)
				{
					float x = 1f * vehicle.input.x;
					Vector3 localVelocity2 = mainThrusterParticles.gameObject.particleEmitter.localVelocity;
					float num3 = (localVelocity2.x = x);
					Vector3 vector3 = (mainThrusterParticles.gameObject.particleEmitter.localVelocity = localVelocity2);
					float y2 = -1f * vehicle.input.y;
					Vector3 localVelocity3 = mainThrusterParticles.gameObject.particleEmitter.localVelocity;
					float num4 = (localVelocity3.y = y2);
					Vector3 vector5 = (mainThrusterParticles.gameObject.particleEmitter.localVelocity = localVelocity3);
				}
				else
				{
					mainThrusterCone.magThrottle = 0f;
				}
			}
			else
			{
				if (vehicle.brakes)
				{
					vehicle.input.z = 0f;
				}
				if (lod.level == 0)
				{
					int num5 = 0;
					Vector3 localVelocity4 = mainThrusterParticles.gameObject.particleEmitter.localVelocity;
					float num6 = (localVelocity4.y = num5);
					Vector3 vector7 = (mainThrusterParticles.gameObject.particleEmitter.localVelocity = localVelocity4);
					int num7 = num5;
					Vector3 localVelocity5 = mainThrusterParticles.gameObject.particleEmitter.localVelocity;
					float num8 = (localVelocity5.x = num7);
					Vector3 vector9 = (mainThrusterParticles.gameObject.particleEmitter.localVelocity = localVelocity5);
				}
				else
				{
					mainThrusterCone.magThrottle = 4f;
				}
				int j = 0;
				Transform[] array2 = hoverThrusters;
				for (int length2 = array2.Length; j < length2; j++)
				{
					array2[j].gameObject.particleEmitter.emit = false;
				}
			}
			float z = Mathf.Min(-10f * ((!vehicle.specialInput) ? vehicle.input.z : 0.1f), 0.5f * -1f);
			Vector3 localVelocity6 = mainThrusterParticles.gameObject.particleEmitter.localVelocity;
			float num9 = (localVelocity6.z = z);
			Vector3 vector11 = (mainThrusterParticles.gameObject.particleEmitter.localVelocity = localVelocity6);
			if (!(mainThrusterParticles.gameObject.particleEmitter.localVelocity.z < -1f))
			{
				mainThrusterParticles.particleRenderMode = ParticleRenderMode.Billboard;
			}
			else
			{
				mainThrusterParticles.particleRenderMode = ParticleRenderMode.Stretch;
			}
			vehicle.camSmooth = !vehicle.specialInput;
		}
	}

	public void FixedUpdate()
	{
		if (!vehicle)
		{
			return;
		}
		if (vehicle.specialInput)
		{
			GameObject[] array = landingGear;
			if (!array[RuntimeServices.NormalizeArrayIndex(array, lod.level)].active)
			{
				GameObject[] array2 = landingGear;
				array2[RuntimeServices.NormalizeArrayIndex(array2, lod.level)].SetActiveRecursively(true);
			}
			if (landingGearScale < 1f)
			{
				landingGearScale += Time.deltaTime;
			}
		}
		else
		{
			GameObject[] array3 = landingGear;
			if (array3[RuntimeServices.NormalizeArrayIndex(array3, lod.level)].active && landingGearScale > 0f)
			{
				landingGearScale -= Time.deltaTime;
			}
			else
			{
				GameObject[] array4 = landingGear;
				if (array4[RuntimeServices.NormalizeArrayIndex(array4, lod.level)].active)
				{
					GameObject[] array5 = landingGear;
					array5[RuntimeServices.NormalizeArrayIndex(array5, lod.level)].SetActiveRecursively(false);
				}
			}
		}
		GameObject[] array6 = landingGear;
		if (array6[RuntimeServices.NormalizeArrayIndex(array6, lod.level)].active)
		{
			float num = landingGearScale;
			GameObject[] array7 = landingGear;
			Vector3 localScale = array7[RuntimeServices.NormalizeArrayIndex(array7, lod.level)].transform.localScale;
			float num2 = (localScale.x = num);
			GameObject[] array8 = landingGear;
			Vector3 vector = (array8[RuntimeServices.NormalizeArrayIndex(array8, lod.level)].transform.localScale = localScale);
			float y = num;
			GameObject[] array9 = landingGear;
			Vector3 localScale2 = array9[RuntimeServices.NormalizeArrayIndex(array9, lod.level)].transform.localScale;
			float num3 = (localScale2.y = y);
			GameObject[] array10 = landingGear;
			Vector3 vector3 = (array10[RuntimeServices.NormalizeArrayIndex(array10, lod.level)].transform.localScale = localScale2);
		}
		if (vehicle.myRigidbody.isKinematic)
		{
			return;
		}
		vehicle.myRigidbody.centerOfMass = Vector3.zero;
		vehicle.myRigidbody.inertiaTensor = inertiaTensor * 0.75f;
		vehicle.myRigidbody.inertiaTensorRotation = inertiaTensorRotation;
		checked
		{
			if (vehicle.specialInput)
			{
				vehicle.myRigidbody.AddForce(transform.up * vehicle.input.z * hoverThrustFactor * grav);
				vehicle.myRigidbody.AddTorque(Vector3.Cross(transform.up, Vector3.up) * Vector3.Angle(transform.up, Vector3.up) * hoverLevelForceFactor * mass);
				vehicle.myRigidbody.AddRelativeTorque(new Vector3(vehicle.input.y * mass * hoverSteerFactor, Mathf.Clamp(vehicle.input.x + vehicle.input.w, -1f, 1f) * mass * hoverSteerFactor, vehicle.input.x * -1f * mass * hoverSteerFactor));
				vehicle.myRigidbody.drag = Game.Settings.jetHDrag * vehicle.myRigidbody.velocity.magnitude * (float)((!vehicle.brakes) ? 1 : 7);
				vehicle.myRigidbody.angularDrag = hoverAngDrag * (float)((!vehicle.brakes) ? 1 : 5);
				Transform[] array11 = mainThruster;
				array11[RuntimeServices.NormalizeArrayIndex(array11, lod.level)].localEulerAngles = Vector3.zero;
			}
			else
			{
				if (vehicle.brakes)
				{
					vehicle.input.z = 0f;
				}
				if (vehicle.myRigidbody.transform.position.y < 5000f)
				{
					atmosDensity = Mathf.Lerp(1.225f, 0.18756f, vehicle.myRigidbody.transform.position.y / 5000f);
				}
				else
				{
					atmosDensity = Mathf.Lerp(0.18756f, 0.017102f, vehicle.myRigidbody.transform.position.y / 10000f);
				}
				speed = vehicle.myRigidbody.velocity.magnitude;
				pitch = ((!(transform.eulerAngles.x > 180f)) ? transform.eulerAngles.x : (transform.eulerAngles.x - 360f));
				roll = ((!(transform.eulerAngles.z > 180f)) ? transform.eulerAngles.z : (transform.eulerAngles.z - 360f));
				locvel = vehicle.myRigidbody.transform.InverseTransformDirection(vehicle.myRigidbody.velocity);
				angleOfAttack = locvel.normalized.y;
				if (speed < (float)Game.Settings.jetStall)
				{
					stallFactor = Mathf.Lerp(1f, 0f, (speed - (float)Game.Settings.jetStall * 0.8f) / (float)Game.Settings.jetStall * 10f);
				}
				else
				{
					stallFactor = Mathf.Max(0f, Mathf.Min(Mathf.Abs(angleOfAttack) - 0.65f, 0.1f)) * 10f;
				}
				Transform[] array12 = mainThruster;
				array12[RuntimeServices.NormalizeArrayIndex(array12, lod.level)].localEulerAngles = new Vector3(vehicle.input.y * -1f * Mathf.Lerp(20f, 5f, speed / (float)(Game.Settings.jetStall * 5)), vehicle.input.x * -1f * 1f + ((vehicle.input.w != 0f) ? Mathf.Clamp(locvel.x * -1f * 0.5f, -10f, 10f) : Mathf.Clamp(locvel.x * -1f * 1f, -10f, 10f)) + vehicle.input.w * -1f * 15f, 0f);
				Rigidbody myRigidbody = vehicle.myRigidbody;
				Transform[] array13 = mainThruster;
				myRigidbody.AddForceAtPosition(array13[RuntimeServices.NormalizeArrayIndex(array13, lod.level)].forward * vehicle.input.z * flightThrustFactor * grav * 0.99f, mainThruster[0].position);
				vehicle.myRigidbody.AddRelativeTorque(new Vector3(vehicle.input.y * mass * (float)Game.Settings.jetSteer * 0.2f, 0f, vehicle.input.x * -1f * mass * (float)Game.Settings.jetSteer * 0.75f) * Mathf.Lerp(0f, 1f, speed / (float)Game.Settings.jetStall * 0.7f) * atmosDensity * ((locvel.z > 0f) ? 1 : (-1)));
				vehicle.myRigidbody.angularDrag = flightAngDrag;
				if (stallFactor < 1f)
				{
					int num4 = 15;
					float num5 = ((!(angleOfAttack > 0f)) ? Mathf.Max(0.3f * -1f, angleOfAttack * -1f) : (Mathf.Min(0.3f, angleOfAttack) * -1f));
					float a = Game.Settings.jetLift * atmosDensity * locvel.z * locvel.z * (float)num4 * num5;
					vehicle.myRigidbody.AddRelativeForce(Vector3.up * Mathf.Lerp(a, 0f, stallFactor));
				}
				if (!(stallFactor < 0.5f))
				{
					vehicle.myRigidbody.drag = speed * Mathf.Lerp(Game.Settings.jetDrag, Game.Settings.jetDrag * 5f, Vector3.Angle(vehicle.myRigidbody.velocity, vehicle.myRigidbody.transform.forward) / 90f) * atmosDensity;
				}
				else
				{
					vehicle.myRigidbody.drag = 0f;
					vehicle.myRigidbody.AddRelativeForce(new Vector3(locvel.x * (Game.Settings.jetDrag * -1f) * 3f, locvel.y * (Game.Settings.jetDrag * -1f) * 3f, locvel.z * (Game.Settings.jetDrag * -1f)) * atmosDensity * ((!vehicle.brakes) ? 1 : 5), ForceMode.VelocityChange);
				}
			}
			if (!(transform.position.y < Game.Settings.lavaAlt + 20f) && !Physics.Raycast(transform.position + Vector3.up * 200f, Vector3.down, out hit, 220f, 1 << 4))
			{
				return;
			}
			Vector3 vector5 = Vector3.up * 200f;
			Vector3 direction = Vector3.up * -1f;
			RaycastHit hitInfo = default(RaycastHit);
			int i = 0;
			Vector3[] vertices = bodyCollider.sharedMesh.vertices;
			for (int length = vertices.Length; i < length; i++)
			{
				float num6 = default(float);
				vertices[i] = transform.TransformPoint(vertices[i]);
				if (vertices[i].y < Game.Settings.lavaAlt)
				{
					num6 = (vertices[i].y - Game.Settings.lavaAlt) * -1f;
				}
				else
				{
					if (hit.distance == 0f || !hit.collider.Raycast(new Ray(vertices[i] + vector5, direction), out hitInfo, 200f))
					{
						continue;
					}
					num6 = 200f - hitInfo.distance;
				}
				Vector3 pointVelocity = vehicle.myRigidbody.GetPointVelocity(vertices[i]);
				vehicle.myRigidbody.AddForceAtPosition((Vector3.up * lavaFloat * Mathf.Min(6f, 3f + num6) * Mathf.Lerp(1.3f, 5f, new Vector2(pointVelocity.x, pointVelocity.z).magnitude / 20f) + pointVelocity * (Game.Settings.jetDrag * -1f) * 70f) / bodyCollider.sharedMesh.vertexCount, vertices[i], ForceMode.VelocityChange);
			}
		}
	}

	public void Main()
	{
	}
}
