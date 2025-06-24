using System;
using UnityEngine;

[Serializable]
public class CustomCursor : MonoBehaviour
{
	public Texture2D cursor;

	public Vector2 cursorOffset;

	public void OnGUI()
	{
		if (Screen.lockCursor)
		{
			GUI.depth = -999;
			GUI.Label(new Rect((float)(Screen.width / 2) - cursorOffset.x, (float)(Screen.height / 2) - cursorOffset.y, cursor.width, cursor.height), cursor);
		}
	}

	public void Main()
	{
	}
}
