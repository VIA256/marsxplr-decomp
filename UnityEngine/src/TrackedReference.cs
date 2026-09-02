using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		private IntPtr m_Ptr;

		protected TrackedReference()
		{
		}

		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		public override int GetHashCode()
		{
			return (int)m_Ptr;
		}

		public unsafe static bool operator ==(TrackedReference x, TrackedReference y)
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

		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}
	}
}
