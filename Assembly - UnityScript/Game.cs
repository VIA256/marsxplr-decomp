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
public class Game : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class registerHost_002468 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal string _0024playerList_0024466;

			internal int _0024lagCount_0024467;

			internal float _0024lagVal_0024468;

			internal int _0024botCount_0024469;

			internal DictionaryEntry _0024plrE_0024470;

			internal VehicleNet _0024vehNet_0024471;

			internal IEnumerator _0024___iterator67_0024472;

			internal string _0024bannedIPs_0024473;

			internal string[] _0024banned_0024474;

			internal string _0024dat_0024475;

			internal string[] _0024val_0024476;

			internal int _0024___temp134_0024477;

			internal string[] _0024___temp135_0024478;

			internal int _0024___temp136_0024479;

			internal Game _0024self_480;

			public _0024(Game self_)
			{
				_0024self_480 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						if (_0024self_480.serverHidden || !Network.isServer)
						{
							break;
						}
						return Yield(2, new WaitForSeconds(1f));
					case 2:
						_0024playerList_0024466 = string.Empty;
						_0024lagCount_0024467 = default(int);
						_0024lagVal_0024468 = default(float);
						_0024botCount_0024469 = default(int);
						_0024___iterator67_0024472 = UnityRuntimeServices.GetEnumerator(Players);
						while (_0024___iterator67_0024472.MoveNext())
						{
							_0024plrE_0024470 = (DictionaryEntry)_0024___iterator67_0024472.Current;
							if (!RuntimeServices.ToBool(_0024plrE_0024470.Value))
							{
								continue;
							}
							if (RuntimeServices.ToBool(RuntimeServices.GetProperty(_0024plrE_0024470.Value, "isBot")))
							{
								_0024botCount_0024469++;
								continue;
							}
							_0024playerList_0024466 = (string)RuntimeServices.Coerce(RuntimeServices.InvokeBinaryOperator("op_Addition", _0024playerList_0024466, RuntimeServices.InvokeBinaryOperator("op_Addition", (!(_0024playerList_0024466 == string.Empty)) ? "," : string.Empty, RuntimeServices.GetProperty(_0024plrE_0024470.Value, "name"))), typeof(string));
							UnityRuntimeServices.Update(_0024___iterator67_0024472, _0024plrE_0024470);
							if (!RuntimeServices.ToBool(RuntimeServices.GetProperty(RuntimeServices.GetProperty(_0024plrE_0024470.Value, "networkView"), "isMine")))
							{
								_0024vehNet_0024471 = (VehicleNet)RuntimeServices.Coerce(UnityRuntimeServices.Invoke(RuntimeServices.GetProperty(_0024plrE_0024470.Value, "gameObject"), "GetComponent", new object[1] { typeof(VehicleNet) }, typeof(MonoBehaviour)), typeof(VehicleNet));
								UnityRuntimeServices.Update(_0024___iterator67_0024472, _0024plrE_0024470);
								if ((bool)_0024vehNet_0024471)
								{
									_0024lagCount_0024467++;
									_0024lagVal_0024468 += _0024vehNet_0024471.ping;
								}
							}
						}
						if (_0024botCount_0024469 > 0)
						{
							_0024playerList_0024466 += ",and " + _0024botCount_0024469 + " bots";
						}
						if (Settings.bannedIPs != string.Empty)
						{
							_0024bannedIPs_0024473 = string.Empty;
							_0024banned_0024474 = Settings.bannedIPs.Split("\n"[0]);
							_0024___temp134_0024477 = 0;
							_0024___temp135_0024478 = _0024banned_0024474;
							for (_0024___temp136_0024479 = _0024___temp135_0024478.Length; _0024___temp134_0024477 < _0024___temp136_0024479; _0024___temp134_0024477++)
							{
								if (!(_0024___temp135_0024478[_0024___temp134_0024477] == string.Empty))
								{
									_0024val_0024476 = _0024___temp135_0024478[_0024___temp134_0024477].Split(" "[0]);
									_0024bannedIPs_0024473 += ((!(_0024bannedIPs_0024473 == string.Empty)) ? string.Empty : ",") + _0024val_0024476[0];
								}
							}
							_0024bannedIPs_0024473 = ";b=" + _0024bannedIPs_0024473;
						}
						MasterServer.RegisterHost(GameData.gameName, _0024self_480.serverName, "v=" + GameData.gameVersion + ";w=" + _0024self_480.WorldDesc.name + ";p=" + _0024playerList_0024466 + ";u=" + _0024self_480.WorldDesc.url + ((_0024lagCount_0024467 <= 0) ? string.Empty : (";s=" + Mathf.RoundToInt(_0024lagVal_0024468 / (float)_0024lagCount_0024467 * 1000f))) + ((!(_0024self_480.serverPassword != string.Empty)) ? string.Empty : ";l=1") + _0024bannedIPs_0024473);
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

		internal Game _0024self_481;

		public registerHost_002468(Game self_)
		{
			_0024self_481 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_481);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class registerHostSet_002469 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Game _0024self_482;

			public _0024(Game self_)
			{
				_0024self_482 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					if (_0024self_482.serverHidden)
					{
						break;
					}
					_0024self_482.hostRegistered = true;
					goto case 2;
				case 2:
					if (_0024self_482.hostRegistered)
					{
						_0024self_482.StartCoroutine_Auto(_0024self_482.registerHost());
						return Yield(2, new WaitForSeconds(60f));
					}
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Game _0024self_483;

		public registerHostSet_002469(Game self_)
		{
			_0024self_483 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_483);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class Init_002471 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024worldOBJ_0024425;

			internal object _0024objs_0024426;

			internal bool _0024thereisalight_0024427;

			internal Light _0024light_0024428;

			internal IEnumerator _0024___iterator66_0024429;

			internal int _0024veh_0024430;

			internal string _0024temp_0024431;

			internal int _0024i_0024432;

			internal Game _0024self_433;

			public _0024(Game self_)
			{
				_0024self_433 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						return Yield(2, null);
					case 2:
						Network.isMessageQueueRunning = true;
						goto case 3;
					case 3:
						if (!_0024self_433.worldLoaded)
						{
							return Yield(3, null);
						}
						Settings.fogColor = RenderSettings.fogColor;
						_0024self_433.GuiAnimate = 1;
						goto case 4;
					case 4:
						if (_0024self_433.GuiAnimate != 0)
						{
							return Yield(4, null);
						}
						return Yield(5, new WaitForSeconds(0.25f));
					case 5:
						_0024self_433.GuiAnimate = -1;
						_0024self_433.loadingWorld = false;
						_0024self_433.worldLoadTime = Time.time;
						_0024worldOBJ_0024425 = GameObject.Find(_0024self_433.whirldIn.worldName);
						_0024objs_0024426 = _0024worldOBJ_0024425.GetComponentsInChildren(typeof(Transform));
						if (!RuntimeServices.EqualityOperator(_0024self_433.whirldIn.worldParams["ccc"], 1) && RuntimeServices.ToBool(RuntimeServices.InvokeBinaryOperator("op_GreaterThan", RuntimeServices.GetProperty(_0024objs_0024426, "Length"), 15)))
						{
							_0024worldOBJ_0024425.AddComponent(typeof(CombineChildren));
						}
						return Yield(6, null);
					case 6:
						if ((bool)GameObject.Find("Base"))
						{
							World.@base = GameObject.Find("Base").transform;
						}
						else
						{
							World.@base = (Transform)RuntimeServices.Coerce(RuntimeServices.GetProperty(UnityEngine.Object.Instantiate(Resources.Load("Base"), Vector3.up * 5f, Quaternion.identity), "transform"), typeof(Transform));
						}
						World.terrains = (Terrain[])UnityEngine.Object.FindObjectsOfType(typeof(Terrain));
						if (!RuntimeServices.ToBool(_0024self_433.whirldIn.worldParams["env"]) || !RuntimeServices.EqualityOperator(_0024self_433.whirldIn.worldParams["env"], "space"))
						{
							if ((bool)GameObject.Find("Sea"))
							{
								World.sea = GameObject.Find("Sea").transform;
								Settings.lavaAlt = World.sea.position.y;
							}
							if (!GameObject.Find("Floor") && !RuntimeServices.ToBool(UnityEngine.Object.FindObjectOfType(typeof(Terrain))) && !World.sea && !RuntimeServices.ToBool(_0024self_433.whirldIn.worldParams["nofloor"]) && !RuntimeServices.EqualityOperator(_0024self_433.whirldIn.worldParams["floor"], "0"))
							{
								UnityEngine.Object.Instantiate(Resources.Load("Floor"), Vector3.zero, Quaternion.identity);
							}
						}
						_0024thereisalight_0024427 = false;
						_0024___iterator66_0024429 = UnityRuntimeServices.GetEnumerator(UnityEngine.Object.FindObjectsOfType(typeof(Light)));
						while (_0024___iterator66_0024429.MoveNext())
						{
							_0024light_0024428 = (Light)RuntimeServices.Coerce(_0024___iterator66_0024429.Current, typeof(Light));
							if (_0024light_0024428.gameObject.name != "VehicleLight")
							{
								_0024thereisalight_0024427 = true;
							}
						}
						if (!_0024thereisalight_0024427)
						{
							UnityEngine.Object.Instantiate(Resources.Load("Light"));
						}
						_0024self_433.mE(World.@base.transform.position);
						return Yield(7, new WaitForSeconds(1f));
					case 7:
						if (Settings.buggyAllowed)
						{
							_0024veh_0024430 = 0;
						}
						else if (Settings.tankAllowed)
						{
							_0024veh_0024430 = 2;
						}
						else if (Settings.hoverAllowed)
						{
							_0024veh_0024430 = 1;
						}
						else if (Settings.jetAllowed)
						{
							_0024veh_0024430 = 3;
						}
						else
						{
							_0024veh_0024430 = 0;
						}
						_0024self_433.GameVehicleID = _0024veh_0024430;
						_0024temp_0024431 = GameData.userName;
						_0024i_0024432 = 1;
						while ((_0024i_0024432 == 1 && GameObject.Find(_0024temp_0024431) != null) || GameObject.Find(_0024temp_0024431 + " " + _0024i_0024432) != null)
						{
							_0024i_0024432++;
						}
						_0024self_433.networkView.RPC("iV", RPCMode.All, Network.AllocateViewID(), Settings.networkMode, _0024self_433.GameVehicleID, (_0024i_0024432 == 1) ? _0024temp_0024431 : (_0024temp_0024431 + " " + _0024i_0024432), 0, _0024self_433.isHost ? 1 : 0, 0, 0);
						if (_0024self_433.isHost)
						{
							if (!Network.isServer)
							{
								_0024self_433.sSHS();
							}
							if (Network.isServer)
							{
								Messaging.broadcast("You are hosting a server!\n" + ((!(_0024self_433.serverPassword != string.Empty)) ? string.Empty : ("Password: " + _0024self_433.serverPassword + "\n")) + "IP: " + Network.player.ipAddress + "\n" + ((Network.player.externalIP == null) ? string.Empty : ("External: " + Network.player.externalIP)) + "\nPort: " + Network.player.port);
								return Yield(8, new WaitForSeconds(1f));
							}
							Messaging.broadcast("You are hosting a game on a dedicated server!\n" + ((!(_0024self_433.serverPassword != string.Empty)) ? string.Empty : ("Password: " + _0024self_433.serverPassword + "\n")) + "IP: " + Network.connections[0].ipAddress + "\n" + ((Network.connections[0].externalIP == null) ? string.Empty : ("External: " + Network.connections[0].externalIP)) + "\nPort: " + Network.connections[0].port);
							goto IL_0831;
						}
						Messaging.broadcast(((_0024i_0024432 == 1) ? _0024temp_0024431 : (_0024temp_0024431 + " " + _0024i_0024432)) + " has joined!");
						goto IL_0997;
					case 8:
						_0024self_433.StartCoroutine_Auto(_0024self_433.registerHostSet());
						goto IL_0831;
					case 9:
						Settings.colorCustom = PlayerPrefs.GetInt("vehColCustom") == 1;
						if (!Settings.colorCustom)
						{
							Settings.StartCoroutine_Auto(Settings.ramdomizeVehicleColor());
						}
						Yield(1, null);
						break;
					case 1:
						break;
						IL_0997:
						Settings.serverDefault = Settings.packServerPrefs();
						Settings.serverString = Settings.serverDefault;
						Settings.updatePrefs();
						return Yield(9, null);
						IL_0831:
						if (RuntimeServices.ToBool(_0024self_433.whirldIn.worldParams["message"]))
						{
							Settings.serverWelcome = (string)RuntimeServices.Coerce(_0024self_433.whirldIn.worldParams["message"], typeof(string));
							_0024self_433.networkView.RPC("msg", RPCMode.All, Settings.serverWelcome, UnityBuiltins.parseInt(1));
						}
						if (RuntimeServices.ToBool(_0024self_433.whirldIn.worldParams["marsxplr"]))
						{
							_0024self_433.networkView.RPC("sSS", RPCMode.All, _0024self_433.whirldIn.worldParams["marsxplr"]);
						}
						goto IL_0997;
					}
					bool result = default(bool);
					return result;
				}
			}
		}

		internal Game _0024self_434;

		public Init_002471(Game self_)
		{
			_0024self_434 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_434);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class OnPlayerConnected_002475 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal string[] _0024banned_0024435;

			internal string _0024dat_0024436;

			internal string[] _0024val_0024437;

			internal bool _0024playerIsConnected_0024438;

			internal NetworkPlayer _0024plyr_0024439;

			internal DictionaryEntry _0024plrE_0024440;

			internal Vehicle _0024veh_0024441;

			internal IEnumerator _0024___iterator74_0024442;

			internal int _0024___temp118_0024443;

			internal string[] _0024___temp119_0024444;

			internal int _0024___temp120_0024445;

			internal int _0024___temp122_0024446;

			internal NetworkPlayer[] _0024___temp123_0024447;

			internal int _0024___temp124_0024448;

			internal NetworkPlayer _0024player449;

			internal Game _0024self_450;

			public _0024(NetworkPlayer player, Game self_)
			{
				_0024player449 = player;
				_0024self_450 = self_;
			}

			public override bool MoveNext()
			{
				checked
				{
					switch (_state)
					{
					default:
						if (Settings.bannedIPs != string.Empty)
						{
							_0024banned_0024435 = Settings.bannedIPs.Split("\n"[0]);
							_0024___temp118_0024443 = 0;
							_0024___temp119_0024444 = _0024banned_0024435;
							for (_0024___temp120_0024445 = _0024___temp119_0024444.Length; _0024___temp118_0024443 < _0024___temp120_0024445; _0024___temp118_0024443++)
							{
								_0024val_0024437 = _0024___temp119_0024444[_0024___temp118_0024443].Split(" "[0]);
								if (_0024val_0024437[0] == _0024player449.ipAddress)
								{
									_0024self_450.networkView.RPC("dN", _0024player449, 2);
									Network.CloseConnection(_0024player449, true);
								}
							}
						}
						goto case 2;
					case 2:
						if (_0024self_450.serverPassword != string.Empty && !_0024self_450.authenticatedPlayers.ContainsKey(_0024player449))
						{
							_0024playerIsConnected_0024438 = false;
							_0024___temp122_0024446 = 0;
							_0024___temp123_0024447 = Network.connections;
							for (_0024___temp124_0024448 = _0024___temp123_0024447.Length; _0024___temp122_0024446 < _0024___temp124_0024448; _0024___temp122_0024446++)
							{
								if (_0024___temp123_0024447[_0024___temp122_0024446] == _0024player449)
								{
									_0024playerIsConnected_0024438 = true;
								}
							}
							if (!_0024playerIsConnected_0024438)
							{
								break;
							}
							return Yield(2, null);
						}
						_0024___iterator74_0024442 = UnityRuntimeServices.GetEnumerator(Players);
						while (_0024___iterator74_0024442.MoveNext())
						{
							_0024plrE_0024440 = (DictionaryEntry)_0024___iterator74_0024442.Current;
							_0024veh_0024441 = (Vehicle)RuntimeServices.Coerce(_0024plrE_0024440.Value, typeof(Vehicle));
							UnityRuntimeServices.Update(_0024___iterator74_0024442, _0024plrE_0024440);
							_0024self_450.networkView.RPC("iV", _0024player449, _0024veh_0024441.networkView.viewID, _0024veh_0024441.networkMode, _0024veh_0024441.vehId, _0024veh_0024441.gameObject.name, _0024veh_0024441.isBot ? 1 : 0, _0024veh_0024441.isIt, _0024veh_0024441.score, _0024veh_0024441.specialInput ? 1 : 0);
							_0024veh_0024441.networkView.RPC("sC", _0024player449, _0024veh_0024441.vehicleColor.r, _0024veh_0024441.vehicleColor.g, _0024veh_0024441.vehicleColor.b, _0024veh_0024441.vehicleAccent.r, _0024veh_0024441.vehicleAccent.g, _0024veh_0024441.vehicleAccent.b);
						}
						if (Settings.serverString != Settings.serverDefault)
						{
							Settings.serverString = Settings.packServerPrefs();
							_0024self_450.networkView.RPC("sSS", _0024player449, Settings.serverString);
						}
						_0024self_450.networkView.RPC("lW", _0024player449, "url=" + _0024self_450.WorldDesc.url);
						if (Settings.serverWelcome != string.Empty)
						{
							_0024self_450.networkView.RPC("msg", _0024player449, Settings.serverWelcome, UnityBuiltins.parseInt(1));
						}
						return Yield(3, new WaitForSeconds(5f));
					case 3:
						_0024self_450.StartCoroutine_Auto(_0024self_450.registerHost());
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

		internal NetworkPlayer _0024player451;

		internal Game _0024self_452;

		public OnPlayerConnected_002475(NetworkPlayer player, Game self_)
		{
			_0024player451 = player;
			_0024self_452 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024player451, _0024self_452);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class OnPlayerDisconnected_002477 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal string _0024pName_0024453;

			internal DictionaryEntry _0024plrE_0024454;

			internal IEnumerator _0024___iterator76_0024455;

			internal NetworkPlayer _0024player456;

			internal Game _0024self_457;

			public _0024(NetworkPlayer player, Game self_)
			{
				_0024player456 = player;
				_0024self_457 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					_0024pName_0024453 = string.Empty;
					_0024___iterator76_0024455 = UnityRuntimeServices.GetEnumerator(Players);
					while (_0024___iterator76_0024455.MoveNext())
					{
						_0024plrE_0024454 = (DictionaryEntry)_0024___iterator76_0024455.Current;
						if (RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(_0024plrE_0024454.Value, "networkView"), "owner"), _0024player456))
						{
							_0024self_457.networkView.RPC("pD", RPCMode.All, _0024plrE_0024454.Key);
							UnityRuntimeServices.Update(_0024___iterator76_0024455, _0024plrE_0024454);
							break;
						}
					}
					Network.RemoveRPCs(_0024player456);
					Network.DestroyPlayerObjects(_0024player456);
					return Yield(2, new WaitForSeconds(1f));
				case 2:
					_0024self_457.eSI();
					_0024self_457.StartCoroutine_Auto(_0024self_457.registerHost());
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal NetworkPlayer _0024player458;

		internal Game _0024self_459;

		public OnPlayerDisconnected_002477(NetworkPlayer player, Game self_)
		{
			_0024player458 = player;
			_0024self_459 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024player458, _0024self_459);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class addBot_002479 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal string _0024temp_0024460;

			internal Game _0024self_461;

			public _0024(Game self_)
			{
				_0024self_461 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					_0024self_461.botsInGame = checked(_0024self_461.botsInGame + 1);
					Messaging.broadcast(Player.name + " added " + ((_0024self_461.botsInGame != 1) ? ("bot " + _0024self_461.botsInGame) : "a bot"));
					_0024self_461.materilizationEffect(World.@base.transform.position);
					_0024temp_0024460 = GameData.userName + "'s bot" + ((_0024self_461.botsInGame != 1) ? (" " + _0024self_461.botsInGame) : string.Empty);
					return Yield(2, new WaitForSeconds(2f));
				case 2:
					_0024self_461.networkView.RPC("iV", RPCMode.All, Network.AllocateViewID(), Settings.networkMode, _0024self_461.GameVehicleID, _0024temp_0024460, 1, 0, 0, 0);
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Game _0024self_462;

		public addBot_002479(Game self_)
		{
			_0024self_462 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_462);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class axeBot_002480 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024bot_0024463;

			internal Game _0024self_464;

			public _0024(Game self_)
			{
				_0024self_464 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					_0024bot_0024463 = GameObject.Find(Player.name + "'s bot" + ((_0024self_464.botsInGame == 1) ? string.Empty : (" " + _0024self_464.botsInGame)));
					if (!_0024bot_0024463)
					{
						break;
					}
					Messaging.broadcast(Player.name + " removed " + ((_0024self_464.botsInGame != 1) ? ("bot " + _0024self_464.botsInGame) : "the last bot"));
					_0024self_464.botsInGame = checked(_0024self_464.botsInGame - 1);
					_0024self_464.materilizationEffect(_0024bot_0024463.transform.position);
					_0024bot_0024463.rigidbody.isKinematic = true;
					Network.Destroy(_0024bot_0024463.rigidbody.networkView.viewID);
					return Yield(2, new WaitForSeconds(0.5f));
				case 2:
					_0024self_464.eSI();
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal Game _0024self_465;

		public axeBot_002480(Game self_)
		{
			_0024self_465 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_465);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class iV_002493 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Vector3 _0024pos_0024484;

			internal GameObject _0024plyObj_0024485;

			internal GameObject _0024vehObj_0024486;

			internal object _0024plyVeh_0024487;

			internal NetworkViewID _0024viewID488;

			internal int _0024networkMode489;

			internal int _0024vehId490;

			internal string _0024vehName491;

			internal int _0024isBot492;

			internal int _0024isIt493;

			internal int _0024score494;

			internal int _0024specialInput495;

			public _0024(NetworkViewID viewID, int networkMode, int vehId, string vehName, int isBot, int isIt, int score, int specialInput)
			{
				_0024viewID488 = viewID;
				_0024networkMode489 = networkMode;
				_0024vehId490 = vehId;
				_0024vehName491 = vehName;
				_0024isBot492 = isBot;
				_0024isIt493 = isIt;
				_0024score494 = score;
				_0024specialInput495 = specialInput;
			}

			public override bool MoveNext()
			{
				GameObject[] gameVehicles;
				switch (_state)
				{
				default:
					if (_0024viewID488.isMine)
					{
						return Yield(2, new WaitForSeconds(0.25f));
					}
					_0024pos_0024484 = Vector3.zero;
					goto IL_008b;
				case 2:
					_0024pos_0024484 = World.@base.position;
					while (Physics.CheckSphere(_0024pos_0024484, 3f))
					{
						_0024pos_0024484 += Vector3.up;
					}
					goto IL_008b;
				case 1:
					break;
					IL_008b:
					if (_0024networkMode489 == 0)
					{
						objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.Unreliable;
					}
					else if (_0024networkMode489 == 1)
					{
						objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
					}
					else
					{
						objectVehicle.networkView.stateSynchronization = NetworkStateSynchronization.Off;
					}
					objectVehicle.networkView.viewID = _0024viewID488;
					_0024plyObj_0024485 = (GameObject)UnityEngine.Object.Instantiate(objectVehicle, _0024pos_0024484, Quaternion.identity);
					_0024plyObj_0024485.networkView.viewID = _0024viewID488;
					gameVehicles = Controller.GameVehicles;
					_0024vehObj_0024486 = (GameObject)UnityEngine.Object.Instantiate(gameVehicles[RuntimeServices.NormalizeArrayIndex(gameVehicles, _0024vehId490)], _0024pos_0024484, Quaternion.identity);
					_0024vehObj_0024486.transform.parent = _0024plyObj_0024485.transform;
					_0024plyVeh_0024487 = _0024plyObj_0024485.GetComponent(typeof(Vehicle));
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "vehObj", _0024vehObj_0024486);
					if (_0024viewID488.isMine && (bool)World.@base)
					{
						_0024plyObj_0024485.transform.rotation = World.@base.rotation;
					}
					else
					{
						_0024plyObj_0024485.transform.rotation = Quaternion.identity;
					}
					_0024plyObj_0024485.name = _0024vehName491;
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "networkMode", _0024networkMode489);
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "vehId", _0024vehId490);
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "isBot", _0024isBot492 == 1);
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "isIt", _0024isIt493);
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "score", _0024score494);
					RuntimeServices.SetProperty(_0024plyVeh_0024487, "specialInput", _0024specialInput495);
					if (_0024viewID488.isMine && _0024isBot492 == 0)
					{
						Player = _0024plyObj_0024485;
					}
					Yield(1, null);
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal NetworkViewID _0024viewID496;

		internal int _0024networkMode497;

		internal int _0024vehId498;

		internal string _0024vehName499;

		internal int _0024isBot500;

		internal int _0024isIt501;

		internal int _0024score502;

		internal int _0024specialInput503;

		public iV_002493(NetworkViewID viewID, int networkMode, int vehId, string vehName, int isBot, int isIt, int score, int specialInput)
		{
			_0024viewID496 = viewID;
			_0024networkMode497 = networkMode;
			_0024vehId498 = vehId;
			_0024vehName499 = vehName;
			_0024isBot500 = isBot;
			_0024isIt501 = isIt;
			_0024score502 = score;
			_0024specialInput503 = specialInput;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024viewID496, _0024networkMode497, _0024vehId498, _0024vehName499, _0024isBot500, _0024isIt501, _0024score502, _0024specialInput503);
		}
	}

	public GUISkin GameSkin;

	public static GUISkin Skin;

	public GUIStyle hudTextStyle;

	public GUIPanel[] GUIPanels;

	private float closePanel;

	public GameObject[] GameVehicles;

	public GameWorldDesc[] GameWorlds;

	private WhirldIn whirldIn;

	[NonSerialized]
	public bool WorldIsCustom;

	[NonSerialized]
	public GameWorldDesc WorldDesc;

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

	public bool isHost;

	public bool isRegistered;

	public Texture2D cursor;

	public Texture2D cursorLook;

	public Vector2 cursorOffset;

	[NonSerialized]
	public float kpDur;

	[NonSerialized]
	public float kpTime;

	private int hostPanelTab;

	private int windowVehicleHeight;

	private Vector2 scrollPosition;

	private bool killServer;

	private int netKillMode;

	private int GameVehicleID;

	[NonSerialized]
	public float quarryDist;

	[NonSerialized]
	public int botsInGame;

	public static float GUIAlpha = 0f;

	private int GuiAnimate;

	public static GameObject Player;

	public static Vehicle PlayerVeh;

	public static Vehicle QuarryVeh;

	public static CameraVehicle CameraVehicle;

	public static Messaging Messaging;

	public static Settings Settings;

	public static Game Controller;

	public bool worldLoaded;

	public bool loadingWorld;

	[NonSerialized]
	public float worldLoadTime;

	[NonSerialized]
	public bool hostRegistered;

	[NonSerialized]
	public string serverName;

	[NonSerialized]
	public string serverPassword;

	private float authTime;

	private float authUpdateTime;

	[NonSerialized]
	public bool serverHidden;

	[NonSerialized]
	public Hashtable authenticatedPlayers;

	private UnityScript.Lang.Array authenticatingPlayers;

	[NonSerialized]
	public static Hashtable Players;

	public UnityScript.Lang.Array unauthPlayers;

	public Color vehicleIsItColor;

	public Color vehicleIsItAccent;

	private float fpsTime;

	private int fpsFrames;

	private int heavyTickRate;

	[HideInInspector]
	public float fps;

	public Game()
	{
		whirldIn = new WhirldIn();
		WorldIsCustom = false;
		WorldDesc = new GameWorldDesc();
		isHost = false;
		isRegistered = false;
		kpDur = 0.1f;
		hostPanelTab = 0;
		windowVehicleHeight = 0;
		killServer = false;
		netKillMode = 0;
		botsInGame = 0;
		GuiAnimate = -1;
		worldLoaded = false;
		loadingWorld = true;
		hostRegistered = false;
		serverName = string.Empty;
		serverPassword = string.Empty;
		authUpdateTime = 2f;
		serverHidden = false;
		authenticatedPlayers = new Hashtable();
		authenticatingPlayers = new UnityScript.Lang.Array();
		unauthPlayers = new UnityScript.Lang.Array();
		heavyTickRate = 2;
	}

	[HideInInspector]
	public void Start()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
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
		CameraVehicle = (CameraVehicle)Camera.main.GetComponent("CameraVehicle");
		Settings = (Settings)gameObject.GetComponent("Settings");
		Messaging = (Messaging)gameObject.GetComponent("Messaging");
		Controller = (Game)gameObject.GetComponent("Game");
		if (GameData.userName == string.Empty)
		{
			GameData.userName = "MarsRacer";
		}
		Settings.bannedIPs = GameData.masterBlacklist;
		Settings.networkMode = GameData.networkMode;
		Players = new Hashtable();
		botsInGame = 0;
		worldLoaded = false;
		if (GameData.gameWorlds == null)
		{
			GameData.gameWorlds = GameWorlds;
		}
		StartCoroutine_Auto(Init());
	}

	public void OnDisable()
	{
		if (whirldIn != null)
		{
			whirldIn.Cleanup();
		}
	}

	public IEnumerator Init()
	{
		return new Init_002471(this).GetEnumerator();
	}

	public void Update()
	{
		Application.runInBackground = true;
		if (Settings.resetTime > 0f && 10f - (Time.time - Settings.resetTime) < 1f)
		{
			if ((bool)PlayerVeh.ramoSphere)
			{
				UnityEngine.Object.Destroy(PlayerVeh.ramoSphere);
			}
			Player.transform.position = World.@base.position;
			Player.transform.rotation = World.@base.rotation;
			Player.rigidbody.isKinematic = false;
			Player.rigidbody.velocity = Vector3.zero;
			Settings.resetTime = -10f;
			Settings.updatePrefs();
		}
		else if (Settings.resetTime < -1f)
		{
			Settings.resetTime += Time.deltaTime;
		}
		if (Settings.serverUpdateTime != 0f && Settings.serverUpdateTime < Time.time)
		{
			Settings.serverString = Settings.packServerPrefs();
			networkView.RPC("sSS", RPCMode.Others, Settings.serverString);
			if (isHost && !Network.isServer)
			{
				sSHS();
			}
			Settings.serverUpdateTime = 0f;
		}
		if (Settings.colorUpdateTime != 0f && Settings.colorUpdateTime < Time.time)
		{
			Settings.colorUpdateTime = 0f;
			Settings.saveVehicleColor();
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
				GUIAlpha += Time.deltaTime * 0.35f;
			}
		}
		if (Network.peerType != NetworkPeerType.Disconnected && !isHost && WorldDesc.url == string.Empty && Time.time - authUpdateTime > 1f && Time.timeSinceLevelLoad > 3f)
		{
			networkView.RPC("lMI", RPCMode.Others, Network.player, GameData.userName);
			authUpdateTime = Time.time;
		}
		if (!worldLoaded && CameraVehicle.mb.blurAmount < 1f)
		{
			CameraVehicle.mb.blurAmount = CameraVehicle.mb.blurAmount + Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.Alpha0) && !Messaging.chatting && Time.time > kpTime)
		{
			kpTime = Time.time + kpDur;
			Settings.toggleFullscreen();
		}
		checked
		{
			fpsFrames++;
			if (!(Time.time > fpsTime))
			{
				return;
			}
			fps = unchecked(fpsFrames / heavyTickRate);
			fpsTime = Time.time + (float)heavyTickRate;
			fpsFrames = 0;
			if (isHost)
			{
				for (int i = 0; i < unauthPlayers.length; i++)
				{
					if (RuntimeServices.ToBool(RuntimeServices.InvokeBinaryOperator("op_GreaterThan", RuntimeServices.InvokeBinaryOperator("op_Subtraction", Time.time, RuntimeServices.GetProperty(unauthPlayers[i], "t")), 2)))
					{
						unauthPlayers.RemoveAt(i);
					}
				}
			}
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Players);
			while (enumerator.MoveNext())
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
				if (!RuntimeServices.ToBool(dictionaryEntry.Value))
				{
					Debug.Log(RuntimeServices.InvokeBinaryOperator("op_Addition", dictionaryEntry.Key, " null - removed from players list"));
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					Players.Remove(dictionaryEntry.Key);
					UnityRuntimeServices.Update(enumerator, dictionaryEntry);
					break;
				}
			}
			int j = 0;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
			for (int length = array.Length; j < length; j++)
			{
				bool flag = false;
				IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(Players);
				while (enumerator2.MoveNext())
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator2.Current;
					if (RuntimeServices.EqualityOperator(dictionaryEntry2.Key, array[j].name))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					UnityEngine.Object.Destroy(array[j]);
					Debug.Log("Abandoned vehicle destroyed: " + array[j].name);
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
					else
					{
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
					if (Vector3.Distance(Player.transform.position, World.@base.transform.position) < 20f && Settings.resetTime > -1f)
					{
						GUIPanels[4].openTime = Time.time;
						GUIPanels[4].open = true;
						GUIPanels[4].active = true;
					}
				}
				else if (!(Vector3.Distance(Player.transform.position, World.@base.transform.position) < 20f) || !(Settings.resetTime > -1f))
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
						if (isHost && unauthPlayers.length > 0)
						{
							text = "* " + text + " (" + unauthPlayers.length + ") *";
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
		return new OnPlayerConnected_002475(player, this).GetEnumerator();
	}

	public IEnumerator OnPlayerDisconnected(NetworkPlayer player)
	{
		return new OnPlayerDisconnected_002477(player, this).GetEnumerator();
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
					Application.OpenURL("http://www.unifycommunity.com/wiki/index.php?title=whirld");
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
		return new addBot_002479(this).GetEnumerator();
	}

	public IEnumerator axeBot()
	{
		return new axeBot_002480(this).GetEnumerator();
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
		return new registerHost_002468(this).GetEnumerator();
	}

	public IEnumerator registerHostSet()
	{
		return new registerHostSet_002469(this).GetEnumerator();
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
	public IEnumerator iV(NetworkViewID viewID, int networkMode, int vehId, string vehName, int isBot, int isIt, int score, int specialInput)
	{
		return new iV_002493(viewID, networkMode, vehId, vehName, isBot, isIt, score, specialInput).GetEnumerator();
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
	public void pI(NetworkPlayer nPlayer, string pName, NetworkMessageInfo info)
	{
	}

	[RPC]
	public void cC(NetworkPlayer nPlayer, string pName, int cMode, NetworkMessageInfo info)
	{
	}

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
		for (int i = 0; i < unauthPlayers.length; i = checked(i + 1))
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

	public void Main()
	{
	}
}
