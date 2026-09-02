using System;
using UnityEngine;

[Serializable]
public class Lobby : MonoBehaviour
{
	public void Awake()
	{
		LevelInfo.LogToFile();
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}