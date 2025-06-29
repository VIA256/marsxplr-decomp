using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Debug
	{
		public static extern bool isDebugBuild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawLine(Vector3 start, Vector3 end, Color color);

		public static void DrawLine(Vector3 start, Vector3 end)
		{
			Color white = Color.white;
			DrawLine(start, end, white);
		}

		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			Color white = Color.white;
			DrawRay(start, dir, white);
		}

		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			DrawLine(start, start + dir, color);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Log(int level, string msg, Object obj);

		public static void Log(object message)
		{
			Internal_Log(0, (message == null) ? "Null" : message.ToString(), null);
		}

		public static void Log(object message, Object context)
		{
			Internal_Log(0, (message == null) ? "Null" : message.ToString(), context);
		}

		public static void LogError(object message)
		{
			Internal_Log(2, message.ToString(), null);
		}

		public static void LogError(object message, Object context)
		{
			Internal_Log(2, message.ToString(), context);
		}

		public static void LogWarning(object message)
		{
			Internal_Log(1, message.ToString(), null);
		}

		public static void LogWarning(object message, Object context)
		{
			Internal_Log(1, message.ToString(), context);
		}
	}
}
