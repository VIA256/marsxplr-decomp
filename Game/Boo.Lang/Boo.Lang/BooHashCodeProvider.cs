using System;
using System.Collections;

namespace Boo.Lang
{
	[Serializable]
	public class BooHashCodeProvider : IEqualityComparer
	{
		public static readonly IEqualityComparer Default = new BooHashCodeProvider();

		private BooHashCodeProvider()
		{
		}

		public int GetHashCode(object o)
		{
			if (o != null)
			{
				Array array = o as Array;
				if (array != null)
				{
					return GetArrayHashCode(array);
				}
				return o.GetHashCode();
			}
			return 0;
		}

		public new bool Equals(object lhs, object rhs)
		{
			return BooComparer.Default.Compare(lhs, rhs) == 0;
		}

		public int GetArrayHashCode(Array array)
		{
			int num = 1;
			int num2 = 0;
			foreach (object item in array)
			{
				num ^= GetHashCode(item) * ++num2;
			}
			return num;
		}
	}
}
