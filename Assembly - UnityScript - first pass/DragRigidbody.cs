using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class DragRigidbody : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class DragObject_00241 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal float _0024oldDrag_00244;

			internal float _0024oldAngularDrag_00245;

			internal Camera _0024mainCamera_00246;

			internal Ray _0024ray_00247;

			internal float _0024distance8;

			internal DragRigidbody _0024self_9;

			public _0024(float distance, DragRigidbody self_)
			{
				_0024distance8 = distance;
				_0024self_9 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					_0024oldDrag_00244 = _0024self_9.springJoint.connectedBody.drag;
					_0024oldAngularDrag_00245 = _0024self_9.springJoint.connectedBody.angularDrag;
					_0024self_9.springJoint.connectedBody.drag = _0024self_9.drag;
					_0024self_9.springJoint.connectedBody.angularDrag = _0024self_9.angularDrag;
					_0024mainCamera_00246 = _0024self_9.FindCamera();
					goto case 2;
				case 2:
					if (Input.GetMouseButton(0))
					{
						_0024ray_00247 = _0024mainCamera_00246.ScreenPointToRay(Input.mousePosition);
						_0024self_9.springJoint.transform.position = _0024ray_00247.GetPoint(_0024distance8);
						return Yield(2, null);
					}
					if ((bool)_0024self_9.springJoint.connectedBody)
					{
						_0024self_9.springJoint.connectedBody.drag = _0024oldDrag_00244;
						_0024self_9.springJoint.connectedBody.angularDrag = _0024oldAngularDrag_00245;
						_0024self_9.springJoint.connectedBody = null;
					}
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal float _0024distance10;

		internal DragRigidbody _0024self_11;

		public DragObject_00241(float distance, DragRigidbody self_)
		{
			_0024distance10 = distance;
			_0024self_11 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024distance10, _0024self_11);
		}
	}

	public float spring;

	public float damper;

	public float drag;

	public float angularDrag;

	public float distance;

	public bool attachToCenterOfMass;

	private SpringJoint springJoint;

	public DragRigidbody()
	{
		spring = 50f;
		damper = 5f;
		drag = 10f;
		angularDrag = 5f;
		distance = 0.2f;
		attachToCenterOfMass = false;
	}

	public void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Camera camera = FindCamera();
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f) && (bool)hitInfo.rigidbody && !hitInfo.rigidbody.isKinematic)
		{
			if (!springJoint)
			{
				GameObject gameObject = new GameObject("Rigidbody dragger");
				object target = gameObject.AddComponent("Rigidbody");
				springJoint = (SpringJoint)gameObject.AddComponent("SpringJoint");
				RuntimeServices.SetProperty(target, "isKinematic", true);
			}
			springJoint.transform.position = hitInfo.point;
			if (attachToCenterOfMass)
			{
				Vector3 position = transform.TransformDirection(hitInfo.rigidbody.centerOfMass) + hitInfo.rigidbody.transform.position;
				position = springJoint.transform.InverseTransformPoint(position);
				springJoint.anchor = position;
			}
			else
			{
				springJoint.anchor = Vector3.zero;
			}
			springJoint.spring = spring;
			springJoint.damper = damper;
			springJoint.maxDistance = distance;
			springJoint.connectedBody = hitInfo.rigidbody;
			StartCoroutine("DragObject", hitInfo.distance);
		}
	}

	public IEnumerator DragObject(float distance)
	{
		return new DragObject_00241(distance, this).GetEnumerator();
	}

	public Camera FindCamera()
	{
		if ((bool)camera)
		{
			return camera;
		}
		return Camera.main;
	}

	public void Main()
	{
	}
}
