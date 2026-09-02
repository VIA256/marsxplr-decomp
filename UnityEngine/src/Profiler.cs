using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Profiler
	{
		public static extern string logFile
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample();
	}
}
