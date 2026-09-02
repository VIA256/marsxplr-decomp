using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class AssetBundle : Object
	{
		public extern Object mainAsset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Contains(string name);

		public Object Load(string name)
		{
			return Load(name, typeof(Object));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object Load(string name, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AssetBundleRequest LoadAsync(string name, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object[] LoadAll(Type type);

		public Object[] LoadAll()
		{
			return LoadAll(typeof(Object));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Unload(bool unloadAllLoadedObjects);
	}
}
