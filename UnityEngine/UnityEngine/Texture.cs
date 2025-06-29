using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Texture : Object
	{
		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public virtual int width
		{
			get
			{
				return Internal_GetWidth(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		public virtual int height
		{
			get
			{
				return Internal_GetHeight(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		public extern FilterMode filterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int anisoLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float mipMapBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 texelSize
		{
			get
			{
				Vector2 output;
				Internal_GetTexelSize(this, out output);
				return output;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(Texture mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(Texture mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexelSize(Texture tex, out Vector2 output);
	}
}
