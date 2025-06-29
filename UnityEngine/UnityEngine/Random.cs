using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Random
	{
		public static extern int seed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float value
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Vector3 insideUnitSphere
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 output;
				GetRandomUnitCircle(out output);
				return output;
			}
		}

		public static extern Vector3 onUnitSphere
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Quaternion rotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float min, float max);

		public static int Range(int min, int max)
		{
			return RandomRangeInt(min, max);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int min, int max);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Range(min, max);
		}

		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Range(min, max);
		}
	}
}
