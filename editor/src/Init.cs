using System;
using UnityEngine;

[Serializable]
public class Init : MonoBehaviour
{
	public void Awake()
	{
		Screen.fullScreen = false;
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
