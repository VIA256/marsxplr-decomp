using System;
using UnityEngine;

[Serializable]
public class HoverThrustMoonOrBust : MonoBehaviour
{
	public Vehicle vehicle;

	public ParticleRenderer particleRenderer;

	public void Start()
	{
		vehicle = (Vehicle)gameObject.transform.root.gameObject.GetComponentInChildren(typeof(Vehicle));
		particleRenderer = (ParticleRenderer)gameObject.GetComponent("ParticleRenderer");
	}

	public void FixedUpdate()
	{
		float x = (float)((vehicle.input.y != 0f) ? 5 : 2) * vehicle.input.x;
		Vector3 localVelocity = particleEmitter.localVelocity;
		float num = (localVelocity.x = x);
		Vector3 vector = (particleEmitter.localVelocity = localVelocity);
		float z = Mathf.Min(-10f * vehicle.input.y, 0.5f * -1f);
		Vector3 localVelocity2 = particleEmitter.localVelocity;
		float num2 = (localVelocity2.z = z);
		Vector3 vector3 = (particleEmitter.localVelocity = localVelocity2);
		if (!(particleEmitter.localVelocity.z < -1f))
		{
			particleRenderer.particleRenderMode = ParticleRenderMode.Billboard;
		}
		else
		{
			particleRenderer.particleRenderMode = ParticleRenderMode.Stretch;
		}
	}

	public void Main()
	{
	}
}
