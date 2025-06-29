using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class Coroutine : YieldInstruction
	{
		private IntPtr ptr;

		private Coroutine()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseCoroutine();

		~Coroutine()
		{
			ReleaseCoroutine();
		}
	}
}
