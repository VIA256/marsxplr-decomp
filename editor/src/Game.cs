using System;
using UnityEngine;

[Serializable]
public class Game : MonoBehaviour
{	
	public GUISkin Skin;
	float TopElemWidth = Screen.width / 19f;
	float TopElemHeight = Screen.height / 38;
	bool fileMenu = false;
	bool fileOpenMenu = false;
	string fileOpenUrl = "http://gitea.moe/lamp/whirlds/raw/branch/master/Geiodo/bagels_skate_park.utw";
	float fileOpenTimeout = 0f;
	
	public static bool textInput = false;

	public void loadPrefs()
	{
		string openWhirldDefault = PlayerPrefs.GetString("openWhirldDefault", "");
		if(openWhirldDefault != "") fileOpenUrl = openWhirldDefault;
	}

	public void Start()
	{
		Camera cam = Camera.main;
		cam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		cam.transform.position = new Vector3(0f, 0f, 0f);
		foreach (ParticleEmitter pE in UnityEngine.Object.FindObjectsOfType(typeof(ParticleEmitter)))
        {
            pE.emit = false;
        }
		
		loadPrefs();
	}
	
	public void Update()
	{
		if(fileOpenTimeout > 0f)
			fileOpenTimeout -= Time.deltaTime;
	}
	
	public void OnGUI()
	{
		if(CameraVehicle.moveCam) return;
		
		if(GUI.Button(
			new Rect(0, 0, TopElemWidth, TopElemHeight),
			"File"))
		{
			if(fileMenu && !fileOpenMenu)
			{
				fileMenu = false;
			}
			else
			{
				fileMenu = true;
			}
		}
		if(fileMenu)
		{
			ShowFileMenu();
		}
		
		if(fileOpenMenu)
		{
			textInput = true;
		}
		else
		{
			textInput = false;
		}
		
		//handle utility keybinds
		if(textInput) return;
		if(Input.GetKey(KeyCode.LeftControl))
		{
			if(Input.GetKeyDown(KeyCode.N))
			{
				onFileNew();
			}
			else if(Input.GetKeyDown(KeyCode.O))
			{
				fileMenu = true;
				fileOpenMenu = true;
			}
			else if(Input.GetKeyDown(KeyCode.Q))
			{
				Application.Quit();
			}
		}
	}
	
	private void onFileNew()
	{
		if(!GameObject.Find("WhirldBuffer"))
				WhirldIn.ResetSpace();
	}
	
	private void ShowFileMenu()
	{
		if(GUI.Button(
			new Rect(0, TopElemHeight, TopElemWidth, TopElemHeight),
			"New"))
		{
			onFileNew();
		}
		
		if(GUI.Button(
			new Rect(0, TopElemHeight * 2, TopElemWidth, TopElemHeight),
			"Open"))
		{
			fileOpenMenu = !fileOpenMenu;
		}
		if(fileOpenMenu)
		{
			ShowFileOpenMenu();
		}
		
		if(GUI.Button(
			new Rect(0, TopElemHeight * 3, TopElemWidth, TopElemHeight),
			"Exit"))
		{
			Application.Quit();
		}
	}
	
	private void ShowFileOpenMenu()
	{
		GUI.SetNextControlName("FileOpenTF");
		fileOpenUrl = GUI.TextField(
			new Rect(TopElemWidth, TopElemHeight * 2, TopElemWidth * 3, TopElemHeight),
			fileOpenUrl);
		GUI.FocusControl("FileOpenTF");
		if(GUI.Button(
				new Rect(TopElemWidth * 4, TopElemHeight * 2, TopElemWidth, TopElemHeight),
				"Submit") ||
			Input.GetKeyDown(KeyCode.Return))
		{
			if(!GameObject.Find("WhirldBuffer") && fileOpenTimeout <= 0)
			{
				fileOpenTimeout = 1f;
				
				WhirldIn wi = new WhirldIn();
				wi.url = fileOpenUrl;
				wi.Load();
				
				fileOpenMenu = false;
				fileMenu = false;
			}
		} else if(Input.GetKeyDown(KeyCode.Escape))
		{
			fileOpenMenu = false;
			fileMenu = false;
		}
	}
}