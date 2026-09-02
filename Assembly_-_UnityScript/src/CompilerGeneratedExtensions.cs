using System;
using CompilerGenerated;

public static class CompilerGeneratedExtensions
{
	public static IAsyncResult BeginInvoke(
		this adaptableMethod self,
		AsyncCallback callback
	)
    {
		return self.BeginInvoke(callback, null);
	}

	public static IAsyncResult BeginInvoke(
		this adaptableMethod self
	)
    {
		return self.BeginInvoke(null, null);
	}
}
