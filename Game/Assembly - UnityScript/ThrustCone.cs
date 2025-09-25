using System;
using UnityEngine;

[Serializable]
public class ThrustCone : MonoBehaviour
{
	public Vehicle vehicle;

	public Material mat;

	public float magSteer;

	public float magThrottle;

	public float minThrottle;

	public ThrustCone()
	{
		magSteer = 15f;
		magThrottle = 1f;
		minThrottle = 0.2f;
	}

	public void Start()
	{
		vehicle = (Vehicle)gameObject.transform.root.gameObject.GetComponentInChildren(typeof(Vehicle));
	}

	public void Update()
	{
		float y = mat.mainTextureOffset.y - Time.deltaTime * 0.8f;
		Vector2 mainTextureOffset = mat.mainTextureOffset;
		float num = (mainTextureOffset.y = y);
		Vector2 vector = (mat.mainTextureOffset = mainTextureOffset);
		if (mat.mainTextureOffset.y < 0.5f * -1f)
		{
			float y2 = mat.mainTextureOffset.y + 0.1f;
			Vector2 mainTextureOffset2 = mat.mainTextureOffset;
			float num2 = (mainTextureOffset2.y = y2);
			Vector2 vector3 = (mat.mainTextureOffset = mainTextureOffset2);
		}
		if (magSteer > 0f)
		{
			float y3 = vehicle.input.x * -1f * magSteer;
			Vector3 localEulerAngles = transform.localEulerAngles;
			float num3 = (localEulerAngles.y = y3);
			Vector3 vector5 = (transform.localEulerAngles = localEulerAngles);
		}
		if (magThrottle > 0f)
		{
			float y4 = Mathf.Max(minThrottle, ((!vehicle.inputThrottle) ? vehicle.input.y : vehicle.input.z) * magThrottle);
			Vector3 localScale = transform.localScale;
			float num4 = (localScale.y = y4);
			Vector3 vector7 = (transform.localScale = localScale);
		}
		else
		{
			float y5 = minThrottle;
			Vector3 localScale2 = transform.localScale;
			float num5 = (localScale2.y = y5);
			Vector3 vector9 = (transform.localScale = localScale2);
		}
	}

	public void Main()
	{
	}
}
