using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class VehicleNet : MonoBehaviour
{
	public Vehicle vehicle;

	public bool simulatePhysics;

	public bool updatePosition;

	public float physInterp;

	public float ping;

	public float jitter;

	public float calcPing;

	public float rpcPing;

	public float lastPing;

	public int pingCheck;

	public bool wePinged;

	public float autoInterp;

	private int m;

	private Vector3 p;

	private Quaternion r;

	public State[] states;

	public VehicleNet()
	{
		simulatePhysics = true;
		updatePosition = true;
		physInterp = 0.1f;
		calcPing = 0f;
		rpcPing = 0f;
		lastPing = 1f * -1f;
		pingCheck = UnityEngine.Random.Range(15, 20);
		wePinged = false;
		autoInterp = 0f;
		states = new State[15];
	}

	public void Start()
	{
		vehicle.networkView.observed = this;
	}

	public void Update()
	{
		if (vehicle.networkMode == 2 && Time.time > lastPing + (float)pingCheck)
		{
			if (lastPing < 0f)
			{
				lastPing = Time.time;
			}
			else
			{
				networkView.RPC("sT", RPCMode.All, 0f);
			}
		}
		if (!updatePosition || states[14] == null || states[14].t == 0f || !vehicle.myRigidbody || !Game.Player || !Game.Player.rigidbody)
		{
			return;
		}
		if (Game.Settings.networkPhysics == 0)
		{
			physInterp = 0.1f;
		}
		else if (Game.Settings.networkPhysics == 1)
		{
			physInterp = 0.2f;
		}
		if (Game.Settings.networkPhysics == 2)
		{
			simulatePhysics = false;
		}
		else
		{
			simulatePhysics = Vector3.Distance(vehicle.myRigidbody.position, Game.Player.rigidbody.position) < 40f;
		}
		vehicle.myRigidbody.isKinematic = !simulatePhysics;
		vehicle.myRigidbody.interpolation = RigidbodyInterpolation.None;
		float num;
		if (Game.Settings.networkInterpolation > 0f)
		{
			num = (float)(Network.time - (double)Game.Settings.networkInterpolation);
		}
		else
		{
			autoInterp = Mathf.Max(0.1f, Mathf.Lerp(autoInterp, ping * 1.5f + jitter * 8f, Time.deltaTime * 0.15f));
			num = (float)(Network.time - (double)autoInterp);
		}
		checked
		{
			if (states[0].t > num)
			{
				for (int i = 1; i < Extensions.get_length((System.Array)states) - 1; i++)
				{
					State[] array = states;
					if (array[RuntimeServices.NormalizeArrayIndex(array, i)] == null)
					{
						continue;
					}
					State[] array2 = states;
					if (array2[RuntimeServices.NormalizeArrayIndex(array2, i)].t <= num || i == Extensions.get_length((System.Array)states) - 3)
					{
						State[] array3 = states;
						State state = array3[RuntimeServices.NormalizeArrayIndex(array3, i - 1)];
						State[] array4 = states;
						State state2 = array4[RuntimeServices.NormalizeArrayIndex(array4, i)];
						float num2 = state.t - state2.t;
						float t = 0f;
						if (num2 > 0.0001f)
						{
							t = (num - state2.t) / num2;
						}
						Vehicle obj = vehicle;
						Vector3 velocity = vehicle.velocity;
						Vector3 vector = state.p;
						State[] array5 = states;
						Vector3 vector2 = vector - array5[RuntimeServices.NormalizeArrayIndex(array5, i + 2)].p;
						float t2 = state.t;
						State[] array6 = states;
						obj.velocity = Vector3.Lerp(velocity, vector2 / (t2 - array6[RuntimeServices.NormalizeArrayIndex(array6, i + 2)].t), Time.deltaTime * 0.3f);
						if (simulatePhysics)
						{
							vehicle.myRigidbody.MovePosition(Vector3.Lerp(vehicle.transform.position, Vector3.Lerp(state2.p, state.p, t), physInterp));
							vehicle.myRigidbody.MoveRotation(Quaternion.Slerp(vehicle.transform.rotation, Quaternion.Slerp(state2.r, state.r, t), physInterp));
							vehicle.myRigidbody.velocity = vehicle.velocity;
						}
						else
						{
							vehicle.myRigidbody.position = Vector3.Lerp(state2.p, state.p, t);
							vehicle.myRigidbody.rotation = Quaternion.Slerp(state2.r, state.r, t);
						}
						vehicle.isResponding = true;
						vehicle.netCode = string.Empty;
						break;
					}
				}
				return;
			}
			float num3 = num - states[0].t;
			vehicle.velocity = Vector3.Lerp(vehicle.velocity, (states[0].p - states[2].p) / (states[0].t - states[2].t), Time.deltaTime * 0.3f);
			if (num3 < 1f)
			{
				if (!simulatePhysics)
				{
					vehicle.myRigidbody.position = states[0].p + vehicle.velocity * num3;
					vehicle.myRigidbody.rotation = states[0].r;
				}
				vehicle.isResponding = true;
				if (num3 < 0.3f)
				{
					vehicle.netCode = ">";
				}
				else
				{
					vehicle.netCode = " (Delayed)";
				}
			}
			else
			{
				vehicle.netCode = " (Not Responding)";
				vehicle.isResponding = false;
			}
		}
	}

	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		checked
		{
			if (stream.isWriting)
			{
				if (networkView.stateSynchronization == NetworkStateSynchronization.Off)
				{
					Debug.Log("sNv NvS: " + gameObject.name);
				}
				else if (states[0] != null)
				{
					p = states[0].p;
					r = states[0].r;
					m = (int)((Network.time - (double)states[0].t) * 1000.0);
					stream.Serialize(ref p);
					stream.Serialize(ref r);
					stream.Serialize(ref m);
				}
				return;
			}
			if (networkView.stateSynchronization == NetworkStateSynchronization.Off)
			{
				Debug.Log("sNv NvN: " + gameObject.name);
				return;
			}
			stream.Serialize(ref p);
			stream.Serialize(ref r);
			stream.Serialize(ref m);
			State state = new State(p, r, (float)(info.timestamp - (double)((m <= 0) ? 0f : (UnityBuiltins.parseFloat(m) / 1000f))), m, (float)(Network.time - info.timestamp));
			if (states[0] != null && state.t == states[0].t)
			{
				state.t += 0.01f;
			}
			if (states[0] == null || state.t > states[0].t)
			{
				float num = (float)(Network.time - (double)state.t);
				jitter = Mathf.Lerp(jitter, Mathf.Abs(ping - num), 1f / Network.sendRate);
				ping = Mathf.Lerp(ping, num, 1f / Network.sendRate);
				for (int num2 = Extensions.get_length((System.Array)states) - 1; num2 > 0; num2--)
				{
					State[] array = states;
					int num3 = RuntimeServices.NormalizeArrayIndex(array, num2);
					State[] array2 = states;
					array[num3] = array2[RuntimeServices.NormalizeArrayIndex(array2, num2 - 1)];
				}
				states[0] = state;
			}
		}
	}

	public void Main()
	{
	}
}
