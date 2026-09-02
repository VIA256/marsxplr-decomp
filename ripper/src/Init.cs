using System;
using UnityEngine;

[Serializable]
public class Init : MonoBehaviour
{
	public void Awake()
	{
		LevelInfo.LogToFile();
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
