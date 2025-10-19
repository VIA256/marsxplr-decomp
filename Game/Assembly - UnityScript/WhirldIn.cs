//********************************************************************************************************************************************
//*********************************** Whirld - by Aubrey Falconer ****************************************************************************
//**** http://AubreyFalconer.com **** http://web.archive.org/web/20120519040400/http://www.unifycommunity.com/wiki/index.php?title=Whirld ****
//********************************************************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using Ionic.Zlib;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public enum WhirldInStatus
{
    Idle,
    Working,
    Success,
    WWWError,
    SyntaxError
}

[Serializable]
public class WhirldIn
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class LoadMeshTexture_002458 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal string _0024thread_0024608;

			internal WWW _0024www_0024609;

			internal Texture2D _0024mshTxt_0024610;

			internal string _0024url611;

			internal string _0024materialName612;

			internal WhirldIn _0024self_613;

			public _0024(string url, string materialName, WhirldIn self_)
			{
				_0024url611 = url;
				_0024materialName612 = materialName;
				_0024self_613 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						_0024self_613.threadTextures++;
						goto case 2;
					case 2:
						if (_0024self_613.threads.Count >= _0024self_613.maxThreads)
						{
							return Yield(2, null);
						}
						_0024thread_0024608 = "MshTxt" + _0024self_613.threadTextures + " - " + _0024materialName612;
						_0024self_613.threads.Add(_0024thread_0024608, string.Empty);
						_0024url611 = (string)RuntimeServices.Coerce(_0024self_613.GetURL(_0024url611), typeof(string));
						_0024www_0024609 = new WWW(_0024url611);
						goto case 3;
					case 3:
						if (!_0024www_0024609.isDone)
						{
							_0024self_613.threads[_0024thread_0024608] = _0024www_0024609.progress;
							return Yield(3, null);
						}
						if (_0024www_0024609.error != null)
						{
							_0024self_613.info += "Failed to download mesh texture: " + _0024url611 + " (" + _0024www_0024609.error + ")\n";
							_0024self_613.threads.Remove(_0024thread_0024608);
							_0024self_613.threadTextures--;
							break;
						}
						_0024self_613.threads[_0024thread_0024608] = "Initializing";
						_0024mshTxt_0024610 = new Texture2D(4, 4, TextureFormat.DXT1, true);
						_0024www_0024609.LoadImageIntoTexture(_0024mshTxt_0024610);
						_0024mshTxt_0024610.wrapMode = TextureWrapMode.Repeat;
						_0024mshTxt_0024610.Apply(true);
						_0024mshTxt_0024610.Compress(true);
						RuntimeServices.SetProperty(_0024self_613.meshMaterials[_0024materialName612], "mainTexture", _0024mshTxt_0024610);
						_0024self_613.threads.Remove(_0024thread_0024608);
						_0024self_613.threadTextures--;
						Yield(1, null);
						break;
					case 1:
						break;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal string _0024url614;

		internal string _0024materialName615;

		internal WhirldIn _0024self_616;

		public LoadMeshTexture_002458(string url, string materialName, WhirldIn self_)
		{
			_0024url614 = url;
			_0024materialName615 = materialName;
			_0024self_616 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024url614, _0024materialName615, _0024self_616);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class LoadMesh_002459 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal string[] _0024vS_0024617;

			internal string _0024thread_0024618;

			internal int _0024hasCollider_0024619;

			internal WWW _0024www_0024620;

			internal int _0024lastDot_0024621;

			internal string _0024data_0024622;

			internal string _0024ext_0024623;

			internal Mesh _0024msh_0024624;

			internal UnityScript.Lang.Array _0024verts_0024625;

			internal UnityScript.Lang.Array _0024norms_0024626;

			internal UnityScript.Lang.Array _0024uvs_0024627;

			internal UnityScript.Lang.Array _0024tris_0024628;

			internal UnityScript.Lang.Array _0024triangles_0024629;

			internal UnityScript.Lang.Array _0024mats_0024630;

			internal float _0024timer_0024631;

			internal string[] _0024file_0024632;

			internal string _0024str_0024633;

			internal string[] _0024l_0024634;

			internal int _0024i_0024635;

			internal string[] _0024meshlib_0024636;

			internal Material _0024curMat_0024637;

			internal int _0024offset_0024638;

			internal string _0024meshline_0024639;

			internal string[] _0024ml_0024640;

			internal string _0024shdr_0024641;

			internal GameObject _0024mshObj_0024642;

			internal int _0024___temp230_0024643;

			internal string[] _0024___temp231_0024644;

			internal int _0024___temp232_0024645;

			internal int _0024___temp234_0024646;

			internal string[] _0024___temp235_0024647;

			internal int _0024___temp236_0024648;

			internal string _0024v649;

			internal WhirldIn _0024self_650;

			public _0024(string v, WhirldIn self_)
			{
				_0024v649 = v;
				_0024self_650 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						if (_0024self_650.threads.Count >= _0024self_650.maxThreads)
						{
							return Yield(2, null);
						}
						_0024vS_0024617 = _0024v649.Split(","[0]);
						_0024thread_0024618 = _0024vS_0024617[0];
						_0024self_650.threads.Add(_0024thread_0024618, string.Empty);
						_0024hasCollider_0024619 = ((Extensions.get_length((System.Array)_0024vS_0024617) > 2) ? UnityBuiltins.parseInt(_0024vS_0024617[2]) : 0);
						_0024www_0024620 = new WWW((string)RuntimeServices.Coerce(_0024self_650.GetURL(_0024vS_0024617[1]), typeof(string)));
						goto case 3;
					case 3:
						if (!_0024www_0024620.isDone)
						{
							_0024self_650.threads[_0024thread_0024618] = _0024www_0024620.progress;
							return Yield(3, null);
						}
						if (_0024www_0024620.error != null)
						{
							_0024self_650.info += "Failed to download mesh: " + _0024self_650.url + " (" + _0024www_0024620.error + ")\n";
							_0024self_650.threads.Remove(_0024thread_0024618);
							break;
						}
						_0024self_650.threads[_0024thread_0024618] = "Decompressing";
						return Yield(4, null);
					case 4:
						_0024lastDot_0024621 = _0024vS_0024617[1].LastIndexOf(".");
						if (_0024vS_0024617[1].Substring(_0024lastDot_0024621 + 1) == "gz")
						{
							_0024data_0024622 = GZipStream.UncompressString(_0024www_0024620.bytes);
							_0024vS_0024617[1] = _0024vS_0024617[1].Substring(0, _0024lastDot_0024621);
						}
						else
						{
							_0024data_0024622 = _0024www_0024620.data;
						}
						_0024self_650.threads[_0024thread_0024618] = "Generating";
						_0024lastDot_0024621 = _0024vS_0024617[1].LastIndexOf(".");
						_0024ext_0024623 = _0024vS_0024617[1].Substring(_0024lastDot_0024621 + 1);
						if (!(_0024ext_0024623 == "utm"))
						{
							if (_0024ext_0024623 == "obj")
							{
								_0024msh_0024624 = new Mesh();
								_0024verts_0024625 = new UnityScript.Lang.Array();
								_0024norms_0024626 = new UnityScript.Lang.Array();
								_0024uvs_0024627 = new UnityScript.Lang.Array();
								_0024tris_0024628 = new UnityScript.Lang.Array();
								_0024triangles_0024629 = new UnityScript.Lang.Array();
								_0024mats_0024630 = new UnityScript.Lang.Array();
								_0024timer_0024631 = Time.time + 0.1f;
								_0024file_0024632 = _0024data_0024622.Split("\n"[0]);
								_0024___temp234_0024646 = 0;
								_0024___temp235_0024647 = _0024file_0024632;
								_0024___temp236_0024648 = _0024___temp235_0024647.Length;
								goto IL_0df5;
							}
							_0024self_650.info += "Mesh Type Unrecognized: " + _0024vS_0024617[0] + " " + _0024vS_0024617[1] + " (." + _0024ext_0024623 + ")\n";
						}
						goto IL_0fe7;
					case 5:
						_0024___temp234_0024646++;
						goto IL_0df5;
					case 1:
						break;
						IL_0df5:
						if (_0024___temp234_0024646 < _0024___temp236_0024648)
						{
							if (!(_0024___temp235_0024647[_0024___temp234_0024646] == string.Empty))
							{
								_0024l_0024634 = _0024___temp235_0024647[_0024___temp234_0024646].Split(" "[0]);
								if (_0024l_0024634[0] == "v")
								{
									_0024verts_0024625.Add(new Vector3(UnityBuiltins.parseFloat(_0024l_0024634[1]) * -1f, UnityBuiltins.parseFloat(_0024l_0024634[2]), UnityBuiltins.parseFloat(_0024l_0024634[3])));
								}
								else if (_0024l_0024634[0] == "vn")
								{
									_0024norms_0024626.Add(new Vector3(UnityBuiltins.parseFloat(_0024l_0024634[1]), UnityBuiltins.parseFloat(_0024l_0024634[2]), UnityBuiltins.parseFloat(_0024l_0024634[3])));
								}
								else if (_0024l_0024634[0] == "vt")
								{
									_0024uvs_0024627.Add(new Vector2(UnityBuiltins.parseFloat(_0024l_0024634[1]), UnityBuiltins.parseFloat(_0024l_0024634[2])));
								}
								else if (_0024l_0024634[0] == "f")
								{
									if (Extensions.get_length((System.Array)_0024l_0024634) == 4)
									{
										_0024tris_0024628.Add(UnityBuiltins.parseInt(_0024l_0024634[2].Substring(0, _0024l_0024634[2].IndexOf("/"))) - 1);
										_0024tris_0024628.Add(UnityBuiltins.parseInt(_0024l_0024634[1].Substring(0, _0024l_0024634[1].IndexOf("/"))) - 1);
										_0024tris_0024628.Add(UnityBuiltins.parseInt(_0024l_0024634[3].Substring(0, _0024l_0024634[3].IndexOf("/"))) - 1);
									}
									else
									{
										for (_0024i_0024635 = 2; _0024i_0024635 < Extensions.get_length((System.Array)_0024l_0024634); _0024i_0024635++)
										{
											UnityScript.Lang.Array array = _0024tris_0024628;
											string[] array2 = _0024l_0024634;
											string obj = array2[RuntimeServices.NormalizeArrayIndex(array2, _0024i_0024635)];
											string[] array3 = _0024l_0024634;
											array.Add(UnityBuiltins.parseInt(obj.Substring(0, array3[RuntimeServices.NormalizeArrayIndex(array3, _0024i_0024635)].IndexOf("/"))) - 1);
											if (unchecked(_0024i_0024635 % 2) == 0)
											{
												_0024tris_0024628.Add(UnityBuiltins.parseInt(_0024l_0024634[1].Substring(0, _0024l_0024634[1].IndexOf("/"))) - 1);
											}
										}
										while (unchecked(_0024tris_0024628.length % 3) != 0)
										{
											UnityScript.Lang.Array array4 = _0024tris_0024628;
											string[] array5 = _0024l_0024634;
											string obj2 = array5[RuntimeServices.NormalizeArrayIndex(array5, _0024i_0024635 - 2)];
											string[] array6 = _0024l_0024634;
											array4.Add(UnityBuiltins.parseInt(obj2.Substring(0, array6[RuntimeServices.NormalizeArrayIndex(array6, _0024i_0024635 - 2)].IndexOf("/"))) - 1);
										}
									}
								}
								else if (_0024l_0024634[0] == "usemtl")
								{
									if (_0024self_650.meshMaterials.ContainsKey(_0024l_0024634[1]))
									{
										_0024mats_0024630.Add(_0024self_650.meshMaterials[_0024l_0024634[1]]);
									}
									else
									{
										_0024self_650.info += "Mesh Material Missing: " + _0024l_0024634[1] + "\n";
										_0024mats_0024630.Add(null);
									}
									if (_0024tris_0024628.length > 0)
									{
										_0024triangles_0024629.Add(_0024tris_0024628);
										_0024tris_0024628 = new UnityScript.Lang.Array();
									}
								}
								else if (_0024l_0024634[0] == "mtllib" && !_0024self_650.meshMatLibs.ContainsKey(_0024l_0024634[1]))
								{
									_0024self_650.meshMatLibs.Add(_0024l_0024634[1], true);
									_0024www_0024620 = new WWW((string)RuntimeServices.Coerce(_0024self_650.GetURL(_0024l_0024634[1]), typeof(string)));
									while (!_0024www_0024620.isDone)
									{
										_0024self_650.threads[_0024thread_0024618] = "Downloading Material Library (" + Mathf.RoundToInt(_0024www_0024620.progress * 100f) + "%)";
									}
									if (_0024www_0024620.error != null)
									{
										_0024self_650.info = (string)RuntimeServices.Coerce(RuntimeServices.InvokeBinaryOperator("op_Addition", _0024self_650.info, RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", "Mesh Material Library Undownloadable: ", _0024self_650.GetURL(_0024l_0024634[1])), " ("), _0024www_0024620.error), ")\n")), typeof(string));
									}
									else
									{
										_0024self_650.threads[_0024thread_0024618] = "Initializing " + _0024vS_0024617[0] + string.Empty;
										_0024meshlib_0024636 = _0024www_0024620.data.Split("\n"[0]);
										_0024curMat_0024637 = null;
										_0024offset_0024638 = -1;
										while (true)
										{
											_0024offset_0024638 = _0024www_0024620.data.IndexOf("map_Ka", _0024offset_0024638 + 1);
											if (_0024offset_0024638 == -1)
											{
												break;
											}
										}
										_0024___temp230_0024643 = 0;
										_0024___temp231_0024644 = _0024meshlib_0024636;
										for (_0024___temp232_0024645 = _0024___temp231_0024644.Length; _0024___temp230_0024643 < _0024___temp232_0024645; _0024___temp230_0024643++)
										{
											_0024ml_0024640 = _0024___temp231_0024644[_0024___temp230_0024643].Split(" "[0]);
											if (_0024ml_0024640[0] == "newmtl")
											{
												if ((bool)_0024curMat_0024637)
												{
													_0024self_650.meshMaterials.Add(_0024curMat_0024637.name, _0024curMat_0024637);
												}
												_0024curMat_0024637 = new Material(Shader.Find("VertexLit"));
												_0024curMat_0024637.name = _0024ml_0024640[1];
											}
											else if (_0024ml_0024640[0] == "#Shader")
											{
												_0024shdr_0024641 = _0024___temp231_0024644[_0024___temp230_0024643].Substring(8).Replace("Diffuse", "VertexLit");
												if (_0024shdr_0024641 != "VertexLit" && _0024shdr_0024641 != "VertexLit Fast")
												{
													_0024curMat_0024637.shader = Shader.Find(_0024shdr_0024641);
												}
											}
											else if (_0024ml_0024640[0] == "Ka")
											{
												_0024curMat_0024637.color = new Color(UnityBuiltins.parseFloat(_0024ml_0024640[1]), UnityBuiltins.parseFloat(_0024ml_0024640[2]), UnityBuiltins.parseFloat(_0024ml_0024640[3]), 1f);
											}
											else if (_0024ml_0024640[0] == "Kd")
											{
												_0024curMat_0024637.SetColor("_Emission", new Color(UnityBuiltins.parseFloat(_0024ml_0024640[1]), UnityBuiltins.parseFloat(_0024ml_0024640[2]), UnityBuiltins.parseFloat(_0024ml_0024640[3]), 1f));
											}
											else if (_0024ml_0024640[0] == "Ks")
											{
												_0024curMat_0024637.SetColor("_SpecColor", new Color(UnityBuiltins.parseFloat(_0024ml_0024640[1]), UnityBuiltins.parseFloat(_0024ml_0024640[2]), UnityBuiltins.parseFloat(_0024ml_0024640[3]), 1f));
											}
											else if (_0024ml_0024640[0] == "Ns")
											{
												_0024curMat_0024637.SetFloat("_Shininess", UnityBuiltins.parseFloat(_0024ml_0024640[1]));
											}
											else if (_0024ml_0024640[0] == "map_Ka")
											{
												_0024curMat_0024637.mainTextureOffset = new Vector2(UnityBuiltins.parseFloat(_0024ml_0024640[2]), UnityBuiltins.parseFloat(_0024ml_0024640[3]));
												_0024curMat_0024637.mainTextureScale = new Vector2(UnityBuiltins.parseFloat(_0024ml_0024640[5]), UnityBuiltins.parseFloat(_0024ml_0024640[6]));
												_0024self_650.monoBehaviour.StartCoroutine(_0024self_650.LoadMeshTexture(_0024ml_0024640[7], _0024curMat_0024637.name));
											}
											else if (!(_0024ml_0024640[0] == "d"))
											{
											}
										}
										if ((bool)_0024curMat_0024637)
										{
											_0024self_650.meshMaterials.Add(_0024curMat_0024637.name, _0024curMat_0024637);
										}
									}
								}
								if (Time.time > _0024timer_0024631)
								{
									_0024timer_0024631 = Time.time + 0.1f;
									return Yield(5, null);
								}
							}
							goto case 5;
						}
						_0024self_650.threads[_0024thread_0024618] = "Initializing";
						_0024msh_0024624.vertices = (Vector3[])_0024verts_0024625.ToBuiltin(typeof(Vector3));
						_0024msh_0024624.normals = (Vector3[])_0024norms_0024626.ToBuiltin(typeof(Vector3));
						_0024msh_0024624.uv = (Vector2[])_0024uvs_0024627.ToBuiltin(typeof(Vector2));
						if (_0024triangles_0024629.length > 0)
						{
							_0024triangles_0024629.Add(_0024tris_0024628);
							_0024msh_0024624.subMeshCount = _0024triangles_0024629.length;
							for (_0024i_0024635 = 0; _0024i_0024635 < _0024triangles_0024629.length; _0024i_0024635++)
							{
								_0024msh_0024624.SetTriangles((int[])RuntimeServices.Coerce(RuntimeServices.Invoke(_0024triangles_0024629[_0024i_0024635], "ToBuiltin", new object[1] { typeof(int) }), typeof(int[])), _0024i_0024635);
							}
						}
						else
						{
							_0024msh_0024624.triangles = (int[])_0024tris_0024628.ToBuiltin(typeof(int));
						}
						goto IL_0fe7;
						IL_0fe7:
						if (_0024hasCollider_0024619 != 1)
						{
							_0024mshObj_0024642 = new GameObject(_0024vS_0024617[0]);
							_0024mshObj_0024642.AddComponent(typeof(MeshFilter));
							RuntimeServices.SetProperty(_0024mshObj_0024642.GetComponent(typeof(MeshFilter)), "mesh", _0024msh_0024624);
							_0024mshObj_0024642.AddComponent(typeof(MeshRenderer));
							RuntimeServices.SetProperty(_0024mshObj_0024642.GetComponent(typeof(MeshRenderer)), "materials", _0024mats_0024630.ToBuiltin(typeof(Material)));
							if (_0024hasCollider_0024619 != -1)
							{
								_0024mshObj_0024642.AddComponent(typeof(MeshCollider));
								RuntimeServices.SetProperty(_0024mshObj_0024642.GetComponent(typeof(MeshCollider)), "mesh", _0024msh_0024624);
							}
							if (Extensions.get_length((System.Array)_0024msh_0024624.uv) < 1)
							{
								_0024self_650.TextureObject(_0024mshObj_0024642);
							}
							_0024self_650.objects.Add(_0024vS_0024617[0], _0024mshObj_0024642);
							_0024mshObj_0024642.transform.parent = _0024self_650.whirldBuffer.transform;
						}
						else if (_0024self_650.objects.ContainsKey(_0024vS_0024617[0]))
						{
							_0024mshObj_0024642 = (GameObject)RuntimeServices.Coerce(_0024self_650.objects[_0024vS_0024617[0]], typeof(GameObject));
							_0024mshObj_0024642.AddComponent(typeof(MeshCollider));
							RuntimeServices.SetProperty(_0024mshObj_0024642.GetComponent(typeof(MeshCollider)), "mesh", _0024msh_0024624);
						}
						else
						{
							_0024mshObj_0024642 = new GameObject(_0024vS_0024617[0]);
							_0024mshObj_0024642.AddComponent(typeof(MeshCollider));
							RuntimeServices.SetProperty(_0024mshObj_0024642.GetComponent(typeof(MeshCollider)), "mesh", _0024msh_0024624);
							_0024self_650.objects.Add(_0024vS_0024617[0], _0024mshObj_0024642);
							_0024mshObj_0024642.transform.parent = _0024self_650.whirldBuffer.transform;
						}
						_0024msh_0024624.Optimize();
						_0024self_650.threads.Remove(_0024thread_0024618);
						Yield(1, null);
						break;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal string _0024v651;

		internal WhirldIn _0024self_652;

		public LoadMesh_002459(string v, WhirldIn self_)
		{
			_0024v651 = v;
			_0024self_652 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024v651, _0024self_652);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class LoadTerrain_002460 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal string[] _0024vS2_0024653;

			internal string _0024tName_0024654;

			internal int _0024i2_0024655;

			internal string[] _0024str_0024656;

			internal string[] _0024tRes_0024657;

			internal string _0024tHtmp_0024658;

			internal string _0024tLtmp_0024659;

			internal string _0024tSpmp_0024660;

			internal string _0024tSpmp2_0024661;

			internal string[] _0024tTxts_0024662;

			internal string _0024tDtmp_0024663;

			internal string _0024thread_0024664;

			internal WWW _0024www_0024665;

			internal int _0024tWidth_0024666;

			internal int _0024tHeight_0024667;

			internal int _0024tLength_0024668;

			internal int _0024tHRes_0024669;

			internal TerrainData _0024trnDat_0024670;

			internal float[,] _0024hmap_0024671;

			internal BinaryReader _0024br_0024672;

			internal int _0024x_0024673;

			internal int _0024y_0024674;

			internal SplatPrototype[] _0024splatPrototypes_0024675;

			internal int _0024i_0024676;

			internal string[] _0024splatTxt_0024677;

			internal string[] _0024splatTxtSize_0024678;

			internal Color[] _0024mapColors2_0024679;

			internal float[,,] _0024splatmapData_0024680;

			internal Color[] _0024mapColors_0024681;

			internal int _0024ht_0024682;

			internal int _0024wd_0024683;

			internal int _0024z_0024684;

			internal GameObject _0024trnObj_0024685;

			internal string _0024v686;

			internal WhirldIn _0024self_687;

			public _0024(string v, WhirldIn self_)
			{
				_0024v686 = v;
				_0024self_687 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						_0024vS2_0024653 = _0024v686.Split(";"[0]);
						_0024tName_0024654 = _0024vS2_0024653[0];
						for (_0024i2_0024655 = 1; _0024i2_0024655 < Extensions.get_length((System.Array)_0024vS2_0024653); _0024i2_0024655++)
						{
							string[] array = _0024vS2_0024653;
							_0024str_0024656 = array[RuntimeServices.NormalizeArrayIndex(array, _0024i2_0024655)].Split(":"[0]);
							if (_0024str_0024656[0] == "r")
							{
								_0024tRes_0024657 = _0024str_0024656[1].Split(","[0]);
							}
							else if (_0024str_0024656[0] == "h")
							{
								_0024tHtmp_0024658 = (string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024str_0024656[1]), typeof(string));
							}
							else if (_0024str_0024656[0] == "l")
							{
								_0024tLtmp_0024659 = (string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024str_0024656[1]), typeof(string));
							}
							else if (_0024str_0024656[0] == "s")
							{
								_0024tSpmp_0024660 = (string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024str_0024656[1]), typeof(string));
							}
							else if (_0024str_0024656[0] == "s2")
							{
								_0024tSpmp2_0024661 = (string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024str_0024656[1]), typeof(string));
							}
							else if (_0024str_0024656[0] == "t")
							{
								_0024tTxts_0024662 = _0024str_0024656[1].Split(","[0]);
							}
							else if (_0024str_0024656[0] == "d")
							{
								_0024tDtmp_0024663 = (string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024str_0024656[1]), typeof(string));
							}
						}
						_0024thread_0024664 = _0024tName_0024654;
						_0024self_687.threads.Add(_0024thread_0024664, string.Empty);
						_0024www_0024665 = new WWW(_0024tHtmp_0024658);
						goto case 2;
					case 2:
						if (!_0024www_0024665.isDone)
						{
							_0024self_687.threads[_0024thread_0024664] = _0024www_0024665.progress;
							return Yield(2, null);
						}
						if (_0024www_0024665.error != null)
						{
							_0024self_687.info += "Terrain Undownloadable: " + _0024tName_0024654 + " " + _0024tHtmp_0024658 + " (" + _0024www_0024665.error + ")\n";
						}
						else
						{
							_0024self_687.threads[_0024thread_0024664] = "Initializing";
							_0024tWidth_0024666 = UnityBuiltins.parseInt(_0024tRes_0024657[0]);
							_0024tHeight_0024667 = UnityBuiltins.parseInt(_0024tRes_0024657[1]);
							_0024tLength_0024668 = UnityBuiltins.parseInt(_0024tRes_0024657[2]);
							_0024tHRes_0024669 = UnityBuiltins.parseInt(_0024tRes_0024657[3]);
							_0024trnDat_0024670 = new TerrainData();
							_0024trnDat_0024670.heightmapResolution = _0024tHRes_0024669;
							_0024hmap_0024671 = _0024trnDat_0024670.GetHeights(0, 0, _0024tHRes_0024669, _0024tHRes_0024669);
							_0024br_0024672 = null;
							if (true)
							{
								_0024br_0024672 = new BinaryReader(new MemoryStream(GZipStream.UncompressBuffer(_0024www_0024665.bytes)));
							}
							else
							{
								_0024br_0024672 = new BinaryReader(new MemoryStream(_0024www_0024665.bytes));
							}
							for (_0024x_0024673 = 0; _0024x_0024673 < _0024tHRes_0024669; _0024x_0024673++)
							{
								for (_0024y_0024674 = 0; _0024y_0024674 < _0024tHRes_0024669; _0024y_0024674++)
								{
									_0024hmap_0024671.SetValue((float)unchecked((int)_0024br_0024672.ReadUInt16()) / 65535f, new int[2] { _0024x_0024673, _0024y_0024674 });
								}
							}
							_0024trnDat_0024670.SetHeights(0, 0, _0024hmap_0024671);
							_0024trnDat_0024670.size = new Vector3(_0024tWidth_0024666, _0024tHeight_0024667, _0024tLength_0024668);
							if (_0024tTxts_0024662 != null)
							{
								_0024splatPrototypes_0024675 = new SplatPrototype[Extensions.get_length((System.Array)_0024tTxts_0024662)];
								for (_0024i_0024676 = 0; _0024i_0024676 < Extensions.get_length((System.Array)_0024tTxts_0024662); _0024i_0024676++)
								{
									string[] array2 = _0024tTxts_0024662;
									_0024splatTxt_0024677 = array2[RuntimeServices.NormalizeArrayIndex(array2, _0024i_0024676)].Split("="[0]);
									_0024splatTxtSize_0024678 = _0024splatTxt_0024677[1].Split("x"[0]);
									_0024www_0024665 = new WWW((string)RuntimeServices.Coerce(_0024self_687.GetURL(_0024splatTxt_0024677[0]), typeof(string)));
									while (!_0024www_0024665.isDone)
									{
									}
									if (_0024www_0024665.error != null)
									{
										_0024self_687.info += "Terrain Texture Undownloadable: #" + (_0024i_0024676 + 1) + " (" + _0024splatTxt_0024677[0] + ")\n";
									}
									else
									{
										SplatPrototype[] array3 = _0024splatPrototypes_0024675;
										array3[RuntimeServices.NormalizeArrayIndex(array3, _0024i_0024676)] = new SplatPrototype();
										SplatPrototype[] array4 = _0024splatPrototypes_0024675;
										array4[RuntimeServices.NormalizeArrayIndex(array4, _0024i_0024676)].texture = new Texture2D(4, 4, TextureFormat.DXT1, true);
										WWW wWW = _0024www_0024665;
										SplatPrototype[] array5 = _0024splatPrototypes_0024675;
										wWW.LoadImageIntoTexture(array5[RuntimeServices.NormalizeArrayIndex(array5, _0024i_0024676)].texture);
										SplatPrototype[] array6 = _0024splatPrototypes_0024675;
										array6[RuntimeServices.NormalizeArrayIndex(array6, _0024i_0024676)].texture.Apply(true);
										SplatPrototype[] array7 = _0024splatPrototypes_0024675;
										array7[RuntimeServices.NormalizeArrayIndex(array7, _0024i_0024676)].texture.Compress(true);
										SplatPrototype[] array8 = _0024splatPrototypes_0024675;
										array8[RuntimeServices.NormalizeArrayIndex(array8, _0024i_0024676)].tileSize = new Vector2(UnityBuiltins.parseInt(_0024splatTxtSize_0024678[0]), UnityBuiltins.parseInt(_0024splatTxtSize_0024678[1]));
									}
								}
							}
							_0024trnDat_0024670.splatPrototypes = _0024splatPrototypes_0024675;
							if (_0024tLtmp_0024659 != null)
							{
								_0024www_0024665 = new WWW(_0024tLtmp_0024659);
								while (!_0024www_0024665.isDone)
								{
								}
								if (_0024www_0024665.error != null)
								{
									_0024self_687.info += "Terrain Lightmap Undownloadable: " + _0024tName_0024654 + " " + _0024tLtmp_0024659 + " (" + _0024www_0024665.error + "\n";
								}
								else
								{
									_0024trnDat_0024670.lightmap = _0024www_0024665.texture;
								}
							}
							if (_0024tSpmp_0024660 != null)
							{
								if (_0024tSpmp2_0024661 != null)
								{
									_0024www_0024665 = new WWW(_0024tSpmp2_0024661);
									while (!_0024www_0024665.isDone)
									{
									}
									_0024mapColors2_0024679 = _0024www_0024665.texture.GetPixels();
								}
								_0024www_0024665 = new WWW(_0024tSpmp_0024660);
								while (!_0024www_0024665.isDone)
								{
								}
								if (_0024www_0024665.error != null)
								{
									_0024self_687.info += "Terrain Texturemap Undownloadable: " + _0024tName_0024654 + " " + _0024tLtmp_0024659 + " (" + _0024www_0024665.error + ")\n";
								}
								else if (_0024www_0024665.texture.format != TextureFormat.ARGB32 || _0024www_0024665.texture.width != _0024www_0024665.texture.height || Mathf.ClosestPowerOfTwo(_0024www_0024665.texture.width) != _0024www_0024665.texture.width)
								{
									_0024self_687.info += "Terrain Splatmap Unusable: Splatmap must be in RGBA 32 bit format, square, and it's size a power of 2\n";
								}
								else
								{
									_0024trnDat_0024670.alphamapResolution = _0024www_0024665.texture.width;
									_0024splatmapData_0024680 = _0024trnDat_0024670.GetAlphamaps(0, 0, _0024www_0024665.texture.width, _0024www_0024665.texture.width);
									_0024mapColors_0024681 = _0024www_0024665.texture.GetPixels();
									_0024ht_0024682 = _0024www_0024665.texture.height;
									_0024wd_0024683 = _0024www_0024665.texture.width;
									for (_0024y_0024674 = 0; _0024y_0024674 < _0024ht_0024682; _0024y_0024674++)
									{
										for (_0024x_0024673 = 0; _0024x_0024673 < _0024wd_0024683; _0024x_0024673++)
										{
											for (_0024z_0024684 = 0; _0024z_0024684 < _0024trnDat_0024670.alphamapLayers; _0024z_0024684++)
											{
												if (_0024z_0024684 < 4)
												{
													float[,,] array9 = _0024splatmapData_0024680;
													Color[] array10 = _0024mapColors_0024681;
													array9.SetValue(array10[RuntimeServices.NormalizeArrayIndex(array10, _0024x_0024673 * _0024wd_0024683 + _0024y_0024674)][_0024z_0024684], new int[3] { _0024x_0024673, _0024y_0024674, _0024z_0024684 });
												}
												else
												{
													float[,,] array11 = _0024splatmapData_0024680;
													Color[] array12 = _0024mapColors2_0024679;
													array11.SetValue(array12[RuntimeServices.NormalizeArrayIndex(array12, _0024x_0024673 * _0024wd_0024683 + _0024y_0024674)][_0024z_0024684 - 4], new int[3] { _0024x_0024673, _0024y_0024674, _0024z_0024684 });
												}
											}
										}
									}
									_0024trnDat_0024670.SetAlphamaps(0, 0, _0024splatmapData_0024680);
								}
							}
							_0024trnObj_0024685 = new GameObject(_0024tName_0024654);
							_0024trnObj_0024685.AddComponent(typeof(Terrain));
							RuntimeServices.SetProperty(_0024trnObj_0024685.GetComponent(typeof(Terrain)), "terrainData", _0024trnDat_0024670);
							_0024trnObj_0024685.AddComponent(typeof(TerrainCollider));
							RuntimeServices.SetProperty(_0024trnObj_0024685.GetComponent(typeof(TerrainCollider)), "terrainData", _0024trnDat_0024670);
							_0024self_687.objects.Add(_0024tName_0024654, _0024trnObj_0024685);
							_0024trnObj_0024685.transform.parent = _0024self_687.whirldBuffer.transform;
						}
						_0024self_687.threads.Remove(_0024thread_0024664);
						Yield(1, null);
						break;
					case 1:
						break;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal string _0024v688;

		internal WhirldIn _0024self_689;

		public LoadTerrain_002460(string v, WhirldIn self_)
		{
			_0024v688 = v;
			_0024self_689 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024v688, _0024self_689);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class Generate_002464 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal WWW _0024www_0024569;

			internal char _0024s_0024570;

			internal string _0024n_0024571;

			internal string _0024v_0024572;

			internal string[] _0024vS_0024573;

			internal Terrain _0024trn_0024574;

			internal IEnumerator _0024___iterator62_0024575;

			internal GameObject _0024go_0024576;

			internal IEnumerator _0024___iterator63_0024577;

			internal WhirldIn _0024self_578;

			public _0024(WhirldIn self_)
			{
				_0024self_578 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						_0024self_578.status = WhirldInStatus.Working;
						if (_0024self_578.url != string.Empty)
						{
							_0024self_578.statusTxt = "Downloading World Definition";
							_0024self_578.info = string.Empty;
							_0024self_578.urlPath = _0024self_578.url.Substring(0, _0024self_578.url.LastIndexOf("/") + 1);
							_0024www_0024569 = new WWW(_0024self_578.url);
							goto case 2;
						}
						goto IL_0182;
					case 2:
						if (!_0024www_0024569.isDone)
						{
							_0024self_578.progress = _0024www_0024569.progress;
							return Yield(2, new WaitForSeconds(0.1f));
						}
						_0024self_578.progress = 1f;
						if (_0024www_0024569.error != null)
						{
							_0024self_578.info = "Failed to download Whirld definition file: " + _0024self_578.url + " (" + _0024www_0024569.error + ")\n";
							_0024self_578.status = WhirldInStatus.WWWError;
							break;
						}
						_0024self_578.data = _0024www_0024569.data;
						goto IL_0182;
					case 3:
						if (_0024self_578.threads.Count > 0)
						{
							return Yield(3, null);
						}
						_0024self_578.statusTxt = "Initializing World";
						_0024self_578.ReadObject(_0024self_578.world.transform);
						_0024___iterator62_0024575 = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(Terrain)));
						while (_0024___iterator62_0024575.MoveNext())
						{
							_0024trn_0024574 = (Terrain)RuntimeServices.Coerce(_0024___iterator62_0024575.Current, typeof(Terrain));
							RuntimeServices.SetProperty(_0024trn_0024574.gameObject.AddComponent(typeof(TerrainController)), "trnDat", _0024trn_0024574.terrainData);
							UnityRuntimeServices.Update(_0024___iterator62_0024575, _0024trn_0024574);
						}
						UnityEngine.Object.Destroy(_0024self_578.whirldBuffer);
						_0024___iterator63_0024577 = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(GameObject)));
						while (_0024___iterator63_0024577.MoveNext())
						{
							_0024go_0024576 = (GameObject)RuntimeServices.Coerce(_0024___iterator63_0024577.Current, typeof(GameObject));
							_0024go_0024576.SendMessage("OnSceneGenerated", SendMessageOptions.DontRequireReceiver);
							UnityRuntimeServices.Update(_0024___iterator63_0024577, _0024go_0024576);
						}
						_0024self_578.status = WhirldInStatus.Success;
						_0024self_578.statusTxt = "World Loaded Successfully";
						if (_0024self_578.info != string.Empty)
						{
							Debug.Log("Whirld Loading Info: " + _0024self_578.info);
						}
						Yield(1, null);
						break;
					case 1:
						break;
						IL_02a5:
						_0024self_578.status = WhirldInStatus.SyntaxError;
						break;
						IL_0182:
						_0024self_578.readChr = 0;
						_0024self_578.world = GameObject.Find("World");
						if ((bool)_0024self_578.world)
						{
							UnityEngine.Object.Destroy(_0024self_578.world);
						}
						_0024self_578.world = new GameObject("World");
						_0024self_578.statusTxt = "Parsing World Definition";
						if (_0024self_578.data[0] != '[' && _0024self_578.data[0] != '{')
						{
							_0024self_578.status = WhirldInStatus.SyntaxError;
							break;
						}
						while (true)
						{
							_0024s_0024570 = _0024self_578.data[_0024self_578.readChr];
							_0024self_578.readChr++;
							if (_0024self_578.readChr < Extensions.get_length(_0024self_578.data))
							{
								if (_0024s_0024570 == '\n' || _0024s_0024570 == '\t')
								{
									continue;
								}
								if (_0024s_0024570 == '{')
								{
									break;
								}
								if (_0024s_0024570 == '[')
								{
									_0024n_0024571 = string.Empty;
									_0024v_0024572 = string.Empty;
								}
								else if (_0024s_0024570 == ':' && _0024n_0024571 == string.Empty)
								{
									_0024n_0024571 = _0024v_0024572;
									_0024v_0024572 = string.Empty;
								}
								else if (_0024s_0024570 == ']')
								{
									if (_0024n_0024571 == string.Empty)
									{
										_0024n_0024571 = _0024v_0024572;
										_0024v_0024572 = string.Empty;
									}
									if (_0024n_0024571 == "ab")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadAssetBundle(_0024v_0024572));
									}
									if (_0024n_0024571 == "ss")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadStreamedScene(_0024v_0024572));
									}
									else if (_0024n_0024571 == "rndSkybox")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadSkybox(_0024v_0024572));
									}
									else if (_0024n_0024571 == "txt")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadTexture(_0024v_0024572));
									}
									else if (_0024n_0024571 == "msh")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadMesh(_0024v_0024572));
									}
									else if (_0024n_0024571 == "trn")
									{
										_0024self_578.monoBehaviour.StartCoroutine(_0024self_578.LoadTerrain(_0024v_0024572));
									}
									else if (_0024n_0024571 == "rndFogColor" || _0024n_0024571 == "rndFogDensity" || _0024n_0024571 == "rndAmbientLight" || _0024n_0024571 == "rndHaloStrength" || _0024n_0024571 == "rndFlareStrength")
									{
										_0024vS_0024573 = _0024v_0024572.Split(","[0]);
										if (_0024n_0024571 == "rndFogColor")
										{
											RenderSettings.fogColor = new Color(UnityBuiltins.parseFloat(_0024vS_0024573[0]), UnityBuiltins.parseFloat(_0024vS_0024573[1]), UnityBuiltins.parseFloat(_0024vS_0024573[2]), 1f);
										}
										else if (_0024n_0024571 == "rndFogDensity")
										{
											RenderSettings.fogDensity = UnityBuiltins.parseFloat(_0024v_0024572);
										}
										else if (_0024n_0024571 == "rndAmbientLight")
										{
											RenderSettings.ambientLight = new Color(UnityBuiltins.parseFloat(_0024vS_0024573[0]), UnityBuiltins.parseFloat(_0024vS_0024573[1]), UnityBuiltins.parseFloat(_0024vS_0024573[2]), UnityBuiltins.parseFloat(_0024vS_0024573[3]));
										}
										else if (_0024n_0024571 == "rndHaloStrength")
										{
											RenderSettings.haloStrength = UnityBuiltins.parseFloat(_0024v_0024572);
										}
										else if (_0024n_0024571 == "rndFlareStrength")
										{
											RenderSettings.flareStrength = UnityBuiltins.parseFloat(_0024v_0024572);
										}
									}
									else
									{
										_0024self_578.worldParams.Add(_0024n_0024571, _0024v_0024572);
									}
								}
								else
								{
									_0024v_0024572 += _0024s_0024570;
								}
								continue;
							}
							goto IL_02a5;
						}
						_0024self_578.statusTxt = "Downloading World Assets";
						goto case 3;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal WhirldIn _0024self_579;

		public Generate_002464(WhirldIn self_)
		{
			_0024self_579 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_579);
		}
	}

	public WhirldInStatus status;

	public string statusTxt;

	public float progress;

	public string info;

	public string url;

	public string data;

	public GameObject world;

	public GameObject whirldBuffer;

	public string worldName;

	public string urlPath;

	public Hashtable worldParams;

	public Hashtable threads;

	public int threadAssetBundles;

	public int threadTextures;

	public int maxThreads;

	public UnityScript.Lang.Array loadedAssetBundles;

	public Hashtable objects;

	public Hashtable textures;

	public Hashtable meshMaterials;

	public Hashtable meshMatLibs;

	public MonoBehaviour monoBehaviour;

	public int readChr;

	public WhirldIn()
	{
		status = WhirldInStatus.Idle;
		statusTxt = string.Empty;
		progress = 0f;
		info = string.Empty;
		url = string.Empty;
		worldName = "World";
		worldParams = new Hashtable();
		threads = new Hashtable();
		threadAssetBundles = 0;
		threadTextures = 0;
		maxThreads = 5;
		loadedAssetBundles = new UnityScript.Lang.Array();
		objects = new Hashtable();
		textures = new Hashtable();
		meshMaterials = new Hashtable();
		meshMatLibs = new Hashtable();
		readChr = 0;
	}

	public void Load()
	{
		whirldBuffer = new GameObject("WhirldBuffer");
		monoBehaviour = (MonoBehaviour)whirldBuffer.AddComponent(typeof(MonoBehaviourScript));
		monoBehaviour.StartCoroutine(Generate());
	}

	public void Cleanup()
	{
		if ((bool)whirldBuffer && (bool)monoBehaviour)
		{
			monoBehaviour.StopAllCoroutines();
			UnityEngine.Object.Destroy(whirldBuffer);
		}
		if (loadedAssetBundles.length > 0)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(loadedAssetBundles);
			while (enumerator.MoveNext())
			{
				AssetBundle assetBundle = (AssetBundle)RuntimeServices.Coerce(enumerator.Current, typeof(AssetBundle));
				assetBundle.Unload(true);
				UnityRuntimeServices.Update(enumerator, assetBundle);
			}
			loadedAssetBundles.Clear();
		}
	}

	public IEnumerator Generate()
	{
		return new Generate_002464(this).GetEnumerator();
	}

    public IEnumerator LoadAssetBundle(string p)
    {
        threadAssetBundles++;

        while (threads.Count >= maxThreads) yield return null;  //Don't overwhelm the computer by doing too many things @ once

        //Presets
        String thread = System.IO.Path.GetFileNameWithoutExtension(p);
        threads.Add(thread, "");
        String url = p;

        //Download StreamedScene
        url = (String)GetURL(url);
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null || !www.assetBundle)
        {
            if (!www.assetBundle) info +=
                 "Referenced file is not an AssetBundle: " +
                 url +
                 "\n";
            else info +=
                "Failed to download asset file: " +
                url +
                " (" +
                www.error +
                ")\n";
            threads.Remove(thread);
            threadAssetBundles--;
            yield break;
        }

        //Load AssetBundle
        threads[thread] = "Initializing Bundle";
        loadedAssetBundles.Add(www.assetBundle);

        //Success
        threads.Remove(thread);
        threadAssetBundles--;

    }

	public IEnumerator LoadStreamedScene(string p)
	{
        while (threads.Count >= maxThreads) yield return null;  //Don't overwhelm the computer by doing too many things @ once

        //Presets
        String thread = "SceneData";
        threads.Add(thread, "");
        String nme = "World";
        String url = "Whirld.unity3d";

        //Object Parameters
        if (p != "")    //[ss:sceneName,url]
        {
            String[] pS = p.Split(","[0]);
            if (pS[0] != null) nme = pS[0];
            if (pS[1] != null) url = pS[1];
        }

        //Download StreamedScene
        url = (String)GetURL(url);
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null || !www.assetBundle)
        {
            if (!www.assetBundle) info +=
                 "StreamedScene file contains no scenes: " +
                 url +
                 "\n";
            else info +=
                "Failed to download asset file: " +
                url +
                " (" +
                www.error +
                ")\n";
            threads.Remove(thread);
            yield break;
        }

        //Wait for all AssetBundles to load
        threads[thread] = "Loading Asset Dependencies";
        while (threadAssetBundles > 0) yield return null;

        threads.Remove(thread);
        thread = "SceneInit";
        threads.Add(thread, "...");

        //Load StreamedScene
        AssetBundle blah = www.assetBundle;
        AsyncOperation async = Application.LoadLevelAdditiveAsync(nme);
        float tme = Time.time;
        while (!async.isDone)
        {
            threads[thread] = (Time.time - tme) + "...";
            yield return null;
        }

        //Success
        loadedAssetBundles.Add(www.assetBundle);
        threads.Remove(thread);

	}

    /*[Serializable]
	[CompilerGenerated]
	internal sealed class LoadTexture_002457 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal string[] _0024vS_0024599;

			internal string _0024thread_0024600;

			internal string _0024url_0024601;

			internal WWW _0024www_0024602;

			internal Texture2D _0024txt_0024603;

			internal string _0024p604;

			internal WhirldIn _0024self_605;

			public _0024(string p, WhirldIn self_)
			{
				_0024p604 = p;
				_0024self_605 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						_0024self_605.threadTextures++;
						goto case 2;
					case 2:
						if (_0024self_605.threads.Count >= _0024self_605.maxThreads)
						{
							return Yield(2, null);
						}
						_0024vS_0024599 = _0024p604.Split(","[0]);
						_0024thread_0024600 = "Txt" + _0024self_605.threadTextures + " - " + _0024vS_0024599[0];
						_0024self_605.threads.Add(_0024thread_0024600, string.Empty);
						_0024url_0024601 = (string)RuntimeServices.Coerce(_0024self_605.GetURL(_0024vS_0024599[1]), typeof(string));
						_0024www_0024602 = new WWW(_0024url_0024601);
						goto case 3;
					case 3:
						if (!_0024www_0024602.isDone)
						{
							_0024self_605.threads[_0024thread_0024600] = _0024www_0024602.progress;
							return Yield(3, null);
						}
						if (_0024www_0024602.error != null)
						{
							_0024self_605.info += "Failed to download texture: " + _0024url_0024601 + " (" + _0024www_0024602.error + ")\n";
							_0024self_605.threads.Remove(_0024thread_0024600);
							_0024self_605.threadTextures--;
							break;
						}
						_0024self_605.threads[_0024thread_0024600] = "Initializing";
						_0024txt_0024603 = new Texture2D(4, 4, TextureFormat.DXT1, true);
						_0024www_0024602.LoadImageIntoTexture(_0024txt_0024603);
						_0024txt_0024603.wrapMode = ((_0024vS_0024599[2] == null || RuntimeServices.EqualityOperator(_0024vS_0024599[2], 0)) ? TextureWrapMode.Clamp : TextureWrapMode.Repeat);
						_0024txt_0024603.anisoLevel = RuntimeServices.UnboxInt32((_0024vS_0024599[3] == null) ? ((object)1) : _0024vS_0024599[3]);
						_0024txt_0024603.Apply(true);
						_0024txt_0024603.Compress(true);
						_0024self_605.textures.Add(_0024vS_0024599[0], _0024txt_0024603);
						_0024self_605.threads.Remove(_0024thread_0024600);
						_0024self_605.threadTextures--;
						Yield(1, null);
						break;
					case 1:
						break;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal string _0024p606;

		internal WhirldIn _0024self_607;

		public LoadTexture_002457(string p, WhirldIn self_)
		{
			_0024p606 = p;
			_0024self_607 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024p606, _0024self_607);
		}
	}*/

	public IEnumerator LoadTexture(string p)    //[txt:name,url,wrapMode,anisoLevel]
	{
        threadTextures++;

        //Don't overwhelm the computer by doing too many things @ once
        while (threads.Count >= maxThreads) yield return null;

        String[] vS = p.Split(","[0]);

        String thread = "Txt" +
            threadTextures +
            " - " +
            vS[0];
        threads.Add(thread, "");

        String url = (String)GetURL(vS[1]);
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null)
        {
            info +=
                "Failed to download texture: " +
                url +
                " (" +
                www.error +
                ")\n";
            threads.Remove(thread);
            threadTextures--;
            yield break;
        }

        threads[thread] = "Initializing";
        //Texture2D txt = www.texture;
        Texture2D txt = new Texture2D(
            4,
            4,
            TextureFormat.DXT1,
            true);
        www.LoadImageIntoTexture(txt);
        txt.wrapMode = (
            (vS[2] == null || float.Parse(vS[2]) == 0f) ?
                TextureWrapMode.Clamp :
                TextureWrapMode.Repeat);
        txt.anisoLevel = (vS[3] != null ? int.Parse(vS[3]) : 1);
        txt.Apply(true);
        txt.Compress(true);
        textures.Add(vS[0], txt);

        threads.Remove(thread);
        threadTextures--;
	}

	public IEnumerator LoadMeshTexture(string url, string materialName)
	{
		return new LoadMeshTexture_002458(url, materialName, this).GetEnumerator();
	}

	public IEnumerator LoadMesh(string v)
	{
		return new LoadMesh_002459(v, this).GetEnumerator();
	}

	public IEnumerator LoadTerrain(string v)
	{
		return new LoadTerrain_002460(v, this).GetEnumerator();
	}

	public IEnumerator LoadSkyboxTexture(string url, int dest)
	{
        threadTextures++;

        //Don't overwhelm the computer by doing too many things @ once
        while (threads.Count >= maxThreads) yield return null;

        //Presets
        String thread = "Skybox" + dest;
        threads.Add(thread, "");


        //Download Skybox Image
        url = (string)GetURL(url);
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }

        threads.Remove(thread);
        threadTextures--;

        if (www.error != null)
        {
            info +=
                "Failed to download skybox # " +
                dest +
                ": " +
                url +
                " (" +
                www.error +
                ")\n";
            yield break; ;
        }

        Texture2D txt = new Texture2D(
            4,
            4,
            TextureFormat.DXT1,
            true);
        www.LoadImageIntoTexture(txt);
        txt.wrapMode = TextureWrapMode.Clamp;
        txt.Apply(true);
        txt.Compress(true);

        //Wait for everything else to load
        while (threads.Count > 0) yield return null;

        //Assign Texture to Skybox!
        if (dest == 0 || dest == 1)
        {
            RenderSettings.skybox.SetTexture("_FrontTex", txt);
        }
        if (dest == 0 || dest == 2)
        {
            RenderSettings.skybox.SetTexture("_BackTex", txt);
        }
        if (dest == 0 || dest == 3)
        {
            RenderSettings.skybox.SetTexture("_LeftTex", txt);
        }
        if (dest == 0 || dest == 4)
        {
            RenderSettings.skybox.SetTexture("_RightTex", txt);
        }
        if (dest == 0 || dest == 5)
        {
            RenderSettings.skybox.SetTexture("_UpTex", txt);
        }
        if (dest == 0 || dest == 6)
        {
            RenderSettings.skybox.SetTexture("_DownTex", txt);
        }
	}

	public IEnumerator LoadSkybox(string v)
	{
        String[] vS = v.Split(","[0]);

        //Multiple Image Skybox
        if (vS.Length > 5)
        { 
            //Material skyMat = RenderSettings.skybox;
            //RenderSettings.skybox = new Material();
            //RenderSettings.skybox.CopyPropertiesFromMaterial(skymat);
            LoadSkyboxTexture(vS[0], 1);
            LoadSkyboxTexture(vS[1], 2);
            LoadSkyboxTexture(vS[2], 3);
            LoadSkyboxTexture(vS[3], 4);
            LoadSkyboxTexture(vS[4], 5);
            LoadSkyboxTexture(vS[5], 6);
            //Wait for everything else to load
            while (threads.Count > 0) yield return null;
            if (vS.Length > 6)
            {
                RenderSettings.skybox.SetColor("_Tint", new Color(
                    float.Parse(vS[6]),
                    float.Parse(vS[7]),
                    float.Parse(vS[8]),
                    0.5f));
            }
        }

        //Single JPG image for all sides
        else if (vS[0].Substring(vS[0].LastIndexOf(".") + 1) == "jpg")
        {
            LoadSkyboxTexture(vS[0], 0);
            //Wait for everything else to load
            while (threads.Count > 0) yield return null;
            if (vS.Length > 1)
            {
                RenderSettings.skybox.SetColor("_Tint", new Color(
                    float.Parse(vS[1]),
                    float.Parse(vS[2]),
                    float.Parse(vS[3]),
                    0.5f));
            }
        }

        //AssetBundle Material Skybox
        else
        {
            //Wait for everything else to load
            while (threads.Count > 0) yield return null;
            RenderSettings.skybox = (Material)GetAsset(v); //, Material
            if (!RenderSettings.skybox)
            {
                info +=
                    "Skybox not found: " +
                    v +
                    "\n";
            }
        }

	}

	[DuckTyped]
	public object GetAsset(string str)
	{
		if (loadedAssetBundles.length > 0)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(loadedAssetBundles);
			while (enumerator.MoveNext())
			{
				AssetBundle assetBundle = (AssetBundle)RuntimeServices.Coerce(enumerator.Current, typeof(AssetBundle));
				if (assetBundle.Contains(str))
				{
					return assetBundle.Load(str);
				}
			}
		}
		object result = default(object);
		return result;
	}

	public void ReadObject(Transform parent)
	{
		string text = null;
		int num = 0;
		string text2 = string.Empty;
		string text3 = string.Empty;
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		GameObject gameObject = null;
		checked
		{
			GameObject gameObject2 = default(GameObject);
			WhirldObject whirldObject = default(WhirldObject);
			Light light = default(Light);
			while (true && readChr < Extensions.get_length(data))
			{
				char c = data[readChr];
				if (!(c == ' ') && !(c == '\n') && !(c == '\t'))
				{
					if (c == ':')
					{
						text2 = text3;
						text3 = string.Empty;
					}
					else if (c == ',')
					{
						array.Add(text3);
						text3 = string.Empty;
					}
					else
					{
						if (c == '{')
						{
							readChr++;
							ReadObject(gameObject.transform);
							continue;
						}
						if (c == ';' || c == '}')
						{
							if (!gameObject)
							{
								if (objects.ContainsKey(text3))
								{
									if (!RuntimeServices.EqualityOperator(objects[text3], null))
									{
										gameObject2 = (GameObject)RuntimeServices.Coerce(objects[text3], typeof(GameObject));
									}
									else
									{
										Debug.Log("Whirld: Objects[" + text3 + "] is null");
									}
								}
								else
								{
									gameObject2 = (GameObject)Resources.Load(text3);
									if ((bool)gameObject2)
									{
										objects.Add(text3, gameObject2);
									}
								}
								if ((bool)gameObject2)
								{
									gameObject = (GameObject)UnityEngine.Object.Instantiate(gameObject2);
									gameObject.name = text3;
								}
								else
								{
									gameObject = new GameObject(text3);
									objects.Add(text3, gameObject);
								}
								if (gameObject.name != "Base" && gameObject.name != "Sea" && gameObject.name != "JumpPoint" && gameObject.name != "Light")
								{
									gameObject.transform.parent = parent;
								}
								whirldObject = (WhirldObject)gameObject.GetComponent(typeof(WhirldObject));
								if ((bool)whirldObject)
								{
									whirldObject.@params = new Hashtable();
								}
								light = (Light)gameObject.GetComponent(typeof(Light));
							}
							else if ((text2 == "p" || (text2 == string.Empty && num == 1)) && array.length == 2)
							{
								gameObject.transform.localPosition = new Vector3(RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[0] })), RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[1] })), UnityBuiltins.parseFloat(text3));
							}
							else if (text2 == "p" || (text2 == string.Empty && num == 1))
							{
								gameObject.transform.localPosition = Vector3.one * UnityBuiltins.parseFloat(text3);
							}
							else if ((text2 == "r" || (text2 == string.Empty && num == 2)) && array.length == 3)
							{
								gameObject.transform.rotation = new Quaternion(RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[0] })), RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[1] })), RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[2] })), UnityBuiltins.parseFloat(text3));
							}
							else if ((text2 == "r" || (text2 == string.Empty && num == 2)) && array.length == 2)
							{
								gameObject.transform.rotation = Quaternion.Euler(RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[0] })), RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[1] })), UnityBuiltins.parseFloat(text3));
							}
							else if ((text2 == "r" || (text2 == string.Empty && num == 2)) && array.length == 0)
							{
								gameObject.transform.rotation = Quaternion.identity;
							}
							else if ((text2 == "s" || (text2 == string.Empty && num == 3)) && array.length == 0)
							{
								gameObject.transform.localScale = Vector3.one * UnityBuiltins.parseFloat(text3);
							}
							else if (text2 == "s" || (text2 == string.Empty && num == 3))
							{
								gameObject.transform.localScale = new Vector3(RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[0] })), RuntimeServices.UnboxSingle(RuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[1] })), UnityBuiltins.parseFloat(text3));
							}
							else if (text2 == "cc")
							{
								gameObject.AddComponent(typeof(CombineChildren));
								worldParams["ccc"] = 1;
							}
							else if (text2 == "m")
							{
								info += "Inline Whirld mesh generation not supported\n";
							}
							else if ((bool)light && text2 == "color")
							{
								object value = UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[0] }, typeof(MonoBehaviour));
								Color color = light.color;
								float num2 = (color.r = RuntimeServices.UnboxSingle(value));
								Color color2 = (light.color = color);
								object value2 = UnityRuntimeServices.Invoke(typeof(UnityBuiltins), "parseFloat", new object[1] { array[1] }, typeof(MonoBehaviour));
								Color color4 = light.color;
								float num3 = (color4.g = RuntimeServices.UnboxSingle(value2));
								Color color5 = (light.color = color4);
								float b = UnityBuiltins.parseFloat(text3);
								Color color7 = light.color;
								float num4 = (color7.b = b);
								Color color8 = (light.color = color7);
							}
							else if ((bool)light && text2 == "intensity")
							{
								light.intensity = UnityBuiltins.parseFloat(text3);
							}
							else if ((bool)whirldObject)
							{
								if (text3.Substring(0, 1) == "#")
								{
									whirldObject.@params.Add(text2, GetAsset(text3.Substring(1)));
								}
								else
								{
									whirldObject.@params.Add(text2, text3);
								}
							}
							else if (text2 != string.Empty)
							{
								Debug.Log(gameObject.name + " Unknown Param: " + text2 + " > " + text3);
							}
							text3 = string.Empty;
							text2 = string.Empty;
							if (array.length > 0)
							{
								array = new UnityScript.Lang.Array();
							}
							num++;
							if (c == '}')
							{
								if (gameObject.name == "cube" || gameObject.name == "pyramid" || gameObject.name == "cone" || gameObject.name == "mesh")
								{
									TextureObject(gameObject);
								}
								readChr++;
								while (readChr < Extensions.get_length(data) && (data[readChr] == ' ' || data[readChr] == '\n' || data[readChr] == '\t'))
								{
									readChr++;
								}
								if (readChr < Extensions.get_length(data) && data[readChr] == '{')
								{
									readChr++;
									ReadObject(parent);
								}
								break;
							}
						}
						else if (text2 != null)
						{
							text3 += c;
						}
						else
						{
							text2 += c;
						}
					}
				}
				readChr++;
			}
		}
	}

	public void TextureObject(GameObject go)
	{
		MeshFilter meshFilter = (MeshFilter)go.GetComponent(typeof(MeshFilter));
		if (!meshFilter)
		{
			return;
		}
		Mesh mesh = meshFilter.mesh;
		Vector2[] array = new Vector2[mesh.vertices.Length];
		int[] triangles = mesh.triangles;
		checked
		{
			for (int i = 0; i < triangles.Length; i += 3)
			{
				Transform transform = go.transform;
				Vector3[] vertices = mesh.vertices;
				Vector3 vector = transform.TransformPoint(vertices[RuntimeServices.NormalizeArrayIndex(vertices, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i)])]);
				Transform transform2 = go.transform;
				Vector3[] vertices2 = mesh.vertices;
				Vector3 vector2 = transform2.TransformPoint(vertices2[RuntimeServices.NormalizeArrayIndex(vertices2, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 1)])]);
				Transform transform3 = go.transform;
				Vector3[] vertices3 = mesh.vertices;
				Vector3 vector3 = transform3.TransformPoint(vertices3[RuntimeServices.NormalizeArrayIndex(vertices3, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 2)])]);
				Vector3 normalized = Vector3.Cross(vector - vector3, vector2 - vector3).normalized;
				if (Vector3.Dot(Vector3.up, normalized) >= 0.5f || !(Vector3.Dot(-Vector3.up, normalized) < 0.5f))
				{
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i)])] = new Vector2(vector.x, vector.z);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 1)])] = new Vector2(vector2.x, vector2.z);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 2)])] = new Vector2(vector3.x, vector3.z);
				}
				else if (Vector3.Dot(Vector3.right, normalized) >= 0.5f || !(Vector3.Dot(Vector3.left, normalized) < 0.5f))
				{
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i)])] = new Vector2(vector.y, vector.z);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 1)])] = new Vector2(vector2.y, vector2.z);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 2)])] = new Vector2(vector3.y, vector3.z);
				}
				else
				{
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i)])] = new Vector2(vector.y, vector.x);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 1)])] = new Vector2(vector2.y, vector2.x);
					array[RuntimeServices.NormalizeArrayIndex(array, triangles[RuntimeServices.NormalizeArrayIndex(triangles, i + 2)])] = new Vector2(vector3.y, vector3.x);
				}
			}
			mesh.uv = array;
		}
	}

	public object GetURL(object url)
	{
		if (!RuntimeServices.EqualityOperator(RuntimeServices.Invoke(url, "Substring", new object[2] { 0, 4 }), "http"))
		{
			url = RuntimeServices.InvokeBinaryOperator("op_Addition", urlPath, url);
		}
		return url;
	}
}
