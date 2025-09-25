using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class RenderBeforeQueues : Attribute
	{
		internal int[] m_Queues;

		public RenderBeforeQueues(params int[] args)
		{
			m_Queues = args;
		}
	}
}
