using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GUILayer : Behaviour
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern GUIElement HitTest(Vector3 screenPosition);
	}
}
