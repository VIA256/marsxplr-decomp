using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Input
	{
		public static extern Vector3 mousePosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool anyKeyDown
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string inputString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(int key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(int key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(int key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxisRaw(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonDown(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonUp(string buttonName);

		public static bool GetKey(string name)
		{
			return GetKeyString(name);
		}

		public static bool GetKey(KeyCode key)
		{
			return GetKeyInt((int)key);
		}

		public static bool GetKeyDown(string name)
		{
			return GetKeyDownString(name);
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return GetKeyDownInt((int)key);
		}

		public static bool GetKeyUp(string name)
		{
			return GetKeyUpString(name);
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return GetKeyUpInt((int)key);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();
	}
}
