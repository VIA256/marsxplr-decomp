using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class SystemInfo
	{
		public static extern string operatingSystem
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string processorType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int processorCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int systemMemorySize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int graphicsMemorySize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string graphicsDeviceName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string graphicsDeviceVendor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string graphicsDeviceVersion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int graphicsShaderLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool supportsShadows
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool supportsRenderTextures
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool supportsImageEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool supportsVertexPrograms
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsRenderTextureFormat(RenderTextureFormat format);
	}
}
