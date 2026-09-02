using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class MovieTexture : Texture
	{
		public extern AudioClip audioClip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool loop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isReadyToPlay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();
	}
}
