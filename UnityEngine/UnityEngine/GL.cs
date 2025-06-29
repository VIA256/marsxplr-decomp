using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GL
	{
		public const int TRIANGLES = 4;

		public const int TRIANGLE_STRIP = 5;

		public const int QUADS = 7;

		public const int LINES = 1;

		public static extern Matrix4x4 modelview
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex(Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Color(Color c);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord(Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord2(float x, float y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord2(int unit, float x, float y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord(int unit, Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadPixelMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadPixelMatrixArgs(float left, float right, float bottom, float top);

		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			LoadPixelMatrixArgs(left, right, bottom, top);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Viewport(Rect pixelRect);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadProjectionMatrix(Matrix4x4 mat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadIdentity();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultMatrix(Matrix4x4 mat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear(bool clearDepth, bool clearColor, Color backgroundColor);
	}
}
