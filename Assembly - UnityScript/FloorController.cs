using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class FloorController : MonoBehaviour
{
	public WhirldObject whirldObject;

	public GameObject floorObject;

	public void OnSceneGenerated()
	{
		if ((bool)whirldObject && RuntimeServices.ToBool(whirldObject.@params["Texture"]) && (bool)floorObject)
		{
			floorObject.renderer.material.mainTexture = (Texture)RuntimeServices.Coerce(whirldObject.@params["Texture"], typeof(Texture));
		}
	}

	public void Main()
	{
	}
}
