using System;
using UnityEngine;

[Serializable]
public class SoundEffect : MonoBehaviour
{
	public void Awake()
	{
		if (!Game.Settings.useSfx)
		{
			UnityEngine.Object.Destroy(gameObject.GetComponent(typeof(AudioSource)));
		}
	}

	public void Main()
	{
	}
}
