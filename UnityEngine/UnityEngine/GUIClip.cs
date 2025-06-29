using System.Collections;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GUIClip
	{
		private static Matrix4x4 s_Matrix = Matrix4x4.identity;

		private static Matrix4x4 s_InverseMatrix = Matrix4x4.identity;

		internal static Vector2 s_AbsoluteMousePosition;

		internal static Vector2 s_AbsoluteLastMousePosition;

		internal static bool enabled = true;

		private Rect physicalRect;

		private Vector2 scrollOffset;

		private Vector2 globalScrollOffset;

		private Vector2 renderOffset;

		private static Rect s_VisibleRect;

		private static float o1 = -10000f;

		private static float o2 = 10000f;

		private static float o3 = 0f;

		private static float o4 = 0f;

		public static Stack s_GUIClips = new Stack();

		public static Rect topmostRect
		{
			get
			{
				return ((GUIClip)s_GUIClips.Peek()).physicalRect;
			}
		}

		public static Matrix4x4 matrix
		{
			get
			{
				return s_Matrix;
			}
			set
			{
				Matrix4x4 dest;
				if (!Matrix4x4.Invert(value, out dest))
				{
					Debug.LogError("Ignoring invalid matrix assinged to GUI.matrix - the matrix needs to be invertible. Did you scale by 0 on Z-axis?");
					return;
				}
				s_Matrix = value;
				s_InverseMatrix = dest;
				Reapply();
			}
		}

		public static Rect visibleRect
		{
			get
			{
				return s_VisibleRect;
			}
		}

		private GUIClip(Rect physicalRect, Vector2 scrollOffset, Vector2 renderOffset, Vector2 globalScrollOffset)
		{
			this.physicalRect = physicalRect;
			this.scrollOffset = scrollOffset;
			this.renderOffset = renderOffset;
			this.globalScrollOffset = globalScrollOffset;
		}

		public static void Push(Rect screenRect)
		{
			Push(screenRect, Vector2.zero, Vector2.zero, false);
		}

		public static void Push(Rect screenRect, Vector2 scrollOffset)
		{
			Push(screenRect, scrollOffset, Vector2.zero, false);
		}

		public static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			float num = screenRect.x + gUIClip.physicalRect.x + gUIClip.scrollOffset.x;
			float num2 = screenRect.xMax + gUIClip.physicalRect.x + gUIClip.scrollOffset.x;
			float num3 = screenRect.y + gUIClip.physicalRect.y + gUIClip.scrollOffset.y;
			float num4 = screenRect.yMax + gUIClip.physicalRect.y + gUIClip.scrollOffset.y;
			if (num < gUIClip.physicalRect.x)
			{
				scrollOffset.x += num - gUIClip.physicalRect.x;
				num = gUIClip.physicalRect.x;
			}
			if (num3 < gUIClip.physicalRect.y)
			{
				scrollOffset.y += num3 - gUIClip.physicalRect.y;
				num3 = gUIClip.physicalRect.y;
			}
			if (num2 > gUIClip.physicalRect.xMax)
			{
				num2 = gUIClip.physicalRect.xMax;
			}
			if (num4 > gUIClip.physicalRect.yMax)
			{
				num4 = gUIClip.physicalRect.yMax;
			}
			if (num2 <= num)
			{
				num2 = num;
			}
			if (num4 <= num3)
			{
				num4 = num3;
			}
			Rect rect = Rect.MinMaxRect(num, num3, num2, num4);
			GUIClip gUIClip2;
			if (!resetOffset)
			{
				gUIClip2 = new GUIClip(rect, scrollOffset, gUIClip.renderOffset, gUIClip.globalScrollOffset + scrollOffset);
			}
			else
			{
				Rect rect2 = rect;
				Vector2 vector = scrollOffset;
				Vector2 vector2 = new Vector2(rect.x + scrollOffset.x + renderOffset.x, rect.y + scrollOffset.y + renderOffset.y);
				gUIClip2 = new GUIClip(rect2, vector, vector2, gUIClip.globalScrollOffset + scrollOffset);
			}
			s_GUIClips.Push(gUIClip2);
			gUIClip2.Apply();
		}

		public static void Pop()
		{
			s_GUIClips.Pop();
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			gUIClip.Apply();
		}

		public static Vector2 Unclip(Vector2 pos)
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			Vector2 vector = (Vector2)s_Matrix.MultiplyPoint(pos) + gUIClip.scrollOffset;
			Vector2 vector2 = new Vector2(gUIClip.physicalRect.x, gUIClip.physicalRect.y);
			return vector + vector2;
		}

		public static Rect Unclip(Rect rect)
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			return new Rect(rect.x + gUIClip.scrollOffset.x + gUIClip.physicalRect.x, rect.y + gUIClip.scrollOffset.y + gUIClip.physicalRect.y, rect.width, rect.height);
		}

		public static Vector2 Clip(Vector2 absolutePos)
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			Vector2 vector = (Vector2)s_InverseMatrix.MultiplyPoint(absolutePos) - gUIClip.scrollOffset;
			Vector2 vector2 = new Vector2(gUIClip.physicalRect.x, gUIClip.physicalRect.y);
			return vector - vector2;
		}

		public static Rect Clip(Rect absoluteRect)
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			return new Rect(absoluteRect.x - gUIClip.globalScrollOffset.x - gUIClip.physicalRect.x, absoluteRect.y - gUIClip.globalScrollOffset.y - gUIClip.physicalRect.y, absoluteRect.width, absoluteRect.height);
		}

		public static void Reapply()
		{
			((GUIClip)s_GUIClips.Peek()).Apply();
		}

		private void CalculateMouseValues()
		{
			GUIClip gUIClip = (GUIClip)s_GUIClips.Peek();
			Event.current.mousePosition = Clip(s_AbsoluteMousePosition);
			enabled = gUIClip.physicalRect.Contains((Vector2)s_InverseMatrix.MultiplyPoint(s_AbsoluteMousePosition));
			if (Event.current.type != EventType.scrollWheel)
			{
				Event.current.delta = Event.current.mousePosition - Clip(s_AbsoluteLastMousePosition);
			}
		}

		private void Apply()
		{
			CalculateMouseValues();
			s_VisibleRect = new Rect(0f - scrollOffset.x, 0f - scrollOffset.y, physicalRect.width, physicalRect.height);
			if (Event.current.type == EventType.repaint)
			{
				Rect rect = new Rect(physicalRect);
				if (rect.width < 0f)
				{
					rect.width = 0f;
				}
				if (rect.height < 0f)
				{
					rect.height = 0f;
				}
				rect.x -= renderOffset.x;
				rect.y -= renderOffset.y;
				rect.x = Mathf.Round(rect.x);
				rect.y = Mathf.Round(rect.y);
				rect.width = Mathf.Round(rect.width);
				rect.height = Mathf.Round(rect.height);
				Matrix4x4 identity = Matrix4x4.identity;
				Vector3 s = new Vector3(rect.width / (float)Screen.width, rect.height / (float)Screen.height, 1f);
				Vector3 v = new Vector3(rect.x, rect.y, 0f);
				Vector3 vector = s_Matrix.MultiplyVector(v);
				Vector2 vector2 = new Vector2(vector.x * s.x, vector.y * s.y);
				identity = Matrix4x4.TRS(vector2, Quaternion.identity, s);
				Rect pixelRect = new Rect(0f, 0f, Screen.width, Screen.height);
				GL.Viewport(pixelRect);
				SetGUIClipRect(s_VisibleRect);
				Vector2 vector3 = s_Matrix.MultiplyVector(-scrollOffset);
				vector3.x *= s.x;
				vector3.y *= s.y;
				LoadPixelMatrix(vector3.x, Mathf.Round(physicalRect.width) + vector3.x, Mathf.Round(physicalRect.height) + vector3.y, vector3.y, identity * s_Matrix);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadPixelMatrix(float left, float right, float bottom, float top, Matrix4x4 mat);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGUIClipRect(Rect r);

		public override string ToString()
		{
			return string.Format("GUIClip: physicalRect {0}, scrollOffset {1}, renderOffset {2}, globalScrollOffset{3}", physicalRect, scrollOffset, renderOffset, globalScrollOffset);
		}

		internal static void Begin()
		{
			s_AbsoluteMousePosition = Event.current.mousePosition;
			s_AbsoluteLastMousePosition = s_AbsoluteMousePosition - Event.current.delta;
			s_GUIClips.Clear();
			s_Matrix = (s_InverseMatrix = Matrix4x4.identity);
			Rect rect = new Rect(o1, o1, 40000f, 40000f);
			Vector2 vector = new Vector2(o2, o2);
			Vector2 vector2 = new Vector2(o3, o3);
			Vector2 vector3 = new Vector2(o4, o4);
			GUIClip gUIClip = new GUIClip(rect, vector, vector2, vector3);
			s_GUIClips.Push(gUIClip);
			gUIClip.Apply();
		}

		internal static void End()
		{
			if (s_GUIClips.Count != 1 && Event.current.type != EventType.ignore && Event.current.type != EventType.used)
			{
				if (s_GUIClips.Count <= 1)
				{
					Debug.LogError(string.Concat("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced (type:", Event.current.type, ")"));
					return;
				}
				Debug.LogError(string.Concat("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced (type:", Event.current.type, ")"));
			}
			s_GUIClips.Pop();
		}

		internal static void EndThroughException()
		{
			s_GUIClips.Clear();
		}
	}
}
