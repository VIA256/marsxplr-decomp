using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Transform : Component, IEnumerable
	{
		private class Enumerator : IEnumerator
		{
			private Transform outer;

			private int currentIndex = -1;

			public object Current
			{
				get
				{
					return outer.GetChild(currentIndex);
				}
			}

			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			public bool MoveNext()
			{
				int childCount = outer.childCount;
				return ++currentIndex < childCount;
			}

			public void Reset()
			{
				currentIndex = -1;
			}
		}

		public extern Vector3 position
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 localPosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 eulerAngles
		{
			get
			{
				return rotation.eulerAngles;
			}
			set
			{
				rotation = Quaternion.Euler(value);
			}
		}

		public extern Vector3 localEulerAngles
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 right
		{
			get
			{
				return rotation * Vector3.right;
			}
			set
			{
				rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		public Vector3 up
		{
			get
			{
				return rotation * Vector3.up;
			}
			set
			{
				rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		public Vector3 forward
		{
			get
			{
				return rotation * Vector3.forward;
			}
			set
			{
				rotation = Quaternion.LookRotation(value);
			}
		}

		public extern Quaternion rotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Quaternion localRotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 localScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Transform parent
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Matrix4x4 worldToLocalMatrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Matrix4x4 localToWorldMatrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Transform root
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int childCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Vector3 lossyScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private Transform()
		{
		}

		public void Translate(Vector3 translation)
		{
			Space relativeTo = Space.Self;
			Translate(translation, relativeTo);
		}

		public void Translate(Vector3 translation, Space relativeTo)
		{
			if (relativeTo == Space.World)
			{
				position += translation;
			}
			else
			{
				position += TransformDirection(translation);
			}
		}

		public void Translate(float x, float y, float z)
		{
			Space relativeTo = Space.Self;
			Translate(x, y, z, relativeTo);
		}

		public void Translate(float x, float y, float z, Space relativeTo)
		{
			Vector3 translation = new Vector3(x, y, z);
			Translate(translation, relativeTo);
		}

		public void Translate(Vector3 translation, Transform relativeTo)
		{
			if ((bool)relativeTo)
			{
				position += relativeTo.TransformDirection(translation);
			}
			else
			{
				position += translation;
			}
		}

		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			Vector3 translation = new Vector3(x, y, z);
			Translate(translation, relativeTo);
		}

		public void Rotate(Vector3 eulerAngles)
		{
			Space relativeTo = Space.Self;
			Rotate(eulerAngles, relativeTo);
		}

		public void Rotate(Vector3 eulerAngles, Space relativeTo)
		{
			Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (relativeTo == Space.Self)
			{
				localRotation *= quaternion;
			}
			else
			{
				rotation *= Quaternion.Inverse(rotation) * quaternion * rotation;
			}
		}

		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			Space relativeTo = Space.Self;
			Rotate(xAngle, yAngle, zAngle, relativeTo);
		}

		public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
		{
			Vector3 vector = new Vector3(xAngle, yAngle, zAngle);
			Rotate(vector, relativeTo);
		}

		public void Rotate(Vector3 axis, float angle)
		{
			Space relativeTo = Space.Self;
			Rotate(axis, angle, relativeTo);
		}

		public void Rotate(Vector3 axis, float angle, Space relativeTo)
		{
			if (relativeTo == Space.Self)
			{
				RotateAround(base.transform.TransformDirection(axis), angle * ((float)Math.PI / 180f));
			}
			else
			{
				RotateAround(axis, angle * ((float)Math.PI / 180f));
			}
		}

		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = position;
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = quaternion * vector2;
			vector = point + vector2;
			position = vector;
			RotateAround(axis, angle * ((float)Math.PI / 180f));
		}

		public void LookAt(Transform target)
		{
			Vector3 worldUp = Vector3.up;
			LookAt(target, worldUp);
		}

		public void LookAt(Transform target, Vector3 worldUp)
		{
			if ((bool)target)
			{
				LookAt(target.position, worldUp);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LookAt(Vector3 worldPosition, Vector3 worldUp);

		public void LookAt(Vector3 worldPosition)
		{
			Vector3 worldUp = Vector3.up;
			LookAt(worldPosition, worldUp);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 TransformDirection(Vector3 direction);

		public Vector3 TransformDirection(float x, float y, float z)
		{
			Vector3 direction = new Vector3(x, y, z);
			return TransformDirection(direction);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 InverseTransformDirection(Vector3 direction);

		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			Vector3 direction = new Vector3(x, y, z);
			return InverseTransformDirection(direction);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 TransformPoint(Vector3 position);

		public Vector3 TransformPoint(float x, float y, float z)
		{
			Vector3 vector = new Vector3(x, y, z);
			return TransformPoint(vector);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 InverseTransformPoint(Vector3 position);

		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			Vector3 vector = new Vector3(x, y, z);
			return InverseTransformPoint(vector);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachChildren();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform Find(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsChildOf(Transform parent);

		public Transform FindChild(string name)
		{
			return Find(name);
		}

		public virtual IEnumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RotateAround(Vector3 axis, float angle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RotateAroundLocal(Vector3 axis, float angle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetChild(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetChildCount();
	}
}
