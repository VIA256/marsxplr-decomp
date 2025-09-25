using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class RenderTexture : Texture
	{
		public override int width
		{
			get
			{
				return Internal_GetWidth(this);
			}
			set
			{
				Internal_SetWidth(this, value);
			}
		}

		public override int height
		{
			get
			{
				return Internal_GetHeight(this);
			}
			set
			{
				Internal_SetHeight(this, value);
			}
		}

		public extern int depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPowerOfTwo
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTextureFormat format
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useMipMap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isCubemap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern RenderTexture active
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use SystemInfo.supportsRenderTextures instead.")]
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public RenderTexture(int width, int height, int depth)
		{
			Internal_CreateRenderTexture();
			this.width = width;
			this.height = height;
			this.depth = depth;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CreateRenderTexture();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format);

		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;
			return GetTemporary(width, height, depthBuffer, renderTextureFormat);
		}

		public static RenderTexture GetTemporary(int width, int height)
		{
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;
			int depthBuffer = 0;
			return GetTemporary(width, height, depthBuffer, renderTextureFormat);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(RenderTexture mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetWidth(RenderTexture mono, int width);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(RenderTexture mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHeight(RenderTexture mono, int width);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Create();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Release();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsCreated();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGlobalShaderProperty(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBorderColor(Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexelOffset(RenderTexture tex, out Vector2 output);

		public Vector2 GetTexelOffset()
		{
			Vector2 output;
			Internal_GetTexelOffset(this, out output);
			return output;
		}
	}
}
