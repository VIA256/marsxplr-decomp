using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Rocket : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class Start_002491 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal float _0024velocity_0024543;

			internal Vector3 _0024bgn_0024544;

			internal Vector3 _0024refvel_0024545;

			internal float _0024reftme_0024546;

			internal Vector3 _0024pos_0024547;

			internal Vector3 _0024vel_0024548;

			internal Vector3 _0024accel_0024549;

			internal Vector3 _0024refpos_0024550;

			internal float _0024dur_0024551;

			internal Rocket _0024self_552;

			public _0024(Rocket self_)
			{
				_0024self_552 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						_0024self_552.name = "lsr#" + _0024self_552.laserID;
						_0024velocity_0024543 = (float)(Game.Settings.laserSpeed * _0024self_552.speedX) + Mathf.Max(0f, _0024self_552.launchVehicle.transform.InverseTransformDirection(_0024self_552.launchVehicle.velocity).z * ((!_0024self_552.targetVehicle) ? Vector3.Dot(_0024self_552.transform.forward, _0024self_552.launchVehicle.transform.forward) : 1f));
						if ((bool)_0024self_552.targetVehicle)
						{
							_0024bgn_0024544 = _0024self_552.transform.position;
							if (_0024self_552.targetVehicle.networkView.isMine)
							{
								_0024refvel_0024545 = _0024self_552.targetVehicle.gameObject.rigidbody.velocity;
								_0024reftme_0024546 = Time.time;
								return Yield(2, new WaitForSeconds(Time.fixedDeltaTime * 5f));
							}
							_0024refpos_0024550 = _0024self_552.targetVehicle.transform.position;
							_0024reftme_0024546 = Time.time;
							return Yield(3, new WaitForSeconds(Time.fixedDeltaTime * 5f));
						}
						if (_0024self_552.lag > 0f)
						{
							_0024self_552.rigidbody.position = _0024self_552.rigidbody.position + _0024self_552.rigidbody.transform.TransformDirection(0f, 0f, _0024velocity_0024543 * _0024self_552.lag);
						}
						goto IL_043c;
					case 2:
						_0024pos_0024547 = _0024self_552.targetVehicle.transform.position;
						_0024vel_0024548 = _0024self_552.targetVehicle.gameObject.rigidbody.velocity;
						_0024accel_0024549 = (_0024vel_0024548 - _0024refvel_0024545) * 1f / (Time.time - _0024reftme_0024546);
						goto IL_031b;
					case 3:
						_0024refvel_0024545 = (_0024self_552.targetVehicle.transform.position - _0024refpos_0024550) * 1f / (Time.time - _0024reftme_0024546);
						_0024refpos_0024550 = _0024self_552.targetVehicle.transform.position;
						_0024reftme_0024546 = Time.time;
						return Yield(4, new WaitForSeconds(Time.fixedDeltaTime * 5f));
					case 4:
						_0024pos_0024547 = _0024self_552.targetVehicle.transform.position;
						_0024vel_0024548 = (_0024self_552.targetVehicle.transform.position - _0024refpos_0024550) * 1f / (Time.time - _0024reftme_0024546);
						_0024accel_0024549 = (_0024vel_0024548 - _0024refvel_0024545) * 1f / (Time.time - _0024reftme_0024546);
						goto IL_031b;
					case 5:
						UnityEngine.Object.Destroy(_0024self_552.gameObject);
						Yield(1, null);
						break;
					case 1:
						break;
						IL_031b:
						_0024dur_0024551 = 0f;
						while (true)
						{
							_0024pos_0024547 += _0024vel_0024548 * Time.fixedDeltaTime;
							_0024vel_0024548 += _0024accel_0024549 * Time.fixedDeltaTime;
							_0024dur_0024551 += Time.fixedDeltaTime;
							if (_0024dur_0024551 > 10f || Vector3.Distance(_0024bgn_0024544, _0024pos_0024547) < _0024dur_0024551 * _0024velocity_0024543)
							{
								break;
							}
						}
						_0024self_552.transform.LookAt(_0024pos_0024547);
						goto IL_043c;
						IL_043c:
						_0024self_552.rigidbody.velocity = _0024self_552.rigidbody.transform.TransformDirection(0f, 0f, _0024velocity_0024543);
						_0024self_552.strtPos = _0024self_552.rigidbody.position;
						_0024self_552.stage = 0;
						return Yield(5, new WaitForSeconds(Mathf.Lerp(13f, 5f, (float)(Game.Settings.laserSpeed * _0024self_552.speedX) * 0.003f)));
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal Rocket _0024self_553;

		public Start_002491(Rocket self_)
		{
			_0024self_553 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_553);
		}
	}

	public float lag;

	public Transform explosion;

	public Vehicle launchVehicle;

	public Vehicle targetVehicle;

	public int speedX;

	public string laserID;

	private object strtPos;

	private int stage;

	public LayerMask mask;

	public LayerMask maskOpt;

	public Rocket()
	{
		stage = -1;
		mask = -1;
		maskOpt = -1;
	}

	public IEnumerator Start()
	{
		return new Start_002491(this).GetEnumerator();
	}

	public void FixedUpdate()
	{
		if (stage != 0 || !launchVehicle || !Game.Settings)
		{
			return;
		}
		if (Game.Settings.laserGrav != 0f)
		{
			float y = rigidbody.velocity.y - Game.Settings.laserGrav * (float)speedX * Time.deltaTime * 20f;
			Vector3 velocity = rigidbody.velocity;
			float num = (velocity.y = y);
			Vector3 vector = (rigidbody.velocity = velocity);
		}
		RaycastHit[] array = null;
		Vector3 direction = (Vector3)RuntimeServices.InvokeBinaryOperator("op_Subtraction", transform.position, strtPos);
		array = Physics.RaycastAll((Vector3)strtPos, direction, direction.magnitude, (!Game.Settings.lasersOptHit) ? mask : maskOpt);
		for (int i = 0; i < Extensions.get_length((System.Array)array); i = checked(i + 1))
		{
			RaycastHit[] array2 = array;
			RaycastHit raycastHit = array2[RuntimeServices.NormalizeArrayIndex(array2, i)];
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(RuntimeServices.GetProperty(raycastHit.collider, "transform"), "root"), "gameObject"), launchVehicle.gameObject) || ((((bool)targetVehicle && !targetVehicle.networkView.isMine) || (!targetVehicle && !launchVehicle.networkView.isMine)) && (bool)raycastHit.rigidbody))
			{
				continue;
			}
			if ((((bool)targetVehicle && targetVehicle.networkView.isMine) || (!targetVehicle && launchVehicle.networkView.isMine)) && (bool)raycastHit.rigidbody)
			{
				Vehicle vehicle = (Vehicle)raycastHit.transform.root.gameObject.GetComponent(typeof(Vehicle));
				if ((bool)vehicle && vehicle.isResponding)
				{
					vehicle.gameObject.networkView.RPC("lH", RPCMode.Others, laserID, raycastHit.transform.InverseTransformPoint(raycastHit.point));
					if (launchVehicle.isIt == 1 && Time.time - vehicle.lastTag > 3f)
					{
						launchVehicle.gameObject.networkView.RPC("iS", RPCMode.All, vehicle.gameObject.name);
						vehicle.lastTag = Time.time;
					}
					else if (vehicle.isIt == 1 && Time.time - launchVehicle.lastTag > 3f && Time.time - vehicle.lastTag > 3f)
					{
						launchVehicle.gameObject.networkView.RPC("sQ", RPCMode.All, 2);
					}
					else if (vehicle.isIt == 0 && launchVehicle.isIt == 0)
					{
						launchVehicle.gameObject.networkView.RPC("dS", RPCMode.All, vehicle.gameObject.name);
					}
				}
			}
			if ((bool)raycastHit.rigidbody || Game.Settings.laserRico == 0f)
			{
				laserHit(raycastHit.transform.root.gameObject, raycastHit.point, raycastHit.normal);
				continue;
			}
			rigidbody.position = raycastHit.point;
			rigidbody.velocity = Game.Settings.laserRico * Vector3.Lerp(Vector3.Scale(rigidbody.velocity, raycastHit.normal), Vector3.Reflect(rigidbody.velocity, raycastHit.normal), Game.Settings.laserRico);
		}
		strtPos = rigidbody.position;
	}

	public void laserHit(GameObject go, Vector3 pos, Vector3 norm)
	{
		stage = 1;
		rigidbody.position = pos;
		rigidbody.velocity = Vector3.zero;
		go.BroadcastMessage("OnLaserHit", launchVehicle.networkView.isMine, SendMessageOptions.DontRequireReceiver);
		Collider[] array = Physics.OverlapSphere(pos, 10f);
		int i = 0;
		Collider[] array2 = array;
		checked
		{
			for (int length = array2.Length; i < length; i++)
			{
				if ((bool)array2[i].attachedRigidbody)
				{
					array2[i].attachedRigidbody.AddExplosionForce(350 + speedX * 300, pos, 1f, 2f);
				}
			}
			UnityEngine.Object.Instantiate(explosion, pos, Quaternion.FromToRotation(Vector3.up, norm));
		}
	}

	public void Main()
	{
	}
}
