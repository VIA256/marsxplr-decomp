using System;
using UnityEngine;

[Serializable]
public class PrefabHere : MonoBehaviour
{
	public GameObject prefab;

	public void Awake()
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab, transform.position, transform.rotation);
		gameObject.transform.parent = transform.parent.transform;
		UnityEngine.Object.Destroy(this.gameObject);
	}

	public void Main()
	{
	}
}
