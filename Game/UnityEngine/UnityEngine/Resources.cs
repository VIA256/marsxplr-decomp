using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Resources
	{
		public static Object Load(string path)
		{
			return Load(path, typeof(Object));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object Load(string path, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] LoadAll(string path, Type type);

		public static Object[] LoadAll(string path)
		{
			return LoadAll(path, typeof(Object));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetBuiltinResource(Type type, string path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object LoadAssetAtPath(string assetPath, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
