using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Physics
	{
		public const int kIgnoreRaycastLayer = 4;

		public const int kDefaultRaycastLayers = -5;

		public const int kAllLayers = -1;

		public static extern Vector3 gravity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float minPenetrationForPenalty
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float bounceThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Please use bounceThreshold instead.")]
		public static float bounceTreshold
		{
			get
			{
				return bounceThreshold;
			}
			set
			{
				bounceThreshold = value;
			}
		}

		public static extern float sleepVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float sleepAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float maxAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int solverIterationCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float penetrationPenaltyForce
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RaycastTest(Vector3 origin, Vector3 direction, float distance, int layermask);

		public static bool Raycast(Vector3 origin, Vector3 direction, float distance)
		{
			int layerMask = -5;
			return Raycast(origin, direction, distance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			int layerMask = -5;
			float distance = float.PositiveInfinity;
			return Raycast(origin, direction, distance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			return Internal_RaycastTest(origin, direction, distance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance)
		{
			int layerMask = -5;
			return Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			int layerMask = -5;
			float distance = float.PositiveInfinity;
			return Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layerMask)
		{
			return Internal_Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		public static bool Raycast(Ray ray, float distance)
		{
			int layerMask = -5;
			return Raycast(ray, distance, layerMask);
		}

		public static bool Raycast(Ray ray)
		{
			int layerMask = -5;
			float distance = float.PositiveInfinity;
			return Raycast(ray, distance, layerMask);
		}

		public static bool Raycast(Ray ray, float distance, int layerMask)
		{
			return Raycast(ray.origin, ray.direction, distance, layerMask);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			int layerMask = -5;
			return Raycast(ray, out hitInfo, distance, layerMask);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			int layerMask = -5;
			float distance = float.PositiveInfinity;
			return Raycast(ray, out hitInfo, distance, layerMask);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float distance, int layerMask)
		{
			return Raycast(ray.origin, ray.direction, out hitInfo, distance, layerMask);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float distance)
		{
			int layerMask = -5;
			return RaycastAll(ray, distance, layerMask);
		}

		public static RaycastHit[] RaycastAll(Ray ray)
		{
			int layerMask = -5;
			float distance = float.PositiveInfinity;
			return RaycastAll(ray, distance, layerMask);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float distance, int layerMask)
		{
			return RaycastAll(ray.origin, ray.direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float distance, int layermask);

		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float distance)
		{
			int layermask = -5;
			return RaycastAll(origin, direction, distance, layermask);
		}

		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			int layermask = -5;
			float distance = float.PositiveInfinity;
			return RaycastAll(origin, direction, distance, layermask);
		}

		public static bool Linecast(Vector3 start, Vector3 end)
		{
			int layerMask = -5;
			return Linecast(start, end, layerMask);
		}

		public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
		{
			Vector3 direction = end - start;
			return Raycast(start, direction, direction.magnitude, layerMask);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			int layerMask = -5;
			return Linecast(start, end, out hitInfo, layerMask);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
		{
			Vector3 direction = end - start;
			return Raycast(start, direction, out hitInfo, direction.magnitude, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Collider[] OverlapSphere(Vector3 position, float radius, int layerMask);

		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			int layerMask = -1;
			return OverlapSphere(position, radius, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CheckSphere(Vector3 position, float radius, int layerMask);

		public static bool CheckSphere(Vector3 position, float radius)
		{
			int layerMask = -5;
			return CheckSphere(position, radius, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask);

		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			int layermask = -5;
			return CheckCapsule(start, end, radius, layermask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision(Collider collider1, Collider collider2, bool ignore);

		public static void IgnoreCollision(Collider collider1, Collider collider2)
		{
			bool ignore = true;
			IgnoreCollision(collider1, collider2, ignore);
		}
	}
}
