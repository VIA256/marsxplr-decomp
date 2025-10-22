using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GUIPanel
{
    public string name;
    public bool active = true;
    public bool open;
    public bool important;
    public int minHeight = 300;
    public int maxHeight;
    public float curHeight;
    public float desHeight;
    public Vector2 scrollPos;
    public float openTime;
}

[Serializable]
public class unauthPlayer
{
    public NetworkPlayer p;
    public string n;
    public float t;
    public unauthPlayer(NetworkPlayer p, string n, float t)
    {
        this.p = p;
        this.n = n;
        this.t = t;
    }
}

[Serializable]
public class Game : MonoBehaviour
{
	public GUISkin GameSkin;
	public static GUISkin Skin;
	public GUIStyle hudTextStyle;
	public GUIPanel[] GUIPanels;
	private float closePanel;
	public GameObject[] GameVehicles;
	public GameWorldDesc[] GameWorlds;
	private WhirldIn whirldIn = new WhirldIn();
	[NonSerialized]
	public bool WorldIsCustom = false;
	[NonSerialized]
	public GameWorldDesc WorldDesc = new GameWorldDesc();
	public GameObject WorldEntryEffect;
	public GameObject objectVehicleObj;
	public static GameObject objectVehicle;
	public GameObject objectMarkerObj;
	public static GameObject objectMarker;
	public GameObject objectMarkerQuarryObj;
	public static GameObject objectMarkerQuarry;
	public GameObject objectMarkerMeObj;
	public static GameObject objectMarkerMe;
	public GameObject objectRocketObj;
	public static GameObject objectRocket;
	public GameObject objectRocketSnipeObj;
	public static GameObject objectRocketSnipe;

	public bool isHost = false;
	public bool isRegistered = false;

	public Texture2D cursor;
	public Texture2D cursorLook;
	public Vector2 cursorOffset;
	[NonSerialized]
	public float kpDur = 0.1f;
	[NonSerialized]
	public float kpTime;

	private int hostPanelTab = 0;
	private int windowVehicleHeight = 0;
	private Vector2 scrollPosition;
	private bool killServer = false;
	private int netKillMode = 0;
	private int GameVehicleID;
	[NonSerialized]
	public float quarryDist;
	[NonSerialized]
	public int botsInGame = 0;
	public static float GUIAlpha = 0f;
	private int GuiAnimate = -1;
	public static GameObject Player;
	public static Vehicle PlayerVeh;
	public static Vehicle QuarryVeh;
	public static CameraVehicle CameraVehicle;
	public static Messaging Messaging;
	public static Settings Settings;
	public static Game Controller;
    //[NonSerialized]
	public bool worldLoaded = false;
    //[NonSerialized]
	public bool loadingWorld = true;
	[NonSerialized]
	public float worldLoadTime;
	[NonSerialized]
	public bool hostRegistered = false;
	[NonSerialized]
	public string serverName = "";
	[NonSerialized]
	public string serverPassword = "";
	private float authTime;
	private float authUpdateTime = 2f;
	[NonSerialized]
	public bool serverHidden = false;
	[NonSerialized]
	public Hashtable authenticatedPlayers = new Hashtable();
	private ArrayList authenticatingPlayers = new ArrayList();
	[NonSerialized]
	public static Hashtable Players;
	public List<unauthPlayer> unauthPlayers = new List<unauthPlayer>();
	
    public Color vehicleIsItColor;
	public Color vehicleIsItAccent;

    //FPS Counter
    private float fpsTime;  // FPS accumulated over the interval
    private int fpsFrames;  // Frames drawn over the interval
	private int heavyTickRate = 2;
	[HideInInspector]
	public float fps;

	[HideInInspector]
	public void Start()
    {
		//Network.minimumAllocatableViewIDs = 3;
		if (Network.peerType == NetworkPeerType.Disconnected)
        {
			//We are running in the editor, and have started the game in world load mode
			Network.InitializeServer(9, 2500);
		}
		if (Network.isServer)
        {
			isHost = true;
		}
		
		objectVehicle = objectVehicleObj;
		objectMarker = objectMarkerObj;
		objectMarkerQuarry = objectMarkerQuarryObj;
		objectMarkerMe = objectMarkerMeObj;
		objectRocket = objectRocketObj;
		objectRocketSnipe = objectRocketSnipeObj;
		Skin = GameSkin;
		CameraVehicle = (CameraVehicle)Camera.main.GetComponent<CameraVehicle>();
		Settings = (Settings)gameObject.GetComponent<Settings>();
		Messaging = (Messaging)gameObject.GetComponent<Messaging>();
		Controller = (Game)gameObject.GetComponent<Game>();
		if (GameData.userName == "") GameData.userName = "MarsRacer";
		Settings.bannedIPs = GameData.masterBlacklist;
		Settings.networkMode = GameData.networkMode;
		Players = new Hashtable();
		
		botsInGame = 0;
		worldLoaded = false;
		
		if (GameData.gameWorlds == null) GameData.gameWorlds = GameWorlds;
		//whirldIn = new WhirldIn();
		//whirldIn.worldTerrainTextures = worldTerrainTextures;
		StartCoroutine_Auto(Init());
	}

	public void OnDisable()
    {
		//Unload whatever AssetBundles we were using
		if (whirldIn != null) whirldIn.Cleanup();
	}

	public IEnumerator Init()
    {
        //Wait for Server to send us players via RPC - Probably totally unnecessary
        yield return null;
        //We are now ready to recieve RPC calls!
        Network.isMessageQueueRunning = true;

        //Allow World to Load
        while (worldLoaded == false) yield return null;
        Settings.fogColor = RenderSettings.fogColor;
        GuiAnimate = 1;
        while (GuiAnimate != 0) yield return null;
        yield return new WaitForSeconds(0.25f);
        GuiAnimate = -1;
        loadingWorld = false;
        worldLoadTime = Time.time;
        GameObject worldOBJ = GameObject.Find(whirldIn.worldName);
        Transform[] objs = worldOBJ.GetComponentsInChildren<Transform>();
        if (
            (whirldIn.worldParams["ccc"] != null && (int)whirldIn.worldParams["ccc"] != 1) &&
            objs.Length > 15)
        {
            worldOBJ.AddComponent<CombineChildren>();
        }
        //Give the combine children script a chance to create the optimized world meshes before we start assigning layers to everything...
        yield return null;
        
        //Ensure standard world components are present
        if (GameObject.Find("Base"))
        {
            World.baseTF = GameObject.Find("Base").transform;
        }
        else
        {
            World.baseTF = ((GameObject)GameObject.Instantiate(
                Resources.Load("Base"),
                Vector3.up * 5,
                Quaternion.identity))
                .transform;
        }

        World.terrains = (Terrain[])FindObjectsOfType(typeof(Terrain));
        
        if (
            whirldIn.worldParams["env"] != null &&
            (String)whirldIn.worldParams["env"] == "space")
        {
            //usedSkybox = RenderSettings.skybox = SkyboxSpace;
            //RenderSettings.fogColor = new Color(0.07f, 0.02f, 0.095f, 1f);
        }
        else
        {
            if (GameObject.Find("Sea"))
            {
                World.sea = GameObject.Find("Sea").transform;
                Settings.lavaAlt = World.sea.position.y;
            }
            /*else
            {
                Settings.lavaAlt = -10f;
                World.sea = ((GameObject)GameObject.Instantiate(
                    Resources.Load("Sea"),
                    new Vector3(0f, Settings.lavaAlt, 0f),
                    Quaternion.identity))
                    .transform;
            }*/
            if (
                !GameObject.Find("Floor") &&
                !FindObjectOfType(typeof(Terrain)) &&
                !World.sea &&
                whirldIn.worldParams["nofloor"] == null &&
                (String)whirldIn.worldParams["floor"] != "0")
            {
                GameObject.Instantiate(
                    Resources.Load("Floor"),
                    Vector3.zero,
                    Quaternion.identity);
            }
        }

        bool thereisalight = false;
        foreach (Light light in (Light[])FindObjectsOfType(typeof(Light)))
        {
            if (light.gameObject.name != "VehicleLight")
            {
                thereisalight = true;
            }
        }
        //Make sure there is a light in the scene
        if (!thereisalight) GameObject.Instantiate(Resources.Load("Light"));

        //Add Player!
        mE(World.baseTF.transform.position);
        yield return new WaitForSeconds(1f); //Give server settings a chance to load
        int veh = 0;
        if (Settings.buggyAllowed) veh = 0;
        else if (Settings.tankAllowed) veh = 2;
        else if (Settings.hoverAllowed) veh = 1;
        else if (Settings.jetAllowed) veh = 3;
        GameVehicleID = veh;
        String temp = GameData.userName;
        int i = 1;
        while (
            (i == 1 && GameObject.Find(temp) != null) ||
            (GameObject.Find(temp + " " + i) != null))
        {
            i++;
        }
        networkView.RPC(
            "iV",
            RPCMode.All,
            Network.AllocateViewID(),
            Settings.networkMode,
            GameVehicleID,
            (i != 1 ? temp + " " + i : temp),
            0,
            (isHost ? 1 : 0),
            0,
            0);

        if (isHost)
        {
            if (!Network.isServer)
            {
                sSHS(); //Send dedicated server my default settings
            }
            
            if (Network.isServer)
            {
                Game.Messaging.broadcast(
                    "You are hosting a server!\n" +
                    (serverPassword != "" ?
                        "Password: " + serverPassword + "\n" :
                        "") +
                    "IP: " +
                    Network.player.ipAddress +
                    "\n" +
                    (Network.player.externalIP != "" ?
                        "External: " + Network.player.externalIP :
                        "") +
                    "\nPort: " +
                    Network.player.port);
                yield return new WaitForSeconds(1f);
                StartCoroutine_Auto(registerHostSet());
            }
            else
            {
                Game.Messaging.broadcast(
                    "You are hosting a game on a dedicated server!\n" +
                    (serverPassword != "" ?
                        "Password: " + serverPassword + "\n" :
                        "") +
                    "IP: " +
                    Network.connections[0].ipAddress +
                    "\n" +
                    (Network.connections[0].externalIP != "" ?
                        "External: " + Network.connections[0].externalIP :
                        "") +
                    "\nPort: " +
                    Network.connections[0].port);
            }

            if (whirldIn.worldParams["message"] != null)
            {
                Settings.serverWelcome = (String)whirldIn.worldParams["message"];
                networkView.RPC(
                    "msg",
                    RPCMode.All,
                    Settings.serverWelcome,
                    (int)chatOrigins.Remote);
            }
            if (whirldIn.worldParams["marsxplr"] != null)
            {
                networkView.RPC(
                    "sSS",
                    RPCMode.All,
                    whirldIn.worldParams["marsxplr"]);
            }
        }
        else
        {
            Game.Messaging.broadcast(
                (i != 1 ? temp + " " + i : temp) +
                " has joined!");
        }

        //Init Game Prefs
        Settings.serverDefault = Settings.packServerPrefs();
        Settings.serverString = Settings.serverDefault;
        Settings.updatePrefs();

        //Randomize new player colors if they are not custom
        yield return null;
        Settings.colorCustom = (PlayerPrefs.GetInt("vehColCustom") == 1);
        if (!Settings.colorCustom) StartCoroutine_Auto(Settings.ramdomizeVehicleColor());
	}

	public void Update()
    {
		Application.runInBackground = true;
		
		if (
			Settings.resetTime > 0f &&
			10f - (Time.time - Settings.resetTime) < 1f)
        {
			if ((bool)PlayerVeh.ramoSphere) Destroy(PlayerVeh.ramoSphere);
			Player.transform.position = World.baseTF.position;
			Player.transform.rotation = World.baseTF.rotation;
			Player.rigidbody.isKinematic = false;
			Player.rigidbody.velocity = Vector3.zero;
			Settings.resetTime = -10f;
			Settings.updatePrefs(); //Rebuild a new ramosphere
		}
		else if (Settings.resetTime < -1f) Settings.resetTime += Time.deltaTime;
		
		if (
			Settings.serverUpdateTime != 0f &&
			Settings.serverUpdateTime < Time.time)
        {
			Settings.serverString = Settings.packServerPrefs();
			networkView.RPC("sSS", RPCMode.Others, Settings.serverString);
			if (isHost && !Network.isServer)
            {
				//Send dedicated server my default settings
				sSHS();
			}
			Settings.serverUpdateTime = 0f;
		}
		
		if (
			Settings.colorUpdateTime != 0f &&
			Settings.colorUpdateTime < Time.time)
        {
			Settings.colorUpdateTime = 0f;
			Settings.saveVehicleColor();
		}

		//Fade GUI Out
		if (GuiAnimate == 1)
        {
			if (GUIAlpha <= 0f)
            {
				GUIAlpha = 0f;
				GuiAnimate = 0;
			}
			else GUIAlpha -= Time.deltaTime * 0.35f;
		}
		//Fade GUI In
		else if (GuiAnimate == -1)
        {
			GUIAlpha = 0f;
			GuiAnimate = -2;
		}
		else if (GuiAnimate == -2)
        {
			if (GUIAlpha >= 1)
            {
				GUIAlpha = 1f;
				GuiAnimate = 0;
			}
			else GUIAlpha += Time.deltaTime * 0.35f;
		}

		//Let server know that we are knocking at the door
		if (
			Network.peerType != NetworkPeerType.Disconnected &&
			!isHost &&
			WorldDesc.url == "" &&
			Time.time - authUpdateTime > 1f &&
			Time.timeSinceLevelLoad > 3f)
        {
			networkView.RPC(
				"lMI",
				RPCMode.Others,
				Network.player,
				GameData.userName);
			authUpdateTime = Time.time;
		}
		
		if (!worldLoaded)
        {
			if(CameraVehicle.mb.blurAmount < 1f)
			{
				CameraVehicle.mb.blurAmount = CameraVehicle.mb.blurAmount + Time.deltaTime;
			}
		}

		//Handle toggling fullscreen
		if (
			Input.GetKeyDown(KeyCode.Alpha0) &&
			!Messaging.chatting &&
			Time.time > kpTime)
        {
			kpTime = Time.time + kpDur;
			Settings.toggleFullscreen();
		}

        fpsFrames++;
        if (Time.time > fpsTime)
        {
			fps = fpsFrames / heavyTickRate;
			fpsTime = Time.time + (float)heavyTickRate;
			fpsFrames = 0;

            //Prune Unauth Players list
			if (isHost)
            {
				for (int i = 0; i < unauthPlayers.Count; i++)
                {
                    if (Time.time - unauthPlayers[i].t > 2f)
                    {
                        unauthPlayers.RemoveAt(i);
                    }
				}
			}

            //Prune Players list
            foreach (DictionaryEntry plrE in Players)
            {
                if (plrE.Value == null)
                {
                    Debug.Log(plrE.Key + " null - removed from players list");
                    Players.Remove(plrE.Key);
                    break; //Don't want to continue iterating through players, as the hashtable is now out of sync.
                }
            }

            //Prune Ghosts
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                bool found = false;
                foreach (DictionaryEntry plrE in Players)
                {
                    if ((String)plrE.Key == go.name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) //This vehicle is deserted
                {
                    Destroy(go);
                    Debug.Log("Abandoned vehicle destroyed: " + go.name);
                    break;
                }
            }
		}
	}

	public void OnGUI()
    {
		Rect position;
		Texture2D image;
		if (Screen.lockCursor)
        {
			GUI.depth = -999;
			position = new Rect((float)(Screen.width / 2) - cursorOffset.x, (float)(Screen.height / 2) - cursorOffset.y, cursor.width, cursor.height);
			if (Settings.lasersAllowed)
            {
				int[] firepower = Settings.firepower;
				if (firepower[RuntimeServices.NormalizeArrayIndex(firepower, PlayerVeh.vehId)] > 0)
                {
					image = cursor;
					goto IL_009c;
				}
			}
			image = cursorLook;
			goto IL_009c;
		}
		goto IL_00a1;
		IL_00a1:
		GUI.skin = Skin;
		float gUIAlpha = GUIAlpha;
		Color color = GUI.color;
		float num = (color.a = gUIAlpha);
		Color color2 = (GUI.color = color);
		GUI.depth = 1;
		checked
        {
			if (loadingWorld)
            {
				GUI.Window(3, new Rect(unchecked(Screen.width / 2) - 300, unchecked(Screen.height / 2) - 250, 600f, 500f), WindowServerSetup, string.Empty, "windowChromeless");
				return;
			}
			int num2 = ((!Settings.simplified) ? 25 : 40);
			int num3 = ((!Settings.simplified) ? Mathf.Min(Mathf.Max(170, unchecked(Screen.width / 4)), 250) : 40);
			if (isHost)
            {
				if (worldLoaded)
                {
					if (!killServer)
                    {
						if (GUI.Button(new Rect(10f, 10f, num3, num2), (!Settings.simplified) ? "<<  Stop Hosting Game" : "<<"))
                        {
							if (!Network.isServer || Extensions.get_length((System.Array)Network.connections) < 1)
                            {
								netKillMode = 1;
								Network.Disconnect();
								unregisterHost();
							}
							else
							{
								killServer = true;
							}
						}
					}
					else {
						if (GUI.Button(new Rect(10f, 10f, num3, num2), (!Settings.simplified) ? "<<  Confirm Stop" : "<<"))
                        {
							netKillMode = 1;
							Network.Disconnect();
							unregisterHost();
						}
						if (GUI.Button(new Rect(unchecked(Screen.width / 2) - 125, unchecked(Screen.height / 2) - 100, 250f, 200f), "Notice:\n\nYou are hosting this game.\nIf you stop hosting, all the players\nin this game will be disconnected.\n\nIf you really want to, press \"Confirm Stop\".\n Otherwise, click this button to cancel\nthe stop and continue hosting!"))
                        {
							killServer = false;
							bool flag = false;
						}
					}
				}
			}
			else if (GUI.Button(new Rect(10f, 10f, num3, num2), (!Settings.simplified) ? "<  Exit Game" : "<<"))
            {
				netKillMode = 1;
				Network.Disconnect();
			}
			if (GUI.Button(new Rect(Screen.width - 50, Screen.height - 50, 40f, 40f), Settings.simplified ? "<<" : ">>"))
            {
				Settings.simplified = !Settings.simplified;
			}
			if (Settings.enteredfullscreen && GUI.Button(new Rect(unchecked(Screen.width / 2) - 125, unchecked(Screen.height / 2) - 100, 250f, 200f), "Welcome to fullscreen mode!\n\nIf you hear a chime noise while holding\nkeyboard buttons; press \"Esc\",\nthen click your mouse, then press \"0\".\n\n{Click this box to play}"))
            {
				Settings.enteredfullscreen = false;
			}
			if (!Player || Settings.simplified)
            {
				return;
			}
			if (PlayerVeh.vehId == 3)
            {
				GUI.Button(new Rect((float)Screen.width * 0.5f - 200f, Screen.height - 63, 400f, 20f), "{Hold Q for no throttle & E for full throttle}", hudTextStyle);
			}
			if (Messaging.chatting)
            {
				GUI.Button(new Rect((float)Screen.width * 0.5f - 200f, Screen.height - 50, 400f, 20f), "{Keyboard Shortcuts Locked ~ Press \"Tab\" to unlock}", hudTextStyle);
			}
			else if (Screen.lockCursor)
            {
				GUI.Button(new Rect((float)Screen.width * 0.5f - 200f, Screen.height - 50, 400f, 20f), "{Cursor Locked ~ " + (Input.GetButton("Fire2") ? "Release \"Alt\" to unlock" : ((!Input.GetButton("Snipe")) ? "Press \"2\" to unlock" : "Release \"Shift\" to unlock")) + "}", hudTextStyle);
			}
			else if (Settings.camMode == 3 || Settings.camMode == 4)
            {
				GUI.Button(new Rect((float)Screen.width * 0.5f - 200f, Screen.height - 50, 400f, 20f), "{Use the UIOJKL keys to adjust camera position}", hudTextStyle);
			}
			if (Event.current.type != EventType.layout)
            {
				if (!GUIPanels[4].active)
                {
					if (Vector3.Distance(Player.transform.position, World.baseTF.transform.position) < 20f && Settings.resetTime > -1f)
                    {
						GUIPanels[4].openTime = Time.time;
						GUIPanels[4].open = true;
						GUIPanels[4].active = true;
					}
				}
				else if (!(Vector3.Distance(Player.transform.position, World.baseTF.transform.position) < 20f) || !(Settings.resetTime > -1f))
                {
					GUIPanels[4].active = false;
				}
				if (!GUIPanels[5].active)
                {
					if (Settings.showHints && ((fps < 20f && Settings.renderLevel > 1) || (fps > 55f && Settings.renderLevel < 5)))
                    {
						GUIPanels[5].openTime = Time.time;
						GUIPanels[5].open = true;
						GUIPanels[5].active = true;
					}
				}
				else if (!Settings.showHints || ((!(fps < 20f) || Settings.renderLevel <= 1) && (!(fps > 55f) || Settings.renderLevel >= 5) && Time.time > GUIPanels[5].openTime + 3f))
                {
					GUIPanels[5].active = false;
				}
			}
			int num4 = 0;
			int num5 = 0;
			int num6 = 25;
			int num7 = 5;
			int num8 = Screen.height - 60;
			int num9 = num7 * 2;
			int num10 = num9;
			for (int i = 0; i < Extensions.get_length((System.Array)GUIPanels); i++)
            {
				GUIPanel[] gUIPanels = GUIPanels;
				if (gUIPanels[RuntimeServices.NormalizeArrayIndex(gUIPanels, i)].active)
                {
					GUIPanel[] gUIPanels2 = GUIPanels;
					if (gUIPanels2[RuntimeServices.NormalizeArrayIndex(gUIPanels2, i)].open)
                    {
						num4++;
					}
					else
                    {
						num5++;
					}
				}
			}
			for (int i = 0; i < Extensions.get_length((System.Array)GUIPanels); i++)
            {
				GUIPanel[] gUIPanels3 = GUIPanels;
				if (!gUIPanels3[RuntimeServices.NormalizeArrayIndex(gUIPanels3, i)].active)
                {
					GUIPanel[] gUIPanels4 = GUIPanels;
					if (!(gUIPanels4[RuntimeServices.NormalizeArrayIndex(gUIPanels4, i)].curHeight > (float)num6))
                    {
						continue;
					}
				}
				GUIPanel[] gUIPanels5 = GUIPanels;
				unchecked
                {
					if (gUIPanels5[RuntimeServices.NormalizeArrayIndex(gUIPanels5, i)].open)
                    {
						GUIPanel[] gUIPanels6 = GUIPanels;
						if (gUIPanels6[RuntimeServices.NormalizeArrayIndex(gUIPanels6, i)].active)
                        {
							GUIPanel[] gUIPanels7 = GUIPanels;
							gUIPanels7[RuntimeServices.NormalizeArrayIndex(gUIPanels7, i)].desHeight = checked(num8 - ((num5 + num4) * num7 + num5 * num6)) / num4;
							GUIPanel[] gUIPanels8 = GUIPanels;
							if (gUIPanels8[RuntimeServices.NormalizeArrayIndex(gUIPanels8, i)].maxHeight > 0)
                            {
								GUIPanel[] gUIPanels9 = GUIPanels;
								float desHeight = gUIPanels9[RuntimeServices.NormalizeArrayIndex(gUIPanels9, i)].desHeight;
								GUIPanel[] gUIPanels10 = GUIPanels;
								if (desHeight > (float)gUIPanels10[RuntimeServices.NormalizeArrayIndex(gUIPanels10, i)].maxHeight)
                                {
									GUIPanel[] gUIPanels11 = GUIPanels;
									GUIPanel obj = gUIPanels11[RuntimeServices.NormalizeArrayIndex(gUIPanels11, i)];
									GUIPanel[] gUIPanels12 = GUIPanels;
									obj.desHeight = gUIPanels12[RuntimeServices.NormalizeArrayIndex(gUIPanels12, i)].maxHeight;
									goto IL_09d6;
								}
							}
							GUIPanel[] gUIPanels13 = GUIPanels;
							if (gUIPanels13[RuntimeServices.NormalizeArrayIndex(gUIPanels13, i)].minHeight > 0)
                            {
								GUIPanel[] gUIPanels14 = GUIPanels;
								float desHeight2 = gUIPanels14[RuntimeServices.NormalizeArrayIndex(gUIPanels14, i)].desHeight;
								GUIPanel[] gUIPanels15 = GUIPanels;
								if (desHeight2 < (float)gUIPanels15[RuntimeServices.NormalizeArrayIndex(gUIPanels15, i)].minHeight)
                                {
									GUIPanel[] gUIPanels16 = GUIPanels;
									GUIPanel obj2 = gUIPanels16[RuntimeServices.NormalizeArrayIndex(gUIPanels16, i)];
									GUIPanel[] gUIPanels17 = GUIPanels;
									obj2.desHeight = gUIPanels17[RuntimeServices.NormalizeArrayIndex(gUIPanels17, i)].minHeight;
								}
							}
							goto IL_09d6;
						}
					}
					GUIPanel[] gUIPanels18 = GUIPanels;
					gUIPanels18[RuntimeServices.NormalizeArrayIndex(gUIPanels18, i)].desHeight = num6;
					goto IL_09d6;
				}
				IL_09d6:
				GUIPanel[] gUIPanels19 = GUIPanels;
				float curHeight = gUIPanels19[RuntimeServices.NormalizeArrayIndex(gUIPanels19, i)].curHeight;
				GUIPanel[] gUIPanels20 = GUIPanels;
				if (curHeight > gUIPanels20[RuntimeServices.NormalizeArrayIndex(gUIPanels20, i)].desHeight - 1f)
                {
					GUIPanel[] gUIPanels21 = GUIPanels;
					float curHeight2 = gUIPanels21[RuntimeServices.NormalizeArrayIndex(gUIPanels21, i)].curHeight;
					GUIPanel[] gUIPanels22 = GUIPanels;
					if (curHeight2 < gUIPanels22[RuntimeServices.NormalizeArrayIndex(gUIPanels22, i)].desHeight + 1f)
                    {
						GUIPanel[] gUIPanels23 = GUIPanels;
						GUIPanel obj3 = gUIPanels23[RuntimeServices.NormalizeArrayIndex(gUIPanels23, i)];
						GUIPanel[] gUIPanels24 = GUIPanels;
						obj3.curHeight = gUIPanels24[RuntimeServices.NormalizeArrayIndex(gUIPanels24, i)].desHeight;
						goto IL_0ab0;
					}
				}
				GUIPanel[] gUIPanels25 = GUIPanels;
				GUIPanel obj4 = gUIPanels25[RuntimeServices.NormalizeArrayIndex(gUIPanels25, i)];
				GUIPanel[] gUIPanels26 = GUIPanels;
				float curHeight3 = gUIPanels26[RuntimeServices.NormalizeArrayIndex(gUIPanels26, i)].curHeight;
				GUIPanel[] gUIPanels27 = GUIPanels;
				obj4.curHeight = Mathf.Lerp(curHeight3, gUIPanels27[RuntimeServices.NormalizeArrayIndex(gUIPanels27, i)].desHeight, Time.deltaTime * 3f);
				goto IL_0ab0;
				IL_0ab0:
				GUIPanel[] gUIPanels28 = GUIPanels;
				if (gUIPanels28[RuntimeServices.NormalizeArrayIndex(gUIPanels28, i)].curHeight < (float)num6 * 1.5f)
                {
					GUIPanel[] gUIPanels29 = GUIPanels;
					string text = gUIPanels29[RuntimeServices.NormalizeArrayIndex(gUIPanels29, i)].name;
					switch (i)
                    {
					case 1:
						text += " (" + fps.ToString("f0") + " FPS)";
						break;
					case 3:
						if (isHost && unauthPlayers.Count > 0)
                        {
							text = "* " + text + " (" + unauthPlayers.Count + ") *";
						}
						break;
					}
					if (Event.current.type != EventType.layout)
                    {
						float left = Screen.width - 180;
						float top = num9;
						float width = 170f;
						GUIPanel[] gUIPanels30 = GUIPanels;
						if (GUI.Button(new Rect(left, top, width, gUIPanels30[RuntimeServices.NormalizeArrayIndex(gUIPanels30, i)].curHeight), text))
                        {
							GUIPanel[] gUIPanels31 = GUIPanels;
							GUIPanel obj5 = gUIPanels31[RuntimeServices.NormalizeArrayIndex(gUIPanels31, i)];
							GUIPanel[] gUIPanels32 = GUIPanels;
							obj5.open = !gUIPanels32[RuntimeServices.NormalizeArrayIndex(gUIPanels32, i)].open;
							GUIPanel[] gUIPanels33 = GUIPanels;
							gUIPanels33[RuntimeServices.NormalizeArrayIndex(gUIPanels33, i)].openTime = Time.time;
							GUI.FocusWindow(20 + i);
						}
					}
				}
				else
                {
					int id = 20 + i;
					float left2 = Screen.width - 180;
					float top2 = num9;
					float width2 = 170f;
					GUIPanel[] gUIPanels34 = GUIPanels;
					Rect position2 = new Rect(left2, top2, width2, gUIPanels34[RuntimeServices.NormalizeArrayIndex(gUIPanels34, i)].curHeight);
					GUI.WindowFunction func = GUIPanel;
					GUIPanel[] gUIPanels35 = GUIPanels;
					string text2 = gUIPanels35[RuntimeServices.NormalizeArrayIndex(gUIPanels35, i)].name + ":";
					GUIPanel[] gUIPanels36 = GUIPanels;
					GUI.Window(id, position2, func, text2, (!gUIPanels36[RuntimeServices.NormalizeArrayIndex(gUIPanels36, i)].important) ? "Window" : "boldWindow");
				}
				float num11 = num9;
				GUIPanel[] gUIPanels37 = GUIPanels;
				num9 = (int)(num11 + (gUIPanels37[RuntimeServices.NormalizeArrayIndex(gUIPanels37, i)].curHeight + (float)num7));
				float num12 = num10;
				GUIPanel[] gUIPanels38 = GUIPanels;
				num10 = (int)(num12 + (gUIPanels38[RuntimeServices.NormalizeArrayIndex(gUIPanels38, i)].desHeight + (float)num7));
			}
			if (Event.current.type == EventType.layout)
            {
				return;
			}
			if (num10 > num8 + num7 * 4)
            {
				closePanel += Time.deltaTime;
				if (!(closePanel > 1.5f))
                {
					return;
				}
				int num13 = -1;
				for (int i = 0; i < Extensions.get_length((System.Array)GUIPanels); i++)
                {
					GUIPanel[] gUIPanels39 = GUIPanels;
					if (!gUIPanels39[RuntimeServices.NormalizeArrayIndex(gUIPanels39, i)].open)
                    {
						continue;
					}
					GUIPanel[] gUIPanels40 = GUIPanels;
					if (!(gUIPanels40[RuntimeServices.NormalizeArrayIndex(gUIPanels40, i)].openTime > 0f))
                    {
						continue;
					}
					GUIPanel[] gUIPanels41 = GUIPanels;
					if (!(gUIPanels41[RuntimeServices.NormalizeArrayIndex(gUIPanels41, i)].openTime < Time.time - 0.5f))
                    {
						continue;
					}
					if (num13 != -1)
                    {
						GUIPanel[] gUIPanels42 = GUIPanels;
						float openTime = gUIPanels42[RuntimeServices.NormalizeArrayIndex(gUIPanels42, i)].openTime;
						GUIPanel[] gUIPanels43 = GUIPanels;
						if (!(openTime < gUIPanels43[RuntimeServices.NormalizeArrayIndex(gUIPanels43, num13)].openTime))
                        {
							continue;
						}
					}
					num13 = i;
				}
				if (num13 != -1)
                {
					GUIPanel[] gUIPanels44 = GUIPanels;
					gUIPanels44[RuntimeServices.NormalizeArrayIndex(gUIPanels44, num13)].open = false;
				}
			}
			else
            {
				closePanel = 0f;
			}
			return;
		}
		IL_009c:
		GUI.Label(position, image);
		goto IL_00a1;
	}

	public void GUIPanel(int id)
    {
		int num = checked(id - 20);
		GUIStyle style = GUI.skin.GetStyle("close_button");
		if (GUI.Button(new Rect(style.padding.left, style.padding.top, style.normal.background.width, style.normal.background.height), string.Empty, "close_button"))
        {
			GUIPanel[] gUIPanels = GUIPanels;
			gUIPanels[RuntimeServices.NormalizeArrayIndex(gUIPanels, num)].open = false;
		}
		GUILayout.Space(5f);
		GUIPanel[] gUIPanels2 = GUIPanels;
		GUIPanel obj = gUIPanels2[RuntimeServices.NormalizeArrayIndex(gUIPanels2, num)];
		GUIPanel[] gUIPanels3 = GUIPanels;
		obj.scrollPos = GUILayout.BeginScrollView(gUIPanels3[RuntimeServices.NormalizeArrayIndex(gUIPanels3, num)].scrollPos);
		switch (num)
        {
		case 0:
			Settings.showDialogServer();
			break;
		case 1:
			Settings.showDialogGame();
			break;
		case 2:
			Settings.showDialogPlayer();
			break;
		case 3:
			Settings.showDialogPlayers();
			break;
		case 4:
			showDialogVehicles();
			break;
		case 5:
			showDialogRenderHints();
			break;
		default:
			GUILayout.Label("{Unknown Panel}");
			break;
		}
		GUILayout.Space(5f);
		GUILayout.EndScrollView();
	}

	public void showDialogVehicles()
    {
		if (RuntimeServices.EqualityOperator(GameVehicleID, null))
        {
			return;
		}
		GUILayout.FlexibleSpace();
		GUILayout.Space(5f);
		GameObject[] gameVehicles = GameVehicles;
		GUILayout.Label("You are currently commanding a " + gameVehicles[RuntimeServices.NormalizeArrayIndex(gameVehicles, GameVehicleID)].name + ":");
		GUILayout.Space(10f);
		GUILayout.FlexibleSpace();
		int num = -1;
		int i = 0;
		GameObject[] gameVehicles2 = GameVehicles;
		checked
        {
			for (int length = gameVehicles2.Length; i < length; i++)
            {
				num++;
				if (num != GameVehicleID)
                {
					if (num == 0 && !Settings.buggyAllowed)
                    {
						GUILayout.Label("(Buggy unavailable)", GUILayout.Height(25f));
					}
					else if (num == 1 && !Settings.hoverAllowed)
                    {
						GUILayout.Label("(Hovercraft unavailable)", GUILayout.Height(25f));
					}
					else if (num == 2 && !Settings.tankAllowed)
                    {
						GUILayout.Label("(Tank unavailable)", GUILayout.Height(25f));
					}
					else if (num == 3 && !Settings.jetAllowed)
                    {
						GUILayout.Label("(Jet unavailable)", GUILayout.Height(25f));
					}
					else if (GUILayout.Button(">> Switch to a " + gameVehicles2[i].name, GUILayout.Height(40f)))
                    {
						Settings.resetTime = -10f;
						setVeh(num);
					}
				}
			}
			GUILayout.Space(10f);
			GUILayout.FlexibleSpace();
			GUILayout.Label("(You can only switch when at this location)");
		}
	}

	public void showDialogRenderHints()
    {
		if (fps < 20f)
        {
			GUILayout.Label("Low Framerate: " + fps.ToString("f0") + " FPS\n\nClick \"Game Settings\" above and decrease (<<) the Rendering Quality to speed up your framerate");
		}
		else
        {
			GUILayout.Label("High Framerate: " + fps.ToString("f0") + " FPS\n\nClick \"Game Settings\" above and increase (>>) the Rendering Quality to make everything look nicer");
		}
		GUILayout.Space(5f);
		if (GUILayout.Toggle(Settings.showHints, "Enable Settings Advisor") != Settings.showHints)
        {
			Settings.showHints = !Settings.showHints;
			PlayerPrefs.SetInt("showHints", Settings.showHints ? 1 : 0);
			Settings.updatePrefs();
		}
	}

	public IEnumerator OnPlayerConnected(NetworkPlayer player)
    {
		//Make sure they aren't banned
        if (Settings.bannedIPs != "")
        {
            String[] banned = Settings.bannedIPs.Split("\n"[0]);
            foreach (String dat in banned)
            {
                String[] val = dat.Split(" "[0]);
                if (val[0] == player.ipAddress)
                {
                    //Game.Messaging.broadcast(player.ipAddress + " attempted to rejoin");
                    networkView.RPC("dN", player, 2);
                    Network.CloseConnection(player, true);
                }
            }
        }

        //Authenticate them if necessary
        while (serverPassword != "")
        {
            if (authenticatedPlayers.ContainsKey(player)) break; //They sent the right password in an RPC - let them join the game
            bool playerIsConnected = false;
            foreach (NetworkPlayer plyr in Network.connections)
            {
                if (plyr == player)
                {
                    playerIsConnected = true;
                }
            }
            if (!playerIsConnected) yield break; //They gave up, and are no longer connected
            yield return null;
        }

        //Sync Vehicles
        foreach (DictionaryEntry plrE in Players)
        {
            Vehicle veh = (Vehicle)plrE.Value;
            networkView.RPC(
                "iV",
                player,
                veh.networkView.viewID,
                veh.networkMode,
                veh.vehId,
                veh.gameObject.name,
                veh.isBot ? 1 : 0,
                veh.isIt,
                veh.score,
                veh.specialInput ? 1 : 0);
            veh.networkView.RPC(
                "sC",
                player,
                veh.vehicleColor.r,
                veh.vehicleColor.g,
                veh.vehicleColor.b,
                veh.vehicleAccent.r,
                veh.vehicleAccent.g,
                veh.vehicleAccent.b);
        }

        //Sync Server Prefs
        if (Settings.serverString != Settings.serverDefault)
        {
            Settings.serverString = Settings.packServerPrefs();
            networkView.RPC("sSS", player, Settings.serverString);
        }

        //Send Them World
        networkView.RPC("lW", player, "url=" + WorldDesc.url);

        //Welcome them!
        if (Settings.serverWelcome != "")
        {
            networkView.RPC(
                "msg",
                player,
                Settings.serverWelcome,
                (int)chatOrigins.Remote);
        }

        //Force-Refresh Everyone's NetworkViews
        // /*DEAD LINK*/ http://forum.unity3d.com/viewtopic.php?t=35724&postdays=0&postorder=asc&start=105
        foreach (DictionaryEntry plrE in Players)
        {
            if (plrE.Value == null) continue;
            ((Vehicle)plrE.Value).networkView.enabled = false;
            yield return null;
            ((Vehicle)plrE.Value).networkView.enabled = true;
        }

        //Update Master Server Listing
        yield return new WaitForSeconds(5f);
        StartCoroutine_Auto(registerHost());
    }

	public IEnumerator OnPlayerDisconnected(NetworkPlayer player)
    {
        // /*UNUSED*/ String pName = "";

        foreach (DictionaryEntry plrE in Players)
        {
            if (((Vehicle)plrE.Value).networkView.owner == player)
            {
                networkView.RPC("pD", RPCMode.All, plrE.Key);
                break;
            }
        }

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        yield return new WaitForSeconds(1f);
        eSI();
        StartCoroutine_Auto(registerHost());
	}

	public void WindowServerSetup(int id)
    {
		if (!isHost && WorldDesc.url == string.Empty)
        {
			GUILayout.FlexibleSpace();
			GUILayout.FlexibleSpace();
			GUILayout.FlexibleSpace();
			GUILayout.Label("This Game is Password Protected:");
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.Space(40f);
			GUILayout.Label("Password:", GUILayout.Width(80f));
			serverPassword = GUILayout.PasswordField(serverPassword, "*"[0]);
			GUILayout.Space(40f);
			GUILayout.EndHorizontal();
			if (authTime > 1f && authTime < Time.time - 3f)
            {
				GUILayout.FlexibleSpace();
				GUILayout.Label("Authentication Failed - please try a different password");
			}
			GUILayout.FlexibleSpace();
			GUILayout.Label("(The host can invite you directly into their game if they desire)");
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.Space(40f);
			if (GUILayout.Button((!(authTime > 1f) || !(authTime > Time.time - 3f)) ? ">> Authenticate" : "Authenticating...", GUILayout.Height(40f)))
            {
				authTime = Time.time;
				networkView.RPC("cP", RPCMode.All, Network.player, serverPassword);
			}
			GUILayout.Space(40f);
			GUILayout.EndHorizontal();
			GUILayout.Space(5f);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("<< Cancel", GUILayout.Height(25f), GUILayout.Width(150f)))
            {
				netKillMode = 1;
				Network.Disconnect();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			return;
		}
		if (whirldIn.status == WhirldInStatus.Success || worldLoaded)
        {
			if (whirldIn.status == WhirldInStatus.Success && !worldLoaded)
            {
				worldLoaded = true;
				Settings.simplified = false;
				scrollPosition = Vector2.zero;
				if (whirldIn.info != string.Empty)
                {
					msg(whirldIn.info, UnityBuiltins.parseInt(2));
				}
			}
			GUILayout.Label("\n\n\nWorld Loaded Successfully...\n\n\n");
			GUILayout.BeginHorizontal();
			GUILayout.Space(40f);
			GUILayout.Button("<< Cancel", GUILayout.Height(40f));
			GUILayout.Space(40f);
			GUILayout.EndHorizontal();
			GUILayout.Space(40f);
			return;
		}
		if (whirldIn.status != WhirldInStatus.Idle)
        {
			if (whirldIn.status == WhirldInStatus.Working)
            {
				string text = string.Empty;
				IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(whirldIn.threads);
				while (enumerator.MoveNext())
                {
					DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
					if (!RuntimeServices.EqualityOperator(dictionaryEntry.Value, string.Empty))
                    {
						text = (string)RuntimeServices.Coerce(RuntimeServices.InvokeBinaryOperator("op_Addition", text, RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", "\n", dictionaryEntry.Key), ": ")), typeof(string));
						UnityRuntimeServices.Update(enumerator, dictionaryEntry);
						try
                        {
							float num = RuntimeServices.UnboxSingle(dictionaryEntry.Value);
							UnityRuntimeServices.Update(enumerator, dictionaryEntry);
							text += Mathf.RoundToInt(RuntimeServices.UnboxSingle(RuntimeServices.InvokeBinaryOperator("op_Multiply", dictionaryEntry.Value, 100))) + "%";
							UnityRuntimeServices.Update(enumerator, dictionaryEntry);
						}
						catch (Exception)
                        {
							text = (string)RuntimeServices.Coerce(RuntimeServices.InvokeBinaryOperator("op_Addition", text, dictionaryEntry.Value), typeof(string));
							UnityRuntimeServices.Update(enumerator, dictionaryEntry);
						}
					}
				}
				scrollPosition = GUILayout.BeginScrollView(scrollPosition);
				GUILayout.Label("Loading World:\n\n" + ((!(whirldIn.statusTxt == string.Empty)) ? (string.Empty + whirldIn.statusTxt + "...") : "Initializing Whirld Library...") + ((!(whirldIn.progress > 0f) || !(whirldIn.progress < 1f)) ? string.Empty : (" (" + whirldIn.progress * 100f + "%)")) + "\n" + text);
				GUILayout.EndScrollView();
			}
			else
            {
				GUILayout.Label("World Loading Error:\n" + whirldIn.status);
			}
			GUILayout.BeginHorizontal();
			GUILayout.Space(40f);
			if (GUILayout.Button((whirldIn.status != WhirldInStatus.Working) ? "<< Retry" : "<< Cancel", GUILayout.Height(40f)))
            {
				whirldIn.Cleanup();
				whirldIn = null;
				whirldIn = new WhirldIn();
			}
			GUILayout.Space(40f);
			GUILayout.EndHorizontal();
			GUILayout.Space(40f);
			return;
		}
		if (!isHost)
        {
			GUILayout.Space(150f);
			GUILayout.Label("Initializing connection with game server...");
			netKillMode = 1;
			Network.Disconnect();
			return;
		}
		if (serverName == string.Empty && GUI.GetNameOfFocusedControl() != "serverName")
        {
			serverName = GameData.userName + "'s Game";
		}
		if (WorldDesc.name == string.Empty && GUI.GetNameOfFocusedControl() != "worldName")
        {
			WorldDesc.name = "Custom World";
		}
		GUILayout.Space(40f);
		string[] array = (string[])new UnityScript.Lang.Array("Select a World", "Use Custom World", "Server Settings").ToBuiltin(typeof(string));
		hostPanelTab = GUILayout.SelectionGrid(hostPanelTab, array, Extensions.get_length((System.Array)array), GUILayout.Height(30f));
		GUILayout.Space(20f);
		checked
        {
			if (hostPanelTab == 0)
            {
				GUILayout.Label("Have fun hosting your game!\n Customize your game's settings in the panels enumerated above,\nand specify a world to explore from the list below:");
				GUILayout.Space(20f);
				GUILayout.BeginHorizontal();
				GUILayout.Space(115f);
				scrollPosition = GUILayout.BeginScrollView(scrollPosition);
				if (WorldIsCustom && !GUILayout.Toggle(true, "Using Custom World") && false)
                {
					WorldIsCustom = false;
				}
				GUILayout.BeginHorizontal();
				int num2 = 0;
				int i = 0;
				GameWorldDesc[] gameWorlds = GameData.gameWorlds;
				for (int length = gameWorlds.Length; i < length; i++)
                {
					if (gameWorlds[i].featured)
                    {
						if (!WorldIsCustom && WorldDesc != null && WorldDesc.name == gameWorlds[i].name)
                        {
							GUILayout.Toggle(true, gameWorlds[i].name, GUILayout.Width(170f), GUILayout.Height(22f));
						}
						else if (GUILayout.Toggle(false, gameWorlds[i].name, GUILayout.Width(170f), GUILayout.Height(22f)))
                        {
							WorldIsCustom = false;
							WorldDesc.name = gameWorlds[i].name;
							WorldDesc.url = gameWorlds[i].url;
						}
						num2++;
						if (unchecked(num2 % 2) == 0)
                        {
							GUILayout.EndHorizontal();
							GUILayout.BeginHorizontal();
						}
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(20f);
				GUILayout.FlexibleSpace();
				GUILayout.BeginHorizontal();
				num2 = 0;
				int j = 0;
				GameWorldDesc[] gameWorlds2 = GameData.gameWorlds;
				for (int length2 = gameWorlds2.Length; j < length2; j++)
                {
					if (!gameWorlds2[j].featured)
                    {
						if (!WorldIsCustom && WorldDesc != null && WorldDesc.name == gameWorlds2[j].name)
                        {
							GUILayout.Toggle(true, gameWorlds2[j].name, GUILayout.Width(170f));
						}
						else if (GUILayout.Toggle(false, gameWorlds2[j].name, GUILayout.Width(170f)))
                        {
							WorldIsCustom = false;
							WorldDesc.name = gameWorlds2[j].name;
							WorldDesc.url = gameWorlds2[j].url;
						}
						num2++;
						if (unchecked(num2 % 2) == 0)
                        {
							GUILayout.EndHorizontal();
							GUILayout.BeginHorizontal();
						}
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.EndScrollView();
				GUILayout.EndHorizontal();
			}
			else if (hostPanelTab == 1)
            {
				GUILayout.Space(20f);
				if (WorldIsCustom)
                {
					GUILayout.BeginHorizontal();
					GUILayout.Space(150f);
					WorldIsCustom = GUILayout.Toggle(WorldIsCustom, "Use Custom World");
					GUILayout.Space(150f);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(70f);
					scrollPosition = GUILayout.BeginScrollView(scrollPosition);
					GUILayout.Space(10f);
					GUILayout.BeginHorizontal();
					GUILayout.Label("World Name:", GUILayout.Width(80f));
					GUI.SetNextControlName("worldName");
					WorldDesc.name = GUILayout.TextField(WorldDesc.name);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUI.SetNextControlName("worldUrl");
					GUILayout.Label("World Url:", GUILayout.Width(80f));
					string url = WorldDesc.url;
					WorldDesc.url = GUILayout.TextField(WorldDesc.url);
					if (WorldDesc.url != url)
                    {
						WorldDesc.name = "Custom World";
					}
					GUILayout.EndHorizontal();
					GUILayout.EndScrollView();
					GUILayout.Space(70f);
					GUILayout.EndHorizontal();
				}
				else
                {
					GUILayout.BeginHorizontal();
					GUILayout.Space(150f);
					scrollPosition = GUILayout.BeginScrollView(scrollPosition);
					WorldIsCustom = GUILayout.Toggle(WorldIsCustom, "Use Custom World");
					GUILayout.EndScrollView();
					GUILayout.Space(150f);
					GUILayout.EndHorizontal();
					GUILayout.Space(20f);
					GUILayout.Label("Mars Explorer incorporates the Unity Whirld system -\nan open source framework which enables you to design your own game worlds,\nand play them inside Mars Exporer!\n\nIf you have a custom world, enable \"Use Custom World\" above to use it in your game.");
				}
				GUILayout.Space(40f);
				GUILayout.BeginHorizontal();
				GUILayout.Space(150f);
				if (GUILayout.Button(">> Learn About the Whirld System"))
                {
                    Lobby.OpenURL("http://web.archive.org/web/20120519040400/http://www.unifycommunity.com/wiki/index.php?title=Whirld");
				}
				GUILayout.Space(150f);
				GUILayout.EndHorizontal();
			}
			else
            {
				GUILayout.BeginHorizontal();
				GUILayout.Space(150f);
				scrollPosition = GUILayout.BeginScrollView(scrollPosition);
				GUILayout.Space(10f);
				GUILayout.Label("Your Game's Name:");
				GUI.SetNextControlName("serverName");
				serverName = GUILayout.TextField(serverName, 45);
				GUILayout.Space(20f);
				serverHidden = GUILayout.Toggle(serverHidden, "Hide This Game From List");
				if (!serverHidden)
                {
					bool flag = GUILayout.Toggle(!(serverPassword == string.Empty), "Password Protect This Game");
					if (flag)
                    {
						serverPassword = ((!(serverPassword == string.Empty)) ? serverPassword : "1");
						GUILayout.BeginHorizontal();
						GUILayout.Label("Password:", GUILayout.Width(80f));
						serverPassword = GUILayout.TextField((!(serverPassword == "1")) ? serverPassword : string.Empty, 45);
						if (flag && serverPassword == string.Empty)
                        {
							serverPassword = "1";
						}
						GUILayout.EndHorizontal();
					}
					else
                    {
						serverPassword = string.Empty;
					}
				}
				else
                {
					GUILayout.Label("(Your game will not be shown in the server list. Friends will need to \"Direct Connect\" to your IP Address)");
					serverPassword = string.Empty;
				}
				GUILayout.EndScrollView();
				GUILayout.Space(150f);
				GUILayout.EndHorizontal();
			}
			GUILayout.FlexibleSpace();
			if (WorldDesc.url != string.Empty && WorldDesc.url != "http://")
            {
				if (GUILayout.Button(">> Begin Hosting Game! <<", GUILayout.Height(40f)))
                {
					serverName = LanguageFilter(serverName);
					if (serverPassword == "1")
                    {
						serverPassword = string.Empty;
						while (Extensions.get_length(serverPassword) < 5)
                        {
							serverPassword += UnityEngine.Random.Range(0, 9);
						}
					}
					LoadWorld();
				}
				GUILayout.Space(5f);
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("<< Cancel", GUILayout.Height(25f), GUILayout.Width(150f)))
                {
					netKillMode = 1;
					Network.Disconnect();
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}
			else
            {
				if (GUILayout.Button("<< Cancel Hosting Game", GUILayout.Height(40f)))
				{
					netKillMode = 1;
					Network.Disconnect();
				}
				GUILayout.Space(32f);
			}
		}
	}

	public IEnumerator addBot()
    {
        botsInGame++;
        Messaging.broadcast(
            Player.name +
            " added " +
            (botsInGame == 1 ? "a bot" : "bot " + botsInGame));
        materilizationEffect(World.baseTF.transform.position);
        String temp =
            GameData.userName +
            "'s bot" +
            (botsInGame == 1 ? "" : " " + botsInGame);
        yield return new WaitForSeconds(2f);
        networkView.RPC(
            "iV",
            RPCMode.All,
            Network.AllocateViewID(),
            Settings.networkMode,
            GameVehicleID,
            temp,
            1,
            0,
            0,
            0);
	}

	public IEnumerator axeBot()
    {
        GameObject bot = GameObject.Find(
            Player.name +
            "'s bot" +
            (botsInGame != 1 ? " " + botsInGame : ""));
        if (!bot) yield break;
        Messaging.broadcast(
            Player.name +
            " removed " +
            (botsInGame == 1 ? "the last bot" : ("bot " + botsInGame)));
        botsInGame--;
        materilizationEffect(bot.transform.position);
        bot.rigidbody.isKinematic = true;

        Network.Destroy(bot.rigidbody.networkView.viewID);

        yield return new WaitForSeconds(0.5f);
        eSI();
        //renderAdjustMax = 0; //Give the autorender settings algorithym a chance to take advantage of new settings...
	}

	public void setVeh(int setVehicleTo)
    {
		Messaging messaging = Messaging;
		string lhs = Player.name + " switched to a ";
		GameObject[] gameVehicles = GameVehicles;
		messaging.broadcast(lhs + gameVehicles[RuntimeServices.NormalizeArrayIndex(gameVehicles, setVehicleTo)].name);
		GameVehicleID = setVehicleTo;
		int isIt = PlayerVeh.isIt;
		int score = PlayerVeh.score;
		int num = (PlayerVeh.specialInput ? 1 : 0);
		string text = Player.name;
		Network.Destroy(Player.rigidbody.networkView.viewID);
		networkView.RPC("iV", RPCMode.All, Network.AllocateViewID(), Settings.networkMode, GameVehicleID, text, 0, isIt, score, num);
	}

	[RPC]
	public void eSI()
    {
		if (!Player)
        {
			return;
		}
		GameObject[] array = null;
		Vehicle vehicle = null;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Players);
		while (enumerator.MoveNext())
        {
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(dictionaryEntry.Value, "isIt"), 1))
            {
				return;
			}
		}
		Player.networkView.RPC("sQ", RPCMode.All, 3);
	}

	public void materilizationEffect(Vector3 position)
    {
		networkView.RPC("mE", RPCMode.All, position);
	}

	[RPC]
	public void mE(Vector3 position)
    {
		UnityEngine.Object.Instantiate(WorldEntryEffect, position, new Quaternion(0f, 0f, 0f, 1f));
	}

	public IEnumerator registerHost()
    {
        if (serverHidden || !Network.isServer) yield break;
        yield return new WaitForSeconds(1f);
        String playerList = "";
        int lagCount = 0;
        float lagVal = 0.0f;
        int botCount = 0;
        foreach (DictionaryEntry plrE in Players)
        {
            if (plrE.Value == null) continue;

            if (((Vehicle)plrE.Value).isBot) botCount++;
            else
            {
                playerList +=
                    (playerList == "" ? "" : ",") +
                    ((Vehicle)plrE.Value).name;
                if (!((Vehicle)plrE.Value).networkView.isMine)
                {
                    VehicleNet vehNet = (VehicleNet)((Vehicle)plrE.Value)
                        .gameObject.GetComponent(typeof(VehicleNet));
                    if (vehNet)
                    {
                        lagCount++;
                        lagVal += vehNet.ping;
                    }
                }
            }
        }
        if (botCount > 0)
        {
            playerList +=
                ",and " +
                botCount +
                " bots";
        }
        String bannedIPs = "";
        if (Settings.bannedIPs != "")
        {
            String[] banned = Settings.bannedIPs.Split("\n"[0]);
            foreach (String dat in banned)
            {
                if (dat == "") continue;
                String[] val = dat.Split(" "[0]);
                bannedIPs += (bannedIPs == "" ? "," : "") + val[0];
            }
            bannedIPs = ";b=" + bannedIPs;
        }
        MasterServer.RegisterHost(
            GameData.gameName,
            serverName,
            "v=" +
                GameData.gameVersion +
                ";w=" +
                WorldDesc.name +
                ";p=" +
                playerList +
                ";u=" +
                WorldDesc.url +
                (lagCount > 0 ?
                    ";s=" + Mathf.RoundToInt((lagVal / lagCount) * 1000f) :
                    "") +
                (serverPassword != "" ? ";l=1" : "") +
                bannedIPs);
	}

	public IEnumerator registerHostSet()
    {
        if (serverHidden) yield break;
        hostRegistered = true;
        while (hostRegistered)
        {
            StartCoroutine_Auto(registerHost());
            yield return new WaitForSeconds(60f);
        }
	}

	public void unregisterHost()
    {
		hostRegistered = false;
		MasterServer.UnregisterHost();
	}

	public void OnDisconnectedFromServer(NetworkDisconnection info)
    {
		if (!Network.isServer && netKillMode != 1)
        {
			GameData.errorMessage = "\n\nDisconnected from Game Server:\n\n";
			if (info == NetworkDisconnection.LostConnection)
            {
				GameData.errorMessage += "Connection lost, please try rejoining the game!";
			}
			else if (netKillMode == 4)
            {
				GameData.errorMessage += "Timeout due to inactivity.\nPlease try rejoining the game.";
			}
			else if (netKillMode == 3)
            {
				GameData.errorMessage += "Network connection to game server timed out.\nPlease try rejoining the game.";
			}
			else if (netKillMode == 2)
            {
				GameData.errorMessage += "The server host has evicted you from their game.\nYou will probably want to find a new server to play at.";
			}
			else
            {
				GameData.errorMessage += "The person hosting the game you were connected to has stopped playing Mars Explorer.";
			}
		}
		Application.LoadLevel(1);
	}

	public void OnApplicationQuit()
    {
		Network.Disconnect();
		if (Network.isServer)
        {
			unregisterHost();
		}
		Application.LoadLevel(1);
	}

	[RPC]
	public IEnumerator iV(
        NetworkViewID viewID,
        int networkMode,
        int vehId,
        string vehName,
        int isBot,
        int isIt,
        int score,
        int specialInput)
    {
		//while(worldLoaded == false) yield return null;

        Vector3 pos = Vector3.zero;
        if (viewID.isMine)
        {
            //Dragonhere: possibly gove other client instances time to build their vehicles before they are bombarded with NetworkViews that they can't handle.
            yield return new WaitForSeconds(0.25f);

            //Determine Safe Spawn Point
            pos = World.baseTF.position;
            while (Physics.CheckSphere(pos, 3)) pos += Vector3.up;
        }

        //Create Vehicle
        switch (networkMode)
        { 
            case 0:
                objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.Unreliable;
                break;
            case 1:
                objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
                break;
            default:
                objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.Off;
                break;
        }
        objectVehicle.networkView.viewID = viewID;
        GameObject plyObj = (GameObject)Instantiate(
            objectVehicle,
            pos,
            Quaternion.identity);
        plyObj.networkView.viewID = viewID;
        GameObject vehObj = (GameObject)Instantiate(
            Game.Controller.GameVehicles[vehId],
            pos,
            Quaternion.identity);
        vehObj.transform.parent = plyObj.transform;
        Vehicle plyVeh = plyObj.GetComponent<Vehicle>();
        plyVeh.vehObj = vehObj;
        if (viewID.isMine && World.baseTF != null)
        {
            //DRAGONHERE - MAJOR UNITY BUG: If we are instantiated at any other than the identity rotation, it totally messes up rotation locking on vehicle joints such as tank tracks
            plyObj.transform.rotation = World.baseTF.rotation;
        }
        else
        {
            plyObj.transform.rotation = Quaternion.identity;
        }

        //Configure Vehicle
        plyObj.name = vehName;
        plyVeh.networkMode = networkMode;
        plyVeh.vehId = vehId;
        plyVeh.isBot = (isBot == 1);
        plyVeh.isIt = isIt;
        plyVeh.score = score;
        plyVeh.specialInput = (specialInput == 1);

        if (viewID.isMine && isBot == 0) Player = plyObj;
	}

	[RPC]
	public void pD(string pName)
    {
		if (RuntimeServices.ToBool(Players[pName]))
        {
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Players);
			while (enumerator.MoveNext())
            {
				DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
				UnityRuntimeServices.Invoke(dictionaryEntry.Value, "setColor", new object[0], typeof(MonoBehaviour));
				UnityRuntimeServices.Update(enumerator, dictionaryEntry);
			}
			mE((Vector3)RuntimeServices.GetProperty(RuntimeServices.GetProperty(RuntimeServices.GetProperty(Players[pName], "gameObject"), "transform"), "position"));
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(Players[pName], "netKillMode"), 0))
            {
				msg(pName + " has disconnected", 2);
			}
			if (RuntimeServices.ToBool(RuntimeServices.GetProperty(Players[pName], "gameObject")))
            {
				UnityEngine.Object.Destroy((UnityEngine.Object)RuntimeServices.Coerce(RuntimeServices.GetProperty(Players[pName], "gameObject"), typeof(UnityEngine.Object)));
			}
			Players.Remove(pName);
		}
	}

	[RPC]
	public void pI(NetworkPlayer nPlayer, string pName, NetworkMessageInfo info){}

	[RPC]
	public void cC(NetworkPlayer nPlayer, string pName, int cMode, NetworkMessageInfo info){}

	[RPC]
	public void cP(NetworkPlayer player, string pass)
    {
		if ((Network.isServer && pass == serverPassword) || pass == "pg904gk7")
        {
			authenticatedPlayers.Add(player, 1);
		}
	}

	[RPC]
	public void sSS(string str, NetworkMessageInfo info)
    {
		Settings.serverString = str;
		if (!info.networkView.isMine)
        {
			if (str == Settings.serverDefault)
            {
				msg("(Server Settings Defaulted)", UnityBuiltins.parseInt(2));
			}
			else
            {
				msg("(Server Settings Updated)", UnityBuiltins.parseInt(2));
			}
		}
		string[] array = str.Split(";"[0]);
		string[] array2 = null;
		int i = 0;
		string[] array3 = array;
		checked
        {
			for (int length = array3.Length; i < length; i++)
            {
				array2 = array3[i].Split(":"[0]);
				if (array2[0] == "lasr")
                {
					Settings.lasersAllowed = array2[1] == "1";
				}
				if (array2[0] == "lsrh")
                {
					Settings.lasersFatal = array2[1] == "1";
				}
				if (array2[0] == "lsro")
                {
					Settings.lasersOptHit = array2[1] == "1";
				}
				else if (array2[0] == "mmap")
                {
					Settings.minimapAllowed = array2[1] == "1";
				}
				else if (array2[0] == "camo")
                {
					Settings.hideNames = array2[1] == "1";
				}
				else if (array2[0] == "rorb")
                {
					Settings.ramoSpheres = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "xspd")
                {
					Settings.zorbSpeed = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "xagt")
                {
					Settings.zorbAgility = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "xbnc")
                {
					Settings.zorbBounce = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "grav")
                {
					Settings.worldGrav = UnityBuiltins.parseFloat(array2[1]) * -1f;
				}
				else if (array2[0] == "wvis")
                {
					Settings.worldViewDist = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "lfog")
                {
					Settings.lavaFog = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "lalt")
                {
					Settings.lavaAlt = UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "lspd")
                {
					Settings.laserSpeed = (int)UnityBuiltins.parseFloat(array2[1]);
				}
				else if (array2[0] == "lgvt")
                {
					Settings.laserGrav = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "lrco")
                {
					Settings.laserRico = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "botfire")
                {
					Settings.botsCanFire = array2[1] == "1";
				}
				else if (array2[0] == "botdrive")
                {
					Settings.botsCanDrive = array2[1] == "1";
				}
				else if (array2[0] == "bugen")
                {
					Settings.buggyAllowed = array2[1] == "1";
				}
				else if (array2[0] == "bugxphy")
                {
					Settings.buggyNewPhysics = array2[1] == "1";
				}
				else if (array2[0] == "bugflsl")
                {
					Settings.buggyFlightSlip = array2[1] == "1";
				}
				else if (array2[0] == "bugflpw")
                {
					Settings.buggyFlightLooPower = array2[1] == "1";
				}
				else if (array2[0] == "bugawd")
                {
					Settings.buggyAWD = array2[1] == "1";
				}
				else if (array2[0] == "bugspn")
                {
					Settings.buggySmartSuspension = array2[1] == "1";
				}
				else if (array2[0] == "bugfldr")
                {
					Settings.buggyFlightDrag = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 1000f);
				}
				else if (array2[0] == "bugflag")
                {
					Settings.buggyFlightAgility = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.5f, 1.5f);
				}
				else if (array2[0] == "bugcg")
                {
					Settings.buggyCG = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), -1f, 0f);
				}
				else if (array2[0] == "bugpow")
                {
					Settings.buggyPower = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.1f, 3f);
				}
				else if (array2[0] == "bugspd")
                {
					Settings.buggySpeed = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 1000f);
				}
				else if (array2[0] == "bugtr")
                {
					Settings.buggyTr = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.1f, 3f);
				}
				else if (array2[0] == "bugsh")
                {
					Settings.buggySh = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 140f);
				}
				else if (array2[0] == "bugfp")
                {
					Settings.firepower[0] = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 3);
				}
				else if (array2[0] == "bugll")
                {
					Settings.laserLock[0] = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "tnken")
                {
					Settings.tankAllowed = array2[1] == "1";
				}
				else if (array2[0] == "tnkgrp")
                {
					Settings.tankGrip = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "tnkspd")
                {
					Settings.tankSpeed = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 100f);
				}
				else if (array2[0] == "tnkpow")
                {
					Settings.tankPower = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 100f, 10000f);
				}
				else if (array2[0] == "tnkcg")
                {
					Settings.tankCG = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), -2f, 2f);
				}
				else if (array2[0] == "tnkfp")
                {
					Settings.firepower[2] = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 3);
				}
				else if (array2[0] == "tnkll")
                {
					Settings.laserLock[2] = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "hvren")
                {
					Settings.hoverAllowed = array2[1] == "1";
				}
				else if (array2[0] == "hvrhe")
                {
					Settings.hoverHeight = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 100f);
				}
				else if (array2[0] == "hvrhv")
                {
					Settings.hoverHover = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 1000f);
				}
				else if (array2[0] == "hvrrp")
                {
					Settings.hoverRepel = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.1f, 10f);
				}
				else if (array2[0] == "hvrth")
                {
					Settings.hoverThrust = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 1000f);
				}
				else if (array2[0] == "hvrfp")
                {
					Settings.firepower[1] = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 3);
				}
				else if (array2[0] == "hvrll")
                {
					Settings.laserLock[1] = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "jeten")
                {
					Settings.jetAllowed = array2[1] == "1";
				}
				else if (array2[0] == "jethd")
                {
					Settings.jetHDrag = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.0005f, 0.1f);
				}
				else if (array2[0] == "jetd")
                {
					Settings.jetDrag = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.0005f, 0.1f);
				}
				else if (array2[0] == "jets")
                {
					Settings.jetSteer = (int)Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 1f, 100f);
				}
				else if (array2[0] == "jetl")
                {
					Settings.jetLift = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.01f, 10f);
				}
				else if (array2[0] == "jetss")
                {
					Settings.jetStall = (int)Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0.1f, 100f);
				}
				else if (array2[0] == "jetfp")
                {
					Settings.firepower[3] = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 3);
				}
				else if (array2[0] == "jetll")
                {
					Settings.laserLock[3] = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 1f);
				}
				else if (array2[0] == "netm")
                {
					Settings.networkMode = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 2);
				}
				else if (array2[0] == "netph")
                {
					Settings.networkPhysics = Mathf.Clamp(UnityBuiltins.parseInt(array2[1]), 0, 2);
				}
				else if (array2[0] == "netin")
                {
					Settings.networkInterpolation = Mathf.Clamp(UnityBuiltins.parseFloat(array2[1]), 0f, 0.5f);
				}
			}
			Settings.updatePrefs();
		}
	}

	[RPC]
	public void sH()
    {
		isHost = true;
	}

	public void sSHS()
    {
		networkView.RPC("sSH", RPCMode.Server, serverName, "url=" + WorldDesc.url + ";nme=" + WorldDesc.name, Settings.serverWelcome, Settings.bannedIPs, serverPassword, GameData.gameVersion, serverHidden);
	}

	[RPC]
	public void sSH(string sname, string sworld, string swelcome, string sblacklist, string spassword, float gVersion, bool shidden, NetworkMessageInfo info)
    {
		serverName = sname;
		Settings.serverWelcome = swelcome;
		Settings.bannedIPs = sblacklist;
		serverPassword = spassword;
		serverHidden = shidden;
		string url = string.Empty;
		int num = 0;
		string text = "0";
		string[] array = sworld.Split(";"[0]);
		int i = 0;
		string[] array2 = array;
		for (int length = array2.Length; i < length; i = checked(i + 1))
        {
			if (!(array2[i] == string.Empty))
            {
				string[] array3 = array2[i].Split("="[0]);
				if (array3[0] == "url")
                {
					url = array3[1];
				}
				else if (array3[0] == "nme")
                {
					WorldDesc.name = array3[1];
				}
			}
		}
		WorldDesc.url = url;
	}

	[RPC]
	public void sSB(string sblacklist)
    {
		Settings.bannedIPs = sblacklist;
	}

	[RPC]
	public void dN(int rsn)
    {
		netKillMode = rsn;
	}

	[RPC]
	public void msg(string str, int origin)
    {
		ChatEntry chatEntry = new ChatEntry();
		chatEntry.text = str;
		chatEntry.origin = (chatOrigins)origin;
		Messaging.entries.Add(chatEntry);
		if (Messaging.entries.Count > 50)
        {
			Messaging.entries.RemoveAt(0);
		}
		Messaging.scrollPosition.y = 1000000f;
	}

	[RPC]
	public void lW(string str)
    {
		string url = string.Empty;
		int num = 0;
		string text = "0";
		string[] array = str.Split(";"[0]);
		int i = 0;
		string[] array2 = array;
		for (int length = array2.Length; i < length; i = checked(i + 1))
        {
			if (!(array2[i] == string.Empty))
            {
				string[] array3 = array2[i].Split("="[0]);
				if (array3[0] == "url")
                {
					url = array3[1];
				}
			}
		}
		WorldDesc.url = url;
		LoadWorld();
	}

	[RPC]
	public void lMI(NetworkPlayer p, string n)
    {
		if (!isHost)
        {
			return;
		}
		for (int i = 0; i < unauthPlayers.Count; i = checked(i + 1))
        {
			if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(unauthPlayers[i], "p"), "externalIP"), p.externalIP) && RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(unauthPlayers[i], "n"), n))
            {
				RuntimeServices.SetProperty(unauthPlayers[i], "t", Time.time);
				return;
			}
		}
		unauthPlayers.Add(new unauthPlayer(p, n, Time.time));
	}

	public void LoadWorld()
    {
		whirldIn.url = WorldDesc.url;
		whirldIn.Load();
	}

	public static string LanguageFilter(string str)
    {
		string pattern = " crap | prawn |d4mn| damn | turd ";
		str = Regex.Replace(str, pattern, ".", RegexOptions.IgnoreCase);
		string pattern2 = "anus|ash0le|ash0les|asholes| ass |Ass Monkey|Assface|assh0le|assh0lez|bastard|bastards|bastardz|basterd|suka|asshole|assholes|assholz|asswipe|azzhole|bassterds|basterdz|Biatch|bitch|bitches|Blow Job|blowjob|in bed|butthole|buttwipe|c0ck|c0cks|c0k|Clit|cnts|cntz|cockhead| cock |cock-head|CockSucker|cock-sucker| cum |cunt|cunts|cuntz|dick|dild0|dild0s|dildo|dildos|dilld0|dilld0s|dominatricks|dominatrics|dominatrix|f.u.c.k|f u c k|f u c k e r|fag|fag1t|faget|fagg1t|faggit|faggot|fagit|fags|fagz|faig|faigs|fuck|fucker|fuckin|mother fucker|fucking|fucks|Fudge Packer|fuk|Fukah|Fuken|fuker|Fukin|Fukk|Fukkah|Fukken|Fukker|Fukkin|gay|gayboy|gaygirl|gays|gayz|God-dam|God dam|h00r|h0ar|h0re|jackoff|jerk-off|jizz|kunt|kunts|kuntz|Lesbian|Lezzian|Lipshits|Lipshitz|masochist|masokist|massterbait|masstrbait|masstrbate|masterbaiter|masterbate|masterbates|Motha Fucker|Motha Fuker|Motha Fukkah|Motha Fukker|Mother Fucker|Mother Fukah|Mother Fuker|Mother Fukkah|Mother Fukker|mother-fucker|Mutha Fucker|Mutha Fukah|Mutha Fuker|Mutha Fukkah|Mutha Fukker|orafis|orgasim|orgasm|orgasum|oriface|orifice|orifiss|packi|packie|packy|paki|pakie|peeenus|peeenusss|peenus|peinus|pen1s|penas|penis|penis-breath|penus|penuus|Phuc|Phuck|Phuk|Phuker|Phukker|polac|polack|polak|Poonani|pr1c|pr1ck|pr1k|pusse|pussee|pussy|puuke|puuker|queer|queers|queerz|qweers|qweerz|qweir|recktum|rectum|retard|sadist|scank|schlong|screwing| sex |sh1t|sh1ter|sh1ts|sh1tter|sh1tz|shit|shits|shitter|Shitty|Shity|shitz|Shyt|Shyte|Shytty|Shyty|skanck|skank|skankee| sob |skankey|skanks|Skanky|slut|sluts|Slutty|slutz|son-of-a-bitch|va1jina|vag1na|vagiina|vagina|vaj1na|vajina|vullva|vulva|xxx|b!+ch|bitch|blowjob|clit|arschloch|fuck|shit|asshole|b!tch|b17ch|b1tch|bastard|bi+ch|boiolas|buceta|c0ck|cawk|chink|clits|cunt|dildo|dirsa|ejakulate|fatass|fcuk|fuk|fux0r|l3itch|lesbian|masturbate|masterbat*|motherfucker|s.o.b.|mofo|nigga|nigger|n1gr|nigur|niiger|niigr|nutsack|phuck|blue balls|blue_balls|blueballs|pussy|scrotum|shemale|sh!t|slut|smut|teets|tits|boobs|b00bs|testical|testicle|titt|jackoff|whoar|whore|fuck|shit|arse|bi7ch|bitch|bollock|breasts|cunt|dick|fag |feces|fuk|futkretzn|gay|jizz|masturbat*|piss|poop|porn|p0rn|pr0n|shiz|splooge|b00b|testicle|titt|wank";
		return Regex.Replace(str, pattern2, "#", RegexOptions.IgnoreCase);
	}
}
