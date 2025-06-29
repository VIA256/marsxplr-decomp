using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct NetworkViewID
	{
		private int a;

		private int b;

		private int c;

		public static extern NetworkViewID unassigned
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public bool isMine
		{
			get
			{
				return Internal_IsMine(this);
			}
		}

		public NetworkPlayer owner
		{
			get
			{
				NetworkPlayer player;
				Internal_GetOwner(this, out player);
				return player;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_IsMine(NetworkViewID value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_GetOwner(NetworkViewID value, out NetworkPlayer player);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_GetString(NetworkViewID value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_Compare(NetworkViewID lhs, NetworkViewID rhs);

		public override int GetHashCode()
		{
			return a ^ b ^ c;
		}

		public override bool Equals(object other)
		{
			if (!(other is NetworkViewID))
			{
				return false;
			}
			NetworkViewID rhs = (NetworkViewID)other;
			return Internal_Compare(this, rhs);
		}

		public override string ToString()
		{
			return Internal_GetString(this);
		}

		public static bool operator ==(NetworkViewID lhs, NetworkViewID rhs)
		{
			return Internal_Compare(lhs, rhs);
		}

		public static bool operator !=(NetworkViewID lhs, NetworkViewID rhs)
		{
			return !Internal_Compare(lhs, rhs);
		}
	}
}
