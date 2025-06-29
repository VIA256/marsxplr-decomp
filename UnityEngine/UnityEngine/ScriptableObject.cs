using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class ScriptableObject : Object
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ScriptableObject();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use EditorUtilty.SetDirty instead")]
		public extern void SetDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ScriptableObject CreateInstance(string className);
	}
}
