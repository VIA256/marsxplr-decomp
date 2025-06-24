using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Settings : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class ramdomizeVehicleColor_002470 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal Settings _0024self_504;

			public _0024(Settings self_)
			{
				_0024self_504 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					if (!Game.PlayerVeh)
					{
						return Yield(2, null);
					}
					Game.PlayerVeh.vehicleColor.r = UnityEngine.Random.value * 0.7f;
					Game.PlayerVeh.vehicleColor.b = UnityEngine.Random.value * 0.7f;
					Game.PlayerVeh.vehicleColor.g = UnityEngine.Random.value * Mathf.Lerp(0.1f, 0.8f, (Game.PlayerVeh.vehicleColor.r + Game.PlayerVeh.vehicleColor.b) / 2f) * 0.7f;
					_0024self_504.colorCustom = false;
					_0024self_504.updateVehicleAccent();
					_0024self_504.saveVehicleColor();
					Game.PlayerVeh.setColor();
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Settings _0024self_505;

		public ramdomizeVehicleColor_002470(Settings self_)
		{
			_0024self_505 = self_;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(_0024self_505);
		}
	}

	[HideInInspector]
	public int showBox;

	[HideInInspector]
	public bool simplified;

	public AudioSource gameMusic;

	public AudioClip[] musicTracks;

	public PhysicMaterial zorbPhysics;

	[HideInInspector]
	public int renderLevel;

	[HideInInspector]
	public int renderAdjustMax;

	[HideInInspector]
	public float renderViewCap;

	public bool enteredfullscreen;

	[HideInInspector]
	public bool renderAutoAdjust;

	[HideInInspector]
	public int renderAdjustTime;

	[HideInInspector]
	public bool showHints;

	[HideInInspector]
	public float serverUpdateTime;

	[HideInInspector]
	public float colorUpdateTime;

	public bool colorCustom;

	public bool disableHints;

	public Color fogColor;

	private bool useFog;

	private bool detailedObjects;

	private bool useParticles;

	private bool useTrails;

	public bool useMinimap;

	private bool foliage;

	private bool terrainQuality;

	private bool terrainDetail;

	private bool terrainLighting;

	public int useMusic;

	public bool useSfx;

	public int useHypersound;

	private bool syncFps;

	private bool playerOnlyLight;

	private Vector2 scrollPosition;

	[HideInInspector]
	public SSAOEffect camSSAO;

	public ContrastStretchEffect camContrast;

	[HideInInspector]
	public string txt;

	[HideInInspector]
	public string str;

	[HideInInspector]
	public string serverString;

	[HideInInspector]
	public string bannedIPs;

	public static string serverDefault;

	public string serverWelcome;

	public int camMode;

	public int camChase;

	public float camDist;

	public bool flightCam;

	public bool gyroCam;

	public float worldGrav;

	public float worldFog;

	public float worldViewDist;

	public float lavaFog;

	public float lavaAlt;

	public int laserSpeed;

	public float laserGrav;

	public float laserRico;

	public bool laserLocking;

	public float resetTime;

	public bool lasersAllowed;

	public bool lasersFatal;

	public bool lasersOptHit;

	public float ramoSpheres;

	public float zorbSpeed;

	public float zorbAgility;

	public float zorbBounce;

	public bool minimapAllowed;

	public bool hideNames;

	public bool botsCanFire;

	public bool botsCanDrive;

	public int[] firepower;

	public float[] laserLock;

	public bool buggyAllowed;

	public bool buggyFlightSlip;

	public bool buggySmartSuspension;

	public bool buggyNewPhysics;

	public bool buggyAWD;

	public float buggyCG;

	public float buggyPower;

	public float buggySpeed;

	public bool buggyFlightLooPower;

	public float buggyFlightDrag;

	public float buggyFlightAgility;

	public float buggyTr;

	public float buggySh;

	public float buggySl;

	public bool tankAllowed;

	public float tankPower;

	public float tankSpeed;

	public float tankGrip;

	public float tankCG;

	public bool hoverAllowed;

	public float hoverHeight;

	public float hoverHover;

	public float hoverRepel;

	public float hoverThrust;

	public bool jetAllowed;

	public float jetHDrag;

	public float jetDrag;

	public int jetSteer;

	public float jetLift;

	public int jetStall;

	public int networkMode;

	public int networkPhysics;

	public float networkInterpolation;

	public bool isAdmin;

	public Settings()
	{
		showBox = 0;
		simplified = true;
		renderLevel = 0;
		renderAdjustMax = 0;
		renderViewCap = 1000f;
		enteredfullscreen = false;
		renderAutoAdjust = false;
		renderAdjustTime = 8;
		showHints = true;
		serverUpdateTime = 0f;
		colorUpdateTime = 0f;
		colorCustom = false;
		disableHints = false;
		fogColor = Color.clear;
		useFog = true;
		detailedObjects = true;
		useParticles = true;
		useTrails = true;
		useMinimap = true;
		foliage = true;
		terrainQuality = true;
		terrainDetail = true;
		terrainLighting = true;
		useMusic = 1;
		useSfx = true;
		useHypersound = 0;
		syncFps = false;
		playerOnlyLight = false;
		serverWelcome = string.Empty;
		camMode = 0;
		camChase = 0;
		camDist = 0f;
		flightCam = false;
		gyroCam = false;
		worldGrav = 9.81f * -1f;
		worldFog = 0.001f;
		worldViewDist = 5000f;
		lavaFog = 0.005f;
		lavaAlt = 300f * -1f;
		laserSpeed = 180;
		laserGrav = 0f;
		laserRico = 0f;
		lasersAllowed = true;
		lasersFatal = false;
		lasersOptHit = false;
		ramoSpheres = 0f;
		zorbSpeed = 7f;
		zorbAgility = 0f;
		zorbBounce = 0.5f;
		minimapAllowed = true;
		hideNames = false;
		botsCanFire = true;
		botsCanDrive = true;
		buggyAllowed = true;
		buggyFlightSlip = false;
		buggySmartSuspension = true;
		buggyNewPhysics = false;
		buggyAWD = true;
		buggyCG = 0.4f * -1f;
		buggyPower = 1f;
		buggySpeed = 30f;
		buggyFlightLooPower = false;
		buggyFlightDrag = 300f;
		buggyFlightAgility = 1f;
		buggyTr = 1f;
		buggySh = 70f;
		buggySl = 50f;
		tankAllowed = true;
		tankPower = 2000f;
		tankSpeed = 25f;
		tankGrip = 0.1f;
		tankCG = 0.2f * -1f;
		hoverAllowed = true;
		hoverHeight = 15f;
		hoverHover = 100f;
		hoverRepel = 2.5f;
		hoverThrust = 220f;
		jetAllowed = true;
		jetHDrag = 0.01f;
		jetDrag = 0.001f;
		jetSteer = 20;
		jetLift = 0.5f;
		jetStall = 20;
		networkMode = 0;
		networkPhysics = 0;
		networkInterpolation = 0f;
		isAdmin = false;
	}

	public void Start()
	{
		simplified = true;
		getPrefs();
		updatePrefs();
		if (GameData.userName == "Aubrey (admin)")
		{
			isAdmin = true;
		}
	}

	public void getPrefs()
	{
		renderLevel = PlayerPrefs.GetInt("renderLevel", 4);
		renderViewCap = PlayerPrefs.GetFloat("viewCap", 1000f);
		Application.targetFrameRate = checked((int)PlayerPrefs.GetFloat("targetFrameRate", 100f));
		renderAutoAdjust = false;
		showHints = ((PlayerPrefs.GetInt("showHints", 1) != 0) ? true : false);
		useMusic = PlayerPrefs.GetInt("useMusic", 1);
		useSfx = ((PlayerPrefs.GetInt("useSfx", 1) != 0) ? true : false);
		useHypersound = PlayerPrefs.GetInt("useHypersound", 0);
		useMinimap = ((PlayerPrefs.GetInt("useMinimap", 1) != 0) ? true : false);
		bool flag = ((PlayerPrefs.GetInt("superCam", 1) != 0) ? true : false);
		flightCam = ((PlayerPrefs.GetInt("flightCam", 1) != 0) ? true : false);
		gyroCam = ((PlayerPrefs.GetInt("gyroCam", 0) != 0) ? true : false);
		camMode = PlayerPrefs.GetInt("cam", 1);
		camChase = PlayerPrefs.GetInt("camChase", 1);
		camDist = PlayerPrefs.GetFloat("camDist", 0.01f);
		flightCam = ((PlayerPrefs.GetInt("flightCam", 0) != 0) ? true : false);
		gyroCam = ((PlayerPrefs.GetInt("gyroCam", 0) != 0) ? true : false);
	}

	public void showDialogGame()
	{
		GUILayout.Label("Resolution:");
		if (GUILayout.Button((Screen.fullScreen ? "Exit" : "Enter") + " Fullscreen (0)"))
		{
			toggleFullscreen();
		}
		checked
		{
			if (Screen.fullScreen || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
			{
				GUILayout.BeginHorizontal();
				if ((Screen.resolutions[0].width < Screen.width || Screen.resolutions[0].height < Screen.height) && GUILayout.Button("<<", GUILayout.Width(28f)))
				{
					for (int num = Extensions.get_length((System.Array)Screen.resolutions) - 1; num >= 0; num--)
					{
						Resolution[] resolutions = Screen.resolutions;
						if (resolutions[RuntimeServices.NormalizeArrayIndex(resolutions, num)].width < Screen.width)
						{
							Resolution[] resolutions2 = Screen.resolutions;
							int width = resolutions2[RuntimeServices.NormalizeArrayIndex(resolutions2, num)].width;
							Resolution[] resolutions3 = Screen.resolutions;
							Screen.SetResolution(width, resolutions3[RuntimeServices.NormalizeArrayIndex(resolutions3, num)].height, Screen.fullScreen);
							break;
						}
					}
				}
				GUILayout.Label(Screen.width + "X" + Screen.height);
				Resolution[] resolutions4 = Screen.resolutions;
				if (resolutions4[RuntimeServices.NormalizeArrayIndex(resolutions4, Extensions.get_length((System.Array)Screen.resolutions) - 1)].width <= Screen.width)
				{
					Resolution[] resolutions5 = Screen.resolutions;
					if (resolutions5[RuntimeServices.NormalizeArrayIndex(resolutions5, Extensions.get_length((System.Array)Screen.resolutions) - 1)].height <= Screen.height)
					{
						goto IL_028f;
					}
				}
				if (GUILayout.Button(">>", GUILayout.Width(28f)))
				{
					int i = 0;
					Resolution[] resolutions6 = Screen.resolutions;
					for (int length = resolutions6.Length; i < length; i++)
					{
						if (resolutions6[i].width > Screen.width)
						{
							Screen.SetResolution(resolutions6[i].width, resolutions6[i].height, Screen.fullScreen);
							break;
						}
					}
				}
				goto IL_028f;
			}
			goto IL_0294;
		}
		IL_028f:
		GUILayout.EndHorizontal();
		goto IL_0294;
		IL_0294:
		GUILayout.FlexibleSpace();
		GUILayout.Space(20f);
		GUILayout.Label("Rendering Quality:");
		GUILayout.BeginHorizontal();
		if (renderLevel == 1)
		{
			GUILayout.Label("Fastest");
		}
		else if (renderLevel == 2)
		{
			GUILayout.Label("Fast");
		}
		else if (renderLevel == 3)
		{
			GUILayout.Label("Simple");
		}
		else if (renderLevel == 4)
		{
			GUILayout.Label("Good");
		}
		else if (renderLevel == 5)
		{
			GUILayout.Label("Beautiful");
		}
		else if (renderLevel == 6)
		{
			GUILayout.Label("Fantastic");
		}
		GUILayout.Label("(" + Game.Controller.fps.ToString("f0") + " FPS)");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		checked
		{
			if (renderLevel > 1 && GUILayout.Button("<<"))
			{
				renderLevel--;
				PlayerPrefs.SetInt("renderLevel", renderLevel);
				updatePrefs();
			}
			if (renderLevel > 1 && renderLevel < 6)
			{
				GUILayout.Space(5f);
			}
			if (renderLevel < 6 && GUILayout.Button(">>"))
			{
				renderLevel++;
				PlayerPrefs.SetInt("renderLevel", renderLevel);
				updatePrefs();
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			UnityRuntimeServices.Invoke(typeof(GUILayout), "Label", new object[1] { RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", "Visibility Cap:   (", (renderViewCap != 1000f) ? ((object)Mathf.Floor(renderViewCap)) : "MAX"), ")") }, typeof(MonoBehaviour));
			float num2 = GUILayout.HorizontalSlider(renderViewCap, 200f, 1000f);
			if (renderViewCap != num2)
			{
				renderViewCap = num2;
				PlayerPrefs.SetFloat("viewCap", num2);
				updatePrefs();
			}
			GUILayout.Space(5f);
			UnityRuntimeServices.Invoke(typeof(GUILayout), "Label", new object[1] { RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", "FPS Cap:   (", (Application.targetFrameRate != -1) ? ((object)Application.targetFrameRate) : "MAX"), ")") }, typeof(MonoBehaviour));
			if (Application.targetFrameRate == -1)
			{
				Application.targetFrameRate = 100;
			}
			num2 = GUILayout.HorizontalSlider(Application.targetFrameRate, 10f, 100f);
			if ((float)Application.targetFrameRate != num2)
			{
				Application.targetFrameRate = (int)num2;
				PlayerPrefs.SetFloat("targetFrameRate", num2);
			}
			if (Application.targetFrameRate == 0 || Application.targetFrameRate == 100)
			{
				Application.targetFrameRate = -1;
			}
			GUILayout.FlexibleSpace();
			GUILayout.Space(20f);
			GUILayout.Label("Interface:");
			if (minimapAllowed && GUILayout.Toggle(useMinimap, "Enable Minimap") != useMinimap)
			{
				useMinimap = !useMinimap;
				PlayerPrefs.SetInt("useMinimap", useMinimap ? 1 : 0);
				updatePrefs();
			}
			if (GUILayout.Toggle(showHints, "Enable Settings Advisor") != showHints)
			{
				showHints = !showHints;
				PlayerPrefs.SetInt("showHints", showHints ? 1 : 0);
				updatePrefs();
			}
			GUILayout.FlexibleSpace();
			GUILayout.Space(20f);
			GUILayout.Label("Audio:");
			if (GUILayout.Toggle(useSfx, "Sound Effects Enabled") != useSfx)
			{
				useSfx = !useSfx;
				PlayerPrefs.SetInt("useSfx", useSfx ? 1 : 0);
			}
			if (GUILayout.Toggle(useMusic == 0, "No Music") != (useMusic == 0))
			{
				useMusic = 0;
				PlayerPrefs.SetInt("useMusic", 0);
				updatePrefs();
			}
			if (GUILayout.Toggle(useMusic == 1, "Classic") != (useMusic == 1))
			{
				useMusic = 1;
				PlayerPrefs.SetInt("useMusic", 1);
				updatePrefs();
			}
			if (GUILayout.Toggle(useMusic == 2, "Ambient") != (useMusic == 2))
			{
				useMusic = 2;
				PlayerPrefs.SetInt("useMusic", 2);
				updatePrefs();
			}
			if (GUILayout.Toggle(useMusic == 3, "Carefree") != (useMusic == 3))
			{
				useMusic = 3;
				PlayerPrefs.SetInt("useMusic", 3);
				updatePrefs();
			}
			if (GUILayout.Toggle(useHypersound == 1, "HyperSound") != (useHypersound == 1))
			{
				useHypersound = ((useHypersound != 1) ? 1 : 0);
				PlayerPrefs.SetInt("useHypersound", (useHypersound != 0) ? 1 : 0);
				updatePrefs();
			}
		}
	}

	public void showDialogPlayer()
	{
		GUILayout.Space(20f);
		if (resetTime > 0f)
		{
			GUILayout.Label("Reset In " + (10f - (Time.time - resetTime)));
		}
		else if (resetTime > -1f && GUILayout.Button("Reset My Position (/r)"))
		{
			resetTime = Time.time;
			Game.Player.rigidbody.isKinematic = true;
			Game.Messaging.broadcast(Game.Player.name + " Resetting in 10 seconds...");
		}
		if (zorbSpeed != 0f)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button((Game.PlayerVeh.zorbBall ? "Deactivate" : "Activate") + " My Xorb (/x)"))
			{
				Game.Player.networkView.RPC("sZ", RPCMode.All, !Game.PlayerVeh.zorbBall);
			}
		}
		GUILayout.Space(20f);
		GUILayout.Label("Camera Mode:");
		if (GUILayout.Toggle(camMode == 0, "Ride (1, alt)") != (camMode == 0))
		{
			camMode = 0;
			PlayerPrefs.SetInt("cam", 0);
		}
		if (GUILayout.Toggle(camMode == 1, "Chase (2)") != (camMode == 1))
		{
			camMode = 1;
			PlayerPrefs.SetInt("cam", 1);
		}
		if (GUILayout.Toggle(camMode == 2, "Soar(5)") != (camMode == 2))
		{
			camMode = 2;
			PlayerPrefs.SetInt("cam", 2);
		}
		if (GUILayout.Toggle(camMode == 3, "Spectate(6)") != (camMode == 3))
		{
			camMode = 3;
			PlayerPrefs.SetInt("cam", 3);
		}
		if (GUILayout.Toggle(camMode == 4, "Roam") != (camMode == 4))
		{
			camMode = 4;
			PlayerPrefs.SetInt("cam", 4);
		}
		if (GUILayout.Toggle(gyroCam, "GyRide Enabled (7)") != gyroCam)
		{
			gyroCam = !gyroCam;
			PlayerPrefs.SetInt("gyroCam", gyroCam ? 1 : 0);
		}
		if (GUILayout.Toggle(flightCam, "HyperCam Enabled") != flightCam)
		{
			flightCam = !flightCam;
			PlayerPrefs.SetInt("flightCam", flightCam ? 1 : 0);
		}
		float num;
		if (camMode == 0)
		{
			GUILayout.Space(10f);
			GUILayout.Label("(Press (2) or (esc) keys to unlock your cursor)");
			GUILayout.Space(10f);
		}
		else if (camMode == 1)
		{
			GUILayout.Space(5f);
			GUILayout.Label("Chase Strategy:");
			if (GUILayout.Toggle(camChase == 0, "Smooth") != (camChase == 0))
			{
				camChase = 0;
				PlayerPrefs.SetInt("camChase", 0);
			}
			if (GUILayout.Toggle(camChase == 1, "Agile") != (camChase == 1))
			{
				camChase = 1;
				PlayerPrefs.SetInt("camChase", 1);
			}
			if (GUILayout.Toggle(camChase == 2, "Arcade") != (camChase == 2))
			{
				camChase = 2;
				PlayerPrefs.SetInt("camChase", 2);
			}
			GUILayout.Space(5f);
			GUILayout.Label("Chase Distance: (3-4)");
			num = GUILayout.HorizontalSlider(camDist, 0f, 20f);
			if (camDist != num)
			{
				camDist = num;
				PlayerPrefs.SetFloat("camDist", num);
			}
		}
		else if (camMode == 3 || camMode == 4)
		{
			GUILayout.Space(10f);
			GUILayout.Label("(Move camera with UIOJKL keys)");
			GUILayout.Space(10f);
		}
		GUILayout.FlexibleSpace();
		GUILayout.FlexibleSpace();
		GUILayout.Space(20f);
		GUILayout.Label("Vehicle Color:");
		if (Game.PlayerVeh.isIt != 0 && Game.Players.Count > 1)
		{
			GUILayout.Label("(You are quarry, and therefore green)");
		}
		if (GUILayout.Button((!colorCustom) ? "Randomize Coloring" : "Random Coloring"))
		{
			StartCoroutine_Auto(ramdomizeVehicleColor());
		}
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Game.PlayerVeh.vehicleColor.r, 0f, 1f);
		if (Game.PlayerVeh.vehicleColor.r != num)
		{
			Game.PlayerVeh.vehicleColor.r = num;
			updateVehicleAccent();
			updateVehicleColor();
		}
		GUILayout.Label("Red", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Mathf.Min(Mathf.Lerp(0.1f, 0.8f, (Game.PlayerVeh.vehicleColor.r + Game.PlayerVeh.vehicleColor.b) / 2f), Game.PlayerVeh.vehicleColor.g), 0f, Mathf.Lerp(0.1f, 0.8f, (Game.PlayerVeh.vehicleColor.r + Game.PlayerVeh.vehicleColor.b) / 2f));
		if (Game.PlayerVeh.vehicleColor.g != num)
		{
			Game.PlayerVeh.vehicleColor.g = num;
			updateVehicleAccent();
			updateVehicleColor();
		}
		GUILayout.Label("Green", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Game.PlayerVeh.vehicleColor.b, 0f, 1f);
		if (Game.PlayerVeh.vehicleColor.b != num)
		{
			Game.PlayerVeh.vehicleColor.b = num;
			updateVehicleAccent();
			updateVehicleColor();
		}
		GUILayout.Label("Blue", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.Space(5f);
		GUILayout.Label("Accent Color:");
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Game.PlayerVeh.vehicleAccent.r, 0f, 1f);
		if (Game.PlayerVeh.vehicleAccent.r != num)
		{
			Game.PlayerVeh.vehicleAccent.r = num;
			updateVehicleColor();
		}
		GUILayout.Label("Red", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Game.PlayerVeh.vehicleAccent.g, 0f, 1f);
		if (Game.PlayerVeh.vehicleAccent.g != num)
		{
			Game.PlayerVeh.vehicleAccent.g = num;
			updateVehicleColor();
		}
		GUILayout.Label("Green", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		num = GUILayout.HorizontalSlider(Game.PlayerVeh.vehicleAccent.b, 0f, 1f);
		if (Game.PlayerVeh.vehicleAccent.b != num)
		{
			Game.PlayerVeh.vehicleAccent.b = num;
			updateVehicleColor();
		}
		GUILayout.Label("Blue", GUILayout.Width(65f));
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
	}

	public void showDialogPlayers()
	{
		GUILayout.FlexibleSpace();
		if (Game.Controller.isHost && Game.Controller.unauthPlayers.length > 0)
		{
			GUILayout.Label("Joining Players:");
			for (int i = 0; i < Game.Controller.unauthPlayers.length; i = checked(i + 1))
			{
				GUILayout.Space(10f);
				UnityRuntimeServices.Invoke(typeof(GUILayout), "Label", new object[1] { RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "n") }, typeof(MonoBehaviour));
				GUILayout.TextArea((string)RuntimeServices.Coerce(RuntimeServices.GetProperty(RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"), "externalIP"), typeof(string)));
				GUILayout.BeginHorizontal();
				if (GUILayout.Button("Evict"))
				{
					UnityRuntimeServices.Invoke(networkView, "RPC", new object[3]
					{
						"dN",
						RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"),
						2
					}, typeof(MonoBehaviour));
					if (Network.isServer)
					{
						Network.CloseConnection((NetworkPlayer)RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"), true);
					}
					else
					{
						networkView.RPC("cC", RPCMode.Server, RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"), RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "n"), 0);
					}
				}
				else if (GUILayout.Button("Invite") && !RuntimeServices.ToBool(Game.Controller.authenticatedPlayers[RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p")]))
				{
					if (Network.isServer)
					{
						Game.Controller.authenticatedPlayers.Add(RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"), 1);
					}
					else
					{
						networkView.RPC("pI", RPCMode.Server, RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "p"), RuntimeServices.GetProperty(Game.Controller.unauthPlayers[i], "n"));
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.FlexibleSpace();
			GUILayout.Space(20f);
			GUILayout.Label("Active Players:");
		}
		GameObject[] array = null;
		Vehicle vehicle = null;
		VehicleNet vehicleNet = null;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Game.Players);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			vehicle = (Vehicle)RuntimeServices.Coerce(dictionaryEntry.Value, typeof(Vehicle));
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
			GameObject gameObject = vehicle.gameObject;
			vehicleNet = (VehicleNet)gameObject.GetComponentInChildren(typeof(VehicleNet));
			GUILayout.Space(10f);
			UnityRuntimeServices.Invoke(typeof(GUILayout), "Label", new object[1] { RuntimeServices.InvokeBinaryOperator("op_Addition", dictionaryEntry.Key, (!vehicle.isPlayer) ? string.Empty : " (Me)") }, typeof(MonoBehaviour));
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
			GUILayout.TextArea((gameObject.networkView.isMine ? string.Empty : ((!vehicleNet) ? string.Empty : (Mathf.RoundToInt(vehicleNet.ping * 1000f) + " png - " + Mathf.RoundToInt(vehicleNet.jitter * 1000f) + " jtr" + vehicle.netCode + "\n"))) + ((!vehicleNet || vehicle.networkMode != 2) ? string.Empty : (Mathf.RoundToInt(vehicleNet.calcPing) + " CalcPng - " + Mathf.RoundToInt(vehicleNet.rpcPing) + " TmstmpOfst\n")) + ((!Network.isServer) ? string.Empty : (gameObject.networkView.owner.externalIP + " " + gameObject.networkView.owner.ipAddress)) + "\n" + gameObject.networkView.viewID.ToString() + " " + vehicle.networkMode, GUILayout.Height(30f));
			GUILayout.BeginHorizontal();
			if ((Game.Controller.isHost || isAdmin) && !vehicle.isBot && !gameObject.networkView.isMine && GUILayout.Button("Evict"))
			{
				Game.Messaging.broadcast(gameObject.name + " was evicted by " + Game.Player.name);
				if (Network.isServer)
				{
					UnityRuntimeServices.Invoke(networkView, "RPC", new object[3]
					{
						"dN",
						RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"),
						2
					}, typeof(MonoBehaviour));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					UnityRuntimeServices.Invoke(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "RPC", new object[3]
					{
						"dN",
						RPCMode.All,
						2
					}, typeof(MonoBehaviour));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					Network.CloseConnection((NetworkPlayer)RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"), true);
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
				}
				else
				{
					networkView.RPC("cC", RPCMode.Server, RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"), dictionaryEntry.Key, 1);
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
				}
			}
			else if ((Game.Controller.isHost || isAdmin) && !vehicle.isBot && !gameObject.networkView.isMine && GUILayout.Button("Ban"))
			{
				Game.Messaging.broadcast(gameObject.name + " was banned by " + Game.Player.name);
				if (Network.isServer)
				{
					bannedIPs = (string)RuntimeServices.Coerce(RuntimeServices.InvokeBinaryOperator("op_Addition", bannedIPs, RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", (!(bannedIPs != string.Empty)) ? string.Empty : "\n", RuntimeServices.GetProperty(RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"), "ipAddress")), " "), gameObject.name)), typeof(string));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					UnityRuntimeServices.Invoke(networkView, "RPC", new object[3]
					{
						"dN",
						RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"),
						2
					}, typeof(MonoBehaviour));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					UnityRuntimeServices.Invoke(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "RPC", new object[3]
					{
						"dN",
						RPCMode.All,
						2
					}, typeof(MonoBehaviour));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					Network.CloseConnection((NetworkPlayer)RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"), true);
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					Game.Controller.StartCoroutine_Auto(Game.Controller.registerHost());
				}
				else
				{
					networkView.RPC("cC", RPCMode.Server, RuntimeServices.GetProperty(RuntimeServices.GetProperty(dictionaryEntry.Value, "networkView"), "owner"), dictionaryEntry.Key, 2);
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		}
		if (Game.Controller.isHost)
		{
			GUILayout.Space(20f);
			GUILayout.Label("Banned Players:");
			if (bannedIPs != string.Empty && GUILayout.Button("Unban All"))
			{
				bannedIPs = string.Empty;
				Game.Controller.StartCoroutine_Auto(Game.Controller.registerHost());
				updateServerPrefs();
			}
			string text = GUILayout.TextField(bannedIPs);
			if (text != bannedIPs)
			{
				bannedIPs = text;
				updateServerPrefs();
			}
		}
	}

	public void showDialogServer()
	{
		checked
		{
			if (Game.Controller.isHost || isAdmin)
			{
				if (GUILayout.Button(">> Default All <<"))
				{
					Game.Controller.networkView.RPC("sSS", RPCMode.All, serverDefault);
				}
				GUILayout.Space(20f);
				GUILayout.Label("Server Name:");
				string text = GUILayout.TextField(Game.Controller.serverName, 45);
				if (text != Game.Controller.serverName)
				{
					Game.Controller.serverName = text;
					updateServerPrefs();
				}
				GUILayout.Space(20f);
				GUILayout.Label("Server Features:");
				Game.Controller.serverHidden = GUILayout.Toggle(Game.Controller.serverHidden, "Hide Server from List");
				if (Game.Controller.serverHidden && Game.Controller.hostRegistered)
				{
					Game.Controller.unregisterHost();
					updateServerPrefs();
				}
				else if (!Game.Controller.serverHidden && !Game.Controller.hostRegistered)
				{
					Game.Controller.StartCoroutine_Auto(Game.Controller.registerHostSet());
					updateServerPrefs();
				}
				GUILayout.BeginHorizontal();
				GUILayout.Label("Password?");
				text = GUILayout.TextField(Game.Controller.serverPassword);
				if (text != Game.Controller.serverPassword)
				{
					Game.Controller.serverPassword = text;
					updateServerPrefs();
				}
				GUILayout.EndHorizontal();
				GUILayout.Label("Welcome Message:");
				text = GUILayout.TextField(serverWelcome);
				if (text != serverWelcome)
				{
					serverWelcome = text;
					updateServerPrefs();
				}
				GUILayout.Space(20f);
				GUILayout.Label("Game Features:");
				if (GUILayout.Toggle(minimapAllowed, "Minimap enabled") != minimapAllowed)
				{
					minimapAllowed = !minimapAllowed;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(hideNames, "Camouflage Badges") != hideNames)
				{
					hideNames = !hideNames;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(ramoSpheres != 0f, "RORBs Enabled") != (ramoSpheres != 0f))
				{
					ramoSpheres = ((ramoSpheres == 0f) ? 0.5f : 0f);
					if (ramoSpheres != 0f)
					{
						zorbSpeed = 7f;
					}
					updateServerPrefs();
				}
				float num;
				if (ramoSpheres != 0f)
				{
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(ramoSpheres, 0.001f, 1f);
					if (ramoSpheres != num)
					{
						ramoSpheres = num;
						updateServerPrefs();
					}
					GUILayout.Label("Size", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (GUILayout.Toggle(zorbSpeed != 0f, "XORBs Available") != (zorbSpeed != 0f))
					{
						zorbSpeed = ((zorbSpeed == 0f) ? 7 : 0);
						updateServerPrefs();
					}
					if (zorbSpeed != 0f)
					{
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(zorbSpeed, 0.001f, 14f);
						if (zorbSpeed != num)
						{
							zorbSpeed = num;
							updateServerPrefs();
						}
						GUILayout.Label("X Speed", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(zorbAgility, -7f, 7f);
						if (zorbAgility != num)
						{
							zorbAgility = num;
							updateServerPrefs();
						}
						GUILayout.Label("X Agility", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(zorbBounce, 0f, 1f);
						if (zorbBounce != num)
						{
							zorbBounce = num;
							updateServerPrefs();
						}
						GUILayout.Label("X Bounce", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
					GUILayout.Space(10f);
				}
				if (GUILayout.Toggle(lasersAllowed, "Lasers enabled") != lasersAllowed)
				{
					lasersAllowed = !lasersAllowed;
					updateServerPrefs();
				}
				if (lasersAllowed)
				{
					if (ramoSpheres != 0f && GUILayout.Toggle(lasersOptHit, "L Hit ORBs") != lasersOptHit)
					{
						lasersOptHit = !lasersOptHit;
						updateServerPrefs();
					}
					if (GUILayout.Toggle(lasersFatal, "L Hits Rematerialize") != lasersFatal)
					{
						lasersFatal = !lasersFatal;
						updateServerPrefs();
					}
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(laserSpeed, 20f, 340f);
					if ((float)laserSpeed != num)
					{
						laserSpeed = (int)num;
						updateServerPrefs();
					}
					GUILayout.Label("Lsr Spd", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(laserGrav, 0f, 1f);
					if (laserGrav != num)
					{
						laserGrav = num;
						updateServerPrefs();
					}
					GUILayout.Label("Lsr Gvt", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(laserRico, 0f, 1f);
					if (laserRico != num)
					{
						laserRico = num;
						updateServerPrefs();
					}
					GUILayout.Label("Lsr Rco", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.Space(10f);
				}
				GUILayout.BeginHorizontal();
				num = GUILayout.HorizontalSlider(worldGrav, 0.81f * -1f, 18.81f * -1f);
				if (worldGrav != num)
				{
					worldGrav = num;
					updateServerPrefs();
				}
				GUILayout.Label("Gravity", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				num = GUILayout.HorizontalSlider(worldViewDist, 300f, 9700f);
				if (worldViewDist != num)
				{
					worldViewDist = num;
					updateServerPrefs();
				}
				GUILayout.Label("Visibility", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
				if ((bool)World.sea)
				{
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(lavaFog, 0.015f, 0.01f * -1f);
					if (lavaFog != num)
					{
						lavaFog = num;
						updateServerPrefs();
					}
					GUILayout.Label("Sea Vis", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(lavaAlt, -100f, 100f);
					if (lavaAlt != num)
					{
						lavaAlt = num;
						updateServerPrefs();
					}
					GUILayout.Label("Sea Alt", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
				}
				GUILayout.Space(20f);
				GUILayout.Label("Bots:");
				if (GUILayout.Toggle(botsCanFire, "Can Fire") != botsCanFire)
				{
					botsCanFire = !botsCanFire;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(botsCanDrive, "Can Drive") != botsCanDrive)
				{
					botsCanDrive = !botsCanDrive;
					updateServerPrefs();
				}
				GUILayout.Space(10f);
				GUILayout.BeginHorizontal();
				if (Game.Controller.botsInGame < 10 && GUILayout.Button("Add Bot"))
				{
					Game.Controller.StartCoroutine_Auto(Game.Controller.addBot());
				}
				if (Game.Controller.botsInGame > 0)
				{
					if (Game.Controller.botsInGame != 10)
					{
						GUILayout.Space(5f);
					}
					if (GUILayout.Button("Axe Bot"))
					{
						Game.Controller.StartCoroutine_Auto(Game.Controller.axeBot());
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(20f);
				GUILayout.Label("Buggy:");
				if (GUILayout.Toggle(buggyAllowed, "Available") != buggyAllowed)
				{
					buggyAllowed = !buggyAllowed;
					updateServerPrefs();
				}
				if (buggyAllowed)
				{
					if (GUILayout.Toggle(buggyFlightSlip, "Stall Blending") != buggyFlightSlip)
					{
						buggyFlightSlip = !buggyFlightSlip;
						updateServerPrefs();
					}
					if (GUILayout.Toggle(buggyFlightLooPower, "Powered Loops") != buggyFlightLooPower)
					{
						buggyFlightLooPower = !buggyFlightLooPower;
						updateServerPrefs();
					}
					if (GUILayout.Toggle(buggySmartSuspension, "Smart Suspension") != buggySmartSuspension)
					{
						buggySmartSuspension = !buggySmartSuspension;
						updateServerPrefs();
					}
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggyFlightDrag, 50f, 550f);
					if (buggyFlightDrag != num)
					{
						buggyFlightDrag = num;
						updateServerPrefs();
					}
					GUILayout.Label("Fl Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggyFlightAgility, 0.5f, 1.5f);
					if (buggyFlightAgility != num)
					{
						buggyFlightAgility = num;
						updateServerPrefs();
					}
					GUILayout.Label("Fl Agility", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggyCG, 0.1f * -1f, 0.7f * -1f);
					if (buggyCG != num)
					{
						buggyCG = num;
						updateServerPrefs();
					}
					GUILayout.Label("Stability", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggyPower, 0.5f, 1.5f);
					if (buggyPower != num)
					{
						buggyPower = num;
						updateServerPrefs();
					}
					GUILayout.Label("Power", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggySpeed, 5f, 55f);
					if (buggySpeed != num)
					{
						buggySpeed = num;
						updateServerPrefs();
					}
					GUILayout.Label("Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggyTr, 0.1f, 1.9f);
					if (buggyTr != num)
					{
						buggyTr = num;
						updateServerPrefs();
					}
					GUILayout.Label("Traction", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(buggySh, 20f, 120f);
					if (buggySh != num)
					{
						buggySh = num;
						updateServerPrefs();
					}
					GUILayout.Label("Shocks", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(firepower[0], 0f, 3f);
						if ((float)firepower[0] != num)
						{
							firepower[0] = (int)num;
							updateServerPrefs();
						}
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(laserLock[0], 0f, 1f);
						if (laserLock[0] != num)
						{
							laserLock[0] = num;
							updateServerPrefs();
						}
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Tank:");
				if (GUILayout.Toggle(tankAllowed, "Available") != tankAllowed)
				{
					tankAllowed = !tankAllowed;
					updateServerPrefs();
				}
				if (tankAllowed)
				{
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(tankCG, 1f, 1.4f * -1f);
					if (tankCG != num)
					{
						tankCG = num;
						updateServerPrefs();
					}
					GUILayout.Label("Stability", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(tankGrip, 0f, 0.2f);
					if (tankGrip != num)
					{
						tankGrip = num;
						updateServerPrefs();
					}
					GUILayout.Label("Grip", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(tankSpeed, 10f, 40f);
					if (tankSpeed != num)
					{
						tankSpeed = num;
						updateServerPrefs();
					}
					GUILayout.Label("Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(tankPower, 500f, 3500f);
					if (tankPower != num)
					{
						tankPower = num;
						updateServerPrefs();
					}
					GUILayout.Label("Power", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(firepower[2], 0f, 3f);
						if ((float)firepower[2] != num)
						{
							firepower[2] = (int)num;
							updateServerPrefs();
						}
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(laserLock[2], 0f, 1f);
						if (laserLock[2] != num)
						{
							laserLock[2] = num;
							updateServerPrefs();
						}
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Hovercraft:");
				if (GUILayout.Toggle(hoverAllowed, "Available") != hoverAllowed)
				{
					hoverAllowed = !hoverAllowed;
					updateServerPrefs();
				}
				if (hoverAllowed)
				{
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(hoverHeight, 5f, 25f);
					if (hoverHeight != num)
					{
						hoverHeight = num;
						updateServerPrefs();
					}
					GUILayout.Label("Height", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(hoverHover, 20f, 180f);
					if (hoverHover != num)
					{
						hoverHover = num;
						updateServerPrefs();
					}
					GUILayout.Label("Hover", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(hoverRepel, 0.5f, 4.5f);
					if (hoverRepel != num)
					{
						hoverRepel = num;
						updateServerPrefs();
					}
					GUILayout.Label("Repulsion", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(hoverThrust, 20f, 420f);
					if (hoverThrust != num)
					{
						hoverThrust = num;
						updateServerPrefs();
					}
					GUILayout.Label("Thrust", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(firepower[1], 0f, 3f);
						if ((float)firepower[1] != num)
						{
							firepower[1] = (int)num;
							updateServerPrefs();
						}
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(laserLock[1], 0f, 1f);
						if (laserLock[1] != num)
						{
							laserLock[1] = num;
							updateServerPrefs();
						}
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Jet:");
				if (GUILayout.Toggle(jetAllowed, "Available") != jetAllowed)
				{
					jetAllowed = !jetAllowed;
					updateServerPrefs();
				}
				if (jetAllowed)
				{
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(jetHDrag, 0.005f, 0.015f);
					if (jetHDrag != num)
					{
						jetHDrag = num;
						updateServerPrefs();
					}
					GUILayout.Label("HoverDrag", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(jetDrag, 0.0005f, 0.0015f);
					if (jetDrag != num)
					{
						jetDrag = num;
						updateServerPrefs();
					}
					GUILayout.Label("Drag", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(jetSteer, 5f, 35f);
					if ((float)jetSteer != num)
					{
						jetSteer = (int)num;
						updateServerPrefs();
					}
					GUILayout.Label("Agility", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(jetLift, 0.1f, 0.9f);
					if (jetLift != num)
					{
						jetLift = num;
						updateServerPrefs();
					}
					GUILayout.Label("Lift", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					num = GUILayout.HorizontalSlider(jetStall, 1f, 39f);
					if ((float)jetStall != num)
					{
						jetStall = (int)num;
						updateServerPrefs();
					}
					GUILayout.Label("Stall", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(firepower[3], 0f, 3f);
						if ((float)firepower[3] != num)
						{
							firepower[3] = (int)num;
							updateServerPrefs();
						}
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						num = GUILayout.HorizontalSlider(laserLock[3], 0f, 1f);
						if (laserLock[3] != num)
						{
							laserLock[3] = num;
							updateServerPrefs();
						}
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Network Mode:");
				if (GUILayout.Toggle(networkMode == 0, "UDP") != (networkMode == 0))
				{
					networkMode = 0;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(networkMode == 1, "RDC") != (networkMode == 1))
				{
					networkMode = 1;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(networkMode == 2, "RPC") != (networkMode == 2))
				{
					networkMode = 2;
					updateServerPrefs();
				}
				if (networkMode == 0)
				{
					GUILayout.Label("\"UDP\" is the fastest mode, but may result in players with \"No Connection\"");
				}
				else if (networkMode == 1)
				{
					GUILayout.Label("\"RDC\" sacrifices speed for reliability");
				}
				else
				{
					GUILayout.Label("\"RPC\" guarantees reliability at the expense of speed");
				}
				GUILayout.Space(20f);
				GUILayout.Label("Network Physics:");
				if (GUILayout.Toggle(networkPhysics == 0, "Advanced") != (networkPhysics == 0))
				{
					networkPhysics = 0;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(networkPhysics == 1, "Enhanced") != (networkPhysics == 1))
				{
					networkPhysics = 1;
					updateServerPrefs();
				}
				if (GUILayout.Toggle(networkPhysics == 2, "Simplified") != (networkPhysics == 2))
				{
					networkPhysics = 2;
					updateServerPrefs();
				}
				if (networkPhysics == 0)
				{
					GUILayout.Label("\"Advanced\" is optimized for smooth movement and realistic collisions over the internet");
				}
				else if (networkPhysics == 1)
				{
					GUILayout.Label("\"Enhanced\" provides maximum movement precision at the cost of higher processor and network load");
				}
				else
				{
					GUILayout.Label("\"Simplified\" provides smooth movement and maximum framerates in games which don't need highly accurate vehicle collisions");
				}
				GUILayout.Space(20f);
				GUILayout.Label("Network Interpolation:");
				GUILayout.BeginHorizontal();
				num = GUILayout.HorizontalSlider(networkInterpolation, 0f, 0.5f);
				if (networkInterpolation != num)
				{
					networkInterpolation = num;
					updateServerPrefs();
				}
				GUILayout.Label((!(networkInterpolation < 0.01f)) ? (networkInterpolation * 1000f + " ms") : "Auto", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
			}
			else
			{
				GUILayout.Space(20f);
				GUILayout.Label("(NOTE: all these parameters are adjustable only by the server host. You can't change anything in this window)");
				GUILayout.Space(60f);
				GUILayout.Toggle(minimapAllowed, "Minimap enabled");
				GUILayout.Toggle(hideNames, "Camouflage Badges");
				GUILayout.Toggle(ramoSpheres != 0f, "RORBs Enabled");
				if (ramoSpheres != 0f)
				{
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(ramoSpheres, 0.001f, 1f);
					GUILayout.Label("Size", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.Toggle(zorbSpeed != 0f, "XORBs Available");
					if (zorbSpeed != 0f)
					{
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(zorbSpeed, 0.001f, 14f);
						GUILayout.Label("X Speed", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(zorbAgility, -7f, 7f);
						GUILayout.Label("X Agility", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(zorbBounce, 0f, 1f);
						GUILayout.Label("X Bounce", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
					GUILayout.Space(10f);
				}
				GUILayout.Toggle(lasersAllowed, "Lasers enabled");
				if (lasersAllowed)
				{
					if (ramoSpheres != 0f)
					{
						GUILayout.Toggle(lasersOptHit, "L Hit ORBs");
					}
					GUILayout.Toggle(lasersFatal, "L Hits Rematerialize");
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(laserSpeed, 20f, 340f);
					GUILayout.Label("Lsr Spd", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(laserGrav, 0f, 1f);
					GUILayout.Label("Lsr Gvt", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(laserRico, 0f, 1f);
					GUILayout.Label("Lsr Rco", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.Space(10f);
				}
				GUILayout.BeginHorizontal();
				GUILayout.HorizontalSlider(worldGrav, 0.81f * -1f, 18.81f * -1f);
				GUILayout.Label("Gravity", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.HorizontalSlider(worldViewDist, 500f, 9500f);
				GUILayout.Label("Visibility", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
				if ((bool)World.sea)
				{
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(lavaFog, 0.015f, 0.01f * -1f);
					GUILayout.Label("Lava Fog", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(lavaAlt, -100f, 100f);
					GUILayout.Label("Lava Alt", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
				}
				GUILayout.Space(20f);
				GUILayout.Label("Bots:");
				GUILayout.Toggle(botsCanFire, "Can Fire");
				GUILayout.Toggle(botsCanDrive, "Can Drive");
				GUILayout.Space(20f);
				GUILayout.Label("Buggy:");
				GUILayout.Toggle(buggyAllowed, "Available");
				if (buggyAllowed)
				{
					GUILayout.Toggle(buggyFlightSlip, "Stall Blending");
					GUILayout.Toggle(buggyFlightLooPower, "Powered Loops");
					GUILayout.Toggle(buggySmartSuspension, "Smart Suspension");
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggyFlightDrag, 50f, 550f);
					GUILayout.Label("Fl Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggyFlightAgility, 0.5f, 1.5f);
					GUILayout.Label("Fl Agility", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggyCG, 0.1f * -1f, 0.7f * -1f);
					GUILayout.Label("Stability", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggyPower, 0.5f, 1.5f);
					GUILayout.Label("Power", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggySpeed, 5f, 55f);
					GUILayout.Label("Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggyTr, 0.1f, 1.9f);
					GUILayout.Label("Traction", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(buggySh, 20f, 120f);
					GUILayout.Label("Shocks", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(firepower[0], 0f, 3f);
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(laserLock[0], 0f, 1f);
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Tank:");
				GUILayout.Toggle(tankAllowed, "Available");
				if (tankAllowed)
				{
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(tankCG, 1f, 1.4f * -1f);
					GUILayout.Label("Stability", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(tankGrip, 0f, 0.2f);
					GUILayout.Label("Grip", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(tankSpeed, 10f, 40f);
					GUILayout.Label("Speed", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(tankPower, 500f, 3500f);
					GUILayout.Label("Power", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(firepower[2], 0f, 3f);
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(laserLock[2], 0f, 1f);
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Hovercraft:");
				GUILayout.Toggle(hoverAllowed, "Available");
				if (hoverAllowed)
				{
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(hoverHeight, 5f, 25f);
					GUILayout.Label("Height", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(hoverHover, 20f, 180f);
					GUILayout.Label("Hover", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(hoverRepel, 0.5f, 4.5f);
					GUILayout.Label("Repulsion", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(hoverThrust, 20f, 420f);
					GUILayout.Label("Thrust", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(firepower[1], 0f, 3f);
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(laserLock[1], 0f, 1f);
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Jet:");
				GUILayout.Toggle(jetAllowed, "Available");
				if (jetAllowed)
				{
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(jetHDrag, 0.005f, 0.015f);
					GUILayout.Label("HoverDrag", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(jetDrag, 0.0005f, 0.0015f);
					GUILayout.Label("Drag", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(jetSteer, 5f, 35f);
					GUILayout.Label("Agility", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(jetLift, 0.1f, 0.9f);
					GUILayout.Label("Lift", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.HorizontalSlider(jetStall, 1f, 39f);
					GUILayout.Label("Stall", GUILayout.Width(65f));
					GUILayout.EndHorizontal();
					if (lasersAllowed)
					{
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(firepower[3], 0f, 3f);
						GUILayout.Label("Firepower", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.HorizontalSlider(laserLock[3], 0f, 1f);
						GUILayout.Label("Lsr Lck", GUILayout.Width(65f));
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.Space(20f);
				GUILayout.Label("Networking:");
				GUILayout.Toggle(networkMode == 0, "UDP");
				GUILayout.Toggle(networkMode == 1, "RDC");
				GUILayout.Toggle(networkMode == 2, "RPC");
				GUILayout.Toggle(networkPhysics == 0, "Advanced");
				GUILayout.Toggle(networkPhysics == 1, "Enhanced");
				GUILayout.Toggle(networkPhysics == 2, "Simplified");
				GUILayout.BeginHorizontal();
				GUILayout.HorizontalSlider(networkInterpolation, 0f, 0.5f);
				GUILayout.Label((!(networkInterpolation < 0.01f)) ? (networkInterpolation * 1000f + " ms") : "Auto", GUILayout.Width(65f));
				GUILayout.EndHorizontal();
			}
			GUILayout.Label("nTime: " + Mathf.RoundToInt((float)Network.time));
			GUILayout.Space(20f);
			GUILayout.Label("Settings I/O:");
			serverString = GUILayout.TextField(serverString);
			if (Game.Controller.isHost && GUILayout.Button("Apply Custom\nSettings"))
			{
				Game.Controller.networkView.RPC("sSS", RPCMode.All, serverString);
			}
		}
	}

	public void updatePrefs()
	{
		renderLevel = PlayerPrefs.GetInt("renderLevel", 4);
		laserLocking = false;
		checked
		{
			for (int i = 0; i < Extensions.get_length((System.Array)laserLock); i++)
			{
				float[] array = laserLock;
				if (array[RuntimeServices.NormalizeArrayIndex(array, i)] > 0f)
				{
					laserLocking = true;
					break;
				}
			}
			if (renderLevel > 4)
			{
				World.lodDist = 1000;
			}
			else if (renderLevel > 3)
			{
				World.lodDist = 400;
			}
			else if (renderLevel > 2)
			{
				World.lodDist = 75;
			}
			else
			{
				World.lodDist = 0;
			}
			if (renderLevel > 4)
			{
				camContrast.enabled = true;
				if (renderLevel > 5)
				{
					camSSAO.enabled = true;
					camSSAO.m_Downsampling = 2;
				}
				else
				{
					camSSAO.enabled = false;
				}
			}
			else
			{
				camSSAO.enabled = false;
				camContrast.enabled = false;
			}
			if (Game.Settings.networkPhysics == 2)
			{
				Network.sendRate = 10f;
			}
			else
			{
				Network.sendRate = 15f;
			}
			if (ramoSpheres == 0f)
			{
				zorbSpeed = 0f;
			}
			if (useMusic == 0)
			{
				gameMusic.enabled = false;
				if (gameMusic.isPlaying)
				{
					gameMusic.Stop();
				}
			}
			else
			{
				gameMusic.enabled = true;
				gameMusic.pitch = 1f;
				AudioSource audioSource = gameMusic;
				AudioClip[] array2 = musicTracks;
				audioSource.clip = array2[RuntimeServices.NormalizeArrayIndex(array2, useMusic - 1)];
				if (!gameMusic.isPlaying)
				{
					gameMusic.Play();
				}
			}
			GameObject.Find("MiniMap").camera.enabled = minimapAllowed && useMinimap;
		}
		QualitySettings.currentLevel = (QualityLevel)checked(renderLevel - 1);
		Time.fixedDeltaTime = ((renderLevel <= 3) ? 0.025f : 0.02f);
		Camera.main.farClipPlane = ((renderViewCap != 1000f) ? Mathf.Min(renderViewCap, worldViewDist) : worldViewDist);
		worldFog = Mathf.Lerp(0.007f, 0.0003f, Camera.main.farClipPlane / 6000f);
		if (World.terrains != null)
		{
			int j = 0;
			Terrain[] terrains = World.terrains;
			for (int length = terrains.Length; j < length; j = checked(j + 1))
			{
				terrains[j].treeCrossFadeLength = 30f;
				if (renderLevel > 4)
				{
					terrains[j].detailObjectDistance = 300f;
					terrains[j].treeDistance = 600f;
					terrains[j].treeMaximumFullLODCount = 100;
					terrains[j].treeBillboardDistance = 150f;
				}
				else if (renderLevel > 3)
				{
					terrains[j].detailObjectDistance = 200f;
					terrains[j].treeDistance = 500f;
					terrains[j].treeMaximumFullLODCount = 50;
					terrains[j].treeBillboardDistance = 100f;
				}
				else if (renderLevel > 2)
				{
					terrains[j].detailObjectDistance = 150f;
					terrains[j].treeDistance = 300f;
					terrains[j].treeMaximumFullLODCount = 10;
					terrains[j].treeBillboardDistance = 75f;
				}
				else
				{
					terrains[j].detailObjectDistance = 0f;
					terrains[j].treeDistance = 0f;
					terrains[j].treeMaximumFullLODCount = 0;
					terrains[j].treeBillboardDistance = 0f;
				}
				terrains[j].basemapDistance = 1500f;
				if (renderLevel > 5)
				{
					terrains[j].heightmapMaximumLOD = 0;
					terrains[j].heightmapPixelError = 5f;
				}
				else if (renderLevel > 2)
				{
					terrains[j].heightmapMaximumLOD = 0;
					terrains[j].heightmapPixelError = 15f;
				}
				else if (renderLevel > 1)
				{
					terrains[j].heightmapMaximumLOD = 0;
					terrains[j].heightmapPixelError = 50f;
				}
				else
				{
					terrains[j].heightmapMaximumLOD = 1;
					terrains[j].heightmapPixelError = 50f;
				}
				if (renderLevel > 4)
				{
					terrains[j].lighting = TerrainLighting.Pixel;
				}
				else
				{
					terrains[j].lighting = TerrainLighting.Lightmap;
				}
			}
		}
		Physics.gravity = new Vector3(0f, worldGrav, 0f);
		if ((bool)World.sea)
		{
			float y = lavaAlt;
			Vector3 position = World.sea.position;
			float num = (position.y = y);
			Vector3 vector = (World.sea.position = position);
		}
		zorbPhysics.bouncyness = zorbBounce;
		updateObjects();
	}

	public void updateServerPrefs()
	{
		serverUpdateTime = Time.time + 3f;
		updatePrefs();
	}

	public IEnumerator ramdomizeVehicleColor()
	{
		return new ramdomizeVehicleColor_002470(this).GetEnumerator();
	}

	public void updateVehicleAccent()
	{
		float num = 0.25f * -1f;
		Game.PlayerVeh.vehicleAccent.r = Mathf.Max(0f, Game.PlayerVeh.vehicleColor.r + num);
		Game.PlayerVeh.vehicleAccent.g = Mathf.Max(0f, Game.PlayerVeh.vehicleColor.g + num);
		Game.PlayerVeh.vehicleAccent.b = Mathf.Max(0f, Game.PlayerVeh.vehicleColor.b + num);
	}

	public void updateVehicleColor()
	{
		colorUpdateTime = Time.time + 3f;
		colorCustom = true;
		Game.PlayerVeh.setColor();
	}

	public void saveVehicleColor()
	{
		PlayerPrefs.SetInt("vehColCustom", colorCustom ? 1 : 0);
		PlayerPrefs.SetFloat("vehColR", Game.PlayerVeh.vehicleColor.r);
		PlayerPrefs.SetFloat("vehColG", Game.PlayerVeh.vehicleColor.g);
		PlayerPrefs.SetFloat("vehColB", Game.PlayerVeh.vehicleColor.b);
		PlayerPrefs.SetFloat("vehColAccR", Game.PlayerVeh.vehicleAccent.r);
		PlayerPrefs.SetFloat("vehColAccG", Game.PlayerVeh.vehicleAccent.g);
		PlayerPrefs.SetFloat("vehColAccB", Game.PlayerVeh.vehicleAccent.b);
		Game.Player.networkView.RPC("sC", RPCMode.Others, Game.PlayerVeh.vehicleColor.r, Game.PlayerVeh.vehicleColor.g, Game.PlayerVeh.vehicleColor.b, Game.PlayerVeh.vehicleAccent.r, Game.PlayerVeh.vehicleAccent.g, Game.PlayerVeh.vehicleAccent.b);
	}

	public string packServerPrefs()
	{
		return "lasr:" + (lasersAllowed ? 1 : 0) + ";" + "lsrh:" + (lasersFatal ? 1 : 0) + ";" + "lsro:" + (lasersOptHit ? 1 : 0) + ";" + "mmap:" + (minimapAllowed ? 1 : 0) + ";" + "camo:" + (hideNames ? 1 : 0) + ";" + "rorb:" + ramoSpheres + ";" + "xspd:" + zorbSpeed + ";" + "xagt:" + zorbAgility + ";" + "xbnc:" + zorbBounce + ";" + "grav:" + worldGrav * -1f + ";" + "wvis:" + worldViewDist + ";" + "lfog:" + lavaFog + ";" + "lalt:" + lavaAlt + ";" + "lspd:" + laserSpeed + ";" + "lgvt:" + laserGrav + ";" + "lrco:" + laserRico + ";" + "botfire:" + (botsCanFire ? 1 : 0) + ";" + "botdrive:" + (botsCanDrive ? 1 : 0) + ";" + "bugen:" + (buggyAllowed ? 1 : 0) + ";" + "bugflsl:" + (buggyFlightSlip ? 1 : 0) + ";" + "bugflpw:" + (buggyFlightLooPower ? 1 : 0) + ";" + "bugawd:" + (buggyAWD ? 1 : 0) + ";" + "bugspn:" + (buggySmartSuspension ? 1 : 0) + ";" + "bugfldr:" + buggyFlightDrag + ";" + "bugflag:" + buggyFlightAgility + ";" + "bugcg:" + buggyCG + ";" + "bugpow:" + buggyPower + ";" + "bugspd:" + buggySpeed + ";" + "bugtr:" + buggyTr + ";" + "bugsl:" + buggySl + ";" + "bugsh:" + buggySh + ";" + "bugfp:" + firepower[0] + ";" + "bugll:" + laserLock[0] + ";" + "tnken:" + (tankAllowed ? 1 : 0) + ";" + "tnkpow:" + tankPower + ";" + "tnkgrp:" + tankGrip + ";" + "tnkspd:" + tankSpeed + ";" + "tnkcg:" + tankCG + ";" + "tnkfp:" + firepower[2] + ";" + "tnkll:" + laserLock[2] + ";" + "hvren:" + (hoverAllowed ? 1 : 0) + ";" + "hvrhe:" + hoverHeight + ";" + "hvrhv:" + hoverHover + ";" + "hvrrp:" + hoverRepel + ";" + "hvrth:" + hoverThrust + ";" + "hvrfp:" + firepower[1] + ";" + "hvrll:" + laserLock[1] + ";" + "jeten:" + (jetAllowed ? 1 : 0) + ";" + "jethd:" + jetHDrag + ";" + "jetd:" + jetDrag + ";" + "jets:" + jetSteer + ";" + "jetl:" + jetLift + ";" + "jetss:" + jetStall + ";" + "jetfp:" + firepower[3] + ";" + "jetll:" + laserLock[3] + ";" + "netm:" + networkMode + ";" + "netph:" + networkPhysics + ";" + "netin:" + networkInterpolation + ";" + string.Empty;
	}

	public void updateObjects()
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(ParticleEmitter)));
		while (enumerator.MoveNext())
		{
			ParticleEmitter particleEmitter = (ParticleEmitter)RuntimeServices.Coerce(enumerator.Current, typeof(ParticleEmitter));
			particleEmitter.emit = ((renderLevel >= 3) ? true : false);
			UnityRuntimeServices.Update(enumerator, particleEmitter);
		}
		IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(Light)));
		while (enumerator2.MoveNext())
		{
			Light light = (Light)RuntimeServices.Coerce(enumerator2.Current, typeof(Light));
			if (!(light.name != "VehicleLight"))
			{
				light.enabled = false;
				UnityRuntimeServices.Update(enumerator2, light);
			}
		}
		IEnumerator enumerator3 = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(GameObject)));
		while (enumerator3.MoveNext())
		{
			GameObject gameObject = (GameObject)RuntimeServices.Coerce(enumerator3.Current, typeof(GameObject));
			gameObject.SendMessage("OnPrefsUpdated", SendMessageOptions.DontRequireReceiver);
			UnityRuntimeServices.Update(enumerator3, gameObject);
		}
	}

	public void toggleFullscreen()
	{
		Resolution[] resolutions = Screen.resolutions;
		checked
		{
			Resolution resolution = resolutions[RuntimeServices.NormalizeArrayIndex(resolutions, Extensions.get_length((System.Array)Screen.resolutions) - 1)];
			if (!Screen.fullScreen)
			{
				Screen.SetResolution(resolution.width, resolution.height, true);
				if (Application.platform == RuntimePlatform.OSXDashboardPlayer)
				{
					enteredfullscreen = true;
				}
				return;
			}
			if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.OSXDashboardPlayer)
			{
				Screen.fullScreen = false;
			}
			else
			{
				Resolution[] resolutions2 = Screen.resolutions;
				resolution = resolutions2[RuntimeServices.NormalizeArrayIndex(resolutions2, Extensions.get_length((System.Array)Screen.resolutions) - 2)];
				Screen.SetResolution(resolution.width, resolution.height, false);
			}
			if (enteredfullscreen)
			{
				enteredfullscreen = false;
			}
		}
	}

	public void Main()
	{
	}
}
