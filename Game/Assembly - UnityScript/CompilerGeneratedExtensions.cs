using System;
using Boo.Lang;
using CompilerGenerated;

namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
		 | AttributeTargets.Method)]
	public sealed class ExtensionAttribute : Attribute { }
}

public static class CompilerGeneratedExtensions
{
	[Extension]
	public static IAsyncResult BeginInvoke(
		this adaptableMethod self,
		AsyncCallback callback
	)
    {
		return self.BeginInvoke(callback, null);
	}

	[Extension]
	public static IAsyncResult BeginInvoke(
		this adaptableMethod self
	)
    {
		return self.BeginInvoke(null, null);
	}
}
