using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class AudioSource : Behaviour
	{
		public extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float pitch
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int timeSamples
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioClip clip
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

		public extern bool loop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool ignoreListenerVolume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playOnAwake
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minVolume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxVolume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float rolloffFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayOneShot(AudioClip clip, float volumeScale);

		public void PlayOneShot(AudioClip clip)
		{
			float volumeScale = 1f;
			PlayOneShot(clip, volumeScale);
		}

		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			float num = 1f;
			PlayClipAtPoint(clip, position, num);
		}

		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length);
		}
	}
}
