using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[ExecuteInEditMode]
public class SeaData : MonoBehaviour
{
	public WhirldObject whirldObject;

	public GameObject seaObject;

	public GameObject seaObjectSimple;

	public GameObject seaObjectSimBot;

	public SeaModes SeaMode;

	private SeaModes setMode;

	public SeaModeData[] seaModeData;

	public SeaData()
	{
		SeaMode = SeaModes.Tropic;
		setMode = SeaModes.unset;
	}

	public void Start()
	{
		if (!(whirldObject == null) && !RuntimeServices.EqualityOperator(whirldObject.@params, null) && !(seaObject == null) && RuntimeServices.ToBool(whirldObject.@params["Mode"]))
		{
			SeaMode = (SeaModes)Enum.Parse(typeof(SeaModes), whirldObject.@params["Mode"].ToString(), true);
		}
	}

	public void Update()
	{
		if (SeaMode != setMode)
		{
			SetSeaMode();
		}
	}

	public void SetSeaMode()
	{
		setMode = SeaMode;
		Material sharedMaterial = seaObject.renderer.sharedMaterial;
		SeaModeData[] array = seaModeData;
		checked
		{
			sharedMaterial.SetColor("_RefrColor", array[RuntimeServices.NormalizeArrayIndex(array, UnityBuiltins.parseInt((int)SeaMode))].color);
			Material sharedMaterial2 = seaObject.renderer.sharedMaterial;
			SeaModeData[] array2 = seaModeData;
			sharedMaterial2.SetFloat("_WaveScale", array2[RuntimeServices.NormalizeArrayIndex(array2, UnityBuiltins.parseInt((int)SeaMode))].waves);
			Material sharedMaterial3 = seaObject.renderer.sharedMaterial;
			SeaModeData[] array3 = seaModeData;
			sharedMaterial3.SetFloat("_ReflDistort", array3[RuntimeServices.NormalizeArrayIndex(array3, UnityBuiltins.parseInt((int)SeaMode))].reflection);
			Material sharedMaterial4 = seaObject.renderer.sharedMaterial;
			SeaModeData[] array4 = seaModeData;
			sharedMaterial4.SetFloat("_RefrDistort", array4[RuntimeServices.NormalizeArrayIndex(array4, UnityBuiltins.parseInt((int)SeaMode))].refraction);
			Material sharedMaterial5 = seaObjectSimple.renderer.sharedMaterial;
			SeaModeData[] array5 = seaModeData;
			sharedMaterial5.SetColor("_Color", array5[RuntimeServices.NormalizeArrayIndex(array5, UnityBuiltins.parseInt((int)SeaMode))].color);
			Material sharedMaterial6 = seaObjectSimBot.renderer.sharedMaterial;
			SeaModeData[] array6 = seaModeData;
			sharedMaterial6.SetColor("_Color", array6[RuntimeServices.NormalizeArrayIndex(array6, UnityBuiltins.parseInt((int)SeaMode))].glowColor);
			float a = 0.85f;
			Color color = seaObjectSimple.renderer.sharedMaterial.color;
			float num = (color.a = a);
			Color color2 = (seaObjectSimple.renderer.sharedMaterial.color = color);
			float a2 = 0.85f;
			Color color4 = seaObjectSimBot.renderer.sharedMaterial.color;
			float num2 = (color4.a = a2);
			Color color5 = (seaObjectSimBot.renderer.sharedMaterial.color = color4);
			SeaModeData[] array7 = seaModeData;
			World.seaFogColor = array7[RuntimeServices.NormalizeArrayIndex(array7, UnityBuiltins.parseInt((int)SeaMode))].color;
			SeaModeData[] array8 = seaModeData;
			World.seaGlowColor = array8[RuntimeServices.NormalizeArrayIndex(array8, UnityBuiltins.parseInt((int)SeaMode))].glowColor;
		}
	}

	public void Main()
	{
	}
}
