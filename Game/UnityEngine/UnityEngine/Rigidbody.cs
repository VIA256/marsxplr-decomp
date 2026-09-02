using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Rigidbody : Component
	{
		public extern Vector3 velocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 angularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float drag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float angularDrag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float mass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useGravity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isKinematic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool freezeRotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 centerOfMass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 worldCenterOfMass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Quaternion inertiaTensorRotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 inertiaTensor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool detectCollisions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useConeFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 position
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Quaternion rotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RigidbodyInterpolation interpolation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int solverIterationCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float sleepVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float sleepAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxAngularVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDensity(float density);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddForce(Vector3 force, ForceMode mode);

		public void AddForce(Vector3 force)
		{
			ForceMode mode = ForceMode.Force;
			AddForce(force, mode);
		}

		public void AddForce(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			AddForce(x, y, z, mode);
		}

		public void AddForce(float x, float y, float z, ForceMode mode)
		{
			Vector3 force = new Vector3(x, y, z);
			AddForce(force, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddRelativeForce(Vector3 force, ForceMode mode);

		public void AddRelativeForce(Vector3 force)
		{
			ForceMode mode = ForceMode.Force;
			AddRelativeForce(force, mode);
		}

		public void AddRelativeForce(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			AddRelativeForce(x, y, z, mode);
		}

		public void AddRelativeForce(float x, float y, float z, ForceMode mode)
		{
			Vector3 force = new Vector3(x, y, z);
			AddRelativeForce(force, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTorque(Vector3 torque, ForceMode mode);

		public void AddTorque(Vector3 torque)
		{
			ForceMode mode = ForceMode.Force;
			AddTorque(torque, mode);
		}

		public void AddTorque(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			AddTorque(x, y, z, mode);
		}

		public void AddTorque(float x, float y, float z, ForceMode mode)
		{
			Vector3 torque = new Vector3(x, y, z);
			AddTorque(torque, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddRelativeTorque(Vector3 torque, ForceMode mode);

		public void AddRelativeTorque(Vector3 torque)
		{
			ForceMode mode = ForceMode.Force;
			AddRelativeTorque(torque, mode);
		}

		public void AddRelativeTorque(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			AddRelativeTorque(x, y, z, mode);
		}

		public void AddRelativeTorque(float x, float y, float z, ForceMode mode)
		{
			Vector3 torque = new Vector3(x, y, z);
			AddRelativeTorque(torque, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode);

		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			ForceMode mode = ForceMode.Force;
			AddForceAtPosition(force, position, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier, ForceMode mode);

		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			ForceMode mode = ForceMode.Force;
			AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			ForceMode mode = ForceMode.Force;
			float upwardsModifier = 0f;
			AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ClosestPointOnBounds(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 GetRelativePointVelocity(Vector3 relativePoint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 GetPointVelocity(Vector3 worldPoint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MovePosition(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MoveRotation(Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		public void SetMaxAngularVelocity(float a)
		{
			maxAngularVelocity = a;
		}
	}
}
