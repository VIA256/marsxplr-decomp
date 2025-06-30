using System;
using Boo.Lang;

namespace UnityScript.Lang
{
	[Serializable]
	public class Extensions
	{
		[Extension]
		public static int get_length(System.Array a) {
			return a.Length;
		}

		public static int get_length(string s) {
			return s.Length;
		}

		[Extension]
		public static bool op_Equality(char lhs, string rhs)
		{
			bool num = 1 == rhs.Length;
			if (num)
			{
				num = lhs == rhs[0];
			}
			return num;
		}
		
		[Extension]
		public static bool op_Equality(string lhs, char rhs)
		{
			bool num = 1 == lhs.Length;
			if (num)
			{
				num = rhs == lhs[0];
			}
			return num;
		}

		[Extension]
		public static bool op_Inequality(char lhs, string rhs)
		{
			bool num = 1 != rhs.Length;
			if (!num)
			{
				num = lhs != rhs[0];
			}
			return num;
		}

		[Extension]
		public static bool op_Inequality(string lhs, char rhs)
		{
			bool num = 1 != lhs.Length;
			if (!num)
			{
				num = rhs != lhs[0];
			}
			return num;
		}

		//FUCK
		/*
		[Extension]
		public static implicit operator bool(Enum e)
		{
			return ((IConvertible)e).ToInt32((IFormatProvider)null) != 0;
		}*/
	}
}
