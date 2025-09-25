using System.Runtime.CompilerServices;
using Boo.Lang;

namespace UnityEngine
{
	public class Network
	{
		public static extern string incomingPassword
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern NetworkPlayer[] connections
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static NetworkPlayer player
		{
			get
			{
				NetworkPlayer result = default(NetworkPlayer);
				result.index = Internal_GetPlayer();
				return result;
			}
		}

		public static extern bool isClient
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isServer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern NetworkPeerType peerType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float sendRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool isMessageQueueRunning
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static double time
		{
			get
			{
				double t;
				Internal_GetTime(out t);
				return t;
			}
		}

		public static extern int minimumAllocatableViewIDs
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool useNat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string natFacilitatorIP
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int natFacilitatorPort
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string connectionTesterIP
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int connectionTesterPort
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int maxConnections
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string proxyIP
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int proxyPort
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool useProxy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string proxyPassword
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkConnectionError InitializeServer(int connections, int listenPort);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeSecurity();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort, string password);

		private static NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort)
		{
			string password = "";
			return Internal_ConnectToSingleIP(IP, remotePort, localPort, password);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort, string password);

		private static NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort)
		{
			string password = "";
			return Internal_ConnectToIPs(IP, remotePort, localPort, password);
		}

		public static NetworkConnectionError Connect(string IP, int remotePort)
		{
			string password = "";
			return Connect(IP, remotePort, password);
		}

		public static NetworkConnectionError Connect(string IP, int remotePort, string password)
		{
			return Internal_ConnectToSingleIP(IP, remotePort, 0, password);
		}

		public static NetworkConnectionError Connect(string[] IPs, int remotePort)
		{
			string password = "";
			return Connect(IPs, remotePort, password);
		}

		public static NetworkConnectionError Connect(string[] IPs, int remotePort, string password)
		{
			return Internal_ConnectToIPs(IPs, remotePort, 0, password);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Disconnect(int timeout);

		public static void Disconnect()
		{
			int timeout = 200;
			Disconnect(timeout);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayer();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_AllocateViewID(out NetworkViewID viewID);

		public static NetworkViewID AllocateViewID()
		{
			NetworkViewID viewID;
			Internal_AllocateViewID(out viewID);
			return viewID;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public static extern Object Instantiate(Object prefab, Vector3 position, Quaternion rotation, int group);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(NetworkViewID viewID);

		public static void Destroy(GameObject gameObject)
		{
			if (gameObject != null)
			{
				NetworkView networkView = gameObject.networkView;
				if (networkView != null)
				{
					Destroy(networkView.viewID);
				}
				else
				{
					Debug.LogError("Couldn't destroy game object because no network view is attached to it.", gameObject);
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyPlayerObjects(NetworkPlayer playerID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RemoveRPCs(NetworkPlayer playerID, NetworkViewID viewID, uint channelMask);

		public static void RemoveRPCs(NetworkPlayer playerID)
		{
			Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, uint.MaxValue);
		}

		public static void RemoveRPCs(NetworkPlayer playerID, int group)
		{
			Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, (uint)(1 << group));
		}

		public static void RemoveRPCsInGroup(int group)
		{
			Internal_RemoveRPCs(NetworkPlayer.unassigned, NetworkViewID.unassigned, (uint)(1 << group));
		}

		public static void RemoveRPCs(NetworkViewID viewID)
		{
			Internal_RemoveRPCs(NetworkPlayer.unassigned, viewID, uint.MaxValue);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLevelPrefix(int prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastPing(NetworkPlayer player);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAveragePing(NetworkPlayer player);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetReceivingEnabled(NetworkPlayer player, int group, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingGlobal(int group, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingSpecific(NetworkPlayer player, int group, bool enabled);

		public static void SetSendingEnabled(int group, bool enabled)
		{
			Internal_SetSendingGlobal(group, enabled);
		}

		public static void SetSendingEnabled(NetworkPlayer player, int group, bool enabled)
		{
			Internal_SetSendingSpecific(player, group, enabled);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTime(out double t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnection(bool forceTest);

		public static ConnectionTesterStatus TestConnection()
		{
			bool forceTest = false;
			return TestConnection(forceTest);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnectionNAT();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HavePublicAddress();
	}
}
