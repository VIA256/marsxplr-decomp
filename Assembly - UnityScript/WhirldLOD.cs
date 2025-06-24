using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class WhirldLOD : MonoBehaviour
{
	public GameObject[] lodObjs;

	public int lodLevMax;

	[HideInInspector]
	public int level;

	private int lastLevel;

	private float lodCheck;

	public WhirldLOD()
	{
		lodLevMax = 0;
		level = 0;
		lastLevel = -1;
		lodCheck = UnityEngine.Random.Range(30, 60) / 10;
	}

	public void Start()
	{
		InvokeRepeating("SetLOD", 0f, lodCheck);
	}

	public void SetLOD()
	{
		checked
		{
			if (lodLevMax == 0)
			{
				level = ((World.lodDist <= 0) ? (Extensions.get_length((System.Array)lodObjs) - 1) : UnityBuiltins.parseInt(Mathf.Lerp(0f, Extensions.get_length((System.Array)lodObjs) - 1, Vector3.Distance(transform.position, Camera.main.transform.position) / (float)World.lodDist)));
			}
			else
			{
				level = (((int)QualitySettings.currentLevel < lodLevMax) ? 1 : 0);
			}
			if (lastLevel == level)
			{
				return;
			}
			BroadcastMessage("OnLOD", level, SendMessageOptions.DontRequireReceiver);
			for (int i = 0; i < Extensions.get_length((System.Array)lodObjs); i++)
			{
				bool flag = i == level;
				GameObject[] array = lodObjs;
				if (array[RuntimeServices.NormalizeArrayIndex(array, i)].active != flag)
				{
					GameObject[] array2 = lodObjs;
					array2[RuntimeServices.NormalizeArrayIndex(array2, i)].SetActiveRecursively(flag);
				}
			}
			lastLevel = level;
		}
	}

	public void Main()
	{
	}
}
