using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Cubemap : Texture
	{
		public extern TextureFormat format
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Cubemap(int size, TextureFormat format, bool mipmap)
		{
			Internal_Create(this, size, format, mipmap);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixel(CubemapFace face, int x, int y, Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixel(CubemapFace face, int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, int miplevel);

		public Color[] GetPixels(CubemapFace face)
		{
			int miplevel = 0;
			return GetPixels(face, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int miplevel);

		public void SetPixels(Color[] colors, CubemapFace face)
		{
			int miplevel = 0;
			SetPixels(colors, face, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply(bool updateMipmaps);

		public void Apply()
		{
			bool updateMipmaps = true;
			Apply(updateMipmaps);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create(Cubemap mono, int size, TextureFormat format, bool mipmap);
	}
}
