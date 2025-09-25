using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Boo.Lang;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		private ReferenceData m_UnityRuntimeReferenceData;

		public extern string name
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern HideFlags hideFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public override bool Equals(object o)
		{
			return CompareBaseObjects(this, o as Object);
		}

		public override int GetHashCode()
		{
			return m_UnityRuntimeReferenceData.instanceID;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareBaseObjects(Object lhs, Object rhs);

		public int GetInstanceID()
		{
			return m_UnityRuntimeReferenceData.instanceID;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingle(Object data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot);

		[DuckTyped]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			return Internal_InstantiateSingle(original, position, rotation);
		}

		[DuckTyped]
		public static Object Instantiate(Object original)
		{
			return Internal_CloneSingle(original);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(Object obj, float t);

		public static void Destroy(Object obj)
		{
			float t = 0f;
			Destroy(obj, t);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyImmediate(Object obj, bool allowDestroyingAssets);

		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			DestroyImmediate(obj, allowDestroyingAssets);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public static extern Object[] FindObjectsOfType(Type type);

		[DuckTyped]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad(Object target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyObject(Object obj, float t);

		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			DestroyObject(obj, t);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindSceneObjectsOfType(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		public static implicit operator bool(Object exists)
		{
			return !CompareBaseObjects(exists, null);
		}

		public static bool operator ==(Object x, Object y)
		{
			return CompareBaseObjects(x, y);
		}

		public static bool operator !=(Object x, Object y)
		{
			return !CompareBaseObjects(x, y);
		}
	}
}
