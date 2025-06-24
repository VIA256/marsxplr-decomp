using System;
using UnityEngine;

[Serializable]
public class State
{
	public Vector3 p;

	public Quaternion r;

	public float t;

	public float m;

	public float n;

	public State(Vector3 p, Quaternion r, float t, float m, float n)
	{
		this.t = 0f;
		this.m = 0f;
		this.n = 0f;
		this.p = p;
		this.r = r;
		this.t = t;
		this.m = m;
		this.n = n;
	}
}
