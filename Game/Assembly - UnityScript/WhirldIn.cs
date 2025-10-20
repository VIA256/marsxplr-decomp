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
        threadTextures++;

        //Don't overwhelm the computer by doing too many things @ once
        while (threads.Count >= maxThreads) yield return null;
        String thread = "MshTxt" +
            threadTextures +
            " - " +
            materialName;
        threads.Add(thread, "");

        url = (String)GetURL(url);
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null)
        {
            info +=
                "Failed to download mesh texture: " +
                url +
                " (" +
                www.error +
                ")\n";
            threads.Remove(thread);
            threadTextures--;
            yield break;
        }

        threads[thread] = "Initializing";

        Texture2D mshTxt = new Texture2D(
            4,
            4,
            TextureFormat.DXT1,
            true);
        www.LoadImageIntoTexture(mshTxt);
        mshTxt.wrapMode = TextureWrapMode.Repeat;
        mshTxt.Apply(true);
        mshTxt.Compress(true);
        ((Material)meshMaterials[materialName]).mainTexture = mshTxt;

        threads.Remove(thread);
        threadTextures--;
	}

	public IEnumerator LoadMesh(string v) //[msh:name,url]
	{
        Mesh msh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> norms = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();
        List<List<int>> triangles = new List<List<int>>();
        List<Material> mats = new List<Material>();

        //Don't overwhelm the computer by doing too many things @ once
        while (threads.Count >= maxThreads) yield return null;

        //Init Thread
        String[] vS = v.Split(","[0]);
        String thread = vS[0];
        threads.Add(thread, "");

        //Download Mesh Object
        int hasCollider = (vS.Length > 2 ? int.Parse(vS[2]) : 0);
        WWW www = new WWW((String)GetURL(vS[1]));
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null)
        {
            info +=
                "Failed to download mesh: " +
                url +
                " (" +
                www.error +
                ")\n";
            threads.Remove(thread);
            yield break;
        }

        //Download All Textures Before Generating Mesh
        //threads[thread] = "Loading Textures";
        //while(threadTextures > 0) yield return null;

        //Uncompress as necessary...
        threads[thread] = "Decompressing";
        yield return null;  //Rebuild GUI as we may be working for a while
        int lastDot = vS[1].LastIndexOf(".");
        String data;
        if (vS[1].Substring(lastDot + 1) == "gz")
        {
            data = GZipStream.UncompressString(www.bytes);
            vS[1] = vS[1].Substring(0, lastDot);
        }
        else data = www.data;

        threads[thread] = "Generating";

        lastDot = vS[1].LastIndexOf(".");
        String ext = vS[1].Substring(lastDot + 1);

        //Binary UnityMesh Object
        if (ext == "utm")
        {
            //MeshSerializer has been depricated - it's totally nonstandard, and it didn't support submeshes anyway
            //Mesh msh = MeshSerializer.ReadMesh(www.bytes);
        }

        //.obj File
        else if (ext == "obj")
        {
            float timer = Time.time + 0.1f;
            String[] file = data.Split("\n"[0]);
            foreach (String str in file)
            {
                if (str == "") continue;
                String[] l = str.Split(" "[0]);
                if (l[0] == "v")
                {
                    verts.Add(new Vector3(
                        -float.Parse(l[1]),
                        float.Parse(l[2]),
                        float.Parse(l[3])));
                }
                else if (l[0] == "vn")
                {
                    norms.Add(new Vector3(
                        float.Parse(l[1]),
                        float.Parse(l[2]),
                        float.Parse(l[3])));
                }
                else if (l[0] == "vt")
                {
                    uvs.Add(new Vector2(
                        float.Parse(l[1]),
                        float.Parse(l[2])));
                }
                else if (l[0] == "f")
                {
                    if (l.Length == 4)
                    {
                        tris.Add(int.Parse(l[2].Substring(
                            0,
                            l[2].IndexOf("/"))) - 1);
                        tris.Add(int.Parse(l[1].Substring(
                            0,
                            l[2].IndexOf("/"))) - 1);
                        tris.Add(int.Parse(l[3].Substring(
                            0,
                            l[2].IndexOf("/"))) - 1);
                    }
                    //Attempt to triangulate face - hardly works, could use better routine here...
                    else
                    {
                        int i;
                        for (i = 2; i < l.Length; i++)
                        {
                            tris.Add(int.Parse(l[i].Substring(
                                0,
                                l[i].IndexOf("/"))) - 1);
                            if (i % 2 == 0)
                            {
                                tris.Add(int.Parse(l[1].Substring(
                                0,
                                l[1].IndexOf("/"))) - 1);
                            }
                        }
                        while (tris.Count % 3 != 0)
                        {
                            tris.Add(int.Parse(l[i = 2].Substring(
                                0,
                                l[i - 2].IndexOf("/"))) - 1);
                        }
                    }
                }
                else if (l[0] == "usemtl")
                {
                    if (meshMaterials.ContainsKey(l[1]))
                    {
                        mats.Add((Material)meshMaterials[l[1]]);
                    }
                    else
                    {
                        info +=
                            "Mesh Material Missing: " +
                            l[1] +
                            "\n";
                        mats.Add(null);
                    }
                    if (tris.Count > 0)
                    {
                        triangles.Add(tris);
                        tris = new List<int>();
                    }
                }
                else if (l[0] == "mtllib") //Time to load a material library!
                {
                    if (!meshMatLibs.ContainsKey(l[1]))
                    {
                        //Only load a material library once, even if it is referenced by multiple meshes
                        meshMatLibs.Add(l[1], true);
                        www = new WWW((String)GetURL(l[1]));
                        while (!www.isDone)
                        {
                            threads[thread] =
                                "Downloading Material Library (" +
                                Mathf.RoundToInt(www.progress * 100) +
                                "%)";
                            //yield return null;
                        }
                        if (www.error != null)
                        {
                            info +=
                                "Mesh Material Library Undownloadable: " +
                                (String)GetURL(l[1]) +
                                " (" +
                                www.error +
                                ")\n";
                        }
                        else
                        {
                            threads[thread] = "Initializing " + vS[0] + "";
                            //yield return null;
                            String[] meshlib = www.data.Split("\n"[0]);
                            Material curMat = null;
                            int offset = -1;
                            while (true)
                            {
                                offset = www.data.IndexOf("map_Ka", offset + 1);
                                if (offset == -1) break;
                            }
                            foreach (String meshline in meshlib)
                            {
                                String[] ml = meshline.Split(" "[0]);
                                if (ml[0] == "newmtl") //Beginning of new material
                                {
                                    if (curMat) //Save current material
                                    {
                                        meshMaterials.Add(curMat.name, curMat);
                                    }
                                    curMat = new Material(Shader.Find("VertexLit"));
                                    curMat.name = ml[1];
                                }
                                else if (ml[0] == "#Shader") //Set shader of current material
                                {
                                    String shdr = meshline.Substring(8).Replace("Diffuse", "VertexLit");
                                    if (shdr != "VertexLit" && shdr != "VertexLit Fast")
                                    {
                                        curMat.shader = Shader.Find(shdr);
                                    }
                                }
                                else if (ml[0] == "Ka") //Set color of current material
                                {
                                    curMat.color = new Color(
                                        float.Parse(ml[1]),
                                        float.Parse(ml[2]),
                                        float.Parse(ml[3]),
                                        1f);
                                }
                                else if (ml[0] == "Kd")
                                {
                                    curMat.SetColor("_Emission", new Color(
                                        float.Parse(ml[1]),
                                        float.Parse(ml[2]),
                                        float.Parse(ml[3]),
                                        1f));
                                }
                                else if (ml[0] == "Ks")
                                {
                                    curMat.SetColor("_SpecColor", new Color(
                                        float.Parse(ml[1]),
                                        float.Parse(ml[2]),
                                        float.Parse(ml[3]),
                                        1f));
                                }
                                else if (ml[0] == "Ns")
                                {
                                    curMat.SetFloat("_Shininess", float.Parse(ml[1]));
                                }
                                else if (ml[0] == "map_Ka") //Set texture of current material
                                {
                                    curMat.mainTextureOffset = new Vector2(
                                        float.Parse(ml[2]),
                                        float.Parse(ml[3]));
                                    curMat.mainTextureScale = new Vector2(
                                        float.Parse(ml[5]),
                                        float.Parse(ml[6]));
                                    monoBehaviour.StartCoroutine_Auto(LoadMeshTexture(
                                        ml[7],
                                        curMat.name));
                                }
                                else if (ml[0] == "d") //Set alpha cutoff of current material
                                {
                                    //curMat.shader = Shader.Find("Transparent/Cutout/VertexLit");
                                    //curMat.SetFloat("_Cutoff", float.Parse(ml[1]));
                                }
                            }
                            if (curMat) //Save last material (others get saved as file is read)
                            {
                                meshMaterials.Add(curMat.name, curMat);
                            }
                        }
                    }
                }
                if (Time.time > timer) //Refresh GUI 10 times per second to keep the user entertained
                {
                    timer = Time.time + 0.1f;
                    yield return null;
                }
            }

            threads[thread] = "Initializing";

            msh.vertices = verts.ToArray();
            msh.normals = norms.ToArray();
            msh.uv = uvs.ToArray();
            if (triangles.Count > 0)
            {
                triangles.Add(tris);
                msh.subMeshCount = triangles.Count;
                for (int i = 0; i < triangles.Count; i++)
                {
                    msh.SetTriangles(triangles[i].ToArray(), i);
                }
            }
            else msh.triangles = tris.ToArray();
        }

        //Unknown File Type
        else info +=
            "Mesh Type Unrecognized: " +
            vS[0] +
            " " +
            vS[1] +
            " (." +
            ext +
            ")\n";

        if (hasCollider == 1) //This mesh is being created, and it has a renderer
        {
            GameObject mshObj = new GameObject(vS[0]);
            mshObj.AddComponent(typeof(MeshFilter));
            ((MeshFilter)mshObj.GetComponent(typeof(MeshFilter))).mesh = msh;
            mshObj.AddComponent(typeof(MeshRenderer));
            ((MeshRenderer)mshObj.GetComponent(typeof(MeshRenderer))).materials = mats.ToArray();
            if (hasCollider != -1) //This mesh has a collider, and it is the same as it's rendered mesh
            {
                mshObj.AddComponent(typeof(MeshCollider));
                ((MeshCollider)mshObj.GetComponent(typeof(MeshCollider))).mesh = msh;
            }
            if (msh.uv.Length < 1) TextureObject(mshObj);
            objects.Add(vS[0], mshObj);
            mshObj.transform.parent = whirldBuffer.transform;
        }
        else //This mesh has a custom collider
        {
            if (objects.ContainsKey(vS[0])) //This mesh already exists, add a custom collider to it
            {
                GameObject mshObj = new GameObject(vS[0]);
                mshObj.AddComponent(typeof(MeshCollider));
                ((MeshCollider)mshObj.GetComponent(typeof(MeshCollider))).mesh = msh;
                objects.Add(vS[0], mshObj);
                mshObj.transform.parent = whirldBuffer.transform;
            }
        }
        msh.Optimize();

        threads.Remove(thread);

	}

    //v = "name;r:width,height,length,heightmapResolution	//,detailResolution,controlResolution,textureResolution;h:heightMapUrl";
	public IEnumerator LoadTerrain(string v)
	{
        String[] vS2 = v.Split(";"[0]);
        String tName = vS2[0];

        String[] tRes = null;
        String tHtmp = null;
        String tLtmp = null;
        String tSpmp = null;
        String tSpmp2 = null;
        String[] tTxts = null;
        String tDtmp = null;

        for (int i2 = 1; i2 < vS2.Length; i2++)
        {
            String[] str = vS2[i2].Split(":"[0]);
            if (str[0] == "r") tRes = str[1].Split(","[0]);
            else if (str[0] == "h") tHtmp = (String)GetURL(str[1]);
            else if (str[0] == "l") tLtmp = (String)GetURL(str[1]);
            else if (str[0] == "s") tSpmp = (String)GetURL(str[1]);
            else if (str[0] == "s2") tSpmp2 = (String)GetURL(str[1]);
            else if (str[0] == "t") tTxts = str[1].Split(","[0]);
            else if (str[0] == "d") tDtmp = (String)GetURL(str[1]);
        }

        String thread = tName;
        threads.Add(thread, "");
        WWW www = new WWW(tHtmp);
        while (!www.isDone)
        {
            threads[thread] = www.progress;
            yield return null;
        }
        if (www.error != null)
        {
            info +=
                "Terrain Undownloadable: " +
                tName +
                " " +
                tHtmp +
                " (" +
                www.error +
                ")\n";
        }
        else
        {
            threads[thread] = "Initializing";
            //yield return null;

            int tWidth = int.Parse(tRes[0]);
            int tHeight = int.Parse(tRes[1]);
            int tLength = int.Parse(tRes[2]);
            int tHRes = int.Parse(tRes[3]);

            TerrainData trnDat = new TerrainData();

            //Heights
            trnDat.heightmapResolution = tHRes;
            float[,] hmap = trnDat.GetHeights(0, 0, tHRes, tHRes);
            System.IO.BinaryReader br;
            if (true) //Terrain RAW file is compressed
            {
                br = new System.IO.BinaryReader(new System.IO.MemoryStream(
                    GZipStream.UncompressBuffer(www.bytes)));
            }
            else br = new System.IO.BinaryReader(new System.IO.MemoryStream(www.bytes));
            for (int x = 0; x < tHRes; x++)
            {
                for (int y = 0; y < tHRes; y++)
                {
                    hmap[x, y] = br.ReadUInt16() / 65535.00000000f;
                }
            }
            trnDat.SetHeights(0, 0, hmap);
            trnDat.size = new Vector3(tWidth, tHeight, tLength);

            //Textures
            SplatPrototype[] splatPrototypes = null;
            if (tTxts != null)
            {
                splatPrototypes = new SplatPrototype[tTxts.Length];
                for (int i = 0; i < tTxts.Length; i++)
                {
                    String[] splatTxt = tTxts[i].Split("="[0]);
                    String[] splatTxtSize = splatTxt[1].Split("x"[0]);
                    www = new WWW((String)GetURL(splatTxt[0]));
                    while (!www.isDone)
                    { 
                        //threads[thread] = "Initializing";
                        //yield return new WaitForSeconds(0.1f);
                    }
                    if (www.error != null)
                    {
                        info +=
                            "Terrain Texture Undownloadable: #" +
                            (i + 1) +
                            " (" +
                            splatTxt[0] +
                            ")\n";
                    }
                    else
                    { 
                        //yield return null;
                        splatPrototypes[i] = new SplatPrototype();
                        splatPrototypes[i].texture = new Texture2D(
                            4,
                            4,
                            TextureFormat.DXT1,
                            true);
                        www.LoadImageIntoTexture(splatPrototypes[i].texture);
                        splatPrototypes[i].texture.Apply(true);
                        splatPrototypes[i].texture.Compress(true);
                        splatPrototypes[i].tileSize = new Vector2(
                            int.Parse(splatTxtSize[0]),
                            int.Parse(splatTxtSize[1]));
                    }
                }
            }
            trnDat.splatPrototypes = splatPrototypes;

            //Lightmap
            if (tLtmp != null)
            { 
                //whirld.statusTxt = "Downloading Terrain Lightmap (" + tName + ")";
                www = new WWW(tLtmp);
                while (!www.isDone)
                { 
                    //whirld.progress = www.progress;
                    //yield return new WaitForSeconds(0.1f);
                }
                if (www.error != null)
                {
                    info +=
                        "Terrain Lightmap Undownloadable: " +
                        tName +
                        " " +
                        tLtmp +
                        " (" +
                        www.error +
                        ")\n";
                }
                else
                {
                    trnDat.lightmap = www.texture;
                }
            }

            //Splatmap

            if (tSpmp != null)
            {
                Color[] mapColors2 = null;
                if (tSpmp2 != null)
                {
                    //whirld.statusTxt = "Downloading Augmentative Terrain Texturemap (" + tName + ")";
                    www = new WWW(tSpmp2);
                    while (!www.isDone)
                    { 
                        //whirld.progress = www.progress;
                        //yield return new WaitForSeconds(0.1f);
                    }
                    mapColors2 = www.texture.GetPixels();
                }
                //whirld.statusTxt = "Downloading Terrain Texturemap (" + tName + ")";
                www = new WWW(tSpmp);
                while (!www.isDone)
                {
                    //whirld.progress = www.progress;
                    //yield return new WaitForSeconds(0.1f);
                }
                //whirld.statusTxt = "Mapping Terrain Textures...";
                //yield return null;
                if (www.error != null)
                {
                    info +=
                        "Terrain Texturemap Undownloadable: " +
                        tName +
                        " " +
                        tLtmp +
                        " (" +
                        www.error +
                        ")\n";
                }
                else
                {
                    trnDat.alphamapResolution = www.texture.width;
                    float[, ,] splatmapData = trnDat.GetAlphamaps(
                        0,
                        0,
                        www.texture.width,
                        www.texture.width);
                    Color[] mapColors = www.texture.GetPixels();
                    int ht = www.texture.height;
                    int wd = www.texture.width;
                    for (int y = 0; y < ht; y++)
                    {
                        for (int x = 0; x < wd; x++)
                        {
                            for (int z = 0; z < trnDat.alphamapLayers; z++)
                            {
                                if (z < 4)
                                {
                                    splatmapData[x, y, z] = mapColors[x * wd + y][z];
                                }
                                else splatmapData[x, y, z] = mapColors2[x * wd + y][z - 4];
                            }
                        }
                    }
                    trnDat.SetAlphamaps(0, 0, splatmapData);
                }
            }

            //Go !
            GameObject trnObj = new GameObject(tName);
            trnObj.AddComponent(typeof(Terrain));
            ((Terrain)trnObj.GetComponent(typeof(Terrain))).terrainData = trnDat;
            trnObj.AddComponent(typeof(TerrainCollider));
            ((TerrainCollider)trnObj.GetComponent(typeof(TerrainCollider))).terrainData = trnDat;

            objects.Add(tName, trnObj);
            //Delete this temporary terrain object AFTER world is fully loaded
            trnObj.transform.parent = whirldBuffer.transform;
        }

        threads.Remove(thread);
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
