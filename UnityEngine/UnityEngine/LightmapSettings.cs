using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class LightmapSettings
	{
		public static extern LightmapData[] lightmaps
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
