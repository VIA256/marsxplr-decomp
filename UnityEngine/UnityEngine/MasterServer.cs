using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class MasterServer
	{
		public static extern string ipAddress
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int port
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int updateRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool dedicatedServer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RequestHostList(string gameTypeName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern HostData[] PollHostList();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterHost(string gameTypeName, string gameName, string comment);

		public static void RegisterHost(string gameTypeName, string gameName)
		{
			string comment = "";
			RegisterHost(gameTypeName, gameName, comment);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterHost();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearHostList();
	}
}
