using System;
using UnityEngine;

[Serializable]
public class GUIPanel
{
	public string name;

	public bool active;

	public bool open;

	public bool important;

	public int minHeight;

	public int maxHeight;

	public float curHeight;

	public float desHeight;

	public Vector2 scrollPos;

	public float openTime;

	public GUIPanel()
	{
		active = true;
		minHeight = 300;
	}
}
