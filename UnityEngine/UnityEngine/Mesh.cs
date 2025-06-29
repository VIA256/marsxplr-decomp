using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Mesh : Object
	{
		public extern Vector3[] vertices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3[] normals
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector4[] tangents
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector2[] uv
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector2[] uv2
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2[] uv1
		{
			get
			{
				return uv2;
			}
			set
			{
				uv2 = value;
			}
		}

		public extern Bounds bounds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Color[] colors
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int[] triangles
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int vertexCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int subMeshCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern BoneWeight[] boneWeights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Matrix4x4[] bindposes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Mesh()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create(Mesh mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateNormals();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Optimize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTriangles(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTriangles(int[] triangles, int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTriangleStrip(int[] triangles, int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTriangleStrip(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices);

		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			bool useMatrices = true;
			CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}

		public void CombineMeshes(CombineInstance[] combine)
		{
			bool useMatrices = true;
			bool mergeSubMeshes = true;
			CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}
	}
}
