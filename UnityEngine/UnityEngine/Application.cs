using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	public class Application
	{
		public delegate void LogCallback(string condition, string stackTrace, LogType type);

		private static LogCallback s_LogCallback;

		public static extern int loadedLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string loadedLevelName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isLoadingLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int levelCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int streamedBytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isEditor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern RuntimePlatform platform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool runInBackground
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !isEditor;
			}
		}

		public static extern string dataPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string srcValue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string absoluteURL
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Please use absoluteURL instead")]
		public static string absoluteUrl
		{
			get
			{
				return absoluteURL;
			}
		}

		public static extern string unityVersion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int targetFrameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern SystemLanguage systemLanguage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ThreadPriority backgroundLoadingPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelQuit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadLevel(int index);

		public static void LoadLevel(string name)
		{
			LoadLevelName(name, false);
		}

		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return LoadLevelAsync(levelName, -1, false);
		}

		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return LoadLevelAsync(levelName, -1, true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation LoadLevelAsync(string monoLevelName, int index, bool additive);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadLevelAdditive(int index);

		public static void LoadLevelAdditive(string name)
		{
			LoadLevelName(name, true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadLevelName(string name, bool add);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStreamProgressForLevelByName(string levelName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStreamProgressForLevel(int levelIndex);

		public static float GetStreamProgressForLevel(string levelName)
		{
			return GetStreamProgressForLevelByName(levelName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanStreamedLevelBeLoadedByName(string levelName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CanStreamedLevelBeLoaded(int levelIndex);

		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return CanStreamedLevelBeLoadedByName(levelName);
		}

		public static void CaptureScreenshot(string filename)
		{
			Internal_CaptureScreenshot(Path.GetFullPath(filename));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static extern void DontDestroyOnLoad(Object mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CaptureScreenshot(string filename);

		private static string ObjectToJSString(object o)
		{
			if (o == null)
			{
				return "null";
			}
			if (o is string)
			{
				string text = o.ToString().Replace("\"", "\\\"");
				return '"' + text + '"';
			}
			if (o is int || o is short || o is uint || o is ushort || o is byte)
			{
				return o.ToString();
			}
			if (o is float)
			{
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				float num = (float)o;
				return num.ToString(numberFormat);
			}
			if (o is double)
			{
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				double num2 = (double)o;
				return num2.ToString(numberFormat2);
			}
			if (o is char)
			{
				if ((char)o == '"')
				{
					return "\"\\\"\"";
				}
				return '"' + o.ToString() + '"';
			}
			if (o is IList)
			{
				IList list = (IList)o;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("new Array(");
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(ObjectToJSString(list[i]));
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
			return ObjectToJSString(o.ToString());
		}

		public static void ExternalCall(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			Internal_ExternalCall(stringBuilder.ToString());
		}

		public static void ExternalEval(string script)
		{
			if (script.Length > 0 && script[script.Length - 1] != ';')
			{
				script += ';';
			}
			Internal_ExternalCall(script);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExternalCall(string script);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBuildUnityVersion();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumericUnityVersion(string version);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OpenURL(string url);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CommitSuicide(int mode);

		public static void RegisterLogCallback(LogCallback handler)
		{
			s_LogCallback = handler;
			SetLogCallbackDefined(handler != null);
		}

		private static void CallLogCallback(string logString, string stackTrace, LogType type)
		{
			if (s_LogCallback != null)
			{
				s_LogCallback(logString, stackTrace, type);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLogCallbackDefined(bool defined);
	}
}
