using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class AudioClip : Object
	{
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int samples
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int channels
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int frequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isReadyToPlay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
