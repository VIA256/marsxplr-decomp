using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct NetworkPlayer
	{
		internal int index;

		public string ipAddress
		{
			get
			{
				if (index == Internal_GetPlayerIndex())
				{
					return Internal_GetLocalIP();
				}
				return Internal_GetIPAddress(index);
			}
		}

		public int port
		{
			get
			{
				if (index == Internal_GetPlayerIndex())
				{
					return Internal_GetLocalPort();
				}
				return Internal_GetPort(index);
			}
		}

		public string externalIP
		{
			get
			{
				return Internal_GetExternalIP();
			}
		}

		public int externalPort
		{
			get
			{
				return Internal_GetExternalPort();
			}
		}

		internal static NetworkPlayer unassigned
		{
			get
			{
				NetworkPlayer result = default(NetworkPlayer);
				result.index = -1;
				return result;
			}
		}

		public NetworkPlayer(string ip, int port)
		{
			Debug.LogError("Not yet implemented");
			index = 0;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetIPAddress(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPort(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetExternalIP();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetExternalPort();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetLocalIP();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetLocalPort();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayerIndex();

		public override int GetHashCode()
		{
			return index.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if (!(other is NetworkPlayer))
			{
				return false;
			}
			NetworkPlayer networkPlayer = (NetworkPlayer)other;
			return networkPlayer.index == index;
		}

		public override string ToString()
		{
			return index.ToString();
		}

		public static bool operator ==(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index == rhs.index;
		}

		public static bool operator !=(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index != rhs.index;
		}
	}
}
