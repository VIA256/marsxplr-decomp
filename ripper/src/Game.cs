using System;
using UnityEngine;

[Serializable]
public class Game : MonoBehaviour
{
	public void Awake()
	{
		LevelInfo.LogToFile();
		
		Application.Quit();
	}
}