using System;
using System.Collections;

namespace UnityEngine
{
	[ExecuteInEditMode]
	public class Terrain : MonoBehaviour
	{
		public enum TerrainRenderFlags
		{
			heightmap = 1,
			trees = 2,
			details = 4,
			all = 7
		}

		internal class Renderer
		{
			internal Camera camera;

			internal TerrainRenderer terrain;

			internal TreeRenderer trees;

			internal DetailRenderer details;

			internal int lastUsedFrame;
		}

		[SerializeField]
		private TerrainData m_TerrainData;

		[SerializeField]
		private float m_TreeDistance = 5000f;

		[SerializeField]
		private float m_TreeBillboardDistance = 50f;

		[SerializeField]
		private float m_TreeCrossFadeLength = 5f;

		[SerializeField]
		private int m_TreeMaximumFullLODCount = 50;

		[SerializeField]
		private float m_DetailObjectDistance = 80f;

		[SerializeField]
		private float m_HeightmapPixelError = 5f;

		[SerializeField]
		private float m_SplatMapDistance = 1000f;

		[SerializeField]
		private int m_HeightmapMaximumLOD;

		[SerializeField]
		private TerrainLighting m_RenderMode;

		[SerializeField]
		private bool m_CastShadows = true;

		[SerializeField]
		private bool m_UseLightmap;

		[NonSerialized]
		private Terrain m_LeftNeighbor;

		[NonSerialized]
		private Terrain m_RightNeighbor;

		[NonSerialized]
		private Terrain m_BottomNeighbor;

		[NonSerialized]
		private Terrain m_TopNeighbor;

		[NonSerialized]
		private Vector3 m_Position;

		[NonSerialized]
		private static int ms_CachedVersion = -1;

		[NonSerialized]
		private bool m_FiguredOutRenderMode;

		public bool m_DrawTreesAndFoliage = true;

		internal bool m_DebugDrawMainCamera;

		[NonSerialized]
		public TerrainRenderFlags m_EditorRenderFlags = TerrainRenderFlags.all;

		[NonSerialized]
		private ArrayList renderers = new ArrayList();

		private static ArrayList ms_ActiveTerrains = new ArrayList();

		private static ArrayList ms_TempCulledTerrains = new ArrayList();

		[NonSerialized]
		private TerrainChangedFlags dirtyFlags;

		private static Terrain ms_ActiveTerrain;

		public static Terrain activeTerrain
		{
			get
			{
				return ms_ActiveTerrain;
			}
		}

		public TerrainData terrainData
		{
			get
			{
				return m_TerrainData;
			}
			set
			{
				m_TerrainData = value;
			}
		}

		public float treeDistance
		{
			get
			{
				return m_TreeDistance;
			}
			set
			{
				m_TreeDistance = value;
			}
		}

		public float treeBillboardDistance
		{
			get
			{
				return m_TreeBillboardDistance;
			}
			set
			{
				m_TreeBillboardDistance = value;
			}
		}

		public float treeCrossFadeLength
		{
			get
			{
				return m_TreeCrossFadeLength;
			}
			set
			{
				m_TreeCrossFadeLength = value;
			}
		}

		public int treeMaximumFullLODCount
		{
			get
			{
				return m_TreeMaximumFullLODCount;
			}
			set
			{
				m_TreeMaximumFullLODCount = value;
			}
		}

		public float detailObjectDistance
		{
			get
			{
				return m_DetailObjectDistance;
			}
			set
			{
				m_DetailObjectDistance = value;
			}
		}

		public float heightmapPixelError
		{
			get
			{
				return m_HeightmapPixelError;
			}
			set
			{
				m_HeightmapPixelError = value;
			}
		}

		public int heightmapMaximumLOD
		{
			get
			{
				return m_HeightmapMaximumLOD;
			}
			set
			{
				m_HeightmapMaximumLOD = value;
			}
		}

		public float basemapDistance
		{
			get
			{
				return m_SplatMapDistance;
			}
			set
			{
				m_SplatMapDistance = value;
			}
		}

		[Obsolete("use basemapDistance", true)]
		public float splatmapDistance
		{
			get
			{
				return m_SplatMapDistance;
			}
			set
			{
				m_SplatMapDistance = value;
			}
		}

		public TerrainLighting lighting
		{
			get
			{
				return m_RenderMode;
			}
			set
			{
				m_RenderMode = value;
			}
		}

		[Obsolete("use lighting", true)]
		public bool useLightmap
		{
			get
			{
				return m_UseLightmap;
			}
			set
			{
				m_UseLightmap = value;
			}
		}

		public bool castShadows
		{
			get
			{
				return m_CastShadows;
			}
			set
			{
				m_CastShadows = value;
			}
		}

		public float SampleHeight(Vector3 worldPosition)
		{
			worldPosition -= GetPosition();
			worldPosition.x /= m_TerrainData.size.x;
			worldPosition.z /= m_TerrainData.size.z;
			return m_TerrainData.GetInterpolatedHeight(worldPosition.x, worldPosition.z);
		}

		public static GameObject CreateTerrainGameObject(TerrainData assignTerrain)
		{
			GameObject gameObject = new GameObject("Terrain", typeof(Terrain), typeof(TerrainCollider));
			Terrain terrain = gameObject.GetComponent(typeof(Terrain)) as Terrain;
			TerrainCollider terrainCollider = gameObject.GetComponent(typeof(TerrainCollider)) as TerrainCollider;
			terrainCollider.terrainData = assignTerrain;
			terrain.terrainData = assignTerrain;
			terrain.OnEnable();
			return gameObject;
		}

		private void FlushDirty()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			TerrainChangedFlags terrainChangedFlags = (TerrainChangedFlags)0;
			if ((dirtyFlags & TerrainChangedFlags.Heightmap) != 0 || (dirtyFlags & TerrainChangedFlags.HeightmapDelayedUpdate) != 0)
			{
				flag3 = true;
				flag = true;
				flag4 = true;
			}
			if ((dirtyFlags & TerrainChangedFlags.DetailPrototypes) != 0)
			{
				flag3 = true;
				flag = true;
			}
			if ((dirtyFlags & TerrainChangedFlags.DetailData) != 0)
			{
				flag3 = true;
				flag4 = true;
				flag2 = true;
			}
			if (flag4 && m_EditorRenderFlags != TerrainRenderFlags.heightmap)
			{
				m_TerrainData.RecalculateTreePositions();
				m_TerrainData.RefreshTreeInstances();
			}
			if (flag3 && m_EditorRenderFlags != TerrainRenderFlags.heightmap)
			{
				foreach (Renderer renderer7 in renderers)
				{
					renderer7.trees.ReloadTrees();
				}
			}
			if (flag2 && !flag && m_EditorRenderFlags != TerrainRenderFlags.heightmap)
			{
				foreach (Renderer renderer8 in renderers)
				{
					renderer8.details.ReloadDirtyDetails();
				}
			}
			if (flag && m_EditorRenderFlags != TerrainRenderFlags.heightmap)
			{
				m_TerrainData.RefreshTreePrototypes();
				foreach (Renderer renderer9 in renderers)
				{
					renderer9.details.ReloadAllDetails();
				}
			}
			if ((dirtyFlags & TerrainChangedFlags.HeightmapDelayedUpdate) != 0)
			{
				if (m_EditorRenderFlags == TerrainRenderFlags.heightmap)
				{
					foreach (Renderer renderer10 in renderers)
					{
						renderer10.terrain.ReloadPrecomputedError();
					}
					terrainChangedFlags |= TerrainChangedFlags.HeightmapDelayedUpdate;
				}
				else
				{
					int[] array = m_TerrainData.ComputeDelayedLod();
					if (array.Length != 0)
					{
						foreach (Renderer renderer11 in renderers)
						{
							renderer11.terrain.ReloadPrecomputedError();
							renderer11.terrain.ReloadBounds();
						}
					}
				}
			}
			if ((dirtyFlags & TerrainChangedFlags.Heightmap) != 0)
			{
				foreach (Renderer renderer12 in renderers)
				{
					renderer12.terrain.ReloadAll();
				}
			}
			dirtyFlags = terrainChangedFlags;
		}

		private static void CullAllTerrains(int cullingMask)
		{
			ms_TempCulledTerrains.Clear();
			int count = ms_ActiveTerrains.Count;
			for (int i = 0; i < count; i++)
			{
				Terrain terrain = (Terrain)ms_ActiveTerrains[i];
				int layer = terrain.gameObject.layer;
				if (((1 << layer) & cullingMask) == 0)
				{
					continue;
				}
				ms_TempCulledTerrains.Add(terrain);
				Vector3 position = terrain.GetPosition();
				if (position != terrain.m_Position)
				{
					terrain.m_Position = position;
					terrain.Flush();
				}
				if (ms_CachedVersion != 1 && !terrain.m_FiguredOutRenderMode)
				{
					terrain.m_RenderMode = (terrain.m_UseLightmap ? TerrainLighting.Lightmap : TerrainLighting.Vertex);
					terrain.m_FiguredOutRenderMode = true;
				}
				terrain.GarbageCollectRenderers();
				Profiler.BeginSample("Terrain.FlushDirty");
				terrain.FlushDirty();
				Profiler.EndSample();
				Profiler.BeginSample("Terrain.Heightmap.RenderStep1");
				Renderer renderer = terrain.GetRenderer();
				if (renderer != null)
				{
					terrain.terrainData.RecalculateBaseMapIfDirty();
					if ((terrain.m_EditorRenderFlags & TerrainRenderFlags.heightmap) != 0)
					{
						float splatDistance = ((terrain.m_EditorRenderFlags != TerrainRenderFlags.heightmap) ? terrain.m_SplatMapDistance : float.PositiveInfinity);
						renderer.terrain.RenderStep1(renderer.camera, terrain.m_RenderMode, terrain.m_HeightmapMaximumLOD, terrain.m_HeightmapPixelError, splatDistance, layer);
					}
				}
				Profiler.EndSample();
			}
			count = ms_TempCulledTerrains.Count;
			for (int j = 0; j < count; j++)
			{
				Terrain terrain2 = (Terrain)ms_TempCulledTerrains[j];
				TerrainRenderer terrainRendererDontCreate = terrain2.GetTerrainRendererDontCreate();
				if (terrainRendererDontCreate != null && (terrain2.m_EditorRenderFlags & TerrainRenderFlags.heightmap) != 0)
				{
					TerrainRenderer left = null;
					TerrainRenderer right = null;
					TerrainRenderer top = null;
					TerrainRenderer bottom = null;
					if (terrain2.m_LeftNeighbor != null)
					{
						left = terrain2.m_LeftNeighbor.GetTerrainRendererDontCreate();
					}
					if (terrain2.m_RightNeighbor != null)
					{
						right = terrain2.m_RightNeighbor.GetTerrainRendererDontCreate();
					}
					if (terrain2.m_TopNeighbor != null)
					{
						top = terrain2.m_TopNeighbor.GetTerrainRendererDontCreate();
					}
					if (terrain2.m_BottomNeighbor != null)
					{
						bottom = terrain2.m_BottomNeighbor.GetTerrainRendererDontCreate();
					}
					terrainRendererDontCreate.SetNeighbors(left, top, right, bottom);
				}
			}
			count = ms_TempCulledTerrains.Count;
			for (int k = 0; k < count; k++)
			{
				Terrain terrain3 = (Terrain)ms_TempCulledTerrains[k];
				TerrainRenderer terrainRendererDontCreate2 = terrain3.GetTerrainRendererDontCreate();
				if (terrainRendererDontCreate2 != null && (terrain3.m_EditorRenderFlags & TerrainRenderFlags.heightmap) != 0)
				{
					Profiler.BeginSample("Terrain.Heightmap.RenderStep2");
					terrainRendererDontCreate2.RenderStep2();
					Profiler.EndSample();
				}
			}
			count = ms_TempCulledTerrains.Count;
			for (int l = 0; l < count; l++)
			{
				Terrain terrain4 = (Terrain)ms_TempCulledTerrains[l];
				Renderer renderer2 = terrain4.GetRenderer();
				if (renderer2 == null)
				{
					continue;
				}
				int layer2 = terrain4.gameObject.layer;
				Light[] lights = Light.GetLights(LightType.Directional, layer2);
				Profiler.BeginSample("Terrain.Heightmap.RenderStep3");
				if ((terrain4.m_EditorRenderFlags & TerrainRenderFlags.heightmap) != 0)
				{
					float realtimeLightDistance = 1f;
					if (terrain4.m_RenderMode == TerrainLighting.Pixel)
					{
						realtimeLightDistance = Mathf.Min(terrain4.m_TreeBillboardDistance, QualitySettings.shadowDistance);
						realtimeLightDistance = Mathf.Min(realtimeLightDistance, terrain4.m_SplatMapDistance);
						realtimeLightDistance = Mathf.Max(realtimeLightDistance, 0.1f);
					}
					renderer2.terrain.RenderStep3(renderer2.camera, layer2, realtimeLightDistance, terrain4.m_CastShadows);
				}
				Profiler.EndSample();
				Profiler.BeginSample("Terrain.Details.Render");
				if ((terrain4.m_EditorRenderFlags & TerrainRenderFlags.details) != 0 && terrain4.m_DrawTreesAndFoliage && (double)terrain4.m_DetailObjectDistance > 0.001)
				{
					renderer2.details.Render(renderer2.camera, lights, terrain4.m_DetailObjectDistance, terrain4.m_RenderMode != TerrainLighting.Vertex, layer2);
				}
				Profiler.EndSample();
				Profiler.BeginSample("Terrain.Trees.Render");
				if ((terrain4.m_EditorRenderFlags & TerrainRenderFlags.trees) != 0 && terrain4.m_DrawTreesAndFoliage && (double)terrain4.m_TreeDistance > 0.001)
				{
					renderer2.trees.Render(renderer2.camera, lights, terrain4.m_TreeBillboardDistance, terrain4.m_TreeDistance, terrain4.m_TreeCrossFadeLength, terrain4.m_TreeMaximumFullLODCount, terrain4.m_RenderMode != TerrainLighting.Vertex, layer2);
				}
				Profiler.EndSample();
			}
		}

		private static void CullAllTerrainsShadowCaster()
		{
			foreach (Terrain ms_ActiveTerrain in ms_ActiveTerrains)
			{
				Renderer renderer = ms_ActiveTerrain.GetRenderer();
				if (renderer != null && (ms_ActiveTerrain.m_EditorRenderFlags & TerrainRenderFlags.trees) != 0 && ms_ActiveTerrain.m_DrawTreesAndFoliage && (double)ms_ActiveTerrain.m_TreeDistance > 0.001)
				{
					renderer.trees.RenderShadowCasters(renderer.camera, Mathf.Min(ms_ActiveTerrain.m_TreeBillboardDistance, ms_ActiveTerrain.m_TreeDistance), ms_ActiveTerrain.m_TreeMaximumFullLODCount, ms_ActiveTerrain.gameObject.layer);
				}
			}
		}

		public void Flush()
		{
			if (terrainData != null)
			{
				m_TerrainData.RefreshTreePrototypes();
				m_TerrainData.RefreshTreeInstances();
			}
			foreach (Renderer renderer in renderers)
			{
				renderer.trees.Cleanup();
				renderer.terrain.Dispose();
				renderer.details.Dispose();
			}
			renderers = new ArrayList();
		}

		private void GarbageCollectRenderers()
		{
			int renderedFrameCount = Time.renderedFrameCount;
			for (int num = renderers.Count - 1; num >= 0; num--)
			{
				Renderer renderer = (Renderer)renderers[num];
				int num2 = renderedFrameCount - renderer.lastUsedFrame;
				if (num2 > 100 || num2 < 0 || renderer.camera == null)
				{
					renderer.trees.Cleanup();
					renderer.terrain.Dispose();
					renderer.details.Dispose();
					renderers.RemoveAt(num);
				}
			}
		}

		public void AddTreeInstance(TreeInstance instance)
		{
			bool flag = m_TerrainData.HasTreeInstances();
			m_TerrainData.AddTree(out instance);
			foreach (Renderer renderer in renderers)
			{
				if (flag)
				{
					renderer.trees.InjectTree(out instance);
					continue;
				}
				renderer.trees.Cleanup();
				renderer.trees = new TreeRenderer(m_TerrainData, GetPosition());
			}
		}

		public void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			if (m_TerrainData.RemoveTrees(position, radius, prototypeIndex) == 0)
			{
				return;
			}
			foreach (Renderer renderer in renderers)
			{
				renderer.trees.RemoveTrees(position, radius, prototypeIndex);
			}
		}

		private void OnTerrainChanged(TerrainChangedFlags flags)
		{
			if ((flags & TerrainChangedFlags.FlushImmediately) != 0)
			{
				Flush();
			}
			else
			{
				dirtyFlags |= flags;
			}
		}

		private void OnEnable()
		{
			if ((bool)m_TerrainData)
			{
				m_TerrainData.RefreshTreePrototypes();
				m_TerrainData.RefreshTreeInstances();
				m_TerrainData.AddUser(base.gameObject);
			}
			ms_ActiveTerrain = this;
			if (!ms_ActiveTerrains.Contains(this))
			{
				ms_ActiveTerrains.Add(this);
			}
		}

		public void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom)
		{
			m_TopNeighbor = top;
			m_LeftNeighbor = left;
			m_RightNeighbor = right;
			m_BottomNeighbor = bottom;
		}

		private void OnDisable()
		{
			ms_ActiveTerrains.Remove(this);
			if (ms_ActiveTerrain == this)
			{
				ms_ActiveTerrain = null;
			}
			if ((bool)m_TerrainData)
			{
				m_TerrainData.RemoveUser(base.gameObject);
			}
			ms_ActiveTerrains.Remove(this);
			Flush();
		}

		private TerrainRenderer GetTerrainRendererDontCreate()
		{
			Camera camera = Camera.current;
			if ((camera.cullingMask & (1 << base.gameObject.layer)) == 0)
			{
				return null;
			}
			if (m_DebugDrawMainCamera && Camera.main != null)
			{
				camera = Camera.main;
			}
			foreach (Renderer renderer in renderers)
			{
				if (renderer.camera == camera)
				{
					return renderer.terrain;
				}
			}
			return null;
		}

		private Renderer GetRenderer()
		{
			Camera camera = Camera.current;
			if ((camera.cullingMask & (1 << base.gameObject.layer)) == 0)
			{
				return null;
			}
			if (m_DebugDrawMainCamera && Camera.main != null)
			{
				camera = Camera.main;
			}
			int renderedFrameCount = Time.renderedFrameCount;
			foreach (Renderer renderer3 in renderers)
			{
				if (renderer3.camera == camera)
				{
					if (renderer3.terrain.terrainData == null)
					{
						Flush();
						break;
					}
					renderer3.lastUsedFrame = renderedFrameCount;
					return renderer3;
				}
			}
			if (m_TerrainData != null)
			{
				Vector3 position = GetPosition();
				Renderer renderer2 = new Renderer();
				renderer2.camera = camera;
				renderer2.terrain = new TerrainRenderer(m_TerrainData, position);
				renderer2.trees = new TreeRenderer(m_TerrainData, position);
				renderer2.details = new DetailRenderer(m_TerrainData, position);
				renderer2.lastUsedFrame = renderedFrameCount;
				renderers.Add(renderer2);
				return renderer2;
			}
			return null;
		}

		private Vector3 GetPosition()
		{
			if (ms_CachedVersion == -1)
			{
				if (Application.GetBuildUnityVersion() >= Application.GetNumericUnityVersion("2.1a1"))
				{
					ms_CachedVersion = 1;
				}
				else
				{
					ms_CachedVersion = 0;
				}
			}
			if (ms_CachedVersion == 1)
			{
				return base.transform.position;
			}
			return Vector3.zero;
		}
	}
}
