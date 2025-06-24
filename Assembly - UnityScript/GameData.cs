using System;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
	public static float gameVersion = 2.3f;

	public static float serverVersion = 0.2f;

	public static string gameName = "marsxplr";

	public static string userName = string.Empty;

	public static string userCode = string.Empty;

	public static string errorMessage = string.Empty;

	public static string masterBlacklist = string.Empty;

	public static GameWorldDesc[] gameWorlds;

	public static int networkMode = 0;

	public void Main()
	{
	}
}
