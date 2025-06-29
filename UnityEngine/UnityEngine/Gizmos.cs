using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Gizmos
	{
		public static extern Color color
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern Matrix4x4 matrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static void DrawRay(Ray r)
		{
			DrawLine(r.origin, r.origin + r.direction);
		}

		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			DrawLine(from, from + direction);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawLine(Vector3 from, Vector3 to);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawWireSphere(Vector3 center, float radius);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawSphere(Vector3 center, float radius);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawWireCube(Vector3 center, Vector3 size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawCube(Vector3 center, Vector3 size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawIcon(Vector3 center, string name);

		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material mat = null;
			DrawGUITexture(screenRect, texture, mat);
		}

		public static void DrawGUITexture(Rect screenRect, Texture texture, Material mat)
		{
			DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat);

		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect);
	}
}
