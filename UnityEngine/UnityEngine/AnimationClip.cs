using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class AnimationClip : Object
	{
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationClip();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCurve(string relativePath, Type type, string propertyName, AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnsureQuaternionContinuity();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearCurves();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddEvent(AnimationEvent evt);
	}
}
