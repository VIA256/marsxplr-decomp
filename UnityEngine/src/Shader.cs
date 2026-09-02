using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Shader : Object
	{
		public extern bool isSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int maximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int globalMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int renderQueue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("There's no reason whatsoever to use this")]
		public static extern void ClearAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader Find(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableKeyword(string keyword);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableKeyword(string keyword);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalColor(string propertyName, Color color);

		public static void SetGlobalVector(string propertyName, Vector4 vec)
		{
			SetGlobalColor(propertyName, vec);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalFloat(string propertyName, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexture(string propertyName, Texture tex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalMatrix(string propertyName, Matrix4x4 mat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexGenMode(string propertyName, TexGenMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTextureMatrixName(string propertyName, string matrixName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetStaticFloat(string propertyName, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetStaticFloat(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetStaticColor(string propertyName, Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetStaticColor(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetStaticTexture(string propertyName, Texture texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetStaticTexture(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PropertyToID(string name);
	}
}
