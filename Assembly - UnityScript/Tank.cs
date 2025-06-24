using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Tank : MonoBehaviour
{
	public Vehicle vehicle;

	public GameObject tracks;

	public GameObject tracksSimple;

	public int tracksPerSide;

	public float trackSpacing;

	public GameObject superTracks;

	public GameObject simpleTracks;

	public Tank()
	{
		tracksPerSide = 3;
		trackSpacing = 2.5f;
	}

	public void InitVehicle(Vehicle veh)
	{
		vehicle = veh;
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		GameObject gameObject = null;
		superTracks = new GameObject("Tracks");
		superTracks.transform.parent = transform;
		checked
		{
			for (int i = 0; i < tracksPerSide; i++)
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(tracks, transform.TransformPoint(new Vector3(-2f, 0f, (float)(tracksPerSide - 1) * trackSpacing / 2f * -1f + (float)i * trackSpacing)), transform.rotation);
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Track").GetComponent(typeof(MeshRenderer)), "material"));
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Tread").GetComponent(typeof(MeshRenderer)), "material"));
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Simple").GetComponent(typeof(MeshRenderer)), "material"));
				gameObject.transform.parent = superTracks.transform;
				gameObject = (GameObject)UnityEngine.Object.Instantiate(tracks, transform.TransformPoint(new Vector3(2f, 0f, (float)(tracksPerSide - 1) * trackSpacing / 2f * -1f + (float)i * trackSpacing)), transform.rotation);
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Track").GetComponent(typeof(MeshRenderer)), "material"));
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Tread").GetComponent(typeof(MeshRenderer)), "material"));
				array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Simple").GetComponent(typeof(MeshRenderer)), "material"));
				gameObject.transform.parent = superTracks.transform;
				RuntimeServices.SetProperty(gameObject.GetComponent(typeof(TankTrack)), "rightSide", true);
			}
			if (!vehicle.networkView.isMine)
			{
				simpleTracks = new GameObject();
				simpleTracks.transform.parent = transform;
				for (int i = 0; i < tracksPerSide; i++)
				{
					gameObject = (GameObject)UnityEngine.Object.Instantiate(tracksSimple, transform.TransformPoint(new Vector3(-2f, 0.2f, (float)(tracksPerSide - 1) * trackSpacing / 2f * -1f + (float)i * trackSpacing)), transform.rotation);
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Track").GetComponent(typeof(MeshRenderer)), "material"));
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Tread").GetComponent(typeof(MeshRenderer)), "material"));
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Simple").GetComponent(typeof(MeshRenderer)), "material"));
					gameObject.transform.parent = simpleTracks.transform;
					gameObject = (GameObject)UnityEngine.Object.Instantiate(tracksSimple, transform.TransformPoint(new Vector3(2f, 0.2f, (float)(tracksPerSide - 1) * trackSpacing / 2f * -1f + (float)i * trackSpacing)), transform.rotation);
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Track").GetComponent(typeof(MeshRenderer)), "material"));
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Detailed/Tread").GetComponent(typeof(MeshRenderer)), "material"));
					array.Add(RuntimeServices.GetProperty(gameObject.transform.Find("Simple").GetComponent(typeof(MeshRenderer)), "material"));
					gameObject.transform.parent = simpleTracks.transform;
				}
			}
			else
			{
				TankMe tankMe = (TankMe)this.gameObject.AddComponent(typeof(TankMe));
				tankMe.vehicle = vehicle;
			}
			vehicle.materialAccent = (Material[])array.ToBuiltin(typeof(Material));
			if ((bool)World.@base)
			{
				transform.position = World.@base.position;
			}
			transform.localPosition = Vector3.zero;
		}
	}

	public void Update()
	{
		if (!vehicle)
		{
			return;
		}
		float tankCG = Game.Settings.tankCG;
		Vector3 centerOfMass = vehicle.myRigidbody.centerOfMass;
		float num = (centerOfMass.y = tankCG);
		Vector3 vector = (vehicle.myRigidbody.centerOfMass = centerOfMass);
		if (!vehicle.networkView.isMine && (bool)vehicle.vehicleNet)
		{
			if (vehicle.vehicleNet.simulatePhysics && simpleTracks.active)
			{
				vehicle.myRigidbody.useGravity = true;
				simpleTracks.SetActiveRecursively(false);
				superTracks.SetActiveRecursively(true);
			}
			else if (!vehicle.vehicleNet.simulatePhysics && superTracks.active)
			{
				vehicle.myRigidbody.useGravity = true;
				simpleTracks.SetActiveRecursively(true);
				superTracks.SetActiveRecursively(false);
			}
		}
	}

	public void OnLOD(int level)
	{
		if ((bool)superTracks)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(superTracks.transform);
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)RuntimeServices.Coerce(enumerator.Current, typeof(Transform));
				RuntimeServices.SetProperty(transform.Find("Detailed/Tread").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level == 0);
				UnityRuntimeServices.Update(enumerator, transform);
				RuntimeServices.SetProperty(transform.Find("Detailed/Track").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level == 0);
				UnityRuntimeServices.Update(enumerator, transform);
				RuntimeServices.SetProperty(transform.Find("Simple").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level != 0);
				UnityRuntimeServices.Update(enumerator, transform);
			}
		}
		if ((bool)simpleTracks)
		{
			IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(simpleTracks.transform);
			while (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)RuntimeServices.Coerce(enumerator2.Current, typeof(Transform));
				RuntimeServices.SetProperty(transform2.Find("Detailed/Tread").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level == 0);
				UnityRuntimeServices.Update(enumerator2, transform2);
				RuntimeServices.SetProperty(transform2.Find("Detailed/Track").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level == 0);
				UnityRuntimeServices.Update(enumerator2, transform2);
				RuntimeServices.SetProperty(transform2.Find("Simple").gameObject.GetComponent(typeof(MeshRenderer)), "enabled", level != 0);
				UnityRuntimeServices.Update(enumerator2, transform2);
			}
		}
	}

	public void Main()
	{
	}
}
