using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	internal class TerrainRenderer
	{
		private IntPtr m_Ptr;

		public extern TerrainData terrainData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TerrainRenderer(TerrainData terrainData, Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadPrecomputedError();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadBounds();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Internal_RenderStep1(Camera camera, int renderMode, int maxLodLevel, float tau, float splatDistance, int layer);

		public void RenderStep1(Camera camera, TerrainLighting renderMode, int maxLodLevel, float tau, float splatDistance, int layer)
		{
			Internal_RenderStep1(camera, (int)renderMode, maxLodLevel, tau, splatDistance, layer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderStep2();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderStep3(Camera camera, int layer, float realtimeLightDistance, bool castShadows);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetNeighbors(TerrainRenderer left, TerrainRenderer top, TerrainRenderer right, TerrainRenderer bottom);
	}
}
