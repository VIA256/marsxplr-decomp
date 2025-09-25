using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class NetworkView : Behaviour
	{
		public extern Component observed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern NetworkStateSynchronization stateSynchronization
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public NetworkViewID viewID
		{
			get
			{
				NetworkViewID result;
				Internal_GetViewID(out result);
				return result;
			}
			set
			{
				Internal_SetViewID(value);
			}
		}

		public extern int group
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool isMine
		{
			get
			{
				return viewID.isMine;
			}
		}

		public NetworkPlayer owner
		{
			get
			{
				return viewID.owner;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC(NetworkView view, string name, RPCMode mode, object[] args);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC_Target(NetworkView view, string name, NetworkPlayer target, object[] args);

		public void RPC(string name, RPCMode mode, params object[] args)
		{
			Internal_RPC(this, name, mode, args);
		}

		public void RPC(string name, NetworkPlayer target, params object[] args)
		{
			Internal_RPC_Target(this, name, target, args);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetViewID(out NetworkViewID viewID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetViewID(NetworkViewID viewID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetScope(NetworkPlayer player, bool relevancy);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkView Find(NetworkViewID viewID);
	}
}
