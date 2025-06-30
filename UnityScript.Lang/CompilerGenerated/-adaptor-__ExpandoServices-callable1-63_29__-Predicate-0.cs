using System;
using UnityScript.Lang;

namespace CompilerGenerated
{
	[Serializable]
	internal sealed class _0024adaptor_0024__ExpandoServices_0024callable1_002463_29___0024Predicate_00240
	{
		protected __ExpandoServices_0024callable1_002463_29__ _0024from;

		public _0024adaptor_0024__ExpandoServices_0024callable1_002463_29___0024Predicate_00240(__ExpandoServices_0024callable1_002463_29__ from)
		{
			_0024from = from;
		}

		public bool Invoke(Expando value)
		{
			return _0024from(value);
		}

		public static Predicate<Expando> Adapt(__ExpandoServices_0024callable1_002463_29__ from)
		{
			return new _0024adaptor_0024__ExpandoServices_0024callable1_002463_29___0024Predicate_00240(from).Invoke;
		}
	}
}
