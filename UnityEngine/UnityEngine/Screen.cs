using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Screen
	{
		public static extern Resolution[] resolutions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Resolution[] GetResolution
		{
			get
			{
				return resolutions;
			}
		}

		public static extern Resolution currentResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool showCursor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool lockCursor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool fullScreen
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate);

		public static void SetResolution(int width, int height, bool fullscreen)
		{
			int preferredRefreshRate = 0;
			SetResolution(width, height, fullscreen, preferredRefreshRate);
		}
	}
}
