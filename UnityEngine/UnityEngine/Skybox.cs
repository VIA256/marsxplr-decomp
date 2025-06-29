using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Skybox : Behaviour
	{
		public extern Material material
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
