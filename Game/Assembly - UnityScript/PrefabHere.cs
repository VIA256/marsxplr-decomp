using System;
using UnityEngine;

[Serializable]
public class PrefabHere : MonoBehaviour
{
	public GameObject prefab;

	public void Awake()
	{
		GameObject newObject = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
		newObject.transform.parent = transform.parent.transform;
		Destroy(this.gameObject);
	}
}
