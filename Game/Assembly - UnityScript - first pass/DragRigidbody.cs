using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class DragRigidbody : MonoBehaviour
{
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;

	private SpringJoint springJoint;

	public void Update()
	{
        // Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		Camera mainCamera = FindCamera();

        // We need to actually hit an object
		RaycastHit hit = default(RaycastHit);
        if (!Physics.Raycast(
            mainCamera.ScreenPointToRay(Input.mousePosition),
            out hit,
            100f))
        {
            return;
        }
        // We need to hit a rigidbody that is not kinematic
        if (
            !(bool)hit.rigidbody ||
            hit.rigidbody.isKinematic)
        {
            return;
        }

		if (!springJoint)
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = (Rigidbody)go.AddComponent("Rigidbody");
			springJoint = (SpringJoint)go.AddComponent("SpringJoint");
			body.isKinematic = true;
		}

		springJoint.transform.position = hit.point;
		if (attachToCenterOfMass)
		{
			Vector3 anchor =
                transform.TransformDirection(hit.rigidbody.centerOfMass) +
                hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}

		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;

		StartCoroutine("DragObject", hit.distance);
	}

	public IEnumerator DragObject(float distance)
	{
        float oldDrag = springJoint.connectedBody.drag;
        float oldAngularDrag = springJoint.connectedBody.angularDrag;
        springJoint.connectedBody.drag = drag;
        springJoint.connectedBody.angularDrag = angularDrag;
        Camera mainCamera = FindCamera();
        while (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            springJoint.transform.position = ray.GetPoint(distance);
            yield return null;
        }
        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }
	}

	public Camera FindCamera()
	{
		if ((bool)camera) return camera;
		return Camera.main;
	}
}
