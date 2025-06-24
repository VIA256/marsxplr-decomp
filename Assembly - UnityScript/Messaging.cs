using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Messaging : MonoBehaviour
{
	public bool chatting;

	public int showChat;

	[HideInInspector]
	public Vector2 scrollPosition;

	[HideInInspector]
	public ArrayList entries;

	private string inputField;

	private bool display;

	private Rect windowRect;

	public Messaging()
	{
		showChat = -1;
		entries = new ArrayList();
		inputField = string.Empty;
		display = true;
	}

	public void OnGUI()
	{
		if (Game.Settings.simplified)
		{
			return;
		}
		GUI.skin = Game.Skin;
		float gUIAlpha = Game.GUIAlpha;
		Color color = GUI.color;
		float num = (color.a = gUIAlpha);
		Color color2 = (GUI.color = color);
		if (!Game.Controller.loadingWorld)
		{
			if (showChat == 1 || showChat < 0)
			{
				windowRect = new Rect(10f, 40f, Mathf.Min(Mathf.Max(170, Screen.width / 4), 250), (!Game.Settings.minimapAllowed || !Game.Settings.useMinimap) ? ((float)checked(Screen.height - 55)) : ((float)Screen.height * 0.75f - 60f));
				GUI.Window(11, windowRect, ChatWindow, "Messaging Console");
			}
			else if (GUI.Button(new Rect(10f, 40f, Mathf.Min(Mathf.Max(170, Screen.width / 4), 250), 25f), "Messaging Console"))
			{
				showChat = 1;
				GUI.FocusControl("Chat input field");
			}
		}
	}

	public void ChatWindow(int id)
	{
		GUIStyle style = GUI.skin.GetStyle("close_button");
		if (GUI.Button(new Rect(style.padding.left, style.padding.top, style.normal.background.width, style.normal.background.height), string.Empty, "close_button"))
		{
			showChat = 0;
		}
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(entries);
		while (enumerator.MoveNext())
		{
			ChatEntry chatEntry = (ChatEntry)RuntimeServices.Coerce(enumerator.Current, typeof(ChatEntry));
			GUILayout.BeginHorizontal();
			if (chatEntry.origin == chatOrigins.Remote)
			{
				GUILayout.FlexibleSpace();
				GUILayout.Label(chatEntry.text, "chatRemote");
				UnityRuntimeServices.Update(enumerator, chatEntry);
			}
			else if (chatEntry.origin == chatOrigins.Local)
			{
				GUILayout.Label(chatEntry.text, "chatLocal");
				UnityRuntimeServices.Update(enumerator, chatEntry);
				GUILayout.FlexibleSpace();
			}
			else
			{
				GUILayout.FlexibleSpace();
				GUILayout.Label(chatEntry.text, "chatServer");
				UnityRuntimeServices.Update(enumerator, chatEntry);
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(3f);
		}
		GUILayout.EndScrollView();
		GUILayout.FlexibleSpace();
		if (Event.current.type == EventType.keyDown && Event.current.character == '\n' && inputField.Length > 0)
		{
			if (inputField == "x" || inputField == "/x" || inputField == "/X")
			{
				if (Game.Settings.zorbSpeed != 0f)
				{
					Game.Player.networkView.RPC("sZ", RPCMode.All, !Game.PlayerVeh.zorbBall);
					Game.Controller.msg("XORB " + ((!Game.PlayerVeh.zorbBall) ? "Deactivated" : "Activated"), 2);
				}
				else
				{
					Game.Controller.msg("XORBs Unavailable", 2);
				}
			}
			else if (inputField == "r" || inputField == "/r" || inputField == "/R")
			{
				Game.Settings.resetTime = Time.time;
				Game.Player.rigidbody.isKinematic = true;
				broadcast(Game.Player.name + " Resetting in 10 seconds...");
			}
			else
			{
				inputField = Game.LanguageFilter(inputField);
				Game.Controller.msg(inputField, 0);
				Game.Controller.networkView.RPC("msg", RPCMode.Others, inputField + " - " + GameData.userName, UnityBuiltins.parseInt(1));
			}
			inputField = string.Empty;
			chatting = false;
			GUI.UnfocusWindow();
		}
		GUI.SetNextControlName("Chat input field");
		inputField = GUILayout.TextField(inputField, 300);
		if (chatting && inputField == string.Empty)
		{
			GUILayout.Label("(Press \"Tab\" to Cancel)");
		}
		else if (chatting)
		{
			GUILayout.Label("(Press \"Enter\" to Send)");
		}
		else
		{
			GUILayout.Label("(Press \"Tab\" to Message)");
		}
		if (chatting)
		{
			GUI.FocusControl("Chat input field");
			GUI.FocusWindow(id);
		}
		checked
		{
			if (showChat < 0 && showChat > -5)
			{
				showChat--;
			}
			else
			{
				if (showChat >= 0 && ((!Input.GetKeyDown(KeyCode.Tab) && !Input.GetKeyDown(KeyCode.Mouse0)) || !(Time.time > Game.Controller.kpTime)))
				{
					return;
				}
				Game.Controller.kpTime = Time.time + Game.Controller.kpDur;
				if ((!chatting && !Input.GetKeyDown(KeyCode.Mouse0)) || (windowRect.Contains(Input.mousePosition) && Input.GetKeyDown(KeyCode.Mouse0)))
				{
					if (showChat == 1)
					{
						chatting = true;
					}
					showChat = 1;
				}
				else
				{
					chatting = false;
					if (Input.GetKeyDown(KeyCode.Tab))
					{
						GUI.UnfocusWindow();
					}
				}
			}
		}
	}

	public void broadcast(string str)
	{
		Game.Controller.networkView.RPC("msg", RPCMode.All, str, UnityBuiltins.parseInt(2));
	}

	public void Main()
	{
	}
}
