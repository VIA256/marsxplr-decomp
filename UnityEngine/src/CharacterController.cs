using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class CharacterController : Collider
	{
		public extern bool isGrounded
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Vector3 velocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern CollisionFlags collisionFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 center
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float slopeLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stepOffset
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

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SimpleMove(Vector3 speed);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CollisionFlags Move(Vector3 motion);
	}
}
