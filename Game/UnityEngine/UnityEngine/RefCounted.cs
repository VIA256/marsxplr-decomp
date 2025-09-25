using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class RefCounted
	{
		private IntPtr m_Ptr;

		protected RefCounted()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ReleaseRefCounted();

		~RefCounted()
		{
			Internal_ReleaseRefCounted();
		}

		public override bool Equals(object o)
		{
			return o as RefCounted == this;
		}

		public override int GetHashCode()
		{
			return (int)m_Ptr;
		}

		public unsafe static bool operator ==(RefCounted x, RefCounted y)
		{
			if ((object)y == null)
			{
				if ((object)x == null)
				{
					return true;
				}
				return x.m_Ptr == (IntPtr)(void*)null;
			}
			if ((object)x == null)
			{
				if ((object)y == null)
				{
					return true;
				}
				return y.m_Ptr == (IntPtr)(void*)null;
			}
			return x.m_Ptr == y.m_Ptr;
		}

		public static bool operator !=(RefCounted x, RefCounted y)
		{
			return !(x == y);
		}

		public static implicit operator bool(RefCounted exists)
		{
			return exists != null;
		}
	}
}
