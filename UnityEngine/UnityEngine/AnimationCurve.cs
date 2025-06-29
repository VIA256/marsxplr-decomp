using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationCurve
	{
		private IntPtr m_Curve;

		public Keyframe[] keys
		{
			get
			{
				return GetKeys();
			}
			set
			{
				SetKeys(value);
			}
		}

		public Keyframe this[int index]
		{
			get
			{
				return GetKey_Internal(index);
			}
		}

		public extern int length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern WrapMode preWrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WrapMode postWrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public AnimationCurve(params Keyframe[] keys)
		{
			Init(keys);
		}

		public AnimationCurve()
		{
			Init(null);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		~AnimationCurve()
		{
			Cleanup();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float Evaluate(float time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddKey(float time, float value);

		public int AddKey(Keyframe key)
		{
			return AddKey_Internal(key);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddKey_Internal(Keyframe key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int MoveKey(int index, Keyframe key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveKey(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetKeys(Keyframe[] keys);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe GetKey_Internal(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe[] GetKeys();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothTangents(int index, float weight);

		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			float num = (valueEnd - valueStart) / (timeEnd - timeStart);
			Keyframe[] array = new Keyframe[2];
			/*ref Keyframe reference = ref array[0];
			Keyframe keyframe = new Keyframe(timeStart, valueStart, 0f, num);
			reference = keyframe;*/
			array[0] = new Keyframe(timeStart, valueStart, 0f, num);
			/*ref Keyframe reference2 = ref array[1];
			Keyframe keyframe2 = new Keyframe(timeEnd, valueEnd, num, 0f);
			reference2 = keyframe2;*/
			array[1] = new Keyframe(timeEnd, valueEnd, num, 0f);
			Keyframe[] array2 = array;
			return new AnimationCurve(array2);
		}

		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			Keyframe[] array = new Keyframe[2];
			/*ref Keyframe reference = ref array[0];
			Keyframe keyframe = new Keyframe(timeStart, valueStart, 0f, 0f);
			reference = keyframe;*/
			array[0] = new Keyframe(timeStart, valueStart, 0f, 0f);
			/*ref Keyframe reference2 = ref array[1];
			Keyframe keyframe2 = new Keyframe(timeEnd, valueEnd, 0f, 0f);
			reference2 = keyframe2;*/
			array[1] = new Keyframe(timeEnd, valueEnd, 0f, 0f);
			Keyframe[] array2 = array;
			return new AnimationCurve(array2);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init(Keyframe[] keys);
	}
}
