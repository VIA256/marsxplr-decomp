using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	internal class DetailRenderer
	{
		private IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern DetailRenderer(TerrainData terrainData, Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render(Camera camera, Light[] lights, float viewDistance, bool lightmap, int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadAllDetails();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadDirtyDetails();
	}
}
