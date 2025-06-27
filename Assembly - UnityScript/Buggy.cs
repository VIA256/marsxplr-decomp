using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Buggy : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class OnSetSpecialInput_0024105 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal Buggy _0024self_538;

			public _0024(Buggy self_)
			{
				_0024self_538 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					if (!_0024self_538.vehicle)
					{
						return Yield(2, null);
					}
					_0024self_538.vehicle.camSmooth = _0024self_538.vehicle.specialInput;
					if (_0024self_538.vehicle.specialInput)
					{
						_0024self_538.wingState = 1f;
						_0024self_538.wingFlaps = 0;
					}
					else
					{
						_0024self_538.wingState = -1f;
						_0024self_538.wingFlaps = 0;
					}
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Buggy _0024self_539;

		public OnSetSpecialInput_0024105(Buggy self_)
		{
			_0024self_539 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024self_539);
		}
	}

	public MeshFilter wing0;

	public MeshFilter wing1;

	public Transform floatPoints;

	public GameObject wheel;

	public GameObject axel;

	public Transform leftTrail;

	public Transform rightTrail;

	public GameObject WheelmarksPrefab;

	public Collider buggyCollider;

	public WhirldLOD lod;

	public Transform[] wheels;

	public Transform[] wheelGraphics;

	public Transform[] axels;

	public Vector3 wheelPos;

	public Vector3 axelPos;

	private Vector3[] baseVertices;

	private Vector3[] baseNormals;

	private Mesh wingMesh;

	private float wingState;

	private int wingFlaps;

	private bool wingOpen;

	private bool isInverted;

	private Vehicle vehicle;

	private Transform[] bouyancyPoints;

	private float suspensionRange;

	private float friction;

	private float[] realComp;

	private float[] hitDistance;

	private float[] hitCompress;

	private float[] hitFriction;

	private Vector3[] hitVelocity;

	private Vector3[] wheelPositn;

	private Vector3[] hitForce;

	private Skidmarks wheelMarks;

	private int[] wheelMarkIndex;

	private bool isDynamic;

	private float frictionTotal;

	private float brakePower;

	private float motorTorque;

	private float motorSpeed;

	private float motorSpd;

	private float motorInputSmoothed;

	private float wheelRadius;

	private float wheelCircumference;

	private int motorMass;

	private int motorDrag;

	private int maxAcceleration;

	private int motorAccel;

	private float motorSpeedNew;

	public Buggy()
	{
		wingState = 0f;
		wingFlaps = 0;
		isInverted = false;
		realComp = new float[4];
		hitDistance = new float[4];
		hitCompress = new float[4];
		hitFriction = new float[4];
		hitVelocity = new Vector3[4];
		wheelPositn = new Vector3[4];
		hitForce = new Vector3[4];
		wheelMarkIndex = new int[4];
		isDynamic = false;
		motorSpeed = 0f;
		motorSpd = 0f;
		motorInputSmoothed = 0f;
		wheelRadius = 0.3f;
		wheelCircumference = wheelRadius * (float)Math.PI * 2f;
		motorMass = 1;
		motorDrag = 1;
		maxAcceleration = 60;
		motorAccel = 60;
		motorSpeedNew = 0f;
	}

	public void InitVehicle(Vehicle veh)
	{
		vehicle = veh;
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		array.Add(wing0.renderer.material);
		array.Add(wing1.renderer.material);
		int i;
		for (i = 0; i < 4; i = checked(i + 1))
		{
			Vector3[] array2 = wheelPositn;
			array2[RuntimeServices.NormalizeArrayIndex(array2, i)] = new Vector3(wheelPos.x * (float)((i % 2 != 0) ? 1 : (-1)), wheelPos.y, wheelPos.z * (float)((i < 2) ? 1 : (-1)));
			GameObject original = wheel;
			Transform obj = this.transform;
			Vector3[] array3 = wheelPositn;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original, obj.TransformPoint(array3[RuntimeServices.NormalizeArrayIndex(array3, i)]), this.transform.rotation);
			Transform[] array4 = wheels;
			array4[RuntimeServices.NormalizeArrayIndex(array4, i)] = gameObject.transform;
			Transform[] array5 = wheelGraphics;
			int num = RuntimeServices.NormalizeArrayIndex(array5, i);
			Transform[] array6 = wheels;
			array5[num] = array6[RuntimeServices.NormalizeArrayIndex(array6, i)].Find("Graphic").transform;
			if (i == 1 || i == 3)
			{
				int num2 = 0;
				Transform[] array7 = wheels;
				Vector3 localEulerAngles = array7[RuntimeServices.NormalizeArrayIndex(array7, i)].Find("Graphic/Simple").transform.localEulerAngles;
				float num3 = (localEulerAngles.y = num2);
				Transform[] array8 = wheels;
				Vector3 vector = (array8[RuntimeServices.NormalizeArrayIndex(array8, i)].Find("Graphic/Simple").transform.localEulerAngles = localEulerAngles);
			}
			Transform[] array9 = wheelGraphics;
			MeshRenderer meshRenderer = (MeshRenderer)array9[RuntimeServices.NormalizeArrayIndex(array9, i)].Find("Detailed/Beadlock").GetComponent(typeof(MeshRenderer));
			array.Add(meshRenderer.material);
			Transform[] array10 = wheels;
			array10[RuntimeServices.NormalizeArrayIndex(array10, i)].parent = this.transform;
			gameObject = (GameObject)UnityEngine.Object.Instantiate(axel, this.transform.TransformPoint(new Vector3(axelPos.x * (float)((i % 2 != 0) ? 1 : (-1)), axelPos.y, axelPos.z * (float)((i < 2) ? 1 : (-1)))), this.transform.rotation);
			Transform[] array11 = axels;
			array11[RuntimeServices.NormalizeArrayIndex(array11, i)] = gameObject.transform;
			Transform[] array12 = axels;
			array12[RuntimeServices.NormalizeArrayIndex(array12, i)].parent = this.transform;
		}
		if (vehicle.isPlayer)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(WheelmarksPrefab, Vector3.zero, Quaternion.identity);
			gameObject.layer = 11;
			wheelMarks = (Skidmarks)gameObject.GetComponentInChildren(typeof(Skidmarks));
		}
		else
		{
			GameObject obj2 = leftTrail.gameObject;
			bool flag = (rightTrail.gameObject.active = false);
			obj2.active = flag;
		}
		i = 0;
		bouyancyPoints = new Transform[floatPoints.childCount];
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(floatPoints);
		while (enumerator.MoveNext())
		{
			Transform transform = (Transform)RuntimeServices.Coerce(enumerator.Current, typeof(Transform));
			Transform[] array13 = bouyancyPoints;
			array13[RuntimeServices.NormalizeArrayIndex(array13, i)] = transform;
			UnityRuntimeServices.Update(enumerator, transform);
			i = checked(i + 1);
		}
		vehicle.materialAccent = (Material[])array.ToBuiltin(typeof(Material));
	}

	public void Update()
	{
		if (wingState != 0f)
		{
			wingState += Time.deltaTime * 2f;
			if (!(wingState < 1f))
			{
				wingOpen = true;
				if (wingState > 2f)
				{
					wingState = 0f;
				}
			}
			else if (wingState > 0f)
			{
				wingOpen = false;
				int num = 0;
				Vector3 localPosition = leftTrail.localPosition;
				float num2 = (localPosition.x = num);
				Vector3 vector = (leftTrail.localPosition = localPosition);
				int num3 = 0;
				Vector3 localPosition2 = rightTrail.localPosition;
				float num4 = (localPosition2.x = num3);
				Vector3 vector3 = (rightTrail.localPosition = localPosition2);
				wingState = 0f;
			}
		}
		wingMesh = ((lod.level != 0) ? wing1 : wing0).mesh;
		((lod.level != 0) ? wing1 : wing0).gameObject.active = wingOpen;
		if (wingOpen)
		{
			if (baseVertices == null)
			{
				baseVertices = wingMesh.vertices;
			}
			Vector3[] array = new Vector3[baseVertices.Length];
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				Vector3[] array2 = baseVertices;
				Vector3 vector5 = array2[RuntimeServices.NormalizeArrayIndex(array2, i)];
				if ((!(wingState < -1f) && wingState < 0f) || (!(wingState < 1f) && wingState < 2f))
				{
					if (wingState > 0f)
					{
						vector5.y *= wingState - 1f;
						vector5.x *= wingState - 1f;
						float x = (wingState - 1f) * (3.5f * -1f);
						Vector3 localPosition3 = leftTrail.localPosition;
						float num5 = (localPosition3.x = x);
						Vector3 vector6 = (leftTrail.localPosition = localPosition3);
						float x2 = (wingState - 1f) * 3.5f;
						Vector3 localPosition4 = rightTrail.localPosition;
						float num6 = (localPosition4.x = x2);
						Vector3 vector8 = (rightTrail.localPosition = localPosition4);
					}
					else
					{
						vector5.y *= wingState;
						vector5.x *= wingState;
						float x3 = wingState * 3.5f;
						Vector3 localPosition5 = leftTrail.localPosition;
						float num7 = (localPosition5.x = x3);
						Vector3 vector10 = (leftTrail.localPosition = localPosition5);
						float x4 = wingState * (3.5f * -1f);
						Vector3 localPosition6 = rightTrail.localPosition;
						float num8 = (localPosition6.x = x4);
						Vector3 vector12 = (rightTrail.localPosition = localPosition6);
					}
				}
				else
				{
					float num9 = vector5.z * (vehicle.input.x * 0.14f);
					num9 = ((!(vector5.z > 0.2f)) ? (num9 * (Mathf.Abs(vector5.x) / 10f)) : 0f);
					num9 += vector5.x * (motorInputSmoothed * 0.04f);
					float num10 = Mathf.Sin(num9);
					float num11 = Mathf.Cos(num9);
					vector5.x = vector5.y * num10 + vector5.x * num11;
					vector5.y = vector5.y * num11 - vector5.x * num10;
				}
				array[RuntimeServices.NormalizeArrayIndex(array, i)] = vector5;
			}
			if ((wingState < -1f || !(wingState < 0f)) && (wingState < 1f || !(wingState < 2f)))
			{
				float x5 = 3.5f * -1f;
				Vector3 localPosition7 = leftTrail.localPosition;
				float num12 = (localPosition7.x = x5);
				Vector3 vector14 = (leftTrail.localPosition = localPosition7);
				float x6 = 3.5f;
				Vector3 localPosition8 = rightTrail.localPosition;
				float num13 = (localPosition8.x = x6);
				Vector3 vector16 = (rightTrail.localPosition = localPosition8);
			}
			wingMesh.vertices = array;
		}
		else
		{
			int num14 = 0;
			Vector3 localPosition9 = rightTrail.localPosition;
			float num15 = (localPosition9.x = num14);
			Vector3 vector18 = (rightTrail.localPosition = localPosition9);
			int num16 = num14;
			Vector3 localPosition10 = leftTrail.localPosition;
			float num17 = (localPosition10.x = num16);
			Vector3 vector20 = (leftTrail.localPosition = localPosition10);
		}
		for (int i = 0; i < 4; i = checked(i + 1))
		{
			float x7 = wheelPos.x * (float)((i % 2 != 0) ? 1 : (-1));
			float y = wheelPos.y;
			float[] array3 = hitDistance;
			float num18;
			if (array3[RuntimeServices.NormalizeArrayIndex(array3, i)] == -1f)
			{
				num18 = suspensionRange;
			}
			else
			{
				float[] array4 = hitDistance;
				num18 = array4[RuntimeServices.NormalizeArrayIndex(array4, i)] - wheelRadius;
			}
			Vector3 vector5 = new Vector3(x7, y - num18, wheelPos.z * (float)((i < 2) ? 1 : (-1)));
			Transform[] array5 = wheels;
			array5[RuntimeServices.NormalizeArrayIndex(array5, i)].transform.position = transform.TransformPoint(vector5);
			Transform[] array6 = wheelGraphics;
			array6[RuntimeServices.NormalizeArrayIndex(array6, i)].transform.Rotate(360f * (motorSpeed / wheelCircumference) * Time.deltaTime * 0.5f, 0f, 0f);
			Transform[] array7 = axels;
			if (array7[RuntimeServices.NormalizeArrayIndex(array7, i)].gameObject.active)
			{
				Transform[] array8 = axels;
				Transform obj = array8[RuntimeServices.NormalizeArrayIndex(array8, i)];
				Transform[] array9 = wheels;
				obj.LookAt(array9[RuntimeServices.NormalizeArrayIndex(array9, i)].position);
			}
		}
	}

	public void FixedUpdate()
	{
		bool flag = false;
		if (Game.Settings.buggySmartSuspension)
		{
			if ((Game.Settings.buggyNewPhysics || wingOpen) && !Physics.Raycast(transform.position, Vector3.up * -1f, 5f, vehicle.terrainMask))
			{
				if (suspensionRange > 0.01f)
				{
					suspensionRange -= Time.deltaTime * 0.5f;
				}
				else
				{
					suspensionRange = 0f;
				}
			}
			else
			{
				suspensionRange = Mathf.Lerp(suspensionRange, (!wingOpen) ? Mathf.Lerp(0.4f, 0.2f, Mathf.Min(1f, ((!Game.Settings.buggyNewPhysics) ? Mathf.Abs(motorSpeed) : vehicle.myRigidbody.velocity.magnitude) / Game.Settings.buggySpeed)) : 0.5f, Time.deltaTime * 3f);
			}
		}
		else
		{
			suspensionRange = 0.4f;
		}
		float z = 0.2f;
		Vector3 centerOfMass = vehicle.myRigidbody.centerOfMass;
		float num = (centerOfMass.z = z);
		Vector3 vector = (vehicle.myRigidbody.centerOfMass = centerOfMass);
		float y = Game.Settings.buggyCG * suspensionRange * 0.5f;
		Vector3 centerOfMass2 = vehicle.myRigidbody.centerOfMass;
		float num2 = (centerOfMass2.y = y);
		Vector3 vector3 = (vehicle.myRigidbody.centerOfMass = centerOfMass2);
		vehicle.myRigidbody.mass = 30f;
		RaycastHit hitInfo = default(RaycastHit);
		checked
		{
			if (vehicle.myRigidbody.isKinematic)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform obj = transform;
					Vector3[] array = wheelPositn;
					if (Physics.Raycast(obj.TransformPoint(array[RuntimeServices.NormalizeArrayIndex(array, i)]), transform.up * -1f, out hitInfo, suspensionRange + wheelRadius, vehicle.terrainMask))
					{
						Vector3[] array2 = hitVelocity;
						motorSpeed = array2[RuntimeServices.NormalizeArrayIndex(array2, i)].z;
						float[] array3 = hitDistance;
						array3[RuntimeServices.NormalizeArrayIndex(array3, i)] = hitInfo.distance;
					}
					else
					{
						float[] array4 = hitDistance;
						array4[RuntimeServices.NormalizeArrayIndex(array4, i)] = -1f;
					}
				}
				return;
			}
			if (wingOpen)
			{
				motorInputSmoothed = Mathf.Lerp(vehicle.input.y, motorInputSmoothed + (float)(vehicle.brakes ? (-1) : 0), 0.8f);
				int num3 = 16;
				Vector3 vector5 = transform.InverseTransformDirection(vehicle.myRigidbody.velocity);
				float num4 = ((!(transform.eulerAngles.z > 180f)) ? transform.eulerAngles.z : (transform.eulerAngles.z - 360f));
				float num5 = ((!(transform.eulerAngles.x > 180f)) ? transform.eulerAngles.x : (transform.eulerAngles.x - 360f));
				if (vector5.sqrMagnitude > (float)num3)
				{
					vehicle.myRigidbody.drag = vehicle.myRigidbody.velocity.magnitude / Game.Settings.buggyFlightDrag * 0.3f;
					if (vehicle.brakes)
					{
						if (brakePower < 1f)
						{
							brakePower += Time.deltaTime * 0.15f;
						}
						float num6 = brakePower * -1f * 2f;
						vehicle.myRigidbody.AddRelativeForce(vector5.x * num6 * 5f, vector5.y * num6 * 100f, vector5.z * 150f * num6);
						vehicle.myRigidbody.AddRelativeTorque(new Vector3((num5 + vehicle.input.y * -100f) * -2f, vehicle.input.x * 280f, num4 * -1f));
					}
					else
					{
						brakePower = 0f;
						float num7 = Vector3.Angle(vehicle.myRigidbody.velocity, transform.TransformDirection(Vector3.forward));
						if (num7 > 10f && Game.Settings.buggyFlightSlip)
						{
							vehicle.myRigidbody.velocity = vehicle.myRigidbody.transform.TransformDirection(vector5.x * 0.95f, vector5.y * 0.95f, vector5.z + (Mathf.Abs(vector5.x) + Mathf.Abs(vector5.y)) * 0.1f * (num7 / 360f));
						}
						else
						{
							vehicle.myRigidbody.velocity = vehicle.myRigidbody.transform.TransformDirection(0f, 0f, vector5.magnitude + Time.deltaTime * 50f * (Game.Settings.buggyFlightLooPower ? (Mathf.Abs(motorInputSmoothed) / 10f) : ((!(motorInputSmoothed < 0.999f) || !(motorInputSmoothed > 0.999f * -1f)) ? 0f : (Mathf.Abs(motorInputSmoothed) / 10f))));
						}
						vehicle.myRigidbody.AddRelativeTorque(new Vector3(motorInputSmoothed * 100f * Game.Settings.buggyFlightAgility, 0f, vehicle.input.x * -100f * Game.Settings.buggyFlightAgility));
					}
					if (vehicle.input.x == 0f && (transform.eulerAngles.z < 90f || transform.eulerAngles.z > 270f))
					{
						vehicle.myRigidbody.AddRelativeTorque(((!(transform.eulerAngles.x < 10f) && !(transform.eulerAngles.x > 350f)) ? 0f : (num5 - 0.95f)) * 0f, num4 * (0.6f * -1f), (!(transform.eulerAngles.z < 20f) && !(transform.eulerAngles.z > 340f)) ? 0f : (num4 * (0.5f * -1f)));
					}
					else if (vehicle.input.x == 0f)
					{
						vehicle.myRigidbody.AddRelativeTorque(0f, (transform.eulerAngles.z - 180f) * 0.4f, 0f);
					}
					if (transform.position.y < Game.Settings.lavaAlt + 10f || Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f, 1 << 4))
					{
						vehicle.myRigidbody.AddForce(Vector3.up * (10f - hitInfo.distance) * 40f);
					}
					vehicle.myRigidbody.angularDrag = 5f;
				}
				else
				{
					vehicle.myRigidbody.angularDrag = 1f;
					vehicle.myRigidbody.drag = vehicle.myRigidbody.velocity.magnitude / Game.Settings.buggyFlightDrag * 9f;
					vehicle.myRigidbody.AddRelativeTorque(new Vector3(vehicle.input.y + 0.5f * 100f, 0f, vehicle.input.x * -30f));
				}
			}
			else if (vehicle.brakes && vehicle.myRigidbody.velocity.magnitude < 1.5f)
			{
				if (vehicle.input.y != 0f)
				{
					vehicle.myRigidbody.drag = 2f;
				}
				else
				{
					vehicle.myRigidbody.drag = 50f;
				}
				vehicle.myRigidbody.angularDrag = 1f;
			}
			else if (vehicle.brakes && vehicle.myRigidbody.velocity.magnitude < 10f)
			{
				if (vehicle.input.y != 0f)
				{
					vehicle.myRigidbody.drag = 2f;
				}
				else
				{
					vehicle.myRigidbody.drag = 10f;
				}
			}
			else
			{
				vehicle.myRigidbody.angularDrag = 0.2f;
				vehicle.myRigidbody.drag = 0.01f;
			}
			float num8 = Mathf.Lerp(40f, 30f, vehicle.myRigidbody.velocity.magnitude / Game.Settings.buggySpeed);
			Transform obj2 = wheels[0];
			Quaternion localRotation = (wheels[1].localRotation = Quaternion.LookRotation(new Vector3(vehicle.input.x * (num8 / 90f), 0f, 1f + -1f * Mathf.Abs(vehicle.input.x * (num8 / 90f)))));
			obj2.localRotation = localRotation;
			num8 = Mathf.Lerp(20f, 0f, vehicle.myRigidbody.velocity.magnitude / Game.Settings.buggySpeed);
			Transform obj3 = wheels[2];
			Quaternion localRotation2 = (wheels[3].localRotation = Quaternion.LookRotation(new Vector3(vehicle.input.x * -1f * (num8 / 90f), 0f, 1f + -1f * Mathf.Abs(vehicle.input.x * (num8 / 90f)))));
			obj3.localRotation = localRotation2;
			if (Game.Settings.buggyNewPhysics)
			{
				motorTorque = vehicle.input.y * -1f * Mathf.Lerp(Game.Settings.buggyPower * 3f, 0f, hitVelocity[0].z / Game.Settings.buggySpeed);
				frictionTotal = 0f;
				for (int i = 0; i < 4; i++)
				{
					Transform obj4 = transform;
					Vector3[] array5 = wheelPositn;
					if (Physics.Raycast(obj4.TransformPoint(array5[RuntimeServices.NormalizeArrayIndex(array5, i)]), transform.up * -1f, out hitInfo, suspensionRange + wheelRadius, vehicle.terrainMask))
					{
						if (motorTorque != 0f)
						{
							float num9 = motorTorque;
							float[] array6 = hitFriction;
							float num10 = array6[RuntimeServices.NormalizeArrayIndex(array6, i)];
							Vector3[] array7 = hitForce;
							if (!(num9 < num10 * array7[RuntimeServices.NormalizeArrayIndex(array7, i)].z))
							{
								float buggySpeed = Game.Settings.buggySpeed;
								float b = 0f;
								float num11 = motorTorque;
								float[] array8 = hitFriction;
								float num12 = array8[RuntimeServices.NormalizeArrayIndex(array8, i)];
								Vector3[] array9 = hitForce;
								motorSpeed = Mathf.Lerp(buggySpeed, b, (num11 - num12 * array9[RuntimeServices.NormalizeArrayIndex(array9, i)].z) / motorTorque);
								goto IL_0d64;
							}
						}
						Vector3[] array10 = hitVelocity;
						motorSpeed = array10[RuntimeServices.NormalizeArrayIndex(array10, i)].z;
						goto IL_0d64;
					}
					float[] array11 = hitDistance;
					array11[RuntimeServices.NormalizeArrayIndex(array11, i)] = -1f;
					int[] array12 = wheelMarkIndex;
					array12[RuntimeServices.NormalizeArrayIndex(array12, i)] = -1;
					continue;
					IL_0d64:
					motorSpd = (frictionTotal - Game.Settings.buggyPower * 3f) / (Game.Settings.buggyPower * 3f / Game.Settings.buggySpeed);
					flag = true;
					float num13 = motorTorque;
					float[] array13 = hitFriction;
					bool num14 = num13 > array13[RuntimeServices.NormalizeArrayIndex(array13, i)];
					if (!num14)
					{
						Vector3[] array14 = hitVelocity;
						float num15 = Mathf.Abs(array14[RuntimeServices.NormalizeArrayIndex(array14, i)].x);
						Vector3[] array15 = hitVelocity;
						num14 = num15 > Mathf.Abs(array15[RuntimeServices.NormalizeArrayIndex(array15, i)].z) * 0.3f;
					}
					isDynamic = num14;
					float[] array16 = hitDistance;
					array16[RuntimeServices.NormalizeArrayIndex(array16, i)] = hitInfo.distance;
					float[] array17 = hitCompress;
					array17[RuntimeServices.NormalizeArrayIndex(array17, i)] = hitInfo.distance / (suspensionRange + wheelRadius) * -1f + 1f;
					Vector3[] array18 = hitVelocity;
					Transform[] array19 = wheels;
					Transform obj5 = array19[RuntimeServices.NormalizeArrayIndex(array19, i)];
					Rigidbody myRigidbody = vehicle.myRigidbody;
					Transform obj6 = transform;
					Vector3[] array20 = wheelPositn;
					hitVelocity[i] = obj5.InverseTransformDirection(myRigidbody.GetPointVelocity(obj6.TransformPoint(array20[RuntimeServices.NormalizeArrayIndex(array20, i)])));
					if (isDynamic)
					{
						float[] array21 = hitFriction;
						array21[RuntimeServices.NormalizeArrayIndex(array21, i)] = Game.Settings.buggyTr * 60f;
					}
					else
					{
						float[] array22 = hitFriction;
						int num16 = RuntimeServices.NormalizeArrayIndex(array22, i);
						float num17 = Game.Settings.buggyTr * 150f;
						float[] array23 = hitCompress;
						array22[num16] = num17 * Mathf.Lerp(1.5f, 0.5f, Mathf.Min(array23[RuntimeServices.NormalizeArrayIndex(array23, i)] * 3f, 1f));
					}
					Vector3[] array24 = hitVelocity;
					float x = array24[RuntimeServices.NormalizeArrayIndex(array24, i)].x;
					float y2 = 0f;
					float z2;
					if (Game.Settings.buggyAWD || i > 1)
					{
						Vector3[] array25 = hitVelocity;
						z2 = array25[RuntimeServices.NormalizeArrayIndex(array25, i)].z - motorSpeed;
					}
					else
					{
						z2 = 0f;
					}
					Vector3 vector6 = new Vector3(x, y2, z2);
					if (vector6.magnitude > 1f)
					{
						vector6 = vector6.normalized;
					}
					Vector3[] array26 = hitForce;
					array26[RuntimeServices.NormalizeArrayIndex(array26, i)] = vector6;
					Transform[] array27 = wheels;
					Transform obj7 = array27[RuntimeServices.NormalizeArrayIndex(array27, i)];
					Vector3 vector7 = vector6;
					float[] array28 = hitFriction;
					Vector3 force = obj7.TransformDirection(vector7 * (array28[RuntimeServices.NormalizeArrayIndex(array28, i)] * -1f));
					vehicle.myRigidbody.AddForceAtPosition(force, hitInfo.point);
					if ((bool)wheelMarks)
					{
						int[] array29 = wheelMarkIndex;
						int num18 = RuntimeServices.NormalizeArrayIndex(array29, i);
						Skidmarks skidmarks = wheelMarks;
						Vector3 point = hitInfo.point;
						Vector3 normal = hitInfo.normal;
						float intensity = ((!isDynamic) ? Mathf.Min(0.5f, force.magnitude * 0.0025f) : 1f);
						int[] array30 = wheelMarkIndex;
						array29[num18] = skidmarks.AddSkidMark(point, normal, intensity, array30[RuntimeServices.NormalizeArrayIndex(array30, i)]);
					}
					float num19 = frictionTotal;
					float[] array31 = hitFriction;
					frictionTotal = num19 + array31[RuntimeServices.NormalizeArrayIndex(array31, i)];
				}
			}
			else
			{
				motorTorque = Mathf.Max(1f, Mathf.Lerp(Game.Settings.buggyPower * 5f, 0f, motorSpeed / (Game.Settings.buggySpeed * 10f)) * Mathf.Abs((!wingOpen) ? vehicle.input.y : 0f));
				motorAccel = (int)Mathf.Lerp(maxAcceleration, 0f, motorSpeed / (Game.Settings.buggySpeed * 10f));
				motorSpeed += vehicle.input.y * (float)motorAccel / (float)motorMass * Time.fixedDeltaTime;
				motorSpeed += motorSpeed * -1f * (float)((!vehicle.brakes) ? motorDrag : 50) / motorTorque * Time.fixedDeltaTime;
				for (int i = 0; i < 4; i++)
				{
					Transform obj8 = transform;
					Vector3[] array32 = wheelPositn;
					if (Physics.Raycast(obj8.TransformPoint(array32[RuntimeServices.NormalizeArrayIndex(array32, i)]), transform.up * -1f, out hitInfo, suspensionRange + wheelRadius, vehicle.terrainMask))
					{
						flag = true;
						float[] array33 = hitCompress;
						array33[RuntimeServices.NormalizeArrayIndex(array33, i)] = hitInfo.distance / (suspensionRange + wheelRadius) * -1f + 1f;
						Vector3[] array34 = hitVelocity;
						//FIXME ref Vector3 reference2 = ref array34[RuntimeServices.NormalizeArrayIndex(array34, i)];
						Transform[] array35 = wheels;
						Transform obj9 = array35[RuntimeServices.NormalizeArrayIndex(array35, i)];
						Rigidbody myRigidbody2 = vehicle.myRigidbody;
						Transform obj10 = transform;
						Vector3[] array36 = wheelPositn;
						//FIXME reference2 = obj9.InverseTransformDirection(myRigidbody2.GetPointVelocity(obj10.TransformPoint(array36[RuntimeServices.NormalizeArrayIndex(array36, i)])));
						float num20 = Game.Settings.buggyTr * 9f;
						float b2 = 1f;
						float[] array37 = hitCompress;
						float num21 = num20 * Mathf.Lerp(0.5f, b2, array37[RuntimeServices.NormalizeArrayIndex(array37, i)]);
						float a = 1f;
						float num22 = 20f;
						Vector3[] array38 = hitVelocity;
						friction = num21 * Mathf.Max(a, (num22 - array38[RuntimeServices.NormalizeArrayIndex(array38, i)].magnitude) / 4f);
						Rigidbody myRigidbody3 = vehicle.myRigidbody;
						Transform[] array39 = wheels;
						Transform obj11 = array39[RuntimeServices.NormalizeArrayIndex(array39, i)];
						Vector3[] array40 = hitVelocity;
						float x2 = array40[RuntimeServices.NormalizeArrayIndex(array40, i)].x * -1f * friction;
						float y3 = 0f;
						Vector3[] array41 = hitVelocity;
						myRigidbody3.AddForceAtPosition(obj11.TransformDirection(Vector3.Min(new Vector3(x2, y3, (array41[RuntimeServices.NormalizeArrayIndex(array41, i)].z - motorSpeed) * -1f * friction), new Vector3(1000f, 1000f, 1000f))), hitInfo.point);
						float num23 = motorSpeed;
						Vector3[] array42 = hitVelocity;
						motorSpeed = num23 + (array42[RuntimeServices.NormalizeArrayIndex(array42, i)].z - motorSpeed) * friction * Time.fixedDeltaTime / motorTorque;
						if ((bool)wheelMarks)
						{
							int[] array43 = wheelMarkIndex;
							int num24 = RuntimeServices.NormalizeArrayIndex(array43, i);
							Skidmarks skidmarks2 = wheelMarks;
							Vector3 point2 = hitInfo.point;
							Vector3 normal2 = hitInfo.normal;
							Vector3[] array44 = hitVelocity;
							float num25 = Mathf.Abs(array44[RuntimeServices.NormalizeArrayIndex(array44, i)].x);
							Vector3[] array45 = hitVelocity;
							float intensity2 = ((!(num25 > Mathf.Abs(array45[RuntimeServices.NormalizeArrayIndex(array45, i)].z) * 0.3f)) ? Mathf.Min(0.5f, friction * 0.05f) : (Mathf.Abs(vehicle.input.y) * 0.5f + 0.25f));
							int[] array46 = wheelMarkIndex;
							array43[num24] = skidmarks2.AddSkidMark(point2, normal2, intensity2, array46[RuntimeServices.NormalizeArrayIndex(array46, i)]);
						}
					}
					else
					{
						hitInfo.distance = -1f;
						int[] array47 = wheelMarkIndex;
						array47[RuntimeServices.NormalizeArrayIndex(array47, i)] = -1;
					}
					float[] array48 = hitDistance;
					array48[RuntimeServices.NormalizeArrayIndex(array48, i)] = hitInfo.distance;
				}
			}
			for (int i = 0; i < 4; i++)
			{
				float[] array49 = hitDistance;
				if (array49[RuntimeServices.NormalizeArrayIndex(array49, i)] != -1f)
				{
					Rigidbody myRigidbody4 = vehicle.myRigidbody;
					Vector3 up = transform.up;
					Vector3[] array50 = hitVelocity;
					float num26 = array50[RuntimeServices.NormalizeArrayIndex(array50, i)].y * -1f * Game.Settings.buggySh * 1f * (float)((!wingOpen) ? 1 : 3);
					float[] array51 = hitCompress;
					Vector3 force2 = up * (num26 + array51[RuntimeServices.NormalizeArrayIndex(array51, i)] * (20f * vehicle.myRigidbody.mass) * (float)((!wingOpen || i >= 2) ? 1 : 8));
					Transform obj12 = transform;
					Vector3[] array52 = wheelPositn;
					myRigidbody4.AddForceAtPosition(force2, obj12.TransformPoint(array52[RuntimeServices.NormalizeArrayIndex(array52, i)]));
				}
			}
			if ((transform.position.y < Game.Settings.lavaAlt + 0.1f && transform.position.y - Game.Settings.lavaAlt > -3f) || Physics.Raycast(transform.position + Vector3.up * 3f, Vector3.down, out hitInfo, 3.1f, 1 << 4))
			{
				if (wingOpen && hitInfo.distance < 2f)
				{
					vehicle.myRigidbody.AddForce(Vector3.up * 400f);
				}
				float num4 = ((!(transform.eulerAngles.z > 180f)) ? transform.eulerAngles.z : (transform.eulerAngles.z - 360f));
				float num5 = ((!(transform.eulerAngles.x > 180f)) ? transform.eulerAngles.x : (transform.eulerAngles.x - 360f));
				vehicle.myRigidbody.angularDrag = 2f;
				float angle = default(float);
				Vector3 axis = default(Vector3);
				if (hitInfo.distance != 0f && (bool)hitInfo.transform)
				{
					hitInfo.transform.rotation.ToAngleAxis(out angle, out axis);
					if (angle != 0f)
					{
						vehicle.myRigidbody.AddForce(hitInfo.transform.rotation.eulerAngles * 0.8f);
					}
				}
				int j = 0;
				Transform[] array53 = bouyancyPoints;
				for (int length = array53.Length; j < length; j++)
				{
					if (array53[j].position.y < Game.Settings.lavaAlt || Physics.Raycast(array53[j].position + Vector3.up * 3f, Vector3.down, out hitInfo, 3f, 1 << 4))
					{
						float num27 = ((hitInfo.distance == 0f) ? (array53[j].position.y - 2f - Game.Settings.lavaAlt) : (hitInfo.distance - 5f));
						if (num27 < 1.8f * -1f)
						{
							num27 = 1.8f * -1f;
						}
						vehicle.myRigidbody.AddForceAtPosition((new Vector3(0f, num27 * -1f * (100f + vehicle.myRigidbody.GetPointVelocity(array53[j].position).magnitude * (float)((!(vehicle.myRigidbody.GetPointVelocity(array53[j].position).magnitude > 15f)) ? 15 : 100)), 0f) + vehicle.myRigidbody.GetPointVelocity(array53[j].position) * -200f) / Extensions.get_length((System.Array)bouyancyPoints), array53[j].position);
					}
				}
				if (!(vehicle.input.y < 0f))
				{
					vehicle.myRigidbody.AddRelativeTorque(new Vector3(vehicle.input.y * -1f * 500f * ((70f - Mathf.Min(70f, Mathf.Max(1f, num5 * -1f))) / 70f), vehicle.input.y * vehicle.input.x * 300f, num4 * -3f + vehicle.input.y * vehicle.input.x * -50f));
				}
				if (!wingOpen && hitInfo.distance < 3f)
				{
					vehicle.myRigidbody.AddRelativeForce(Vector3.forward * vehicle.input.y * 1200f);
				}
			}
			else if (transform.position.y < Game.Settings.lavaAlt || Physics.Raycast(transform.position + Vector3.up * 200f, Vector3.down, 200f, 1 << 4))
			{
				vehicle.myRigidbody.AddForce(vehicle.myRigidbody.velocity * -8f + Vector3.up * ((!wingOpen) ? 200 : 400));
				vehicle.myRigidbody.angularDrag = 2f;
			}
			if (wingOpen || flag || Physics.Raycast(transform.position, transform.up * -1f, 3f, vehicle.terrainMask))
			{
				buggyCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;
			}
			else
			{
				buggyCollider.material.frictionCombine = PhysicMaterialCombine.Maximum;
			}
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
			if (isInverted && Vector3.Angle(transform.up, contacts[i].normal) < 50f)
			{
				isInverted = false;
			}
			else if (!isInverted && vehicle.myRigidbody.angularVelocity.sqrMagnitude < 5f && !wingOpen && Vector3.Angle(transform.up, contacts[i].normal) > 120f)
			{
				isInverted = true;
			}
			if (isInverted)
			{
				vehicle.myRigidbody.AddTorque(Vector3.Cross(transform.up, Vector3.up) * Vector3.Angle(transform.up, Vector3.up) * 3f);
			}
		}
	}

	public IEnumerator OnSetSpecialInput()
	{
		return new OnSetSpecialInput_0024105(this).GetEnumerator();
	}

	public void OnDisable()
	{
		if ((bool)wheelMarks)
		{
			UnityEngine.Object.Destroy(wheelMarks.gameObject);
		}
	}

	public void OnLOD(int level)
	{
		for (int i = 0; i < 4; i = checked(i + 1))
		{
			Transform[] array = wheelGraphics;
			array[RuntimeServices.NormalizeArrayIndex(array, i)].Find("Detailed").gameObject.SetActiveRecursively(level == 0);
			Transform[] array2 = wheelGraphics;
			array2[RuntimeServices.NormalizeArrayIndex(array2, i)].Find("Simple").gameObject.active = level != 0;
			Transform[] array3 = axels;
			array3[RuntimeServices.NormalizeArrayIndex(array3, i)].gameObject.SetActiveRecursively(level == 0);
		}
	}

	public void Main()
	{
	}
}
