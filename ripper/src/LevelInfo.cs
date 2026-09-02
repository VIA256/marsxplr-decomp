using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

//TODO: dont display the header of an object type if no objects of that type are found in the scene.

public class LevelInfo
{
	private static void Discriminate<T>(ref List<T> objs, T heir) where T : UnityEngine.Object
	{
		for(int i = objs.Count - 1; i >= 0; i--)
		{
			if(objs[i].GetInstanceID() == heir.GetInstanceID())
			{
				objs.RemoveAt(i);
			}
		}
	}
	
	private struct ObjsWithType {
		public List<UnityEngine.Object> objs;
		public string type;
		
		public ObjsWithType(List<UnityEngine.Object> o, string t)
		{
			objs = o;
			type = t;
		}
	}
	
	public static void LogToFile()
	{
		string ripLogPath = Path.Combine(
			Directory.GetParent(Application.dataPath).ToString(),
			Application.loadedLevelName + "_level" + Application.loadedLevel + "_info.txt");
		
		List<int> taken = new List<int>();		
		List<ObjsWithType> owts = new List<ObjsWithType>();
		Type[] types = {
			// : MonoBehaviour
			typeof(Terrain),
			
			// : Renderer
			typeof(LineRenderer),
			typeof(MeshRenderer),
			typeof(ParticleRenderer),
			typeof(SkinnedMeshRenderer),
			typeof(TrailRenderer),
			
			// : GUIElement
			typeof(GUIText),
			typeof(GUITexture),
			
			// : ScriptableObject
			typeof(GUISkin),
			typeof(ScriptableShaderPass),
			
			// : Texture
			typeof(Cubemap),
			typeof(MovieTexture),
			typeof(RenderTexture),
			typeof(Texture2D),
			
			// : Joint
			typeof(CharacterJoint),
			typeof(ConfigurableJoint),
			typeof(FixedJoint),
			typeof(HingeJoint),
			typeof(SpringJoint),
			
			// : Collider
			typeof(BoxCollider),
			typeof(CapsuleCollider),
			typeof(CharacterController),
			typeof(MeshCollider),
			typeof(RaycastCollider),
			typeof(SphereCollider),
			typeof(TerrainCollider),
			typeof(WheelCollider),
			
			//	: Behaviour
			typeof(Animation),
			typeof(AudioListener),
			typeof(AudioSource),
			typeof(Camera),
			typeof(ConstantForce),
			typeof(GUIElement),
			typeof(GUILayer),
			typeof(LensFlare),
			typeof(Light),
			typeof(MonoBehaviour),
			typeof(NetworkView),
			typeof(Projector),
			typeof(Skybox),
			
			//	: Component
			typeof(Behaviour),
			typeof(Collider),
			typeof(Joint),
			typeof(Renderer),
			typeof(MeshFilter),
			typeof(ParticleAnimator),
			typeof(ParticleEmitter),
			typeof(Rigidbody),
			typeof(TextMesh),
			typeof(Transform),
			
			//	: UnityEngine.Object
			typeof(GameObject),
			typeof(Component),
			typeof(AnimationClip),
			typeof(AssetBundle),
			typeof(AudioClip),
			typeof(Texture),
			typeof(Flare),
			typeof(Font),
			typeof(ScriptableObject),
			typeof(Material),
			typeof(Mesh),
			typeof(PhysicMaterial),
			typeof(Shader),
			typeof(TerrainData),
			typeof(TextAsset),
			
			typeof(UnityEngine.Object),
		};

		foreach(Type t in types)
		{
			owts.Add(new ObjsWithType(
				new List<UnityEngine.Object>(UnityEngine.Object.FindObjectsOfTypeAll(t)),
				t.ToString()
			));
			if(owts[owts.Count - 1].objs.Count < 1) owts.RemoveAt(owts.Count - 1);
		}
		
		using(StreamWriter ripLog = new StreamWriter(ripLogPath, false))
		{
			string objformat = "{0,-8} | {1,-32} | {2}";
			ripLog.WriteLine(objformat,
				"id", "name", "hideFlags");
			ripLog.WriteLine();
			ripLog.WriteLine();
			
			foreach(ObjsWithType owt in owts)
			{
				ripLog.WriteLine("\t-- " + owt.type + " --");
				
				foreach(UnityEngine.Object o in owt.objs)
				{
					if(taken.Contains(o.GetInstanceID())) continue;
					ripLog.WriteLine(
						objformat,
						o.GetInstanceID().ToString(),
						o.name,
						o.hideFlags.ToString());
					
					taken.Add(o.GetInstanceID());
				}
				ripLog.WriteLine();
			}
		}
	}
}