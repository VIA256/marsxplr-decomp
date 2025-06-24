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
	/*private CompilerGeneratedExtensions()
	{
	}*/

	[Extension]
	public static IAsyncResult BeginInvoke(this __Lobby_OnGUI_0024callable0_0024274_261__ self, AsyncCallback callback)
	{
		return self.BeginInvoke(callback, null);
	}

	[Extension]
	public static IAsyncResult BeginInvoke(this __Lobby_OnGUI_0024callable0_0024274_261__ self)
	{
		return self.BeginInvoke(null, null);
	}
}
