using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Boo.Lang;
using Boo.Lang.Runtime;
using CompilerGenerated;
using UnityEngine;
using UnityScript.Lang;
using System.Threading;

[Serializable]
public class Lobby : MonoBehaviour
{
	[NonSerialized]
	public int port;

	private int GuiAnimate;
	[NonSerialized]
	public float GUIAlpha;
	[NonSerialized]
	public float GUIHide;
	private string gameName;
	private float timeoutHostList;
    private float lastHostListRequest;
	private float lastHostListRefresh;
	private float hostListRefreshTimeout;

	private ConnectionTesterStatus natCapable;
	private bool NATHosts;
	private bool probingPublicIP;
	private bool doneTesting;
	private float timer;
	private bool filterNATHosts;
	private bool forceNAT;
	private bool hideTest;
	private string masterServerMessage;
	private int masterServerConFailures;
	private string testMessage;
	private string[] netConIP;
	private int netConPort;
	private int netConAttempts;
	private bool useMasterServer;
	private bool disableMasterServer;
	private bool doNetworking;
	[NonSerialized]
	public string userName;
	[NonSerialized]
	public string userCode;
	private bool userIsRegistered;
	private string remoteIP;
	private string userNameTemp;
	private string userPassword;
	private bool userRemembered;
	private string userAuthenticating;
	[NonSerialized]
	public Rect windowRect;
	[NonSerialized]
	public string temp;
	private Vector2 scrollPosition;
	private string outdated;
	private bool hostRegistered;
	private string serverLevel;
	private bool showSettings;
	private bool autoHostListRefresh;
	public bool useAlternateServer;
	[NonSerialized]
	public string[] messages;
	private string listServerIP;
	private int listServerPort;
	private string backupServerIP;
	private int backupServerPort;
	private float buttonHeight;
	private float buttonHeightTarget;
	private bool mouseInServerList;
	private string serverDetails;
	private float serverDetailsBoxAlpha;
	public GUISkin Skin;
	public GUIStyle serverDetailsBox;
	public LobbyDecor lobbyDecor;
	public int contentWidth;

	private bool hostDedicated;
	private string[] dedicatedIP;
	private int dedicatedPort;
	private bool dedicatedNAT;
	private int dedicatedHostAttempts;

    private bool showAds;
    adDesc[] gameAds;
    class adDesc : UnityEngine.Object
    {
        public String url = "";
        public String title = "";
        public String desc = "";

        public adDesc(String u, String t, String d)
        {
            this.url = u;
            this.title = t;
            this.desc = d;
        }
    }

	public Lobby()
	{
		port = 2500;

		GuiAnimate = -1;
		GUIAlpha = 0f;
		GUIHide = 0f;
		gameName = "marsxplr";
		timeoutHostList = 0.0f;
		lastHostListRequest = -1000.0f;
		lastHostListRefresh = -1000.0f;
		hostListRefreshTimeout = 5.0f;

		natCapable = ConnectionTesterStatus.Undetermined;
		NATHosts = false;
		probingPublicIP = false;
		doneTesting = false;
		timer = 0.0f;
		filterNATHosts = false;
		forceNAT = false;
		hideTest = false;
		masterServerMessage = "";
		masterServerConFailures = 0;
		testMessage = "";
		netConAttempts = 0;
		useMasterServer = true;
		disableMasterServer = false;
		doNetworking = true;
		userName = string.Empty;
		userCode = string.Empty;
		userIsRegistered = false;
		userNameTemp = string.Empty;
		userPassword = string.Empty;
		userRemembered = false;
		userAuthenticating = string.Empty;
		temp = string.Empty;
		outdated = string.Empty;
		hostRegistered = false;
		serverLevel = string.Empty;
		showSettings = false;
		autoHostListRefresh = true;
		useAlternateServer = false;
		listServerIP = string.Empty;
		backupServerPort = 23456;
		serverDetails = string.Empty;
		contentWidth = 0;

		hostDedicated = false;

        showAds = false;
	}

	public void Awake()
	{
		QualitySettings.currentLevel = QualityLevel.Fantastic;
		Screen.lockCursor = false;
		Application.runInBackground = false;
	}

    /*[Serializable]
    [CompilerGenerated]
    internal sealed class Start_0024101 : GenericGenerator<WWW>
    {
        [Serializable]
        [CompilerGenerated]
        private sealed class _0024 : GenericGeneratorEnumerator<WWW>, IEnumerator
        {
            internal UnityScript.Lang.Array _0024msgs_0024506;

            internal UnityScript.Lang.Array _0024wlds_0024507;

            internal WWW _0024www_0024508;

            internal string[] _0024data_0024509;

            internal string[] _0024val_0024510;

            internal string _0024dat_0024511;

            internal string _0024nme_0024512;

            internal string _0024url_0024513;

            internal bool _0024featured_0024514;

            internal string[] _0024wrld_0024515;

            internal string _0024str_0024516;

            internal string[] _0024vals_0024517;

            internal string[] _0024ipStr_0024518;

            internal int _0024___temp166_0024519;

            internal string[] _0024___temp167_0024520;

            internal int _0024___temp168_0024521;

            internal int _0024___temp170_0024522;

            internal string[] _0024___temp171_0024523;

            internal int _0024___temp172_0024524;

            internal Lobby _0024self_525;

            public _0024(Lobby self_)
            {
                _0024self_525 = self_;
            }

            public override bool MoveNext()
            {
                checked
                {
                    switch (_state)
                    {
                    default:
                        _0024self_525.userPassword = PlayerPrefs.GetString("userPassword", string.Empty);
                        _0024self_525.userCode = PlayerPrefs.GetString("userCode", string.Empty);
                        _0024self_525.userRemembered = PlayerPrefs.GetInt("userRemembered", 0) == 1;
                        _0024self_525.userIsRegistered = PlayerPrefs.GetInt("userRegistered", 0) == 1;
                        GameData.masterBlacklist = string.Empty;
                        if (GameData.userName != string.Empty)
                        {
                            _0024self_525.userName = GameData.userName;
                            if (_0024self_525.userRemembered)
                            {
                                _0024self_525.userNameTemp = _0024self_525.userName;
                            }
                        }
                        else if (_0024self_525.userPassword != string.Empty)
                        {
                            _0024self_525.userNameTemp = PlayerPrefs.GetString("userName", string.Empty);
                            _0024self_525.StartCoroutine_Auto(_0024self_525.authenticateUser());
                        }
                        else
                        {
                            _0024self_525.userNameTemp = PlayerPrefs.GetString("userName", string.Empty);
                            _0024self_525.userName = _0024self_525.userNameTemp + ((!(_0024self_525.userNameTemp != string.Empty)) ? string.Empty : "–");
                        }
                        _0024msgs_0024506 = new UnityScript.Lang.Array();
                        _0024wlds_0024507 = new UnityScript.Lang.Array();
                        _0024www_0024508 = new WWW("http://73.189.4.24/upd3");
                        return Yield(2, _0024www_0024508);
                    case 2:
                        if (_0024www_0024508.error == null)
                        {
                            _0024data_0024509 = _0024www_0024508.data.Split("\n"[0]);
                            _0024val_0024510 = null;
                            _0024___temp170_0024522 = 0;
                            _0024___temp171_0024523 = _0024data_0024509;
                            for (_0024___temp172_0024524 = _0024___temp171_0024523.Length; _0024___temp170_0024522 < _0024___temp172_0024524; _0024___temp170_0024522++)
                            {
                                if (!(_0024___temp171_0024523[_0024___temp170_0024522] == string.Empty))
                                {
                                    _0024val_0024510 = _0024___temp171_0024523[_0024___temp170_0024522].Split("="[0]);
                                    if (_0024val_0024510[0] == "v" && float.Parse(_0024val_0024510[1], CultureInfo.InvariantCulture.NumberFormat) > UnityBuiltins.parseFloat(GameData.gameVersion))
                                    {
                                        _0024self_525.outdated = _0024val_0024510[1];
                                    }
                                    else if (_0024val_0024510[0] == "d")
                                    {
                                        Lobby lobby = _0024self_525;
                                        bool num = _0024val_0024510[1] == "1";
                                        if (!num)
                                        {
                                            num = _0024val_0024510[1] == "true";
                                        }
                                        lobby.hostDedicated = num;
                                    }
                                    else if (_0024val_0024510[0] == "m")
                                    {
                                        _0024msgs_0024506.Add(_0024val_0024510[1]);
                                    }
                                    else if (_0024val_0024510[0] == "w")
                                    {
                                        _0024nme_0024512 = string.Empty;
                                        _0024url_0024513 = string.Empty;
                                        _0024featured_0024514 = false;
                                        _0024wrld_0024515 = _0024val_0024510[1].Split(";"[0]);
                                        _0024___temp166_0024519 = 0;
                                        _0024___temp167_0024520 = _0024wrld_0024515;
                                        for (_0024___temp168_0024521 = _0024___temp167_0024520.Length; _0024___temp166_0024519 < _0024___temp168_0024521; _0024___temp166_0024519++)
                                        {
                                            if (!(_0024___temp167_0024520[_0024___temp166_0024519] == string.Empty))
                                            {
                                                if (_0024___temp167_0024520[_0024___temp166_0024519] == "featured")
                                                {
                                                    _0024featured_0024514 = true;
                                                }
                                                else
                                                {
                                                    _0024vals_0024517 = _0024___temp167_0024520[_0024___temp166_0024519].Split(":"[0]);
                                                    if (_0024vals_0024517[0] == "nme")
                                                    {
                                                        _0024nme_0024512 = _0024vals_0024517[1];
                                                    }
                                                    else if (_0024vals_0024517[0] == "url")
                                                    {
                                                        _0024url_0024513 = _0024___temp167_0024520[_0024___temp166_0024519].Substring(4);
                                                    }
                                                }
                                            }
                                        }
                                        _0024wlds_0024507.Add(new GameWorldDesc(_0024nme_0024512, _0024url_0024513, _0024featured_0024514));
                                    }
                                    else if ((_0024val_0024510[0] == "s" && !_0024self_525.useAlternateServer) || (_0024val_0024510[0] == "s2" && _0024self_525.useAlternateServer))
                                    {
                                        _0024ipStr_0024518 = _0024val_0024510[1].Split(":"[0]);
                                        _0024self_525.listServerIP = _0024ipStr_0024518[0];
                                        _0024self_525.listServerPort = UnityBuiltins.parseInt(_0024ipStr_0024518[1]);
                                        MasterServer.ipAddress = _0024self_525.listServerIP;
                                        MasterServer.port = _0024self_525.listServerPort;
                                    }
                                    else if ((_0024val_0024510[0] == "f" && !_0024self_525.useAlternateServer) || (_0024val_0024510[0] == "f2" && _0024self_525.useAlternateServer))
                                    {
                                        _0024ipStr_0024518 = _0024val_0024510[1].Split(":"[0]);
                                        Network.natFacilitatorIP = _0024ipStr_0024518[0];
                                        Network.natFacilitatorPort = UnityBuiltins.parseInt(_0024ipStr_0024518[1]);
                                    }
                                    else if ((_0024val_0024510[0] == "t" && !_0024self_525.useAlternateServer) || (_0024val_0024510[0] == "t2" && _0024self_525.useAlternateServer))
                                    {
                                        _0024ipStr_0024518 = _0024val_0024510[1].Split(":"[0]);
                                        Network.connectionTesterIP = _0024ipStr_0024518[0];
                                        Network.connectionTesterPort = UnityBuiltins.parseInt(_0024ipStr_0024518[1]);
                                    }
                                    else if (_0024val_0024510[0] == "b")
                                    {
                                        GameData.masterBlacklist += ((!(GameData.masterBlacklist != string.Empty)) ? string.Empty : "\n") + _0024val_0024510[1];
                                    }
                                    else if (_0024val_0024510[0] == "n")
                                    {
                                        GameData.networkMode = UnityBuiltins.parseInt(_0024val_0024510[1]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            GameData.errorMessage = "Alert: Update server is unreachable.\nIf this computer is online, the update server may be down.\n\nYou need to be connected to the internet to play Mars Explorer.\n\nPlease check MarsXPLR.com for news & updates!";
                        }
                        GameData.gameWorlds = (GameWorldDesc[])_0024wlds_0024507.ToBuiltin(typeof(GameWorldDesc));
                        _0024self_525.messages = (string[])_0024msgs_0024506.ToBuiltin(typeof(string));
                        MasterServer.RequestHostList(_0024self_525.gameName);
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

        internal Lobby _0024self_526;

        public Start_0024101(Lobby self_)
        {
            _0024self_526 = self_;
        }

        public override IEnumerator<WWW> GetEnumerator()
        {
            return new _0024(_0024self_526);
        }
    }*/

    public IEnumerator<WWW> Start()
    {
        String adBrightUrl = "";
        String adSenseUrl = "";

        userPassword = PlayerPrefs.GetString("userPassword", "");
        userCode = PlayerPrefs.GetString("userCode", "");
        userRemembered = (PlayerPrefs.GetInt("userRemembered", 0) == 1 ? true : false);
        userIsRegistered = (PlayerPrefs.GetInt("userRegistered", 0) == 1 ? true : false);
        GameData.masterBlacklist = "";

        if (GameData.userName != "") //They just left a server
        {
            userName = GameData.userName;
            if (userRemembered) userNameTemp = userName;
        }
        else if (userPassword != "")
        {
            userNameTemp = PlayerPrefs.GetString("userName", "");
            authenticateUser(); //Attempt Auto Login
        }
        else
        {
            userNameTemp = PlayerPrefs.GetString("userName", "");
            userName = userNameTemp + (userNameTemp != "" ? "–" : "");
        }

        List<String> msgs = new List<String>();
        List<GameWorldDesc> wlds = new List<GameWorldDesc>();
        WWW www = new WWW("http://73.189.4.24/upd3");
        yield return www;
        if (www.error == null)
        {
            String[] data = www.data.Split("\n"[0]);
            foreach (String dat in data)
            {
                if (dat == "") continue;

                int pos = dat.IndexOf("="[0]);
                if (pos == 0 || pos == -1) continue;
                List<String> tmp = new List<String>(new String[]{
                    dat.Substring(0, pos),
                    dat.Substring(pos + 1)});
                String[] val = tmp.ToArray();

                if (
                    val[0] == "v" &&
                    float.Parse(
                        val[1],
                        CultureInfo.InvariantCulture.NumberFormat
                    ) > (float)GameData.gameVersion)
                {
                    outdated = val[1];
                }
                else if (val[0] == "d")
                {
                    hostDedicated = (val[1] == "1" || val[1] == "true");
                }
                else if (val[0] == "a")
                {
                    showAds = (val[1] == "1" || val[1] == "true");
                }
                else if (val[0] == "m") msgs.Add(val[1]);
                else if (val[0] == "w")
                {
                    String nme = "";
                    String url = "";
                    bool featured = false;
                    String[] wrld = val[1].Split(";"[0]);
                    foreach (String str in wrld)
                    {
                        if (str == "") continue;
                        if (str == "featured")
                        {
                            featured = true;
                            continue;
                        }
                        String[] vals = str.Split(":"[0]);
                        if (vals[0] == "nme") nme = vals[1];
                        else if (vals[0] == "url") url = str.Substring(4);
                    }
                    wlds.Add(new GameWorldDesc(nme, url, featured));
                }
                else if (
                    (val[0] == "s" && !useAlternateServer) ||
                    (val[0] == "s2" && useAlternateServer))
                {
                    String[] ipStr = val[1].Split(":"[0]);
                    listServerIP = ipStr[0];
                    listServerPort = int.Parse(ipStr[1]);
                    MasterServer.ipAddress = listServerIP;
                    MasterServer.port = listServerPort;
                }
                else if (
                    (val[0] == "f" && !useAlternateServer) ||
                    (val[0] == "f2" && useAlternateServer))
                {
                    String[] ipStr = val[1].Split(":"[0]);
                    Network.natFacilitatorIP = ipStr[0];
                    Network.natFacilitatorPort = int.Parse(ipStr[1]);
                }
                else if (
                    (val[0] == "t" && !useAlternateServer) ||
                    (val[0] == "t2" && useAlternateServer))
                {
                    String[] ipStr = val[1].Split(":"[0]);
                    Network.connectionTesterIP = ipStr[0];
                    Network.connectionTesterPort = int.Parse(ipStr[1]);
                }
                else if (val[0] == "b")
                {
                    GameData.masterBlacklist +=
                        (GameData.masterBlacklist != "" ? "\n" : "") +
                        val[1];
                }
                else if (val[0] == "n")
                {
                    GameData.networkMode = int.Parse(val[1]);
                }
                else if (val[0] == "adbr")
                {
                    adBrightUrl = val[1];
                }
                else if (val[0] == "adsn")
                {
                    adSenseUrl = val[1];
                }
            }
        }
        else
        {
            GameData.errorMessage = "Alert: Update server is unreachable.\nIf this computer is online, the update server may be down.\n\nYou need to be connected to the internet to play Mars Explorer.\n\nPlease check MarsXPLR.com for news & updates!";
        }

        GameData.gameWorlds = wlds.ToArray();
        messages = msgs.ToArray();

        MasterServer.RequestHostList(gameName);

        if (showAds)
        { 
            List<adDesc> ads = new List<adDesc>();

            //Adbrite
            www = new WWW(adBrightUrl);
            yield return www;
            if (www.error == null)
            {
                MatchCollection matches = Regex.Matches(
                    www.data.Replace(
                        "\\\"",
                        "\""),
                    "<a[^>]*?class=\\\"adHeadline\\\"[^>]*?href=\\\"(.*?)\\\"[^>]*?>(.*?)</a>[^.]*?<a[^>]*?class=\\\"adText\\\"[^>]*?>(.*?)</a>");
                foreach (Match match in matches)
                {
                    adDesc ad = new adDesc(
                        match.Groups[1].ToString(),
                        htmlDecode(match.Groups[2].ToString()),
                        htmlDecode(match.Groups[3].ToString()));
                    if (UnityEngine.Random.value > .5) ads.Add(ad);
                    else ads.Insert(0, ad);
                }
            }

            //Adsense
            www = new WWW(adSenseUrl);
            yield return www;
            if (www.error == null)
            {
                MatchCollection matches = Regex.Matches(
                    www.data.Replace(
                        "\\\"",
                        "\""),
                    "<a[^>]*?class=adt[^>]*?href=\\\"(.*?)\\\"[^>]*?>(.*?)</a>[^.]*?<div[^>]*?class=adb[^>]*?>(.*?)</div>");
                foreach (Match match in matches)
                {
                    adDesc ad = new adDesc(
                        "http://googleads.g.doubleclick.net" +
                            match.Groups[1].ToString(),
                        htmlDecode(match.Groups[2].ToString()),
                        htmlDecode(match.Groups[3].ToString()));
                    if (ad.url.IndexOf("&nh=1") == -1) ad.url += "&nh=1";
                    ads.Insert(0, ad);
                }
            }

            //Tally
            if (ads.Count > 0)
            {
                gameAds = ads.ToArray();
            }
        }
    }
    
	public void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		checked
		{
			if (info != NetworkConnectionError.CreateSocketOrThreadFailure || !(masterServerMessage != string.Empty))
			{
				MasterServer.ClearHostList();
				masterServerMessage = " Master server connection failure: " + info;
				masterServerConFailures++;
			}
		}
	}

	public void OnFailedToConnect(NetworkConnectionError info)
	{
		checked
		{
			if (!Network.isClient)
			{
				object rhs;
				switch (info)
				{
				case NetworkConnectionError.ConnectionFailed:
					rhs = "You may be blocked by a network firewall.\n(See \"How Do I configure My Router\" on the FAQ @ MarsXPLR.com)\n";
					break;
				case NetworkConnectionError.NATTargetNotConnected:
					rhs = "\nThe person hosting the game you were connecting to\nmay have stopped playing Mars Explorer.";
					break;
				default:
					rhs = info;
					break;
				}
				GameData.errorMessage = "Game could not be connected to.\nPlease try joining a different game!\n\n\n" + rhs;
				if (netConAttempts < 2)
				{
					Network.Connect(netConIP, netConPort);
					netConAttempts++;
					GameData.errorMessage += "\n\n...Reattempting Connection - Attempt # " + netConAttempts + "...";
				}
				else
				{
					GameData.errorMessage += "\n\nReconnection Attempt Failed.";
				}
			}
		}
	}

	public void OnConnectedToServer()
	{
		GameData.errorMessage = string.Empty;
		Network.isMessageQueueRunning = false;
		GameData.userName = userName;
		GameData.userCode = userCode;
		StartCoroutine_Auto(LoadGame());
	}

	public void OnGUI()
	{
		GUI.skin = Skin;
		float gUIAlpha = GUIAlpha;
		Color color = GUI.color;
		float num = (color.a = gUIAlpha);
		Color color2 = (GUI.color = color);
		checked
		{
			if ((int)QualitySettings.currentLevel < 3)
			{
				QualitySettings.currentLevel = QualityLevel.Good;
			}
			if (!(Time.time > 4.25f))
			{
				return;
			}
			if (GuiAnimate == 1)
			{
				if (!(GUIAlpha > 0f))
				{
					GUIAlpha = 0f;
					GuiAnimate = 0;
				}
				else
				{
					GUIAlpha -= Time.deltaTime * 0.35f;
				}
				if (GUIHide > 1f)
				{
					GUIHide = 1f;
				}
				else
				{
					GUIHide += Time.deltaTime * 0.5f;
				}
			}
			else if (GuiAnimate == -1)
			{
				GUIAlpha = 0f;
				GuiAnimate = -2;
			}
			else if (GuiAnimate == -2)
			{
				if (!(GUIAlpha < 1f))
				{
					GUIAlpha = 1f;
					GuiAnimate = 0;
				}
				else
				{
					GUIAlpha += Time.deltaTime * 0.2f;
				}
			}
			float num2 = (float)Screen.height * 1.2f;
			if (num2 > 800f)
			{
				num2 -= (num2 - 800f) * 0.5f;
			}
			if (num2 > 1200f)
			{
				num2 = 1200f;
			}
			if (num2 > (float)(Screen.width - 30))
			{
				num2 = Screen.width - 30;
			}
			if (num2 < 600f)
			{
				num2 = 600f;
			}
			contentWidth = (int)num2;
			if (GameData.errorMessage != string.Empty)
			{
				GUILayout.Window(1, new Rect((float)Screen.width * 0.5f - num2 / 2f + 50f, lobbyDecor.logoOffset - 20, num2 - 100f, 300f), errorWindow, string.Empty, "windowAlert");
				GUI.BringWindowToFront(1);
			}
			if (outdated == string.Empty)
			{
				GUILayout.Window(0, new Rect((float)Screen.width * 0.5f - num2 / 2f - 50f, lobbyDecor.logoOffset - 70, num2 + 100f, ((!doNetworking || !(userName != string.Empty)) ? 320 : (Screen.height - lobbyDecor.logoOffset + 10)) + 100), MakeWindow, string.Empty, "windowChromeless");
			}
			else
			{
				GUILayout.Window(0, new Rect((float)Screen.width * 0.5f - num2 / 2f, lobbyDecor.logoOffset - 20, num2, 150f), makeWindowUpdate, string.Empty, "windowChromeless");
			}
			GUI.FocusWindow(0);
			if (serverDetails != string.Empty && GuiAnimate != 1 && GUIAlpha != 0f && GameData.errorMessage == string.Empty)
			{
				if (serverDetailsBoxAlpha < 0.99f)
				{
					serverDetailsBoxAlpha += Time.deltaTime * 0.5f;
				}
				else
				{
					serverDetailsBoxAlpha = 1f;
				}
			}
			else if (serverDetailsBoxAlpha > 0.01f)
			{
				serverDetailsBoxAlpha -= Time.deltaTime * 0.5f;
			}
			else
			{
				serverDetailsBoxAlpha = 0f;
			}
			if (serverDetailsBoxAlpha > 0.01f)
			{
				float a = serverDetailsBoxAlpha;
				Color color4 = GUI.color;
				float num3 = (color4.a = a);
				Color color5 = (GUI.color = color4);
				GUIStyle style = GUI.skin.GetStyle("serverDetailsBox");
				GUILayout.Window(2, new Rect(Input.mousePosition.x - style.fixedWidth - 1f, (float)Screen.height - Input.mousePosition.y - style.fixedHeight - 4f, style.fixedWidth, style.fixedHeight), MethodToWindowFunction.Adapt(serverDetailsWindow), string.Empty, "serverDetailsBox");
				GUI.BringWindowToFront(2);
			}
		}
	}

	public void serverDetailsWindow()
	{
		GUI.contentColor = GUI.skin.GetStyle("serverDetailsBox").normal.textColor;
		GUILayout.Label(serverDetails);
	}

    public IEnumerator TestConnection(bool force)
    {
        disableMasterServer = false;
        probingPublicIP = false;
        if (timer == null) timer = Time.time + 15;
        testMessage = "testing";
        doneTesting = false;
        doNetworking = false;

        natCapable = Network.TestConnection(force);
        while (natCapable == ConnectionTesterStatus.Undetermined)
        {
            natCapable = Network.TestConnection();
            yield return null;
            Thread.Sleep(500);
        }

        switch (natCapable)
        { 
            case ConnectionTesterStatus.Error:
                GameData.errorMessage = "Your computer does not appear to be online...\nNetworking was defaulted to Local Area Network mode.\n\n(You can play with friends over a LAN, but not with people all around the world over the internet)";
                testMessage = "Computer is Offline.";
                doneTesting = true;
                doNetworking = true;
                useMasterServer = false;
                break;
            case ConnectionTesterStatus.PrivateIPNoNATPunchthrough:
                GameData.errorMessage = "You appear to have a private IP and no NAT punchthrough capability...\n\nHit \"Dismiss\" click \"Show Settings\", then click \"Retest Internet Connection\".\nIf you get this message again, read \"How Do I configure My Router\" on the FAQ @ MarsXPLR.com";
                testMessage = "Private IP with no NAT punchthrough - Local network, non internet games only.";
                //filterNATHosts = true;
                Network.useNat = true;
                doneTesting = true;
                doNetworking = true;
                useMasterServer = false;
                break;
            case ConnectionTesterStatus.PrivateIPHasNATPunchThrough:
                if (probingPublicIP)
                {
                    testMessage = "Non-connectable public IP address (port " + port + " blocked), NAT punchthrough can circumvent the firewall.";
                }
                else
                {
                    testMessage = "NAT punchthrough enabled.";
                }
                // NAT functionality is enabled in case a server is started,
                // clients should enable this based on if the host requires it
                Network.useNat = true;
                doneTesting = true;
                doNetworking = true;
                useMasterServer = true;
                break;
            case ConnectionTesterStatus.PublicIPIsConnectable:
                testMessage = "Directly connectable public IP address.";
                Network.useNat = false;
                doneTesting = true;
                doNetworking = true;
                useMasterServer = true;
                break;
            // This case is a bit special as we now need to check if we can 
            // cicrumvent the blocking by using NAT punchthrough
            case ConnectionTesterStatus.PublicIPPortBlocked:
                testMessage = "Non-connectible public IP address with NAT punchthrough disabled, running a server is impossible.\n\n(Please setup port forwarding for port # " + port + " in your router)";
                Network.useNat = false;
                // If no NAT punchthrough test has been performed on this public IP, force a test
                if (!probingPublicIP)
                {
                    Debug.Log("Testing if firewall can be circumvented");
                    natCapable = Network.TestConnectionNAT();
                    probingPublicIP = true;
                    timer = Time.time + 10;
                }
                // NAT punchthrough test was performed but we still get blocked
                else if (Time.time > timer)
                {
                    probingPublicIP = false;    // reset
                    Network.useNat = true;
                    doneTesting = true;
                    doNetworking = true;
                    useMasterServer = false;
                }
                break;
            case ConnectionTesterStatus.PublicIPNoServerStarted:
                testMessage = "Public IP address but server not initialized, it must be started to check server accessibility. Restart connection test when ready.";
                doNetworking = true;
                doneTesting = true;
                useMasterServer = true;
                break;
            default:
                testMessage = "Error in test routine, got " + natCapable;
                doNetworking = false;
                break;
        }
    }

	public void MakeWindow(int id)
	{
		if (!doNetworking)
		{
			GUILayout.FlexibleSpace();
			if (testMessage == "testing")
			{
				if (Time.time > timer)
				{
					GUILayout.Label("Network status could not be determined.");
				}
				else
				{
					GUILayout.Label("Determining your network's configuration... " + (timer - Time.time));
				}
				GUILayout.Space(10f);
			}
			else
			{
				GUILayout.Label("Unfortunatley, your computer's network settings will not allow Mars Explorer to network. Specific error was: " + testMessage);
			}
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical(GUILayout.Width(300f));
			if (GUILayout.Button("Restart Connection Test"))
			{
				timer = 0f;
				StartCoroutine_Auto(TestConnection(true));
			}
			if (GUILayout.Button("Force Enable Networking (May Not Work)"))
			{
				Network.useNat = true;
				doneTesting = true;
				doNetworking = true;
				useMasterServer = true;
				testMessage = "Network testing failed, networking force enabled";
			}
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		}
		else if (userName == string.Empty)
		{
			GUILayout.Space(50f);
			GUILayout.Label("Welcome, Space Cadet!\nPlease enter a pseudonym for others to know you by:");
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			GUILayout.Space((float)contentWidth / 3.5f);
			GUILayout.BeginScrollView(Vector2.zero);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Your Name:", GUILayout.Width(100f));
			GUI.SetNextControlName("Name");
			userNameTemp = Regex.Replace(GUILayout.TextField(userNameTemp, 25), "[^A-Za-z0-9-_.&<>]", string.Empty);
			GUILayout.EndHorizontal();
			if (userIsRegistered)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Your Password:", GUILayout.Width(100f));
				userPassword = GUILayout.PasswordField(userPassword, "*"[0]);
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal();
			GUILayout.Label(string.Empty, GUILayout.Width(100f));
			userIsRegistered = GUILayout.Toggle(userIsRegistered, "I am registered");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label(string.Empty, GUILayout.Width(100f));
			userRemembered = GUILayout.Toggle(userRemembered, "Remember Me");
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			GUILayout.Space((float)contentWidth / 3.5f);
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			GUILayout.Space(contentWidth / 4);
			if (userNameTemp != string.Empty)
			{
				if (userIsRegistered)
				{
					if (userPassword != string.Empty)
					{
						buttonHeightTarget = 40f;
						if (GUILayout.Button((!(userAuthenticating != string.Empty)) ? ">> Authenticate and Explore Mars! <<" : ((!(userAuthenticating == "true")) ? ("Authentication Failed: " + userAuthenticating + "\n>> Retry <<") : "...Authenticating..."), GUILayout.Height(buttonHeight)) || Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
						{
							StartCoroutine_Auto(authenticateUser());
						}
					}
					else
					{
						buttonHeightTarget = 0f;
						if (buttonHeight > 4f)
						{
							GUILayout.Button((!(userAuthenticating != string.Empty)) ? ">> Authenticate and Explore Mars! <<" : ((!(userAuthenticating == "true")) ? ("Authentication Failed: " + userAuthenticating + "\n>> Retry <<") : "...Authenticating..."), GUILayout.Height(buttonHeight));
						}
					}
				}
				else
				{
					buttonHeightTarget = 40f;
					if (GUILayout.Button(">> I Am Ready to Explore Mars! <<", GUILayout.Height(buttonHeight)) || Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
					{
						userName = Game.LanguageFilter(userNameTemp);
						if (userName.IndexOf("ADMIN") != -1 || userName.IndexOf("admin") != -1 || userName.IndexOf("Admin") != -1 || userName.IndexOf("aubrey") != -1 || userName.IndexOf("Aubrey") != -1 || userName.IndexOf("AUBREY") != -1 || userName.IndexOf("abrey") != -1 || userName.IndexOf("Abrey") != -1 || userName.IndexOf("aubry") != -1 || userName.IndexOf("Aubry") != -1 || userName.IndexOf("aubery") != -1 || userName.IndexOf("Aubery") != -1)
						{
							userName += " 2";
						}
						if (userCode == "{Code}")
						{
							userCode = string.Empty;
						}
						if (userRemembered)
						{
							PlayerPrefs.SetString("userName", userName);
							PlayerPrefs.SetString("userPassword", string.Empty);
							PlayerPrefs.SetInt("userRemembered", 1);
						}
						else
						{
							PlayerPrefs.SetString("userName", string.Empty);
							PlayerPrefs.SetString("userPassword", string.Empty);
							PlayerPrefs.SetInt("userRemembered", 0);
							userNameTemp = string.Empty;
							userPassword = string.Empty;
						}
						PlayerPrefs.SetInt("userRegistered", 0);
						userName += "–";
						lastHostListRefresh = -1f;
						lastHostListRequest = Time.time;
					}
				}
			}
			else
			{
				buttonHeightTarget = 0f;
				if (buttonHeight > 4f)
				{
					GUILayout.Button((!userIsRegistered) ? ">> I Am Ready to Explore Mars! <<" : ((!(userAuthenticating != string.Empty)) ? ">> Authenticate and Explore Mars! <<" : ((!(userAuthenticating == "true")) ? ("Authentication Failed: " + userAuthenticating + "\n>> Retry <<") : "...Authenticating...")), GUILayout.Height(buttonHeight));
				}
			}
			GUILayout.Space(contentWidth / 4);
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			if (userIsRegistered)
			{
				if (userPassword == string.Empty)
				{
					GUILayout.Label("(Make sure the credentials you enter match your MarsXPLR.com login)");
				}
			}
			else
			{
				GUILayout.BeginHorizontal();
				GUILayout.Space(contentWidth / 3);
				if (GUILayout.Button("Want to own your name?\n>> Register an account! <<"))
				{
					Application.OpenURL("http://MarsXPLR.com/user/register");
				}
				GUILayout.Space(contentWidth / 3);
				GUILayout.EndHorizontal();
			}
			GUILayout.FlexibleSpace();
			if (GUIUtility.keyboardControl == 0)
			{
				GUI.FocusControl("Name");
			}
			if (buttonHeightTarget == 0f)
			{
				buttonHeight -= buttonHeight * Time.deltaTime * 6f;
			}
			else
			{
				buttonHeight += (buttonHeightTarget - buttonHeight) * Time.deltaTime * 6f;
			}
		}
		else
		{
			GUILayout.Space(5f);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("<< Change Name: " + userName, GUILayout.Width(238f), GUILayout.Height(30f)))
			{
				userName = string.Empty;
			}
			GUILayout.FlexibleSpace();
			checked
			{
				if (Network.peerType == NetworkPeerType.Disconnected)
				{
					if (GUILayout.Button("Host Game >>", GUILayout.Width(238f), GUILayout.Height(30f)))
					{
						Network.Disconnect();
						if (hostDedicated && Extensions.get_length((System.Array)dedicatedIP) > 0)
						{
							bool num = dedicatedNAT;
							if (!num)
							{
								num = forceNAT;
							}
							Network.useNat = num;
							temp = Network.Connect(dedicatedIP, dedicatedPort) + string.Empty;
							netConIP = dedicatedIP;
							netConPort = dedicatedPort;
							netConAttempts = 1;
							dedicatedHostAttempts = 1;
							if (temp != "NoError")
							{
								GameData.errorMessage = "Dedicated server initialization failed: " + temp + "\n\nTo resolve this issue, please uncheck the\n\"Utilize dedicated servers for hosting games\"\n option in the Settings below.";
							}
							else
							{
								GameData.errorMessage = "...Initializing connection to dedicated server...\n(" + netConIP[0] + ":" + netConPort + ((!Network.useNat) ? string.Empty : " NAT") + ")\n\n\n\nIf this fails, please uncheck the\n\"Utilize dedicated servers for hosting games\"\n option in the Settings below.";
							}
						}
						else
						{
							Network.useNat = !Network.HavePublicAddress();
							port = 2500;
							NetworkConnectionError networkConnectionError;
							while (true)
							{
								networkConnectionError = Network.InitializeServer(9, port);
								if (port < 2600 && networkConnectionError != NetworkConnectionError.NoError)
								{
									port++;
									continue;
								}
								break;
							}
							if (networkConnectionError != NetworkConnectionError.NoError)
							{
								GameData.errorMessage = "\nCould not start server: " + networkConnectionError;
							}
							else
							{
								GameData.userName = userName;
								StartCoroutine_Auto(LoadGame());
							}
						}
					}
				}
				else
				{
					GUILayout.Button("Hosting Game...", GUILayout.Width(238f), GUILayout.Height(30f));
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(5f);
				scrollPosition = GUILayout.BeginScrollView(scrollPosition);
				string empty = string.Empty;
				if (Event.current.type != EventType.layout)
				{
					serverDetails = string.Empty;
				}
				int num2 = default(int);
				int num3 = default(int);
				int num4 = default(int);
				if (useMasterServer && !disableMasterServer)
				{
					num2 = default(int);
					num3 = default(int);
					num4 = default(int);
					dedicatedIP = (string[])new UnityScript.Lang.Array().ToBuiltin(typeof(string));
					HostData[] array = MasterServer.PollHostList();
					if (Extensions.get_length((System.Array)array) > 0)
					{
						System.Array.Sort(array, sortHostArray);
						int num5 = 0;
						int i = 0;
						HostData[] array2 = array;
						for (int length = array2.Length; i < length; i++)
						{
							masterServerConFailures = 0;
							masterServerMessage = string.Empty;
							empty = string.Empty;
							if (!filterNATHosts || !array2[i].useNat)
							{
								string[] array3 = array2[i].comment.Split(";"[0]);
								string[] array4 = null;
								float num6 = 0f;
								float num7 = 0f;
								string text = null;
								string text2 = null;
								string text3 = null;
								string text4 = string.Empty;
								string text5 = string.Empty;
								bool flag = default(bool);
								int j = 0;
								string[] array5 = array3;
								for (int length2 = array5.Length; j < length2; j++)
								{
									if (!(array5[j] == string.Empty))
									{
										array4 = array5[j].Split("="[0]);
										if (array4[0] == "v")
										{
											num6 = UnityBuiltins.parseFloat(array4[1]);
										}
										if (array4[0] == "d")
										{
											num7 = UnityBuiltins.parseFloat(array4[1]);
										}
										else if (array4[0] == "w")
										{
											text = array4[1];
										}
										else if (array4[0] == "p")
										{
											text2 = array4[1];
										}
										else if (array4[0] == "u")
										{
											text3 = array4[1];
										}
										else if (array4[0] == "b")
										{
											text4 = array4[1];
										}
										else if (array4[0] == "s")
										{
											text5 = array4[1];
										}
										else if (array4[0] == "l")
										{
											flag = true;
										}
									}
								}
								num2 += array2[i].connectedPlayers;
								if (num7 == GameData.serverVersion && array2[i].connectedPlayers == 0)
								{
									num4++;
									dedicatedIP = array2[i].ip;
									dedicatedPort = array2[i].port;
									dedicatedNAT = array2[i].useNat;
									continue;
								}
								if (GameData.gameVersion != num6)
								{
									continue;
								}
								num3 += array2[i].connectedPlayers;
								GUILayout.BeginHorizontal();
								GUILayout.Label(array2[i].connectedPlayers.ToString(), GUILayout.Width(40f));
								GUILayout.Label(array2[i].gameName);
								GUILayout.Label(text);
								if (num7 != 0f)
								{
									GUILayout.Label("»", GUILayout.Width(15f));
								}
								else
								{
									GUILayout.Label(" ", GUILayout.Width(15f));
								}
								if (array2[i].connectedPlayers == array2[i].playerLimit)
								{
									GUILayout.Label("Game Full", GUILayout.Width(150f));
								}
								else
								{
									bool flag2 = false;
									if (text4 != string.Empty)
									{
										string[] array6 = text4.Split(","[0]);
										int k = 0;
										string[] array7 = array6;
										for (int length3 = array7.Length; k < length3; k++)
										{
											if (array7[k] == Network.player.ipAddress)
											{
												flag2 = true;
												break;
											}
										}
									}
									if (GUILayout.Button(flag2 ? "(You Are Banned)" : ((!flag) ? "Join Game" : "(Password Protected)"), GUILayout.Width(170f)))
									{
										bool useNat = array2[i].useNat;
										if (!useNat)
										{
											useNat = forceNAT;
										}
										Network.useNat = useNat;
										temp = Network.Connect(array2[i].ip, array2[i].port) + string.Empty;
										netConIP = array2[i].ip;
										netConPort = array2[i].port;
										netConAttempts = 1;
										if (temp != "NoError")
										{
											GameData.errorMessage = "Cound not join game: " + temp + string.Empty;
										}
										else
										{
											GameData.errorMessage = "...Connecting to Game...\n(" + array2[i].ip[0] + ":" + array2[i].port + ((!array2[i].useNat && !forceNAT) ? string.Empty : " NAT") + ")\n\n\n\nPlay safe! Don't share personal information online,\nand don't trust anyone who asks you for it.";
										}
									}
								}
								GUILayout.EndHorizontal();
								if (Event.current.type != EventType.layout && mouseInServerList && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
								{
									serverDetails = ((array2[i].connectedPlayers <= 0) ? string.Empty : array2[i].connectedPlayers.ToString()) + ((text2 == null) ? string.Empty : (" Players: " + text2.Replace(",", ", "))) + ((!(text5 != string.Empty)) ? string.Empty : ("\nAvg Ping: " + text5 + " ms")) + "\n" + array2[i].ip[0] + ":" + array2[i].port + ((!array2[i].useNat) ? string.Empty : " NAT") + ((num7 == 0f) ? string.Empty : " (» Dedicated Host Server)");
								}
							}
							num5++;
						}
					}
					if (num3 == 0)
					{
						if (Time.time < lastHostListRefresh + hostListRefreshTimeout)
						{
							GUILayout.Label("\n\n\nLoading Server List...\n" + (hostListRefreshTimeout + lastHostListRefresh - Time.time));
						}
						else
						{
							GUILayout.Label("\n\n\nNo active games could be found.\nPress \"Host Game >>\" above to start your own!\n");
							GUILayout.Label((!(userCode != string.Empty)) ? ("You are running Mars Explorer version " + GameData.gameVersion + ",\nand will only see games hosted by others using this version.") : ("You are viewing only games with the secret code \"" + userCode + "\".\n(Press \"<< Change Name\" above to edit this code)"));
							GUILayout.Label((!autoHostListRefresh) ? "\n(Press the \"Refresh List\" button in the Networking Settings panel to check for new games)" : "\n(This list refreshes automatically)");
						}
					}
					if (useAlternateServer)
					{
						GUILayout.Label("\n(You are using the backup list server, and will only see games of others doing the same)");
					}
					if (filterNATHosts)
					{
						GUILayout.Label("\n(All games requiring Network Address Translation have been hidden)");
					}
				}
				else
				{
					GUILayout.Label("\n\n" + ((!disableMasterServer) ? "Even if your computer" : "Your computer") + " isn't connected to the internet - don't worry!\nYou can still play Mars Explorer on your own, or with friends on your local network.\n\nIf a friend is already hosting a game, enter their IP address here:\n");
					if (remoteIP == null)
					{
						getRemoteIP();
					}
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.BeginVertical(GUILayout.Width(300f));
					GUILayout.BeginHorizontal();
					remoteIP = GUILayout.TextField(remoteIP);
					GUILayout.Space(5f);
					port = UnityBuiltins.parseInt(GUILayout.TextField(port + string.Empty, GUILayout.Width(60f)));
					GUILayout.EndHorizontal();
					GUILayout.Space(5f);
					if (GUILayout.Button("Connect to Game Server"))
					{
						GameData.errorMessage = "...Connecting to Game...\n(" + remoteIP + ":" + port + ((!Network.useNat) ? string.Empty : " NAT") + ")\n\n\n\nPlay safe! Don't share personal information online,\nand don't trust anyone who asks you for it.";
						Network.Connect(remoteIP, port);
						UnityScript.Lang.Array array8 = new UnityScript.Lang.Array();
						array8.Add(remoteIP);
						netConIP = (string[])array8.ToBuiltin(typeof(string));
						netConPort = port;
						netConAttempts = 1;
						PlayerPrefs.SetString("remoteIP", remoteIP);
					}
					GUILayout.Space(5f);
					Network.useNat = GUILayout.Toggle(Network.useNat, " Enable NAT (generally unneeded)");
					GUILayout.EndVertical();
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
				if (num2 > 0)
				{
					GUILayout.Space(30f);
					GUILayout.Label(num2 + " players online - " + num3 + " players in this version - " + num4 + " available dedicated servers");
				}
				GUILayout.EndScrollView();
				if (Event.current.type != EventType.layout)
				{
					mouseInServerList = GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
				}
				GUILayout.FlexibleSpace();
				GUILayout.Space(5f);
				if (showSettings)
				{
					GUILayout.BeginHorizontal();
					if (!disableMasterServer)
					{
						if (useMasterServer)
						{
							if (GUILayout.Button("Switch to Direct Connect"))
							{
								useMasterServer = false;
								Network.useNat = false;
							}
						}
						else if (GUILayout.Button("Switch to Server List"))
						{
							useMasterServer = true;
						}
					}
					if (useMasterServer)
					{
						GUILayout.Space(5f);
						if (GUILayout.Button("Refresh Games"))
						{
							MasterServer.ClearHostList();
							MasterServer.RequestHostList(gameName);
							lastHostListRefresh = -1f;
							lastHostListRequest = Time.time;
						}
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						hostDedicated = GUILayout.Toggle(hostDedicated, "Utilize dedicated servers for hosting games");
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						GUILayout.Label(testMessage + masterServerMessage + ((masterServerConFailures <= 0) ? string.Empty : (" (" + masterServerConFailures + " failures)")) + ((!useMasterServer) ? string.Empty : (" (Master Server @ " + MasterServer.ipAddress + ":" + MasterServer.port + ")")) + ((!autoHostListRefresh || !useMasterServer) ? string.Empty : (" (Autorefresh in " + Mathf.Ceil(lastHostListRequest + hostListRefreshTimeout - Time.time) + ")")));
						if (useMasterServer)
						{
							forceNAT = GUILayout.Toggle(forceNAT, "Force NAT");
						}
					}
					else
					{
						GUILayout.EndHorizontal();
						GUILayout.Space(3f);
						GUILayout.BeginHorizontal();
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.BeginHorizontal();
				if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
				{
					if (GUILayout.Button("<< Exit Game", GUILayout.Height(30f)))
					{
						Application.Quit();
					}
					GUILayout.Space(5f);
				}
				if (showSettings)
				{
					if (GUILayout.Button("Hide Settings", GUILayout.Height(30f)))
					{
						showSettings = false;
					}
				}
				else if (GUILayout.Button("Show Settings", GUILayout.Height(30f)))
				{
					showSettings = true;
				}
				if (messages != null && Extensions.get_length((System.Array)messages) > 0)
				{
					int l = 0;
					string[] array9 = messages;
					for (int length4 = array9.Length; l < length4; l++)
					{
						string[] array10 = array9[l].Split(","[0]);
						GUILayout.Space(5f);
						if (GUILayout.Button(array10[0], GUILayout.Height(30f)))
						{
							Application.OpenURL(array10[1]);
						}
					}
				}
				GUILayout.EndHorizontal();
			}
		}
		if (autoHostListRefresh && (Time.time > lastHostListRequest + hostListRefreshTimeout || lastHostListRefresh < 0f) && useMasterServer)
		{
			MasterServer.RequestHostList(gameName);
			if (!(lastHostListRefresh > 0f))
			{
				lastHostListRefresh = Time.time;
			}
			lastHostListRequest = Time.time;
		}
	}

    public IEnumerator LoadGame()
    { 
        GuiAnimate = 1;
        yield return null;
        Thread.Sleep(750);
        Application.LoadLevel(2);
    }

    public IEnumerator<WWW> authenticateUser()
    {
        userAuthenticating = "true";
        WWW www = new WWW(
            "http://marsxplr.com/user/authenticate.atis-u-" +
            Regex.Replace(
                WWW.EscapeURL(userNameTemp),
                "-",
                "%2d").Replace(".", "%2e") +
            "-p-" +
            Regex.Replace(
                WWW.EscapeURL(userPassword),
                "-",
                "%2d").Replace(".", "%2e"));
        yield return www;
        if (www == null || www.data == "")
        {
            userAuthenticating = "Authentication server is unreachable";
        }
        else if (www.data == "-1")
        {
            userAuthenticating = "";
        }
        else if (www.data == "-2")
        {
            userAuthenticating = "Username not found";
        }
        else if (www.data == "-3")
        {
            userAuthenticating = "Incorrect password";
        }
        else if (www.data == "-4")
        {
            userAuthenticating = "Too many login attempts";
        }
        else {
            String[] data = www.data.Split(":"[0]);
            if (
                String.Equals(
                    data[0],
                    userNameTemp,
                    StringComparison.CurrentCultureIgnoreCase)
                )
            {
                userName = "(" + userNameTemp + ")+";
            }
            else
            {
                userName = "" + data[0] + " (" + userNameTemp + ")+";
            }
            if (userCode == "{Code}")
            {
                userCode = "";
            }

            if (userRemembered)
            {
                PlayerPrefs.SetString("userName", userNameTemp);
                PlayerPrefs.SetString("userPassword", userPassword);
                PlayerPrefs.SetInt("userRemembered", 1);
                PlayerPrefs.SetInt("userRegistered", 1);
            }
            else
            {
                PlayerPrefs.SetString("userName", "");
                PlayerPrefs.SetString("userPassword", "");
                PlayerPrefs.SetInt("userRemembered", 0);
                PlayerPrefs.SetInt("userRegistered", 0);
                userNameTemp = "";
                userPassword = "";
            }
        }
    }

	public int sortHostArray(HostData a, HostData b)
	{
		return (a.connectedPlayers > b.connectedPlayers) ? (-1) : ((a.connectedPlayers < b.connectedPlayers) ? 1 : 0);
	}

	public void errorWindow(int id)
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		GUILayout.Label(GameData.errorMessage);
		GUILayout.EndScrollView();
		if (GUILayout.Button("(Dismiss)") || Input.GetKeyDown("return") || Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Mouse0))
		{
			GameData.errorMessage = string.Empty;
			if (!Network.isServer && Network.peerType != NetworkPeerType.Disconnected)
			{
				Network.Disconnect();
			}
		}
	}

	public void makeWindowUpdate(int id)
	{
		GUILayout.Space(40f);
		GUILayout.Label("A new Mars Explorer version is now available:");
		GUILayout.Space(10f);
		if (GUILayout.Button(">> Download Mars Explorer version " + outdated + "! <<", GUILayout.Height(40f)))
		{
			Application.OpenURL("http://marsxplr.com/view-267");
		}
		GUILayout.Space(30f);
		GUILayout.BeginHorizontal();
		GUILayout.Space(100f);
		if (GUILayout.Button("Ignore warning, Play Anyway"))
		{
			outdated = string.Empty;
		}
		GUILayout.Space(100f);
		GUILayout.EndHorizontal();
		GUILayout.Space(40f);
	}

	public void getRemoteIP()
	{
		remoteIP = PlayerPrefs.GetString("remoteIP", "127.0.0.1");
	}

    public String htmlDecode(String str)
    {
        return Regex.Replace(str, "<[^>]*?>", "")
            .Replace("&#34;", "\"")
            .Replace("&#39;", "'")
            .Replace("&amp;", "&");
    }

	public static string sha1sum(object strToEncrypt)
	{
		UTF8Encoding target = new UTF8Encoding();
		object obj = UnityRuntimeServices.Invoke(target, "GetBytes", new object[1] { strToEncrypt }, typeof(MonoBehaviour));
		SHA1CryptoServiceProvider target2 = new SHA1CryptoServiceProvider();
		byte[] array = (byte[])RuntimeServices.Coerce(UnityRuntimeServices.Invoke(target2, "ComputeHash", new object[1] { obj }, typeof(MonoBehaviour)), typeof(byte[]));
		string text = string.Empty;
		for (int i = 0; i < array.Length; i = checked(i + 1))
		{
			text += Convert.ToString(array[RuntimeServices.NormalizeArrayIndex(array, i)], 16).PadLeft(2, "0"[0]);
		}
		return text.PadLeft(32, "0"[0]);
	}
}
