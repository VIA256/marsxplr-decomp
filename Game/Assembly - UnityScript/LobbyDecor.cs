using System;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class LobbyDecor : MonoBehaviour
{
	public Lobby Lobby;

	public GUITexture bg;

	public int logoOffset;

	public void Awake()
	{
		if (Time.time > 6f)
		{
			int num = 0;
			Color color = guiTexture.color;
			float num2 = (color.a = num);
			Color color2 = (guiTexture.color = color);
		}
		else if ((bool)bg)
		{
			bg.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		}
	}

	public void Update()
	{
		float num = (float)Screen.height * 0.84f;
		if (num > 600f)
		{
			num -= (num - 600f) * 0.5f;
		}
		checked
		{
			if (num > (float)(Screen.width - 30))
			{
				num = Screen.width - 30;
			}
			if (Time.time > 1.5f && !Camera.main.audio.isPlaying && PlayerPrefs.GetInt("useMusic", 1) != 0)
			{
				Camera.main.audio.Play();
			}
		}
		if (Time.time < 5f)
		{
			float a = 1f - Time.time / 4f;
			Color color = bg.color;
			float num2 = (color.a = a);
			Color color2 = (bg.color = color);
			if (Time.time < 2.5f)
			{
				float a2 = Mathf.Lerp(0f, 0.6f, Time.time * (1f / 2.5f));
				Color color4 = guiTexture.color;
				float num3 = (color4.a = a2);
				Color color5 = (guiTexture.color = color4);
				guiTexture.pixelInset = new Rect((float)(Screen.width / 2) - num / 2f, (float)(Screen.height / 2) - num / 4f, num, num / 2f);
			}
			if (Time.time > 4.25f)
			{
				guiTexture.pixelInset = new Rect((float)(Screen.width / 2) - num / 2f, RuntimeServices.UnboxSingle(easeOutExpo(Time.time - 4.25f, (float)(Screen.height / 2) - num / 4f, (float)Screen.height - num / 2f, 0.75f)), num, num / 2f);
			}
		}
		else if (Time.time > 6f)
		{
			if ((bool)bg)
			{
				UnityEngine.Object.Destroy(bg);
			}
			float a3 = Mathf.Lerp(guiTexture.color.a, Lobby.GUIAlpha - 0.4f, Time.deltaTime * 4f);
			Color color7 = guiTexture.color;
			float num4 = (color7.a = a3);
			Color color8 = (guiTexture.color = color7);
			guiTexture.pixelInset = new Rect((float)(Screen.width / 2) - num / 2f, (float)Screen.height - num / 2f, num, num / 2f);
		}
		logoOffset = checked((int)(num / 2f));
	}

	[DuckTyped]
	public object easeOutExpo(object t, object b, object c, object d)
	{
		c = RuntimeServices.InvokeBinaryOperator("op_Subtraction", c, b);
		return RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Multiply", c, Mathf.Pow(2f, RuntimeServices.UnboxSingle(RuntimeServices.InvokeBinaryOperator("op_Division", RuntimeServices.InvokeBinaryOperator("op_Multiply", -10, t), d))) * -1f + 1f), b);
	}

	[DuckTyped]
	public object easeInOutExpo(object t, object b, object c, object d)
	{
		c = RuntimeServices.InvokeBinaryOperator("op_Subtraction", c, b);
		if (RuntimeServices.ToBool(RuntimeServices.InvokeBinaryOperator("op_LessThan", t, RuntimeServices.InvokeBinaryOperator("op_Division", d, 2))))
		{
			return RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Division", RuntimeServices.InvokeBinaryOperator("op_Multiply", RuntimeServices.InvokeBinaryOperator("op_Multiply", RuntimeServices.InvokeBinaryOperator("op_Multiply", 2, c), t), t), RuntimeServices.InvokeBinaryOperator("op_Multiply", d, d)), b);
		}
		object rhs = RuntimeServices.InvokeBinaryOperator("op_Subtraction", t, RuntimeServices.InvokeBinaryOperator("op_Division", d, 2));
		return RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Addition", RuntimeServices.InvokeBinaryOperator("op_Division", RuntimeServices.InvokeBinaryOperator("op_Multiply", RuntimeServices.InvokeBinaryOperator("op_Multiply", RuntimeServices.InvokeBinaryOperator("op_Multiply", -2, c), rhs), rhs), RuntimeServices.InvokeBinaryOperator("op_Multiply", d, d)), RuntimeServices.InvokeBinaryOperator("op_Division", RuntimeServices.InvokeBinaryOperator("op_Multiply", RuntimeServices.InvokeBinaryOperator("op_Multiply", 2, c), rhs), d)), RuntimeServices.InvokeBinaryOperator("op_Division", c, 2)), b);
	}

	public void Main()
	{
	}
}
