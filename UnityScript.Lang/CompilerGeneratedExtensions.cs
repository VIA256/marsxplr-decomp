using System;
using Boo.Lang;
using CompilerGenerated;
using UnityScript.Lang;

public static class CompilerGeneratedExtensions
{
	/*private CompilerGeneratedExtensions()
	{
	}*/

	[Extension]
	public static IAsyncResult BeginInvoke(this __ExpandoServices_0024callable1_002463_29__ self, Expando e, AsyncCallback callback)
	{
		return self.BeginInvoke(e, callback, null);
	}

	[Extension]
	public static IAsyncResult BeginInvoke(this __ExpandoServices_0024callable1_002463_29__ self, Expando e)
	{
		return self.BeginInvoke(e, null, null);
	}
}
