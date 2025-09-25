using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Vehicle : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class OnPrefsUpdated_002487 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Vector3 _0024tnsor_0024560;

			internal Vector3 _0024cg_0024561;

			internal object _0024colliders_0024562;

			internal Collider _0024cldr_0024563;

			internal IEnumerator _0024___iterator86_0024564;

			internal Vehicle _0024self_565;

			public _0024(Vehicle self_)
			{
				_0024self_565 = self_;
			}

			public override bool MoveNext()
			{
				float[] laserLock;
				switch (_state)
				{
				default:
					if ((bool)_0024self_565.laserAimer)
					{
						_0024self_565.laserAimer.particleEmitter.emit = true;
					}
					if ((bool)_0024self_565.laserAimerLocked)
					{
						_0024self_565.laserAimerLocked.particleEmitter.emit = true;
					}
					if (Game.Settings.ramoSpheres != 0f)
					{
						return Yield(2, new WaitForSeconds(1f));
					}
					if ((bool)_0024self_565.ramoSphere)
					{
						_0024self_565.ramoSphereScale = 0.1f;
						return Yield(3, new WaitForSeconds(2f));
					}
					goto IL_03bb;
				case 2:
					_0024tnsor_0024560 = _0024self_565.rigidbody.inertiaTensor;
					_0024cg_0024561 = _0024self_565.rigidbody.centerOfMass;
					if (!_0024self_565.ramoSphere)
					{
						_0024self_565.ramoSphere = (GameObject)UnityEngine.Object.Instantiate(_0024self_565.ramoSphereObj, _0024self_565.transform.position, _0024self_565.transform.rotation);
						_0024self_565.ramoSphere.transform.parent = _0024self_565.transform;
						_0024colliders_0024562 = _0024self_565.vehObj.GetComponentsInChildren(typeof(Collider));
						_0024___iterator86_0024564 = UnityRuntimeServices.GetEnumerator(_0024colliders_0024562);
						while (_0024___iterator86_0024564.MoveNext())
						{
							_0024cldr_0024563 = (Collider)RuntimeServices.Coerce(_0024___iterator86_0024564.Current, typeof(Collider));
							Physics.IgnoreCollision((Collider)RuntimeServices.Coerce(_0024self_565.ramoSphere.collider, typeof(Collider)), _0024cldr_0024563);
							UnityRuntimeServices.Update(_0024___iterator86_0024564, _0024cldr_0024563);
						}
					}
					RuntimeServices.SetProperty(_0024self_565.ramoSphere.collider, "active", false);
					_0024self_565.ramoSphereScale = Game.Settings.ramoSpheres * 15f + (float)checked(_0024self_565.camOffset * 1);
					if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(_0024self_565.ramoSphere.collider, "isTrigger"), _0024self_565.zorbBall))
					{
						RuntimeServices.SetProperty(_0024self_565.ramoSphere.collider, "isTrigger", !_0024self_565.zorbBall);
						_0024self_565.ramoSphere.transform.localScale = Vector3.zero;
						RuntimeServices.SetProperty(_0024self_565.ramoSphere.collider, "active", true);
						UnityRuntimeServices.Invoke(_0024self_565.ramoSphere.GetComponent("RamoSphere"), "colorSet", new object[1] { _0024self_565.zorbBall }, typeof(MonoBehaviour));
					}
					else
					{
						RuntimeServices.SetProperty(_0024self_565.ramoSphere.collider, "active", true);
					}
					_0024self_565.rigidbody.inertiaTensor = _0024tnsor_0024560;
					_0024self_565.rigidbody.centerOfMass = _0024cg_0024561;
					goto IL_03bb;
				case 3:
					UnityEngine.Object.Destroy(_0024self_565.ramoSphere);
					goto IL_03bb;
				case 1:
					break;
					IL_03bb:
					laserLock = Game.Settings.laserLock;
					if (laserLock[RuntimeServices.NormalizeArrayIndex(laserLock, _0024self_565.vehId)] > 0f)
					{
						_0024self_565.laserLock.active = true;
						Transform transform = _0024self_565.laserLock.transform;
						Vector3 one = Vector3.one;
						float[] laserLock2 = Game.Settings.laserLock;
						transform.localScale = one * ((laserLock2[RuntimeServices.NormalizeArrayIndex(laserLock2, _0024self_565.vehId)] + (float)_0024self_565.camOffset * 0.1f) * 10f);
					}
					else
					{
						_0024self_565.laserLock.active = false;
						_0024self_565.laserLock.transform.localScale = Vector3.zero;
					}
					Yield(1, null);
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Vehicle _0024self_566;

		public OnPrefsUpdated_002487(Vehicle self_)
		{
			_0024self_566 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_566);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class lR_002488 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Vehicle _0024self_567;

			public _0024(Vehicle self_)
			{
				_0024self_567 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					if (Time.time - _0024self_567.lastReset < 3f || !_0024self_567.myRigidbody || !World.@base)
					{
						break;
					}
					_0024self_567.lastReset = Time.time;
					if (_0024self_567.isPlayer || _0024self_567.isBot)
					{
						_0024self_567.myRigidbody.isKinematic = true;
					}
					Game.Controller.mE(_0024self_567.transform.position);
					Game.Controller.mE(World.@base.position);
					_0024self_567.ramoSphereScale = 0.01f;
					return Yield(2, new WaitForSeconds(2f));
				case 2:
					if ((bool)_0024self_567.ramoSphere)
					{
						UnityEngine.Object.Destroy(_0024self_567.ramoSphere);
					}
					if (_0024self_567.isPlayer || _0024self_567.isBot)
					{
						_0024self_567.transform.position = World.@base.position;
						_0024self_567.myRigidbody.isKinematic = false;
					}
					_0024self_567.StartCoroutine_Auto(_0024self_567.OnPrefsUpdated());
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Vehicle _0024self_568;

		public lR_002488(Vehicle self_)
		{
			_0024self_568 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_568);
		}
	}

	public int camOffset;

	public Transform ridePos;

	public LayerMask terrainMask;

	public GameObject laserAimer;

	public GameObject laserAimerLocked;

	public GameObject laserLock;

	public ParticleEmitter bubbles;

	public int vehId;

	public int networkMode;

	[NonSerialized]
	public string shortName;

	[NonSerialized]
	public int isIt;

	[NonSerialized]
	public float lastTag;

	private float startTime;

	[NonSerialized]
	public float lastReset;

	[NonSerialized]
	public int score;

	[NonSerialized]
	public int scoreTime;

	public Vector4 input;

	public bool specialInput;

	public bool inputThrottle;

	[NonSerialized]
	public bool zorbBall;

	[NonSerialized]
	public bool brakes;

	[NonSerialized]
	public bool camSmooth;

	[NonSerialized]
	public Vector3 velocity;

	public bool isBot;

	[NonSerialized]
	public bool isPlayer;

	[NonSerialized]
	public bool isResponding;

	[NonSerialized]
	public string netCode;

	[NonSerialized]
	public GameObject vehObj;

	[NonSerialized]
	public Rigidbody myRigidbody;

	[NonSerialized]
	public GameObject ramoSphere;

	public GameObject ramoSphereObj;

	[NonSerialized]
	public float ramoSphereScale;

	private GameObject marker;

	private GameObject markerQuarry;

	[NonSerialized]
	public VehicleNet vehicleNet;

	[NonSerialized]
	public int netKillMode;

	private float updateTick;

	public Color vehicleColor;

	public Color vehicleAccent;

	public Material[] materialMain;

	public Material[] materialAccent;

	private bool updateColor;

	public Vehicle()
	{
		camOffset = 2;
		terrainMask = ~1 << 4;
		vehId = 0;
		networkMode = 0;
		isIt = 0;
		lastTag = 0f;
		lastReset = 0f;
		specialInput = false;
		zorbBall = false;
		brakes = false;
		isBot = false;
		isPlayer = false;
		isResponding = false;
		netCode = " (No Connection)";
		vehicleNet = null;
		netKillMode = 0;
		updateTick = 0f;
		updateColor = false;
	}

	public void Start()
	{
		if (networkView.viewID.isMine)
		{
			VehicleLocal vehicleLocal = (VehicleLocal)gameObject.AddComponent(typeof(VehicleLocal));
			vehicleLocal.vehicle = this;
		}
		if (networkView.viewID.isMine && !isBot)
		{
			marker = (GameObject)UnityEngine.Object.Instantiate(Game.objectMarkerMe, transform.position, transform.rotation);
			marker.transform.parent = transform;
			isPlayer = true;
			Game.PlayerVeh = this;
			VehicleMe vehicleMe = (VehicleMe)gameObject.AddComponent(typeof(VehicleMe));
			vehicleMe.vehicle = this;
			vehicleColor.r = PlayerPrefs.GetFloat("vehColR");
			vehicleColor.g = PlayerPrefs.GetFloat("vehColG");
			vehicleColor.b = PlayerPrefs.GetFloat("vehColB");
			vehicleAccent.r = PlayerPrefs.GetFloat("vehColAccR");
			vehicleAccent.g = PlayerPrefs.GetFloat("vehColAccG");
			vehicleAccent.b = PlayerPrefs.GetFloat("vehColAccB");
			if (Game.Settings.colorCustom)
			{
				Game.Settings.saveVehicleColor();
			}
			setColor();
		}
		else
		{
			UnityEngine.Object.Destroy(laserAimer);
			UnityEngine.Object.Destroy(laserAimerLocked);
			marker = (GameObject)UnityEngine.Object.Instantiate(Game.objectMarker, transform.position, transform.rotation);
			marker.transform.parent = transform;
			markerQuarry = (GameObject)UnityEngine.Object.Instantiate(Game.objectMarkerQuarry, transform.position, transform.rotation);
			markerQuarry.transform.parent = transform;
			if (isBot && networkView.viewID.isMine)
			{
				VehicleBot vehicleBot = (VehicleBot)gameObject.AddComponent(typeof(VehicleBot));
				vehicleBot.vehicle = this;
				vehicleColor = Game.PlayerVeh.vehicleColor;
				vehicleAccent = Game.PlayerVeh.vehicleAccent;
			}
			else
			{
				vehicleNet = (VehicleNet)gameObject.AddComponent(typeof(VehicleNet));
				vehicleNet.vehicle = this;
			}
		}
		gameObject.AddComponent(typeof(Rigidbody));
		myRigidbody = rigidbody;
		myRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		if (Game.Players.ContainsKey(name))
		{
			Game.Players.Remove(name);
		}
		Game.Players.Add(name, this);
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			UnityRuntimeServices.Invoke(dictionaryEntry.Value, "setColor", new object[0], typeof(MonoBehaviour));
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
		}
		vehObj.BroadcastMessage("InitVehicle", this);
		lastTag = Time.time;
		startTime = Time.time;
		Game.Settings.updateObjects();
	}

	public void Update()
	{
		ParticleEmitter obj = bubbles;
		bool num = transform.position.y < Game.Settings.lavaAlt - 2f;
		if (!num)
		{
			num = Physics.Raycast(transform.position + Vector3.up * 200f, Vector3.down, 198f, 1 << 4);
		}
		obj.emit = num;
		ParticleEmitter obj2 = bubbles;
		float maxEnergy = (bubbles.minEnergy = (bubbles.emit ? 5 : 0));
		obj2.maxEnergy = maxEnergy;
		if (isBot || !networkView.isMine)
		{
			if (isIt != 0 && (bool)markerQuarry && !markerQuarry.active)
			{
				markerQuarry.SetActiveRecursively(true);
				marker.SetActiveRecursively(false);
			}
			else if (isIt == 0 && (bool)markerQuarry && markerQuarry.active)
			{
				markerQuarry.SetActiveRecursively(false);
				marker.SetActiveRecursively(true);
			}
			if (isIt != 0 && (bool)Game.Player)
			{
				Game.Controller.quarryDist = Vector3.Distance(transform.position, Game.Player.transform.position);
			}
		}
		checked
		{
			if (updateColor)
			{
				updateColor = false;
				bool flag = ((isIt != 0 && Game.Players.Count > 1) ? true : false);
				if (Extensions.get_length((System.Array)materialMain) > 0)
				{
					Color b = ((!flag) ? vehicleColor : Game.Controller.vehicleIsItColor);
					materialMain[0].color = Color.Lerp(materialMain[0].color, b, Time.deltaTime * 2f);
					float a = 0.5f;
					Color color = materialMain[0].color;
					float num3 = (color.a = a);
					Color color2 = (materialMain[0].color = color);
					if (Extensions.get_length((System.Array)materialAccent) > 0)
					{
						materialMain[0].SetColor("_SpecColor", materialAccent[0].color);
					}
					else
					{
						materialMain[0].SetColor("_SpecColor", vehicleAccent);
					}
					if (materialMain[0].color.r < b.r - 0.05f || materialMain[0].color.r > b.r + 0.05f || materialMain[0].color.g < b.g - 0.05f || materialMain[0].color.g > b.g + 0.05f || materialMain[0].color.b < b.b - 0.05f || materialMain[0].color.b > b.b + 0.05f)
					{
						updateColor = true;
					}
					if (Extensions.get_length((System.Array)materialMain) > 1)
					{
						for (int i = 1; i < Extensions.get_length((System.Array)materialMain); i++)
						{
							Material[] array = materialMain;
							array[RuntimeServices.NormalizeArrayIndex(array, i)].color = materialMain[0].color;
						}
					}
				}
				if (Extensions.get_length((System.Array)materialAccent) > 0)
				{
					Color b = ((!flag) ? vehicleAccent : Game.Controller.vehicleIsItAccent);
					materialAccent[0].color = Color.Lerp(materialAccent[0].color, b, Time.deltaTime * 2f);
					float a2 = 0.5f;
					Color color4 = materialAccent[0].color;
					float num4 = (color4.a = a2);
					Color color5 = (materialAccent[0].color = color4);
					materialAccent[0].SetColor("_SpecColor", materialMain[0].color);
					if (materialAccent[0].color.r < b.r - 0.05f || materialAccent[0].color.r > b.r + 0.05f || materialAccent[0].color.g < b.g - 0.05f || materialAccent[0].color.g > b.g + 0.05f || materialAccent[0].color.b < b.b - 0.05f || materialAccent[0].color.b > b.b + 0.05f)
					{
						updateColor = true;
					}
					if (Extensions.get_length((System.Array)materialAccent) > 1)
					{
						for (int i = 1; i < Extensions.get_length((System.Array)materialAccent); i++)
						{
							Material[] array2 = materialAccent;
							array2[RuntimeServices.NormalizeArrayIndex(array2, i)].color = materialAccent[0].color;
						}
					}
				}
			}
			if (Time.time > updateTick)
			{
				updateTick = Time.time + 1f;
				if (!Game.Players.ContainsKey(name))
				{
					Game.Players.Add(name, this);
				}
			}
		}
	}

	public void FixedUpdate()
	{
		if (ramoSphereScale != 0f && (bool)ramoSphere)
		{
			ramoSphere.transform.localScale = Vector3.Lerp(ramoSphere.transform.localScale, Vector3.one * ramoSphereScale, Time.fixedDeltaTime);
			if (ramoSphere.transform.localScale.x > ramoSphereScale - 0.01f && ramoSphere.transform.localScale.x < ramoSphereScale + 0.01f)
			{
				ramoSphereScale = 0f;
			}
		}
	}

	public void OnGUI()
	{
		if (!myRigidbody || (netCode != string.Empty && Time.time < startTime + 5f))
		{
			return;
		}
		GUI.skin = Game.Skin;
		float gUIAlpha = Game.GUIAlpha;
		Color color = GUI.color;
		float num = (color.a = gUIAlpha);
		Color color2 = (GUI.color = color);
		GUI.depth = -1;
		if (networkView.isMine && !isBot)
		{
			GUI.Button(new Rect((float)Screen.width * 0.5f - 75f, checked(Screen.height - 30), 150f, 20f), ((!(myRigidbody.velocity.magnitude < 0.05f)) ? (Mathf.RoundToInt(myRigidbody.velocity.magnitude * 2.23f) + " MPH") : "Static") + "     " + Mathf.RoundToInt(myRigidbody.transform.position.y) + " ALT" + ((isIt != 0) ? string.Empty : ("     " + Mathf.RoundToInt(Game.Controller.quarryDist) + " DST")), "hudText");
		}
		GUI.depth = 5;
		Vector3 vector = Camera.main.WorldToScreenPoint(transform.position);
		if (((!networkView.isMine || isBot) && Game.Settings.hideNames && (!(Vector3.Distance(new Vector3(vector.x, vector.y, 0f), Input.mousePosition) < 40f) || Physics.Linecast(transform.position, Camera.main.transform.position, 1 << 8))) || (!(vector.z > 0f) && (!networkView.isMine || isBot)))
		{
			return;
		}
		if (vector.z < 0f)
		{
			vector.z = 0f;
		}
		float num2 = Mathf.Max(50f, Mathf.Min(150f, (float)Screen.width * 0.16f) - vector.z / 1.5f);
		float num3 = Mathf.Max(20f, Mathf.Min(50f, (float)Screen.width * 0.044f) - vector.z * 0.2f);
		if ((vector.z <= 1f || vector.y < num3 * 1.9f) && networkView.isMine && !isBot)
		{
			if (!(vector.z > 1f))
			{
				vector.x = Screen.width / 2;
			}
			vector.y = num3 + 100f;
		}
		GUI.Button(new Rect(vector.x - num2 * 0.5f, (float)Screen.height - vector.y + num3 * 1f, num2, num3), name + "\n" + shortName + " " + score + netCode, "player_nametag" + ((isIt == 0) ? string.Empty : "_it"));
	}

	public IEnumerator OnPrefsUpdated()
	{
		return new OnPrefsUpdated_002487(this).GetEnumerator();
	}

	public void OnCollisionEnter(Collision collision)
	{
		if ((bool)ramoSphere && RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(ramoSphere.collider, "isTrigger"), false))
		{
			ramoSphere.SendMessage("OnCollisionEnter", collision);
		}
	}

	public void OnRam(GameObject other)
	{
		Vehicle vehicle = (Vehicle)other.GetComponent(typeof(Vehicle));
		if ((bool)vehicle && vehicle.isIt == 1 && vehicle.isResponding && !(Time.time - lastTag < 3f) && !(Time.time - vehicle.lastTag < 3f))
		{
			lastTag = Time.time;
			networkView.RPC("sQ", RPCMode.All, 1);
		}
	}

	public void OnLaserHit(bool isFatal)
	{
		if (isFatal && Game.Settings.lasersFatal && Vector3.Distance(transform.position, World.@base.position) > 10f)
		{
			myRigidbody.isKinematic = true;
			networkView.RPC("lR", RPCMode.All);
		}
	}

	[RPC]
	public IEnumerator lR()
	{
		return new lR_002488(this).GetEnumerator();
	}

	[RPC]
	public void fR(NetworkViewID LaunchedByViewID, string id, Vector3 pos, Vector3 ang, NetworkMessageInfo info)
	{
		object target = UnityEngine.Object.Instantiate(Game.objectRocket, pos, Quaternion.Euler(ang));
		Rocket rocket = (Rocket)RuntimeServices.Coerce(UnityRuntimeServices.Invoke(target, "GetComponent", new object[1] { "Rocket" }, typeof(MonoBehaviour)), typeof(Rocket));
		rocket.laserID = id;
		if (!info.networkView.isMine)
		{
			rocket.lag = (float)(Network.time - info.timestamp);
		}
		rocket.launchVehicle = this;
	}

	[RPC]
	public void fS(NetworkViewID LaunchedByViewID, string id, Vector3 pos, Vector3 ang, NetworkMessageInfo info)
	{
		object target = UnityEngine.Object.Instantiate(Game.objectRocketSnipe, pos, Quaternion.Euler(ang));
		Rocket rocket = (Rocket)RuntimeServices.Coerce(UnityRuntimeServices.Invoke(target, "GetComponent", new object[1] { "Rocket" }, typeof(MonoBehaviour)), typeof(Rocket));
		rocket.laserID = id;
		if (!info.networkView.isMine)
		{
			rocket.lag = (float)(Network.time - info.timestamp);
		}
		rocket.launchVehicle = this;
	}

	[RPC]
	public void fRl(NetworkViewID LaunchedByViewID, string id, Vector3 pos, NetworkViewID targetViewID, NetworkMessageInfo info)
	{
		object target = UnityEngine.Object.Instantiate(Game.objectRocket, pos, Quaternion.identity);
		Rocket rocket = (Rocket)RuntimeServices.Coerce(UnityRuntimeServices.Invoke(target, "GetComponent", new object[1] { "Rocket" }, typeof(MonoBehaviour)), typeof(Rocket));
		rocket.laserID = id;
		if (!info.networkView.isMine)
		{
			rocket.lag = (float)(Network.time - info.timestamp);
		}
		rocket.launchVehicle = this;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "viewID"), targetViewID))
			{
				rocket.targetVehicle = (Vehicle)RuntimeServices.Coerce(dictionaryEntry.Value, typeof(Vehicle));
				UnityRuntimeServices.Update(enumerator, dictionaryEntry);
				break;
			}
		}
	}

	[RPC]
	public void fSl(NetworkViewID LaunchedByViewID, string id, Vector3 pos, NetworkViewID targetViewID, NetworkMessageInfo info)
	{
		object target = UnityEngine.Object.Instantiate(Game.objectRocketSnipe, pos, Quaternion.identity);
		Rocket rocket = (Rocket)RuntimeServices.Coerce(UnityRuntimeServices.Invoke(target, "GetComponent", new object[1] { "Rocket" }, typeof(MonoBehaviour)), typeof(Rocket));
		rocket.laserID = id;
		if (!info.networkView.isMine)
		{
			rocket.lag = (float)(Network.time - info.timestamp);
		}
		rocket.launchVehicle = this;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "viewID"), targetViewID))
			{
				rocket.targetVehicle = (Vehicle)RuntimeServices.Coerce(dictionaryEntry.Value, typeof(Vehicle));
				UnityRuntimeServices.Update(enumerator, dictionaryEntry);
				break;
			}
		}
	}

	[RPC]
	public void lH(string n, Vector3 pos)
	{
		GameObject gameObject = GameObject.Find("lsr#" + n);
		if ((bool)gameObject)
		{
			UnityRuntimeServices.Invoke(gameObject.GetComponent(typeof(Rocket)), "laserHit", new object[3]
			{
				this.gameObject,
				transform.TransformPoint(pos),
				Vector3.up
			}, typeof(MonoBehaviour));
		}
	}

	[RPC]
	public void sP(Vector3 pos, Quaternion rot, NetworkMessageInfo info)
	{
		if (!vehicleNet)
		{
			return;
		}
		if (networkView.stateSynchronization != NetworkStateSynchronization.Off)
		{
			Debug.Log("sP NvN: " + gameObject.name);
			return;
		}
		vehicleNet.rpcPing = (float)(Network.time - info.timestamp);
		if (vehicleNet.states[0] != null && !((double)vehicleNet.states[0].t < info.timestamp))
		{
			Debug.Log("sP OoO: " + vehicleNet.states[0].t + " * " + Time.time);
			return;
		}
		checked
		{
			for (int num = Extensions.get_length((System.Array)vehicleNet.states) - 1; num > 0; num--)
			{
				State[] states = vehicleNet.states;
				int num2 = RuntimeServices.NormalizeArrayIndex(states, num);
				State[] states2 = vehicleNet.states;
				states[num2] = states2[RuntimeServices.NormalizeArrayIndex(states2, num - 1)];
			}
			vehicleNet.states[0] = new State(pos, rot, (float)info.timestamp, 0f, 0f);
			float num3 = (float)(Network.time - (double)vehicleNet.states[0].t);
			vehicleNet.jitter = Mathf.Lerp(vehicleNet.jitter, Mathf.Abs(vehicleNet.ping - num3), 1f / Network.sendRate);
			vehicleNet.ping = Mathf.Lerp(vehicleNet.ping, num3, 1f / Network.sendRate);
		}
	}

	[RPC]
	public void sT(float time, NetworkMessageInfo info)
	{
		if ((bool)vehicleNet || networkView.isMine)
		{
			if (time > 0f)
			{
				vehicleNet.calcPing = Mathf.Lerp(vehicleNet.calcPing, (Time.time - vehicleNet.lastPing) / (float)(vehicleNet.wePinged ? 1 : 2), 0.5f);
				vehicleNet.wePinged = false;
			}
			else if (networkView.isMine)
			{
				networkView.RPC("sT", RPCMode.Others, 1f);
			}
			else
			{
				vehicleNet.lastPing = Time.time;
				vehicleNet.wePinged = ((info.sender == Network.player) ? true : false);
			}
		}
	}

	[RPC]
	public void s4(int x, int y, int z, int w)
	{
		input = new Vector4(x / 10, y / 10, z / 10, w / 10);
	}

	[RPC]
	public void sI(bool input)
	{
		specialInput = input;
		gameObject.BroadcastMessage("OnSetSpecialInput", SendMessageOptions.DontRequireReceiver);
	}

	[RPC]
	public void sB(bool input)
	{
		brakes = input;
	}

	[RPC]
	public void sZ(bool input)
	{
		zorbBall = input;
		StartCoroutine_Auto(OnPrefsUpdated());
	}

	[RPC]
	public void sQ(int mode)
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			RuntimeServices.SetProperty(dictionaryEntry.Value, "isIt", 0);
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
			UnityRuntimeServices.Invoke(dictionaryEntry.Value, "setColor", new object[0], typeof(MonoBehaviour));
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
		}
		isIt = 1;
		Game.QuarryVeh = this;
		setColor();
		switch (mode)
		{
		case 1:
			Game.Controller.msg(gameObject.name + " rammed the Quarry", UnityBuiltins.parseInt(2));
			break;
		case 2:
			Game.Controller.msg(gameObject.name + " is now the Quarry", UnityBuiltins.parseInt(2));
			break;
		case 3:
			Game.Controller.msg(gameObject.name + " Defaulted to Quarry", UnityBuiltins.parseInt(2));
			break;
		case 0:
			return;
		}
		lastTag = Time.time;
	}

	[RPC]
	public void iS(string name)
	{
		checked
		{
			score++;
			Game.Controller.msg(gameObject.name + " Got  " + name, UnityBuiltins.parseInt(2));
		}
	}

	[RPC]
	public void dS(string name)
	{
		checked
		{
			score--;
		}
	}

	[RPC]
	public void iT()
	{
		checked
		{
			scoreTime++;
		}
	}

	[RPC]
	public void sS(int s)
	{
		score = s;
	}

	[RPC]
	public void sC(float cR, float cG, float cB, float aR, float aG, float aB)
	{
		vehicleColor.r = cR;
		vehicleColor.g = cG;
		vehicleColor.b = cB;
		vehicleAccent.r = aR;
		vehicleAccent.g = aG;
		vehicleAccent.b = aB;
		updateColor = true;
	}

	public void setColor()
	{
		updateColor = true;
	}

	[RPC]
	public void dN(int rsn)
	{
		netKillMode = rsn;
	}

	public void Main()
	{
	}
}
