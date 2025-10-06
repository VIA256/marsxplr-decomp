using System;
using UnityEngine;

[Serializable]
public class TankTrackSimple : MonoBehaviour
{
	public LayerMask terrainMask = ~1 << 4;
	// /*UNUSED*/ private RaycastHit hit;
    // /*UNUSED*/ private Transform myTransform;
    // /*UNUSED*/ private Vector3 linkPos;

	public void Start()
	{
        //myTransform = transform;
	}

	public void FixedUpdate()
	{
    	//if(linkPos == Vector3.zero) linkPos = myTransform.InverseTransformPoint(myTransform.position);

        /*if (Physics.Raycast(linkPos + transform.TransformPoint(Vector3.up * 5), transform.parent.TransformDirection(Vector3.down), hit, 5.5, terrainMask)) {
	        myTransform.position = hit.point;
	        myTransform.LookAt(hit.point + hit.normal);
        }
        else {*/
	        //myTransform.position = myTransform.TransformPoint(linkPos);
	        //myTransform.rotation = transform.parent.rotation;
        //}
	}
}
