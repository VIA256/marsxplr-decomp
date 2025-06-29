using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GUIElement : Behaviour
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HitTest(Vector3 screenPosition, Camera camera);

		public bool HitTest(Vector3 screenPosition)
		{
			Camera camera = null;
			return HitTest(screenPosition, camera);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect GetScreenRect(Camera camera);

		public Rect GetScreenRect()
		{
			Camera camera = null;
			return GetScreenRect(camera);
		}
	}
}
