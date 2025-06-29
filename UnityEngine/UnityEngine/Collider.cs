using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Collider : Component
	{
		public extern Rigidbody attachedRigidbody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isTrigger
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterial material
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterial sharedMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Bounds bounds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ClosestPointOnBounds(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float distance);

		public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Internal_Raycast(this, ray, out hitInfo, distance);
		}
	}
}
