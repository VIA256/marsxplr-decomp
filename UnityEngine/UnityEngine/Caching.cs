using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Caching
	{
		[Obsolete("this API is for not for public use.")]
		public static extern CacheIndex[] index
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int spaceAvailable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int spaceUsed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int expirationDelay
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
		public static extern bool Authorize(string name, string domain, int size, string signature);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CleanCache();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("this API is for not for public use.")]
		public static extern void CleanNamedCache(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DeleteFromCache(string url);
	}
}
