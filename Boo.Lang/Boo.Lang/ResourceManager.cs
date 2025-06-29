using System;
using System.Resources;

namespace Boo.Lang
{
	public sealed class ResourceManager
	{
		private const string StringsResourceId = "strings";

		private static System.Resources.ResourceManager _rm = new System.Resources.ResourceManager("strings", typeof(ResourceManager).Assembly);

		private ResourceManager()
		{
		}

		public static string GetString(string name)
		{
			try
			{
				return _rm.GetString(name);
			}
			catch (Exception)
			{
				return "Resource not found: " + name;
			}
		}

		public static string Format(string name, params object[] args)
		{
			return string.Format(GetString(name), args);
		}

		public static string Format(string name, object param)
		{
			return string.Format(GetString(name), param);
		}
	}
}
