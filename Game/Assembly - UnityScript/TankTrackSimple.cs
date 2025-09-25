using System;
using UnityEngine;

[Serializable]
public class TankTrackSimple : MonoBehaviour
{
	public LayerMask terrainMask;

	private RaycastHit hit;

	private Transform myTransform;

	private Vector3 linkPos;

	public TankTrackSimple()
	{
		terrainMask = ~1 << 4;
	}

	public void Start()
	{
	}

	public void FixedUpdate()
	{
	}

	public void Main()
	{
	}
}
