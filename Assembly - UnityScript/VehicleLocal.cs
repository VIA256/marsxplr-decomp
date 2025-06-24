using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class VehicleLocal : MonoBehaviour
{
	public Vehicle vehicle;

	private Vector3 p;

	private Quaternion r;

	private int m;

	public float syncPosTimer;

	public float syncInpTimer;

	public Vector4 inputS;

	private Vector3 prevPos;

	public VehicleLocal()
	{
		m = 0;
		syncPosTimer = 0f;
		syncInpTimer = 0f;
		prevPos = Vector3.zero;
	}

	public void Start()
	{
		vehicle.networkView.observed = this;
		vehicle.netCode = string.Empty;
		vehicle.isResponding = true;
	}

	public void Update()
	{
		if (vehicle.networkMode == 2 && Time.time > syncPosTimer)
		{
			syncPosTimer = Time.time + 1f / Network.sendRate;
			networkView.RPC("sP", RPCMode.Others, vehicle.myRigidbody.position, vehicle.myRigidbody.rotation);
		}
		if (Time.time > syncInpTimer && vehicle.input != inputS)
		{
			syncInpTimer = Time.time + 1f;
			inputS = vehicle.input;
			networkView.RPC("s4", RPCMode.Others, Mathf.RoundToInt(inputS.x * 10f), Mathf.RoundToInt(inputS.y * 10f), Mathf.RoundToInt(inputS.z * 10f), Mathf.RoundToInt(inputS.w * 10f));
		}
	}

	public void FixedUpdate()
	{
		vehicle.velocity = vehicle.myRigidbody.velocity;
		RaycastHit raycastHit = default(RaycastHit);
		RaycastHit[] array = default(RaycastHit[]);
		if (prevPos != Vector3.zero)
		{
			array = Physics.RaycastAll(prevPos, (vehicle.transform.position - prevPos).normalized, (vehicle.transform.position - prevPos).magnitude, vehicle.terrainMask);
		}
		if (array != null && Extensions.get_length((System.Array)array) > 0)
		{
			for (int i = 0; i < Extensions.get_length((System.Array)array); i = checked(i + 1))
			{
				RaycastHit[] array2 = array;
				if (array2[RuntimeServices.NormalizeArrayIndex(array2, i)].transform.root != transform.root)
				{
					RaycastHit[] array3 = array;
					raycastHit = array3[RuntimeServices.NormalizeArrayIndex(array3, i)];
					break;
				}
			}
		}
		if (raycastHit.point != Vector3.zero)
		{
			if ((prevPos - transform.position).sqrMagnitude < 500f)
			{
				vehicle.transform.position = raycastHit.point + raycastHit.normal * 0.1f;
			}
			else
			{
				prevPos = vehicle.transform.position;
			}
		}
		else
		{
			prevPos = vehicle.transform.position;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer != 14 && (bool)other.attachedRigidbody)
		{
			vehicle.OnRam(other.attachedRigidbody.gameObject);
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		vehicle.vehObj.BroadcastMessage("OnCollisionStay", collision, SendMessageOptions.DontRequireReceiver);
		if (!RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(collision.collider, "transform"), "root"), transform.root))
		{
			if ((bool)collision.transform.parent && (bool)collision.transform.parent.gameObject.rigidbody)
			{
				vehicle.OnRam(collision.transform.parent.gameObject);
			}
			else if ((bool)collision.rigidbody)
			{
				vehicle.OnRam((GameObject)RuntimeServices.Coerce(collision.gameObject, typeof(GameObject)));
			}
		}
	}

	public void OnSerializeNetworkView(BitStream stream)
	{
		if (networkView.stateSynchronization == NetworkStateSynchronization.Off)
		{
			Debug.Log("sNv NvL: " + gameObject.name);
			return;
		}
		p = vehicle.myRigidbody.position;
		r = vehicle.myRigidbody.rotation;
		stream.Serialize(ref p);
		stream.Serialize(ref r);
		m = 0;
		stream.Serialize(ref m);
	}

	public void Main()
	{
	}
}
