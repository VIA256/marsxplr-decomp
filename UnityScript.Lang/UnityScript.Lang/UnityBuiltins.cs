using System;

namespace UnityScript.Lang
{
	[Serializable]
	public class UnityBuiltins
	{
		public static object eval(string code)
		{
			return null;
		}

		public static int parseInt(string value)
		{
			return int.Parse(value);
		}

		public static int parseInt(float value)
		{
			return checked((int)value);
		}

		public static int parseInt(int value)
		{
			return value;
		}

		public static float parseFloat(string value)
		{
			return float.Parse(value);
		}

		public static float parseFloat(float value)
		{
			return value;
		}

		public static float parseFloat(int value)
		{
			return value;
		}
	}
}
