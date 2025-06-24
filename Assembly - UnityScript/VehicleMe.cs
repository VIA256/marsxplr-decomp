using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class VehicleMe : MonoBehaviour
{
	public Vehicle vehicle;

	private float rocketFireTime;

	public VehicleMe()
	{
		rocketFireTime = 0f;
	}

	public void Update()
	{
		if (Input.GetButtonDown("Jump") && (!Game.Messaging || !Game.Messaging.chatting) && Time.time > Game.Controller.kpTime)
		{
			networkView.RPC("sI", RPCMode.All, !vehicle.specialInput);
			Game.Controller.kpTime = Time.time + Game.Controller.kpDur;
		}
		if (Input.GetButton("Fire3") && !vehicle.brakes)
		{
			networkView.RPC("sB", RPCMode.All, true);
		}
		else if (!Input.GetButton("Fire3") && vehicle.brakes)
		{
			networkView.RPC("sB", RPCMode.All, false);
		}
		vehicle.input.x = Input.GetAxis("Horizontal");
		vehicle.input.y = Input.GetAxis("Vertical");
		vehicle.input.z = Input.GetAxis("Throttle");
		vehicle.input.w = Input.GetAxis("Yaw");
		if (vehicle.inputThrottle)
		{
			vehicle.input.z = (vehicle.input.z + 1f) * 0.5f;
		}
		else
		{
			if (vehicle.input.x > 0.1f * -1f && vehicle.input.x < 0.1f)
			{
				vehicle.input.x = vehicle.input.w;
			}
			if (vehicle.input.y > 0.1f * -1f && vehicle.input.y < 0.1f)
			{
				vehicle.input.y = vehicle.input.z;
			}
		}
		GameObject gameObject;
		if (Game.Settings.laserLocking)
		{
			RaycastHit hitInfo = default(RaycastHit);
			Vector3 position = transform.position;
			Vector3 forward = transform.forward;
			float[] laserLock = Game.Settings.laserLock;
			if (Physics.Raycast(position + forward * ((laserLock[RuntimeServices.NormalizeArrayIndex(laserLock, vehicle.vehId)] + (float)vehicle.camOffset * 0.1f) * 15f), transform.forward, out hitInfo, float.PositiveInfinity, 1 << 14) && (vehicle.isIt != 0 || RuntimeServices.ToBool(RuntimeServices.GetProperty(hitInfo.transform.gameObject.GetComponent(typeof(Vehicle)), "isIt"))))
			{
				gameObject = hitInfo.transform.gameObject;
				vehicle.laserAimer.active = false;
				vehicle.laserAimerLocked.active = true;
			}
			else
			{
				gameObject = null;
				vehicle.laserAimer.active = true;
				vehicle.laserAimerLocked.active = false;
			}
		}
		else
		{
			GameObject laserAimer = vehicle.laserAimer;
			bool flag = (vehicle.laserAimerLocked.active = false);
			laserAimer.active = flag;
			gameObject = null;
		}
		if ((bool)vehicle.ridePos && Game.Settings.lasersAllowed && rocketFireTime < Time.time)
		{
			int[] firepower = Game.Settings.firepower;
			if (firepower[RuntimeServices.NormalizeArrayIndex(firepower, vehicle.vehId)] > 0 && ((bool)gameObject || (Input.GetButton("Fire1") && !Input.GetMouseButton(0)) || (Input.GetButton("Fire1") && (Input.GetButton("Fire2") || Input.GetButton("Snipe") || Game.Settings.camMode == 0)) || (Input.GetButton("Fire1") && Input.mousePosition.x > (float)Screen.width * 0.25f && Input.mousePosition.x < (float)checked(Screen.width - 200))))
			{
				bool flag3;
				if ((bool)gameObject)
				{
					int[] firepower2 = Game.Settings.firepower;
					bool num = firepower2[RuntimeServices.NormalizeArrayIndex(firepower2, vehicle.vehId)] > 2;
					if (!num)
					{
						int[] firepower3 = Game.Settings.firepower;
						num = firepower3[RuntimeServices.NormalizeArrayIndex(firepower3, vehicle.vehId)] > 1;
						if (num)
						{
							num = gameObject.rigidbody.velocity.sqrMagnitude > 500f;
							if (!num)
							{
								num = Vector3.Distance(transform.position, gameObject.transform.position) > 500f;
							}
						}
					}
					flag3 = num;
					networkView.RPC((!flag3) ? "fRl" : "fSl", RPCMode.All, networkView.viewID, Network.time + string.Empty, vehicle.ridePos.position + vehicle.transform.up * (0.1f * -1f), gameObject.networkView.viewID);
				}
				else
				{
					Quaternion quaternion = ((Game.Settings.camMode != 0 && !Input.GetButton("Fire2") && !Input.GetButton("Snipe")) ? vehicle.ridePos.rotation : Camera.main.transform.rotation);
					bool num2 = Input.GetButton("Snipe");
					if (num2)
					{
						int[] firepower4 = Game.Settings.firepower;
						num2 = firepower4[RuntimeServices.NormalizeArrayIndex(firepower4, vehicle.vehId)] > 1;
					}
					if (!num2)
					{
						int[] firepower5 = Game.Settings.firepower;
						num2 = firepower5[RuntimeServices.NormalizeArrayIndex(firepower5, vehicle.vehId)] > 2;
					}
					flag3 = num2;
					networkView.RPC((!flag3) ? "fR" : "fS", RPCMode.All, networkView.viewID, Network.time + string.Empty, vehicle.ridePos.position + vehicle.transform.up * (0.1f * -1f), quaternion.eulerAngles);
				}
				float time = Time.time;
				float num3;
				if (flag3)
				{
					int[] firepower6 = Game.Settings.firepower;
					num3 = ((firepower6[RuntimeServices.NormalizeArrayIndex(firepower6, vehicle.vehId)] <= 2) ? 2f : 0.5f);
				}
				else
				{
					num3 = 0.25f;
				}
				rocketFireTime = time + num3;
			}
		}
		if (vehicle.myRigidbody.position.y < -300f)
		{
			vehicle.myRigidbody.velocity = vehicle.myRigidbody.velocity.normalized;
			vehicle.myRigidbody.isKinematic = true;
			vehicle.transform.position = World.@base.position;
			vehicle.myRigidbody.isKinematic = false;
			if ((bool)Game.Messaging)
			{
				Game.Messaging.broadcast(name + " fell off the planet");
			}
		}
		if (vehicle.myRigidbody.velocity.magnitude > 500f)
		{
			vehicle.myRigidbody.velocity = vehicle.myRigidbody.velocity * 0.5f;
		}
	}

	public void FixedUpdate()
	{
		if ((bool)vehicle.ramoSphere && vehicle.zorbBall && (vehicle.input.y != 0f || vehicle.input.x != 0f))
		{
			rigidbody.AddForce(Vector3.Scale(new Vector3(1f, 0f, 1f), Camera.main.transform.TransformDirection(new Vector3(vehicle.input.x * Mathf.Max(0f, Game.Settings.zorbSpeed + Game.Settings.zorbAgility), 0f, vehicle.input.y * Game.Settings.zorbSpeed))), ForceMode.Acceleration);
			rigidbody.AddTorque(Camera.main.transform.TransformDirection(new Vector3(vehicle.input.y, 0f, vehicle.input.x * -1f)) * Game.Settings.zorbSpeed, ForceMode.Acceleration);
		}
	}

	public void OnPrefsUpdated()
	{
		if (Game.Settings.renderLevel > 4)
		{
			Light light = (Light)gameObject.GetComponentInChildren(typeof(Light));
			if ((bool)light)
			{
				light.enabled = true;
			}
		}
		if (Game.Settings.renderLevel > 3)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(gameObject.GetComponentsInChildren(typeof(TrailRenderer)));
			while (enumerator.MoveNext())
			{
				TrailRenderer trailRenderer = (TrailRenderer)RuntimeServices.Coerce(enumerator.Current, typeof(TrailRenderer));
				trailRenderer.enabled = true;
				UnityRuntimeServices.Update(enumerator, trailRenderer);
			}
		}
	}

	public void Main()
	{
	}
}
