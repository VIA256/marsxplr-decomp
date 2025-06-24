using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class WhirldObject : MonoBehaviour
{
	public WhirldData[] data;

	public Hashtable @params;

	public void Awake()
	{
		Activate();
	}

	public void OnSceneGenerated()
	{
		Activate();
	}

	public void Activate()
	{
		if (@params != null || data.Length <= 0)
		{
			return;
		}
		@params = new Hashtable();
		int i = 0;
		WhirldData[] array = data;
		for (int length = array.Length; i < length; i = checked(i + 1))
		{
			if ((bool)array[i].o)
			{
				@params.Add(array[i].n, array[i].o);
			}
			else
			{
				@params.Add(array[i].n, array[i].v);
			}
		}
	}

	public void Main()
	{
	}
}
