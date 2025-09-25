using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class VehicleBot : MonoBehaviour
{
	public Vehicle vehicle;

	private GameObject enemy;

	private int botMovement;

	private int botEnemySelection;

	private int enemyUpdateTime;

	private float rocketFireTime;

	public VehicleBot()
	{
		rocketFireTime = 0f;
	}

	public void Update()
	{
		if ((float)enemyUpdateTime == 0f || Time.time - 2f > (float)enemyUpdateTime)
		{
			enemyUpdateTime = checked((int)Time.time);
			if (botEnemySelection == 1)
			{
				float num = float.PositiveInfinity;
				IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
				while (enumerator.MoveNext())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
					if (RuntimeServices.ToBool(RuntimeServices.GetProperty(dictionaryEntry.Value, "gameObject")))
					{
						GameObject gameObject = (GameObject)RuntimeServices.Coerce(RuntimeServices.GetProperty(dictionaryEntry.Value, "gameObject"), typeof(GameObject));
						UnityRuntimeServices.Update(enumerator, dictionaryEntry);
						float sqrMagnitude = (gameObject.transform.position - transform.position).sqrMagnitude;
						if (sqrMagnitude < num && gameObject.name != name)
						{
							enemy = gameObject;
							num = sqrMagnitude;
						}
					}
				}
			}
			else if (botEnemySelection == 2)
			{
				IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(Game.Players);
				while (enumerator2.MoveNext())
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator2.Current;
					if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(dictionaryEntry2.Value, "isIt"), 1))
					{
						enemy = (GameObject)RuntimeServices.Coerce(RuntimeServices.GetProperty(dictionaryEntry2.Value, "gameObject"), typeof(GameObject));
						UnityRuntimeServices.Update(enumerator2, dictionaryEntry2);
						break;
					}
				}
			}
		}
		if (true)
		{
			if (vehicle.isIt != 0)
			{
				botMovement = 1;
				botEnemySelection = 1;
			}
			else
			{
				botMovement = 2;
				botEnemySelection = 2;
			}
		}
		else if (vehicle.isIt == 0)
		{
			botMovement = 1;
		}
		else
		{
			botMovement = 2;
			botEnemySelection = 1;
		}
		if (Game.Settings.botsCanDrive)
		{
			if (botMovement == 1)
			{
				if (vehicle.vehId == 2)
				{
					vehicle.input.x = ((Time.time % 2f != 0f) ? (UnityEngine.Random.value * 2f - 1f) : 0f);
					vehicle.input.y = 1f;
				}
				else
				{
					vehicle.input.x = UnityEngine.Random.value * 2f - 1f;
					vehicle.input.y = 1f;
				}
			}
			else if (botMovement == 2 && (bool)enemy)
			{
				float num = Vector3.Distance(enemy.transform.position, vehicle.myRigidbody.position);
				float num2 = Quaternion.LookRotation(enemy.transform.position - vehicle.myRigidbody.position).eulerAngles.y - transform.localRotation.eulerAngles.y;
				if (num2 > 180f)
				{
					num2 -= 360f;
				}
				if (num2 < -180f)
				{
					num2 += 360f;
				}
				if (vehicle.vehId == 0)
				{
					num2 /= 20f;
					if (num < 15f)
					{
						vehicle.input.x = ((!(Mathf.Abs(num2) < 0.1f)) ? Mathf.Clamp(num2, -1f, 1f) : 0f);
					}
					else
					{
						vehicle.input.x = ((!(Mathf.Abs(num2) < 0.1f)) ? Mathf.Clamp(num2, 0.6f * -1f, 0.6f) : 0f);
					}
					vehicle.input.y = 1f;
					vehicle.specialInput = false;
				}
				else if (vehicle.vehId == 1)
				{
					num2 /= 15f;
					vehicle.input.x = ((!(Mathf.Abs(num2) < 0.1f)) ? Mathf.Clamp(num2, -1f, 1f) : 0f);
					vehicle.input.y = 1f;
					vehicle.specialInput = false;
				}
				else if (vehicle.vehId == 2)
				{
					num2 /= 80f;
					vehicle.input.x = ((!(Mathf.Abs(num2) < 0.3f)) ? Mathf.Clamp(num2, -1f, 1f) : 0f);
					vehicle.input.y = ((!(Mathf.Abs(num2) > 1f)) ? 1 : 0);
					vehicle.specialInput = false;
				}
				else if (vehicle.vehId == 3)
				{
					num2 /= 10f;
					vehicle.input.x = ((!(Mathf.Abs(num2) < 0.1f)) ? Mathf.Clamp(num2, -1f, 1f) : 0f);
					vehicle.input.y = 1f;
					vehicle.input.z = ((!Physics.Raycast(transform.position, Vector3.up * -1f, 4f)) ? 0.3f : 1f);
					vehicle.specialInput = true;
				}
			}
		}
		else
		{
			vehicle.input.x = 0f;
			vehicle.input.y = 0f;
		}
		if ((bool)enemy && Game.Settings.botsCanFire && Time.time - 1f > rocketFireTime && !Physics.Linecast(transform.position, enemy.transform.position, 1 << 8))
		{
			rocketFireTime = Time.time;
			networkView.RPC("fRl", RPCMode.All, networkView.viewID, Network.time + string.Empty, vehicle.ridePos.position + vehicle.transform.up * (0.1f * -1f), enemy.networkView.viewID);
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
	}

	public void Main()
	{
	}
}
