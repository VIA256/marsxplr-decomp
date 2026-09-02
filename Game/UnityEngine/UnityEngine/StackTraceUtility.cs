using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace UnityEngine
{
	public class StackTraceUtility
	{
		private static string projectFolder = "";

		public static void SetProjectFolder(string folder)
		{
			projectFolder = folder;
		}

		public static string ExtractStackTrace()
		{
			StackTrace stackTrace = new StackTrace(1, true);
			return ExtractFormattedStackTrace(stackTrace).ToString();
		}

		private static bool IsSystemStacktraceType(object name)
		{
			string text = (string)name;
			return text.StartsWith("UnityEditor.") || text.StartsWith("UnityEngine.") || text.StartsWith("System.") || text.StartsWith("UnityScript.Lang.") || text.StartsWith("Boo.Lang.") || text.StartsWith("UnityEngine.SetupCoroutine");
		}

		public static string ExtractStringFromException(Exception exception)
		{
			string message = "";
			string stackTrace = "";
			ExtractStringFromExceptionInternal(exception, out message, out stackTrace);
			return message + "\n" + stackTrace;
		}

		internal static void ExtractStringFromExceptionInternal(Exception exception, out string message, out string stackTrace)
		{
			Exception ex = exception;
			if (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			message = exception.GetType().Name;
			if (exception.Message.Trim().Length != 0)
			{
				message += ": ";
				message += exception.Message;
			}
			StringBuilder stringBuilder = new StringBuilder(exception.StackTrace.Length * 2);
			if (exception.Message.IndexOf('\n') != -1)
			{
				stringBuilder.Append("\n");
			}
			string text = "";
			while (ex != null)
			{
				text = ((text.Length != 0) ? (text + ex.StackTrace) : ex.StackTrace);
				ex = ex.InnerException;
			}
			stringBuilder.Append(NicifyStacktrace(text));
			StackTrace stackTrace2 = new StackTrace(1, true);
			StringBuilder value = ExtractFormattedStackTrace(stackTrace2);
			stringBuilder.Append(value);
			stackTrace = stringBuilder.ToString();
		}

		public static StringBuilder NicifyStacktrace(string oldString)
		{
			string[] array = oldString.Split('\n');
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder(oldString.Length);
			string[] array2 = array;
			int num = array2.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array2[i];
				stringBuilder.Remove(0, stringBuilder.Length);
				if (text.Length == 0 || text[0] == '\n')
				{
					continue;
				}
				string text2 = text;
				text2 = text2.Trim();
				if (!text2.StartsWith("in (unmanaged)") && text2.IndexOf("(wrapper managed-to-native)") == -1 && text2.IndexOf("(wrapper delegate-invoke)") == -1 && text2.IndexOf("at <0x00000> <unknown method>") == -1)
				{
					if (text2.IndexOf("at ") != -1)
					{
						text2 = text2.Remove(0, 3);
					}
					int num2 = text2.IndexOf("[0x");
					int num3 = text2.IndexOf("]");
					if (num2 != -1 && num3 > num2)
					{
						text2 = text2.Remove(num2, num3 - num2 + 1);
					}
					text2 = text2.Replace(projectFolder, "");
					int num4 = text2.LastIndexOf(" in ");
					if (num4 != -1)
					{
						text2 = text2.Remove(num4, 4);
						text2 = text2.Insert(num4, "  (at ");
						text2 = text2.Insert(text2.Length, ")");
					}
					stringBuilder.Append(text2);
					arrayList.Add(stringBuilder.ToString());
				}
			}
			return ProcessSplitCleanup(arrayList);
		}

		public static StringBuilder ProcessSplitCleanup(ArrayList cleanedUpsplit)
		{
			StringBuilder stringBuilder = new StringBuilder(cleanedUpsplit.Count * 100);
			foreach (string item in cleanedUpsplit)
			{
				string text2 = item;
				if (text2.Contains("UnityEngine.Debug:Internal_Log(Int32, String, Object)"))
				{
					continue;
				}
				if (text2.Contains("UnityEngine.Debug:Log"))
				{
					int num = text2.IndexOf(") (at");
					if (num != -1)
					{
						text2 = text2.Substring(0, num + 1);
					}
				}
				stringBuilder.Append(text2);
				stringBuilder.Append("\n");
			}
			return stringBuilder;
		}

		public static StringBuilder ExtractFormattedStackTrace(StackTrace stackTrace)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				stringBuilder.Remove(0, stringBuilder.Length);
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (method == null)
				{
					continue;
				}
				Type declaringType = method.DeclaringType;
				if (declaringType == null)
				{
					continue;
				}
				string text = declaringType.Namespace;
				if (text != null && text.Length != 0)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(".");
				}
				stringBuilder.Append(declaringType.Name);
				stringBuilder.Append(":");
				stringBuilder.Append(method.Name);
				stringBuilder.Append("(");
				int j = 0;
				ParameterInfo[] parameters = method.GetParameters();
				bool flag = true;
				for (; j < parameters.Length; j++)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						flag = false;
					}
					stringBuilder.Append(parameters[j].ParameterType.Name);
				}
				stringBuilder.Append(")");
				string text2 = frame.GetFileName();
				if (text2 != null)
				{
					stringBuilder.Append(" (at ");
					if (text2.StartsWith(projectFolder))
					{
						text2 = text2.Substring(projectFolder.Length, text2.Length - projectFolder.Length);
					}
					stringBuilder.Append(text2);
					stringBuilder.Append(":");
					stringBuilder.Append(frame.GetFileLineNumber().ToString());
					stringBuilder.Append(")");
				}
				arrayList.Add(stringBuilder.ToString());
			}
			return ProcessSplitCleanup(arrayList);
		}
	}
}
