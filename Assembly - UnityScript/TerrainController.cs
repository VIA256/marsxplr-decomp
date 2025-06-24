using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class TerrainController : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class OnPrefsUpdated_002461 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal TerrainController _0024self_704;

			public _0024(TerrainController self_)
			{
				_0024self_704 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					if (!World.sea || _0024self_704.seaLevel == World.sea.position.y)
					{
						break;
					}
					if (_0024self_704.updateTime == -1f)
					{
						_0024self_704.updateTime = Time.time + 3f;
						goto case 2;
					}
					_0024self_704.updateTime = Time.time + 3f;
					goto IL_00c1;
				case 2:
					if (Time.time < _0024self_704.updateTime)
					{
						return Yield(2, null);
					}
					_0024self_704.ReSplat();
					goto IL_00c1;
				case 1:
					break;
					IL_00c1:
					Yield(1, null);
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal TerrainController _0024self_705;

		public OnPrefsUpdated_002461(TerrainController self_)
		{
			_0024self_705 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024self_705);
		}
	}

	public WhirldObject whirldObject;

	public Terrain trn;

	public TerrainData trnDat;

	public float updateTime;

	public float seaLevel;

	public TerrainControllerData dat;

	public TerrainController()
	{
		updateTime = -1f;
	}

	public void OnSceneGenerated()
	{
		if (!trnDat)
		{
			UnityEngine.Object.Destroy(this);
		}
		trn = (Terrain)GetComponent(typeof(Terrain));
		whirldObject = (WhirldObject)gameObject.GetComponent(typeof(WhirldObject));
		if (!whirldObject || whirldObject.@params == null || !RuntimeServices.ToBool(whirldObject.@params["SeaFloorTexture"]))
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		whirldObject.Activate();
		SplatPrototype[] splatPrototypes = trnDat.splatPrototypes;
		checked
		{
			SplatPrototype[] array = new SplatPrototype[Extensions.get_length((System.Array)trnDat.splatPrototypes) + 1];
			for (int i = 0; i < Extensions.get_length((System.Array)trnDat.splatPrototypes); i++)
			{
				array[RuntimeServices.NormalizeArrayIndex(array, i)] = splatPrototypes[RuntimeServices.NormalizeArrayIndex(splatPrototypes, i)];
			}
			array[RuntimeServices.NormalizeArrayIndex(array, Extensions.get_length((System.Array)array) - 1)] = new SplatPrototype();
			array[RuntimeServices.NormalizeArrayIndex(array, Extensions.get_length((System.Array)array) - 1)].texture = (Texture2D)RuntimeServices.Coerce(whirldObject.@params["SeaFloorTexture"], typeof(Texture2D));
			array[RuntimeServices.NormalizeArrayIndex(array, Extensions.get_length((System.Array)array) - 1)].tileSize = new Vector2(15f, 15f);
			trnDat.splatPrototypes = array;
			dat = new TerrainControllerData();
			dat.alphaMap = trnDat.GetAlphamaps(0, 0, trnDat.alphamapResolution, trnDat.alphamapResolution);
			dat.heightMap = trnDat.GetHeights(0, 0, trnDat.heightmapWidth, trnDat.heightmapHeight);
			ReSplat();
		}
	}

	public IEnumerator OnPrefsUpdated()
	{
		return new OnPrefsUpdated_002461(this).GetEnumerator();
	}

	public void ReSplat()
	{
		updateTime = -1f;
		if ((bool)World.sea)
		{
			seaLevel = World.sea.position.y;
		}
		else
		{
			if (!GameObject.Find("Sea"))
			{
				return;
			}
			seaLevel = GameObject.Find("Sea").transform.position.y;
		}
		float[,,] alphamaps = trnDat.GetAlphamaps(0, 0, trnDat.alphamapResolution, trnDat.alphamapResolution);
		checked
		{
			for (int i = 0; i < trnDat.alphamapResolution; i++)
			{
				for (int j = 0; j < trnDat.alphamapResolution; j++)
				{
					bool flag = false;
					float num = transform.position.y + UnityBuiltins.parseFloat(RuntimeServices.UnboxSingle(dat.heightMap.GetValue(new int[2]
					{
						(int)(UnityBuiltins.parseFloat((float)j / UnityBuiltins.parseFloat(trnDat.alphamapResolution)) * (float)trnDat.heightmapWidth),
						(int)(UnityBuiltins.parseFloat((float)i / UnityBuiltins.parseFloat(trnDat.alphamapResolution)) * (float)trnDat.heightmapHeight)
					})) * trnDat.size.y);
					if (num < seaLevel)
					{
						flag = true;
						float num2 = seaLevel + num;
					}
					for (int k = 0; k < trnDat.alphamapLayers; k++)
					{
						if (flag)
						{
							alphamaps.SetValue((k == trnDat.alphamapLayers - 1) ? 1 : 0, new int[3] { j, i, k });
							continue;
						}
						alphamaps.SetValue((k != trnDat.alphamapLayers - 1) ? RuntimeServices.UnboxSingle(dat.alphaMap.GetValue(new int[3] { j, i, k })) : 0f, new int[3] { j, i, k });
					}
				}
			}
			trnDat.SetAlphamaps(0, 0, alphamaps);
			trn.terrainData = trnDat;
			trnDat.SetBaseMapDirty();
			trnDat.ResetDirtyDetails();
			trnDat.RefreshPrototypes();
			trn.Flush();
		}
	}

	public void Main()
	{
	}
}
