using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	internal class TreeRenderer
	{
		private IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TreeRenderer(TerrainData data, Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReloadTrees();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render(Camera camera, Light[] lights, float meshTreeDistance, float billboardTreeDistance, float crossFadeLength, int maximumMeshTrees, bool useLightMap, int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RenderShadowCasters(Camera camera, float meshTreeDistance, int maximumMeshTrees, int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Cleanup();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InjectTree(out TreeInstance newTree);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveTrees(Vector3 pos, float radius, int prototypeIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InvalidateImposters();
	}
}
