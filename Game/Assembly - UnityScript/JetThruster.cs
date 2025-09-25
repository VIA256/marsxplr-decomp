using System;
using UnityEngine;

[Serializable]
public class JetThruster : MonoBehaviour
{
	public Vehicle vehicle;

	public ParticleRenderer particleRenderer;

	public void Start()
	{
		vehicle = (Vehicle)gameObject.transform.root.gameObject.GetComponentInChildren(typeof(Vehicle));
		if (!vehicle)
		{
			UnityEngine.Object.Destroy(this);
		}
		particleRenderer = (ParticleRenderer)gameObject.GetComponent("ParticleRenderer");
	}

	public void FixedUpdate()
	{
		float x = 1f * vehicle.input.x;
		Vector3 localVelocity = particleEmitter.localVelocity;
		float num = (localVelocity.x = x);
		Vector3 vector = (particleEmitter.localVelocity = localVelocity);
		float y = 1f * vehicle.input.y;
		Vector3 localVelocity2 = particleEmitter.localVelocity;
		float num2 = (localVelocity2.y = y);
		Vector3 vector3 = (particleEmitter.localVelocity = localVelocity2);
		float z = Mathf.Min(-10f * vehicle.input.z, 0.5f * -1f);
		Vector3 localVelocity3 = particleEmitter.localVelocity;
		float num3 = (localVelocity3.z = z);
		Vector3 vector5 = (particleEmitter.localVelocity = localVelocity3);
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
