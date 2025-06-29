using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GeometryUtility
	{
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			return CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix);
		}

		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			Internal_ExtractPlanes(array, worldToProjectionMatrix);
			return array;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExtractPlanes(Plane[] planes, Matrix4x4 worldToProjectionMatrix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TestPlanesAABB(Plane[] planes, Bounds bounds);
	}
}
