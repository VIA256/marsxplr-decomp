using System;
using System.Collections;
using System.Collections.Generic;
using CompilerGenerated;

namespace UnityScript.Lang
{
	[Serializable]
	public class ExpandoServices
	{
		[Serializable]
		internal class GetExpandoFor_0024closure_00244
		{
			internal ___locals18 _____locals19;

			public GetExpandoFor_0024closure_00244(___locals18 _____locals19)
			{
				this._____locals19 = _____locals19;
			}

			public bool Invoke(Expando e)
			{
				return e.Target == _____locals19.___o_0;
			}
		}

		protected static List<Expando> _expandos = new List<Expando>();

		public static int ExpandoObjectCount
		{
			get
			{
				Purge();
				return ((ICollection)_expandos).Count;
			}
		}

		public static object GetExpandoProperty(object target, string name)
		{
			Expando expandoFor = GetExpandoFor(target);
			if (expandoFor == null)
			{
				return null;
			}
			return expandoFor[name];
		}

		public static object SetExpandoProperty(object target, string name, object value)
		{
			Expando orCreateExpandoFor = GetOrCreateExpandoFor(target);
			orCreateExpandoFor[name] = value;
			return value;
		}

		public static Expando GetExpandoFor(object o)
		{
			___locals18 __locals = new ___locals18();
			__locals.___o_0 = o;
			lock (_expandos)
			{
				Purge();
				return _expandos.Find(_0024adaptor_0024__ExpandoServices_0024callable1_002463_29___0024Predicate_00240.Adapt(new GetExpandoFor_0024closure_00244(__locals).Invoke));
			}
		}

		public static Expando GetOrCreateExpandoFor(object o)
		{
			lock (_expandos)
			{
				Expando expando = GetExpandoFor(o);
				if (expando == null)
				{
					expando = new Expando(o);
					_expandos.Add(expando);
				}
				return expando;
			}
		}

		public static void Purge()
		{
			lock (_expandos)
			{
				_expandos.RemoveAll(_0024adaptor_0024__ExpandoServices_0024callable1_002463_29___0024Predicate_00240.Adapt((Expando e) => e.Target == null));
			}
		}

		internal static bool Purge_0024closure_00245(Expando e)
		{
			return e.Target == null;
		}
	}
}
