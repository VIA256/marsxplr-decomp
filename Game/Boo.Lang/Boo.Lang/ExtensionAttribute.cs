using System;

namespace Boo.Lang
{
	[Serializable]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public class ExtensionAttribute : Attribute
	{
	}
}
