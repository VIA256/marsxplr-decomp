using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Graphics
	{
		[Obsolete("Use SystemInfo.graphicsDeviceName instead.")]
		public static string deviceName
		{
			get
			{
				return SystemInfo.graphicsDeviceName;
			}
		}

		[Obsolete("Use SystemInfo.graphicsDeviceVendor instead.")]
		public static string deviceVendor
		{
			get
			{
				return SystemInfo.graphicsDeviceVendor;
			}
		}

		[Obsolete("Use SystemInfo.graphicsDeviceVersion instead.")]
		public static string deviceVersion
		{
			get
			{
				return SystemInfo.graphicsDeviceVersion;
			}
		}

		[Obsolete("Use SystemInfo.supportsVertexPrograms instead.")]
		public static bool supportsVertexProgram
		{
			get
			{
				return SystemInfo.supportsVertexPrograms;
			}
		}

		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			MaterialPropertyBlock properties = null;
			DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Camera camera = null;
			DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Internal_DrawMeshTR(mesh, position, rotation, material, layer, camera, submeshIndex, properties, true, true);
		}

		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			MaterialPropertyBlock properties = null;
			DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Camera camera = null;
			DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties);
		}

		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Internal_DrawMeshMatrix(mesh, matrix, material, layer, camera, submeshIndex, properties, true, true);
		}

		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Internal_DrawMeshTR(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Internal_DrawMeshMatrix(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshTR(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshMatrix(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows);

		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Internal_DrawMeshNow1(mesh, position, rotation, -1);
		}

		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Internal_DrawMeshNow1(mesh, position, rotation, materialIndex);
		}

		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Internal_DrawMeshNow2(mesh, matrix, materialIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow1(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow2(Mesh mesh, Matrix4x4 matrix, int materialIndex);

		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Internal_DrawMeshNow1(mesh, position, rotation, -1);
		}

		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Internal_DrawMeshNow1(mesh, position, rotation, materialIndex);
		}

		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix)
		{
			Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		[Obsolete("Use Graphics.DrawMeshNow instead.")]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Internal_DrawMeshNow2(mesh, matrix, materialIndex);
		}

		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Material mat = null;
			DrawTexture(screenRect, texture, mat);
		}

		public static void DrawTexture(Rect screenRect, Texture texture, Material mat)
		{
			DrawTexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
			DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Color color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
			DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat);

		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Material mat = null;
			DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Blit(Texture source, RenderTexture dest);

		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			int pass = -1;
			Blit(source, dest, mat, pass);
		}

		public static void Blit(Texture source, RenderTexture dest, Material mat, int pass)
		{
			Internal_BlitMaterial(source, dest, mat, pass);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, Material mat, int pass);

		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			Internal_BlitMultiTap(source, dest, mat, offsets);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, Material mat, Vector2[] offsets);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetupVertexLights(Light[] lights);
	}
}
