using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Texture2D : Texture
	{
		public extern int mipmapCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern TextureFormat format
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Texture2D(int width, int height)
		{
			Internal_Create(this, width, height, TextureFormat.ARGB32, true);
		}

		public Texture2D(int width, int height, TextureFormat format, bool mipmap)
		{
			Internal_Create(this, width, height, format, mipmap);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create(Texture2D mono, int width, int height, TextureFormat format, bool mipmap);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixel(int x, int y, Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixel(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixelBilinear(float u, float v);

		public void SetPixels(Color[] colors)
		{
			int miplevel = 0;
			SetPixels(colors, miplevel);
		}

		public void SetPixels(Color[] colors, int miplevel)
		{
			int num = width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			SetPixels(0, 0, num, num2, colors, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, int miplevel);

		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			int miplevel = 0;
			SetPixels(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadImage(byte[] data);

		public Color[] GetPixels()
		{
			int miplevel = 0;
			return GetPixels(miplevel);
		}

		public Color[] GetPixels(int miplevel)
		{
			int num = width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			return GetPixels(0, 0, num, num2, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, int miplevel);

		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			int miplevel = 0;
			return GetPixels(x, y, blockWidth, blockHeight, miplevel);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply(bool updateMipmaps);

		public void Apply()
		{
			bool updateMipmaps = true;
			Apply(updateMipmaps);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Resize(int width, int height, TextureFormat format, bool hasMipMap);

		public bool Resize(int width, int height)
		{
			return Internal_ResizeWH(width, height);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_ResizeWH(int width, int height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Compress(bool highQuality);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize);

		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			int maximumAtlasSize = 2048;
			return PackTextures(textures, padding, maximumAtlasSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReadPixels(Rect source, int destX, int destY, bool recalculateMipMaps);

		public void ReadPixels(Rect source, int destX, int destY)
		{
			bool recalculateMipMaps = true;
			ReadPixels(source, destX, destY, recalculateMipMaps);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] EncodeToPNG();
	}
}
