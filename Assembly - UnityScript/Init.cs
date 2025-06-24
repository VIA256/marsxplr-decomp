using System;
using UnityEngine;

[Serializable]
public class Init : MonoBehaviour
{
	public GUIText txt;

	public void Update()
	{
		checked
		{
			float streamProgressForLevel = Application.GetStreamProgressForLevel(Application.loadedLevel + 1);
			if (streamProgressForLevel == 1f)
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
			else
			{
				txt.text = Mathf.RoundToInt(streamProgressForLevel * 100f) + "%";
			}
		}
	}

	public void Main()
	{
	}
}
