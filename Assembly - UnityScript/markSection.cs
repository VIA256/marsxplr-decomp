using System;
using UnityEngine;

[Serializable]
public class markSection
{
	public Vector3 pos;

	public Vector3 normal;

	public Vector3 posl;

	public Vector3 posr;

	public float intensity;

	public int lastIndex;

	public markSection()
	{
		intensity = 0f;
		lastIndex = -1;
	}
}
