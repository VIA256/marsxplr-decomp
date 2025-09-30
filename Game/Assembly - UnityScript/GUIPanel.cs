using System;
using UnityEngine;

[Serializable]
public class GUIPanel
{
	public string name;
	public bool active = true;
	public bool open;
	public bool important;
	
	public int minHeight = 300;
	public int maxHeight;
	public float curHeight;
	public float desHeight;
	
	public Vector2 scrollPos;
	public float openTime;
}
