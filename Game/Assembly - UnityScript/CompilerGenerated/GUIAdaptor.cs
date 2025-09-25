using System;
using UnityEngine;

namespace CompilerGenerated
{
	[Serializable]
    public delegate void adaptableMethod();

	internal sealed class MethodToWindowFunction
	{
		private adaptableMethod method;

		public MethodToWindowFunction(adaptableMethod from)
		{
			method = from;
		}

		public void Invoke(int capacity)
		{
			method();
		}

		public static GUI.WindowFunction Adapt(adaptableMethod from)
		{
			return new MethodToWindowFunction(from).Invoke;
		}
	}
}
