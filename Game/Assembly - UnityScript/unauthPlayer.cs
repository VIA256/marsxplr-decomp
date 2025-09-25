using System;
using UnityEngine;

[Serializable]
public class unauthPlayer
{
	public NetworkPlayer p;

	public string n;

	public float t;

	public unauthPlayer(NetworkPlayer p, string n, float t)
	{
		this.p = p;
		this.n = n;
		this.t = t;
	}
}
