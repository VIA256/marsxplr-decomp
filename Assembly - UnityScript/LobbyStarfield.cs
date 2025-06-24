using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class LobbyStarfield : MonoBehaviour
{
	public Lobby Lobby;

	public void Start()
	{
	}

	public void Update()
	{
		if (Application.loadedLevel == 1 && Lobby.GUIHide > 0f)
		{
			Color color = (Color)UnityRuntimeServices.Invoke(RuntimeServices.GetProperty(gameObject.GetComponent(typeof(ParticleRenderer)), "material"), "GetColor", new object[1] { "_TintColor" }, typeof(MonoBehaviour));
			color.a = 0.5f - Lobby.GUIHide / 2f;
			UnityRuntimeServices.Invoke(RuntimeServices.GetProperty(gameObject.GetComponent(typeof(ParticleRenderer)), "material"), "SetColor", new object[2] { "_TintColor", color }, typeof(MonoBehaviour));
		}
		else if (Application.loadedLevel > 1)
		{
			float y = Mathf.Min(Time.timeSinceLevelLoad, 5f) * -1f;
			Vector3 worldVelocity = particleEmitter.worldVelocity;
			float num = (worldVelocity.y = y);
			Vector3 vector = (particleEmitter.worldVelocity = worldVelocity);
			if (Time.timeSinceLevelLoad > 7f || QualitySettings.currentLevel < QualityLevel.Good)
			{
				particleEmitter.emit = false;
			}
		}
	}

	public void Main()
	{
	}
}
