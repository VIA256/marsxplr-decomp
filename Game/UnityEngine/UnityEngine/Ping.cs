using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Ping
	{
		private IntPtr pingWrapper;

		public extern bool isDone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string ip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Ping(string address);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DestroyPing();

		~Ping()
		{
			DestroyPing();
		}
	}
}
