//********************************************************************************************************************************************
//*********************************** Whirld - by Aubrey Falconer ****************************************************************************
//**** http://AubreyFalconer.com **** http://web.archive.org/web/20120519040400/http://www.unifycommunity.com/wiki/index.php?title=Whirld ****
//********************************************************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class BaseSimple : MonoBehaviour {}

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
public class WhirldIn : System.Object
{
	public WhirldInStatus status = WhirldInStatus.Idle;
	public string statusTxt = "";
	public float progress = 0.00f;
	public string info = "";
	public string url = "";
	public string data;
	public GameObject world;
	public GameObject whirldBuffer;
	public string worldName = "World";
	public string urlPath;
	public Hashtable objects = new Hashtable();
    public MonoBehaviour monoBehaviour; //Needed for attaching Coroutines too
	public int readChr = 0;

	public static void ResetSpace()
	{
		GameObject wld = GameObject.Find("World");
        if (wld) GameObject.Destroy(wld);
		GameObject bse = GameObject.Find("Base");
		if (bse) GameObject.Destroy(bse);
	}

	public void Load()
	{
		whirldBuffer = new GameObject("WhirldBuffer");
		monoBehaviour = (MonoBehaviour)whirldBuffer.AddComponent(typeof(MonoBehaviourScript));
		
        monoBehaviour.StartCoroutine(Generate());
	}

	public void Cleanup()
	{
        //We are still loading the world
		if ((bool)whirldBuffer && (bool)monoBehaviour)
		{
			monoBehaviour.StopAllCoroutines();
			GameObject.Destroy(whirldBuffer);
		}
	}

	public IEnumerator Generate()
	{

        status = WhirldInStatus.Working;

        if (url != "")
        { 
            
            //Download Whirld File
            statusTxt = "Downloading World Definition";
            info = "";
            urlPath = url.Substring(0, url.LastIndexOf("/") + 1);
            WWW www = new WWW(url);
            while (!www.isDone)
            {
                progress = www.progress;
                yield return new WaitForSeconds(0.1f);
            }
            progress = 1f;

            //Verify Successful Download
            if (www.error != null)
            {
                info =
                    "Failed to download Whirld definition file: " +
                    url +
                    " (" +
                    www.error +
                    ")\n";
                status = WhirldInStatus.WWWError;
                yield break;
            }
            data = www.data;

        }

        //Init
        readChr = 0;
        ResetSpace();
        world = new GameObject("World");
        statusTxt = "Parsing World Definition";

        //Sanity Check
        if (
            data == null ||
            data.Length < 10 ||
            (data[0] != '[' && data[0] != '{'))
        {
            status = WhirldInStatus.SyntaxError;
            yield break;
        }
		
		//Read Whirld Headers
        String n = null;
        String v = null;
        while (true)
        {
            //Read next char
            char s = data[readChr];
            readChr++;

            //Incorrectly nested header []s
            if (readChr >= data.Length)
            {
                status = WhirldInStatus.SyntaxError;
                yield break;
            }

            //Ignore Newlines and Tabs
            else if (s == '\n' || s == '\t') continue;

            else if (s == '{') break;    //Finished reading headers
            else if (s == '[')          //Beginning new header
            {
                n = "";
                v = "";
            }

            //Header name read, read value
            else if (s == ':' && n == "")
            {
                n = v;
                v = "";
            }

            //Header ended
            else if (s == ']')
            {
                //[name] header
                if (n == "")
                {
                    n = v;
                    v = "";
                }

                //AssetBundle
//                if (n == "ab") monoBehaviour.StartCoroutine_Auto(LoadAssetBundle(v));

                //StreamedScene
//                if (n == "ss") monoBehaviour.StartCoroutine_Auto(LoadStreamedScene(v));

                //Skybox
//                else if (n == "rndSkybox") monoBehaviour.StartCoroutine_Auto(LoadSkybox(v));

                //Texture
//                else if (n == "txt") monoBehaviour.StartCoroutine_Auto(LoadTexture(v));

                //Mesh
//                else if (n == "msh") monoBehaviour.StartCoroutine_Auto(LoadMesh(v));

                //Terrain
//                else if (n == "trn") monoBehaviour.StartCoroutine_Auto(LoadTerrain(v));

                //Rendering Settings
                /*else */if (
                    n == "rndFogColor" ||
                    n == "rndFogDensity" ||
                    n == "rndAmbientLight" ||
                    n == "rndHaloStrength" ||
                    n == "rndFlareStrength")
                {
                    String[] vS = v.Split(","[0]);
                    if (n == "rndFogColor")
                    {
                        RenderSettings.fogColor = new Color(
                            float.Parse(vS[0]),
                            float.Parse(vS[1]),
                            float.Parse(vS[2]),
                            1);
                    }
                    else if (n == "rndFogDensity")
                    {
                        RenderSettings.fogDensity = float.Parse(v);
                    }
                    else if (n == "rndAmbientLight")
                    {
                        RenderSettings.ambientLight = new Color(
                            float.Parse(vS[0]),
                            float.Parse(vS[1]),
                            float.Parse(vS[2]),
                            float.Parse(vS[3]));
                    }
                    else if (n == "rndHaloStrength")
                    {
                        RenderSettings.haloStrength = float.Parse(v);
                    }
                    else if (n == "rndFlareStrength")
                    {
                        RenderSettings.flareStrength = float.Parse(v);
                    }
                }

                //Arbitrary Data
//                else worldParams.Add(n, v);

            }

            //Header char read
            else v += s;
        }

        statusTxt = "Downloading World Assets";

        //Wait for all "threads" to finish working
//        while (threads.Count > 0)
//        {
//            yield return null;
//        }
		
        //Generate World
        statusTxt = "Initializing World";
        ReadObject(world.transform);

        //Cleanup
        GameObject.Destroy(whirldBuffer);

        //Send Scene Generation Notice to each object
        foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            go.SendMessage(
                "OnSceneGenerated",
                SendMessageOptions.DontRequireReceiver);
        }

        //Success!
        status = WhirldInStatus.Success;
        statusTxt = "World Loaded Successfully";
        if (info != "")
        {
            Debug.Log("Whirld Loading Info: " + info);
        }
		
		PlayerPrefs.SetString("openWhirldDefault", url);
	}

	public void ReadObject(Transform parent)
	{
		// /*UNUSED*/ string c = null;          //Character
		int i = 0;                              //Index of param
        string n = "";                          //Param name we are reading data for
        string v = "";                          //Value we are building
        List<String> d = new List<String>();    //Array of all values in current param data
        GameObject obj = null;                  //Object we have created

		GameObject goP = default(GameObject);
		Light lightSource = default(Light);
		while (true)
		{
            if (readChr >= data.Length) return;

            //Get Char
			char s = data[readChr];

            //Ignore spaces
            if (s == ' ' || s == '\n' || s == '\r' || s == '\t') { ; }

            //Name fully read, begin collecting param value(s)
			else if (s == ':')
			{
				n = v;
				v = "";
			}

            //Move to next section of value
			else if (s == ',')
			{
				d.Add(v);
				v = "";
			}

            //Move to next section of value
			else if (s == '{')
			{
				readChr++;
				ReadObject(obj.transform);
                //Continue to next obj once the child "thread" we just launched has finished parsing objects at it's level
                continue;
			}

            //Assign current value to object, Begin reading new value
			else if (s == ';' || s == '}')
			{

                //Object name just read, create object
                if (!obj)
                {
                    if (objects.ContainsKey(v))
                    {
                        if (objects[v] != null)
                        {
                            goP = (GameObject)objects[v];
                        }
                        else
                        {
                            Debug.Log("Whirld: Objects[" + v + "] is null");
                        }
                        //else goP = gameObject.Find();
                    }
                    else
                    {
                        goP = (GameObject)Resources.Load(v);
                        if ((bool)goP) objects.Add(v, goP);
                    }
                    if ((bool)goP)
                    {
                        obj = (GameObject)GameObject.Instantiate(goP);
                        obj.name = v;
                    }
                    else
                    {
                        obj = new GameObject(v);
                        objects.Add(v, obj);
                    }
                    if (
                        obj.name != "Base" &&
                        obj.name != "Sea" &&
                        obj.name != "JumpPoint" &&
                        obj.name != "Light")
                    {
                        obj.transform.parent = parent;
                    }
                    lightSource = (Light)obj.GetComponent(typeof(Light));
                }

                //Object already created, assign property to object
                else
                {
                    if (
                        (n == "p" || (n == "" && i == 1)) &&
                        d.Count == 2)
                    {
                        obj.transform.localPosition = new Vector3(
                            float.Parse(d[0]),
                            float.Parse(d[1]),
                            float.Parse(v));
                    }
                    else if (
                        n == "p" ||
                        (n == "" && i == 1))
                    {
                        obj.transform.localPosition = Vector3.one * float.Parse(v);
                    }
                    else if (
                        (n == "r" || (n == string.Empty && i == 2)) &&
                        d.Count == 3)
                    {
                        obj.transform.rotation = new Quaternion(
                            float.Parse(d[0]),
                            float.Parse(d[1]),
                            float.Parse(d[2]),
                            float.Parse(v));
                    }
                    else if (
                        (n == "r" || (n == string.Empty && i == 2)) &&
                        d.Count == 2)
                    {
                        obj.transform.rotation = Quaternion.Euler(
                            float.Parse(d[0]),
                            float.Parse(d[1]),
                            float.Parse(v));
                    }
                    else if (
                        (n == "r" || (n == string.Empty && i == 2)) &&
                        d.Count == 0)
                    {
                        obj.transform.rotation = Quaternion.identity;
                    }
                    else if (
                        (n == "s" || (n == string.Empty && i == 3)) &&
                        d.Count == 0)
                    {
                        obj.transform.localScale = Vector3.one * float.Parse(v);
                    }
                    else if (
                        n == "s" ||
                        (n == "" && i == 3))
                    {
                        obj.transform.localScale = new Vector3(
                            float.Parse(d[0]),
                            float.Parse(d[1]),
                            float.Parse(v));
                    }
                    else if (n == "m")
                    {
                        //d.Add(v);
                        //ReadMesh(obj, d);
                        info += "Inline Whirld mesh generation not supported\n";
                    }
                    else if ((bool)lightSource && n == "color")
                    {
                        Color lsc = lightSource.color;
                        lsc.r = float.Parse(d[0]);
                        lsc.g = float.Parse(d[1]);
                        lsc.b = float.Parse(v);
                        lightSource.color = lsc;
                    }
                    else if ((bool)lightSource && n == "intensity")
                    {
                        lightSource.intensity = float.Parse(v);
                    }
                    else 
                    if (n != "")
                    {
                        Debug.Log(
                            obj.name +
                            " Unknown/NotYetImplimented Param: " +
                            n +
                            " > " +
                            v);
                    }
                }

                //Reset properties
				v = "";
				n = "";
				if (d.Count > 0) d = new List<String>();
				i++;

                //Done reading this object
				if (s == '}')
				{
                    //Finish up this object
					if (
                        obj.name == "cube" ||
                        obj.name == "pyramid" ||
                        obj.name == "cone" ||
                        obj.name == "mesh")
					{
						TextureObject(obj);
					}

                    //Increment ReadChar
					readChr++;
                    
                    //Handle spaces
					while (
                        readChr < data.Length &&
                        (
                            data[readChr] == ' ' ||
                            data[readChr] == '\n' ||
                            data[readChr] == '\r' ||
                            data[readChr] == '\t'))
					{
						readChr++;
					}

                    //Read the next object
                    if (readChr < data.Length && data[readChr] == '{')
                    {
                        readChr++;
                        ReadObject(parent);
                        return;
                    }

                    //Done reading objects at this level of recursion
                    else return;
				}
			}

            //Assign char to property we are reading
			else 
            {
                if (n != null) v += s;
			    else n += s;
            }
			readChr++;
		}
	}

	public void TextureObject(GameObject go)
	{
		MeshFilter mf = (MeshFilter)go.GetComponent(typeof(MeshFilter));
		if (!mf) return;
		Mesh mesh = mf.mesh;
		Vector2[] uvs = new Vector2[mesh.vertices.Length];
		int[] tris = mesh.triangles;
		for (int i = 0; i < tris.Length; i += 3)
		{
            Vector3 a = go.transform.TransformPoint(mesh.vertices[tris[i]]);
            Vector3 b = go.transform.TransformPoint(mesh.vertices[tris[i+1]]);
            Vector3 c = go.transform.TransformPoint(mesh.vertices[tris[i+2]]);
			Vector3 n = Vector3.Cross(a-c, b-c).normalized;
			if (
                Vector3.Dot(Vector3.up, n) >= 0.5f ||
                (Vector3.Dot(-Vector3.up, n) >= 0.5f))
			{
                uvs[tris[i]] = new Vector2(a.x, a.z);
                uvs[tris[i+1]] = new Vector2(b.x, b.z);
                uvs[tris[i+2]] = new Vector2(c.x, c.z);
			}
			else if (
                Vector3.Dot(Vector3.right, n) >= 0.5f ||
                (Vector3.Dot(Vector3.left, n) >= 0.5f))
			{
                uvs[tris[i]] = new Vector2(a.y, a.z);
                uvs[tris[i+1]] = new Vector2(b.y, b.z);
                uvs[tris[i+2]] = new Vector2(c.y, c.z);
			}
			else
			{
                uvs[tris[i]] = new Vector2(a.y, a.x);
                uvs[tris[i + 1]] = new Vector2(b.y, b.x);
                uvs[tris[i + 2]] = new Vector2(c.y, c.x);
			}
		}
		mesh.uv = uvs;
	}

	public String GetURL(String url)
	{
        if (url.Substring(0, 4) != "http") url = urlPath + url;
        return url;
	}
}