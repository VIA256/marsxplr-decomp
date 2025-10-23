using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("Camera-Control/Smooth Look At")]
public class SmoothLookAt : MonoBehaviour
{
	public Transform target;
	public float damping = 6.0f;
	public bool smooth = true;

	public void LateUpdate()
	{
		if ((bool)target)
		{
			if (smooth)
			{
                // Look at and dampen the rotation
				Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
				transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rotation,
                    Time.deltaTime * damping);
			}
			else
			{
                // Just lookat
				transform.LookAt(target);
			}
		}
	}

	public void Start()
	{
        // Make the rigid body not change rotation
		if ((bool)rigidbody) rigidbody.freezeRotation = true;
	}
}
