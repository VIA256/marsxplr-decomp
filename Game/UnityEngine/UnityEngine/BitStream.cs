using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class BitStream
	{
		private IntPtr m_Ptr;

		public extern bool isReading
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isWriting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializeb(ref int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializec(ref char value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializes(ref short value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializei(ref int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializef(ref float value, float maximumDelta);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializeq(ref Quaternion value, float maximumDelta);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializev(ref Vector3 value, float maximumDelta);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializen(ref NetworkViewID viewID);

		public void Serialize(ref bool value)
		{
			int value2 = (value ? 1 : 0);
			Serializeb(ref value2);
			value = ((value2 != 0) ? true : false);
		}

		public void Serialize(ref char value)
		{
			Serializec(ref value);
		}

		public void Serialize(ref short value)
		{
			Serializes(ref value);
		}

		public void Serialize(ref int value)
		{
			Serializei(ref value);
		}

		public void Serialize(ref float value)
		{
			float maxDelta = 1E-05f;
			Serialize(ref value, maxDelta);
		}

		public void Serialize(ref float value, float maxDelta)
		{
			Serializef(ref value, maxDelta);
		}

		public void Serialize(ref Quaternion value)
		{
			float maxDelta = 1E-05f;
			Serialize(ref value, maxDelta);
		}

		public void Serialize(ref Quaternion value, float maxDelta)
		{
			Serializeq(ref value, maxDelta);
		}

		public void Serialize(ref Vector3 value)
		{
			float maxDelta = 1E-05f;
			Serialize(ref value, maxDelta);
		}

		public void Serialize(ref Vector3 value, float maxDelta)
		{
			Serializev(ref value, maxDelta);
		}

		public void Serialize(ref NetworkPlayer value)
		{
			int value2 = value.index;
			Serializei(ref value2);
			value.index = value2;
		}

		public void Serialize(ref NetworkViewID viewID)
		{
			Serializen(ref viewID);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serialize(ref string value);
	}
}
