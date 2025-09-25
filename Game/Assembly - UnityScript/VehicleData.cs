using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class VehicleData : MonoBehaviour
{
	public int camOffset;

	public Transform ridePos;

	public int mass;

	public float drag;

	public float angularDrag;

	public string shortName;

	public bool inputThrottle;

	public MeshRenderer[] materialMain;

	public MeshRenderer[] materialAccent;

	public VehicleData()
	{
		camOffset = 2;
	}

	public void InitVehicle(Vehicle veh)
	{
		veh.camOffset = camOffset;
		veh.ridePos = ridePos;
		veh.shortName = shortName;
		veh.myRigidbody.mass = mass;
		veh.myRigidbody.drag = drag;
		veh.myRigidbody.angularDrag = angularDrag;
		checked
		{
			if (Extensions.get_length((System.Array)materialMain) > 0)
			{
				UnityScript.Lang.Array array = new UnityScript.Lang.Array();
				int i = 0;
				MeshRenderer[] array2 = materialMain;
				for (int length = array2.Length; i < length; i++)
				{
					array.Add(array2[i].material);
				}
				veh.materialMain = (Material[])array.ToBuiltin(typeof(Material));
			}
			if (Extensions.get_length((System.Array)materialAccent) > 0)
			{
				UnityScript.Lang.Array array = new UnityScript.Lang.Array();
				int j = 0;
				MeshRenderer[] array3 = materialAccent;
				for (int length2 = array3.Length; j < length2; j++)
				{
					array.Add(array3[j].material);
				}
				veh.materialAccent = (Material[])array.ToBuiltin(typeof(Material));
			}
			veh.inputThrottle = inputThrottle;
			UnityEngine.Object.Destroy(this);
		}
	}

	public void Main()
	{
	}
}
