using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class TerrainData : Object
	{
		public extern Texture2D lightmap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int heightmapWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int heightmapHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int heightmapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 heightmapScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Vector3 size
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int mipLevels
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Texture2D plantAtlasTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float wavingGrassStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wavingGrassAmount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wavingGrassSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Color wavingGrassTint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int detailWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int detailHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int detailResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int patchCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern DetailPrototype[] detailPrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TreeInstance[] treeInstances
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TreePrototype[] treePrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int alphamapLayers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int alphamapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int alphamapWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int alphamapHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int baseMapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int alphamapTextureCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = GetAlphamapTexture(i);
				}
				return array;
			}
		}

		public extern SplatPrototype[] splatPrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TerrainData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Init(int splatRes, int detailRes, int basemapRes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddUser(GameObject user);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveUser(GameObject user);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpdateUsers(TerrainChangedFlags whatChanged);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetHeight(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInterpolatedHeight(float x, float y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,] GetHeights(int xBase, int yBase, int width, int height);

		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new NullReferenceException();
			}
			if (xBase + heights.GetLength(1) > heightmapWidth || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > heightmapHeight)
			{
				throw new Exception(string.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{3}", xBase + heights.GetLength(1), yBase + heights.GetLength(0), heightmapWidth, heightmapHeight));
			}
			Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetAdjustedSize(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetSteepness(float x, float y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 GetInterpolatedNormal(float x, float y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetMaximumHeightError(int x, int y, int level);

		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			Internal_SetHeightsDelayLOD(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int[] ComputeDelayedLod();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Bounds GetBounds(int xPatch, int yPatch, int mipLevel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetTotalPatchCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsPatchEmpty(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsPatchDirty(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetDirtyDetails();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshPrototypes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Mesh GenerateDetailMesh(int patchX, int patchY, Vector3 size, bool useLightmap, DetailRenderMode renderMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateTreePositions();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveTreePrototype(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveDetailPrototype(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpdateDetailPrototypesIfDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,,] GetAlphamaps(int x, int y, int width, int height);

		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			if (map.GetLength(2) != alphamapLayers)
			{
				throw new Exception(string.Format("Float array size wrong (layers should be {0})", alphamapLayers));
			}
			Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Texture2D GetAlphamapTexture(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture2D GetBaseMap();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecalculateBaseMapIfDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBaseMapDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasTreeInstances();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RefreshTreeInstances();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RefreshTreePrototypes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddTree(out TreeInstance tree);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int RemoveTrees(Vector2 position, float radius, int prototypeIndex);
	}
}
