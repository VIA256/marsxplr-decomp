using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[RequireComponent(typeof(Camera))]
public class MiniMap : MonoBehaviour
{
	public float camHeight;

	public UnityScript.Lang.Array terrains;

	private bool revertFogState;

	private bool terrainLighting;

	private int terrainLOD;

	private int terrainTreeDistance;

	private int terrainDetailDistance;

	private int terrainBasemapDistance;

	public MiniMap()
	{
		camHeight = 650f;
		terrains = new UnityScript.Lang.Array();
		revertFogState = false;
		terrainLighting = false;
		terrainLOD = 0;
		terrainTreeDistance = 0;
		terrainDetailDistance = 0;
		terrainBasemapDistance = 0;
	}

	public void Update()
	{
		if (!Game.Settings.minimapAllowed || !Game.Settings.useMinimap || Game.Controller.loadingWorld || !Game.Player)
		{
			camera.enabled = false;
			return;
		}
		camera.enabled = true;
		transform.position = Camera.main.transform.position;
		float y = Game.Player.transform.position.y + camHeight;
		Vector3 position = transform.position;
		float num = (position.y = y);
		Vector3 vector = (transform.position = position);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90f, Game.Player.transform.eulerAngles.y, 0f), Time.deltaTime);
	}

	public void OnPreRender()
	{
		revertFogState = RenderSettings.fog;
		RenderSettings.fog = false;
		if (World.terrains == null || Extensions.get_length((System.Array)World.terrains) <= 0)
		{
			return;
		}
		if (World.terrains[0].lighting == TerrainLighting.Pixel)
		{
			terrainLighting = true;
		}
		else
		{
			terrainLighting = false;
		}
		terrainLOD = World.terrains[0].heightmapMaximumLOD;
		checked
		{
			terrainTreeDistance = (int)World.terrains[0].treeDistance;
			terrainDetailDistance = (int)World.terrains[0].detailObjectDistance;
			int i = 0;
			Terrain[] array = World.terrains;
			for (int length = array.Length; i < length; i++)
			{
				if (Game.Settings.renderLevel > 4)
				{
					array[i].heightmapMaximumLOD = 3;
				}
				else if (Game.Settings.renderLevel > 3)
				{
					array[i].heightmapMaximumLOD = 4;
				}
				else
				{
					array[i].heightmapMaximumLOD = 5;
				}
				array[i].lighting = TerrainLighting.Lightmap;
				array[i].treeDistance = 0f;
				array[i].detailObjectDistance = 0f;
			}
		}
	}

	public void OnPostRender()
	{
		RenderSettings.fog = revertFogState;
		if (World.terrains != null)
		{
			int i = 0;
			Terrain[] array = World.terrains;
			for (int length = array.Length; i < length; i = checked(i + 1))
			{
				array[i].lighting = ((!terrainLighting) ? TerrainLighting.Lightmap : TerrainLighting.Pixel);
				array[i].treeDistance = terrainTreeDistance;
				array[i].detailObjectDistance = terrainDetailDistance;
				array[i].heightmapMaximumLOD = terrainLOD;
			}
		}
	}

	public void OnGUI()
	{
		if (Game.Settings.minimapAllowed && Game.Settings.useMinimap && (bool)Game.Player)
		{
			camHeight = GUI.HorizontalSlider(new Rect((float)Screen.width * 0.01f + 25f, (float)checked(Screen.height - 20) - (float)Screen.height * 0.001f, (float)Screen.width * 0.25f - 50f, 20f), camHeight, 200f, 1300f);
		}
	}

	public void Main()
	{
	}
}
