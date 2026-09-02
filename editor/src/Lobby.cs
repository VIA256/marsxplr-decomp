using System;
using UnityEngine;

[Serializable]
public class Lobby : MonoBehaviour
{
	public void Awake()
	{
		QualitySettings.currentLevel = QualityLevel.Simple;
		Screen.lockCursor = false;
		Application.runInBackground = false;
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}