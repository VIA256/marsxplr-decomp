using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class LineRenderer : Renderer
	{
		public extern bool useWorldSpace
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetWidth(float start, float end);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetColors(Color start, Color end);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVertexCount(int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPosition(int index, Vector3 position);
	}
}
