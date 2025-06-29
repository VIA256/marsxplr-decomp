using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class BoxCollider : Collider
	{
		public extern Vector3 center
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 size
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 extents
		{
			get
			{
				return size * 0.5f;
			}
			set
			{
				size = value * 2f;
			}
		}
	}
}
