using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class MaterialPropertyBlock
	{
		private IntPtr blockPtr;

		public MaterialPropertyBlock()
		{
			InitBlock();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitBlock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DestroyBlock();

		~MaterialPropertyBlock()
		{
			DestroyBlock();
		}

		public void AddFloat(string name, float value)
		{
			AddFloat(Shader.PropertyToID(name), value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddFloat(int nameID, float value);

		public void AddVector(string name, Vector4 value)
		{
			AddVector(Shader.PropertyToID(name), value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddVector(int nameID, Vector4 value);

		public void AddColor(string name, Color value)
		{
			AddColor(Shader.PropertyToID(name), value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddColor(int nameID, Color value);

		public void AddMatrix(string name, Matrix4x4 value)
		{
			AddMatrix(Shader.PropertyToID(name), value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddMatrix(int nameID, Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();
	}
}
