using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class BaseSimple : MonoBehaviour
{
	private Material mat;

	public bool upMode;

	public void Start()
	{
		mat = (Material)RuntimeServices.Coerce(RuntimeServices.GetProperty(GetComponent(typeof(MeshRenderer)), "material"), typeof(Material));
		int num = 1;
		Vector2 mainTextureScale = mat.mainTextureScale;
		float num2 = (mainTextureScale.x = num);
		Vector2 vector = (mat.mainTextureScale = mainTextureScale);
		float y = 0.1f;
		Vector2 mainTextureScale2 = mat.mainTextureScale;
		float num3 = (mainTextureScale2.y = y);
		Vector2 vector3 = (mat.mainTextureScale = mainTextureScale2);
	}

	public void Update()
	{
		transform.localScale = Vector3.one * Mathf.Max(0.5f, Mathf.Min(10f, Vector3.Distance(transform.position, Camera.main.transform.position) / 10f));
		float y = transform.localEulerAngles.y + Time.deltaTime * 10f;
		Vector3 localEulerAngles = transform.localEulerAngles;
		float num = (localEulerAngles.y = y);
		Vector3 vector = (transform.localEulerAngles = localEulerAngles);
		if (transform.localEulerAngles.y > 360f)
		{
			float y2 = transform.localEulerAngles.y - 360f;
			Vector3 localEulerAngles2 = transform.localEulerAngles;
			float num2 = (localEulerAngles2.y = y2);
			Vector3 vector3 = (transform.localEulerAngles = localEulerAngles2);
		}
		float x = mat.mainTextureOffset.x + Time.deltaTime * 0.5f;
		Vector2 mainTextureOffset = mat.mainTextureOffset;
		float num3 = (mainTextureOffset.x = x);
		Vector2 vector5 = (mat.mainTextureOffset = mainTextureOffset);
		if (mat.mainTextureOffset.x > 1f)
		{
			float x2 = mat.mainTextureOffset.x - 1f;
			Vector2 mainTextureOffset2 = mat.mainTextureOffset;
			float num4 = (mainTextureOffset2.x = x2);
			Vector2 vector7 = (mat.mainTextureOffset = mainTextureOffset2);
		}
		if (upMode)
		{
			float y3 = mat.mainTextureOffset.y + Time.deltaTime * 0.1f;
			Vector2 mainTextureOffset3 = mat.mainTextureOffset;
			float num5 = (mainTextureOffset3.y = y3);
			Vector2 vector9 = (mat.mainTextureOffset = mainTextureOffset3);
			if (mat.mainTextureOffset.y > 0.6f)
			{
				upMode = false;
			}
		}
		else
		{
			float y4 = mat.mainTextureOffset.y - Time.deltaTime * 0.1f;
			Vector2 mainTextureOffset4 = mat.mainTextureOffset;
			float num6 = (mainTextureOffset4.y = y4);
			Vector2 vector11 = (mat.mainTextureOffset = mainTextureOffset4);
			if (mat.mainTextureOffset.y < 0.4f)
			{
				upMode = true;
			}
		}
	}

	public void Main()
	{
	}
}
