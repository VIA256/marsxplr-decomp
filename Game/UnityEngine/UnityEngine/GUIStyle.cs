using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class GUIStyle
	{
		[NonSerialized]
		private IntPtr m_Ptr;

		[SerializeField]
		private string m_Name = string.Empty;

		[SerializeField]
		private GUIStyleState m_Normal = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_Hover = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_Active = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_Focused = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_OnNormal = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_OnHover = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_OnActive = new GUIStyleState();

		[SerializeField]
		private GUIStyleState m_OnFocused = new GUIStyleState();

		[SerializeField]
		internal RectOffset m_Border = new RectOffset();

		[SerializeField]
		internal RectOffset m_Padding = new RectOffset();

		[SerializeField]
		internal RectOffset m_Margin = new RectOffset();

		[SerializeField]
		internal RectOffset m_Overflow = new RectOffset();

		[SerializeField]
		private Font m_Font;

		[SerializeField]
		private ImagePosition m_ImagePosition;

		[SerializeField]
		private TextAnchor m_Alignment;

		[SerializeField]
		private bool m_WordWrap;

		[SerializeField]
		private TextClipping m_TextClipping = TextClipping.Clip;

		[SerializeField]
		private Vector2 m_ContentOffset = Vector2.zero;

		[SerializeField]
		[HideInInspector]
		private Vector2 m_ClipOffset = Vector2.zero;

		[SerializeField]
		private float m_FixedWidth;

		[SerializeField]
		private float m_FixedHeight;

		[SerializeField]
		private bool m_StretchWidth = true;

		[SerializeField]
		private bool m_StretchHeight;

		internal static bool showKeyboardFocus = true;

		private static GUIStyle s_None = new GUIStyle();

		public string name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		public GUIStyleState normal
		{
			get
			{
				return m_Normal;
			}
			set
			{
				m_Normal.CopyFrom(value);
			}
		}

		public GUIStyleState hover
		{
			get
			{
				return m_Hover;
			}
			set
			{
				m_Hover.CopyFrom(value);
			}
		}

		public GUIStyleState active
		{
			get
			{
				return m_Active;
			}
			set
			{
				m_Active.CopyFrom(value);
			}
		}

		public GUIStyleState onNormal
		{
			get
			{
				return m_OnNormal;
			}
			set
			{
				m_OnNormal.CopyFrom(value);
			}
		}

		public GUIStyleState onHover
		{
			get
			{
				return m_OnHover;
			}
			set
			{
				m_OnHover.CopyFrom(value);
			}
		}

		public GUIStyleState onActive
		{
			get
			{
				return m_OnActive;
			}
			set
			{
				m_OnActive.CopyFrom(value);
			}
		}

		public GUIStyleState focused
		{
			get
			{
				return m_Focused;
			}
			set
			{
				m_Focused.CopyFrom(value);
			}
		}

		public GUIStyleState onFocused
		{
			get
			{
				return m_OnFocused;
			}
			set
			{
				m_OnActive.CopyFrom(value);
			}
		}

		public RectOffset border
		{
			get
			{
				return m_Border;
			}
			set
			{
				m_Border.CopyFrom(value);
			}
		}

		public RectOffset margin
		{
			get
			{
				return m_Margin;
			}
			set
			{
				m_Margin.CopyFrom(value);
			}
		}

		public RectOffset padding
		{
			get
			{
				return m_Padding;
			}
			set
			{
				m_Padding.CopyFrom(value);
			}
		}

		public RectOffset overflow
		{
			get
			{
				return m_Overflow;
			}
			set
			{
				m_Overflow.CopyFrom(value);
			}
		}

		public Font font
		{
			get
			{
				return m_Font;
			}
			set
			{
				m_Font = value;
				Apply();
			}
		}

		public ImagePosition imagePosition
		{
			get
			{
				return m_ImagePosition;
			}
			set
			{
				m_ImagePosition = value;
				Apply();
			}
		}

		public TextAnchor alignment
		{
			get
			{
				return m_Alignment;
			}
			set
			{
				m_Alignment = value;
				Apply();
			}
		}

		public bool wordWrap
		{
			get
			{
				return m_WordWrap;
			}
			set
			{
				m_WordWrap = value;
				Apply();
			}
		}

		public TextClipping clipping
		{
			get
			{
				return m_TextClipping;
			}
			set
			{
				m_TextClipping = value;
				Apply();
			}
		}

		public Vector2 contentOffset
		{
			get
			{
				return m_ContentOffset;
			}
			set
			{
				m_ContentOffset = value;
				Apply();
			}
		}

		[Obsolete("Don't use clipOffset - put things inside begingroup instead. This functionality will be removed in a later version.")]
		public Vector2 clipOffset
		{
			get
			{
				return m_ClipOffset;
			}
			set
			{
				m_ClipOffset = value;
				Apply();
			}
		}

		internal Vector2 Internal_clipOffset
		{
			get
			{
				return m_ClipOffset;
			}
			set
			{
				m_ClipOffset = value;
				Apply();
			}
		}

		public float fixedWidth
		{
			get
			{
				return m_FixedWidth;
			}
			set
			{
				m_FixedWidth = value;
				Apply();
			}
		}

		public float fixedHeight
		{
			get
			{
				return m_FixedHeight;
			}
			set
			{
				m_FixedHeight = value;
				Apply();
			}
		}

		public bool stretchWidth
		{
			get
			{
				return m_StretchWidth;
			}
			set
			{
				m_StretchWidth = value;
				Apply();
			}
		}

		public bool stretchHeight
		{
			get
			{
				return m_StretchHeight;
			}
			set
			{
				m_StretchHeight = value;
				Apply();
			}
		}

		public float lineHeight
		{
			get
			{
				Apply();
				return Mathf.Round(Internal_GetLineHeight(m_Ptr));
			}
		}

		public static GUIStyle none
		{
			get
			{
				return s_None;
			}
		}

		public bool isHeightDependantOnWidth
		{
			get
			{
				return m_FixedHeight == 0f && m_WordWrap && imagePosition != ImagePosition.ImageOnly;
			}
		}

		public GUIStyle()
		{
			Init();
			GrabChildrenObjects();
		}

		public GUIStyle(GUIStyle other)
		{
			Init();
			normal = other.normal;
			hover = other.hover;
			active = other.active;
			focused = other.focused;
			onNormal = other.onNormal;
			onHover = other.onHover;
			onActive = other.onActive;
			onFocused = other.onFocused;
			border = other.border;
			padding = other.padding;
			overflow = other.overflow;
			margin = other.margin;
			m_Font = other.m_Font;
			m_ImagePosition = other.m_ImagePosition;
			m_Alignment = other.m_Alignment;
			m_WordWrap = other.m_WordWrap;
			m_TextClipping = other.m_TextClipping;
			m_ContentOffset = other.m_ContentOffset;
			m_ClipOffset = other.m_ClipOffset;
			m_FixedWidth = other.m_FixedWidth;
			m_FixedHeight = other.m_FixedHeight;
			m_StretchWidth = other.m_StretchWidth;
			m_StretchHeight = other.m_StretchHeight;
			m_Name = other.m_Name;
			GrabChildrenObjects();
			Apply();
		}

		private void GrabChildrenObjects()
		{
			m_Normal.owner = this;
			m_Hover.owner = this;
			m_Active.owner = this;
			m_OnNormal.owner = this;
			m_OnHover.owner = this;
			m_OnActive.owner = this;
			m_Focused.owner = this;
			m_Border.owner = this;
			m_Padding.owner = this;
			m_Overflow.owner = this;
			m_Margin.owner = this;
		}

		~GUIStyle()
		{
			Cleanup();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		internal void Apply()
		{
			Internal_ApplyToCache(m_Ptr, font, m_Normal, m_Hover, m_Active, m_Focused, m_OnNormal, m_OnHover, m_OnActive, m_OnFocused, m_Border, m_Padding, m_Margin, m_Overflow, m_WordWrap, m_Alignment, m_TextClipping, m_ContentOffset, m_ClipOffset, (int)m_ImagePosition, m_FixedWidth, m_FixedHeight);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ApplyToCache(IntPtr target, Font font, GUIStyleState normal, GUIStyleState hover, GUIStyleState active, GUIStyleState focused, GUIStyleState onNormal, GUIStyleState onHover, GUIStyleState onActive, GUIStyleState onFocused, RectOffset border, RectOffset padding, RectOffset margin, RectOffset overflow, bool wordWrap, TextAnchor alignment, TextClipping textClipping, Vector2 contentOffset, Vector2 clipOffset, int imagePosition, float fixedWidth, float fixedHeight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw(IntPtr target, Rect position, string text, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			if (Event.current.type == EventType.repaint)
			{
				Apply();
				Internal_Draw(m_Ptr, position, null, null, isHover, isActive, on, hasKeyboardFocus);
			}
		}

		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			if (Event.current.type == EventType.repaint)
			{
				Apply();
				Internal_Draw(m_Ptr, position, text, null, isHover, isActive, on, hasKeyboardFocus);
			}
		}

		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			if (Event.current.type == EventType.repaint)
			{
				Apply();
				Internal_Draw(m_Ptr, position, null, image, isHover, isActive, on, hasKeyboardFocus);
			}
		}

		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			if (Event.current.type != EventType.repaint)
			{
				return;
			}
			Apply();
			Internal_Draw(m_Ptr, position, content.text, content.image, isHover, isActive, on, hasKeyboardFocus);
			if (content.tooltip != null && content.tooltip != "")
			{
				if (isHover || isActive)
				{
					GUI.s_EditorTooltip = (GUI.s_MouseTooltip = content.tooltip);
					Vector2 guiPoint = new Vector2(position.x, position.y);
					Vector2 vector = GUIUtility.GUIToScreenPoint(guiPoint);
					GUI.s_ToolTipRect = new Rect(vector.x, vector.y, position.width, position.height);
				}
				if (hasKeyboardFocus)
				{
					GUI.s_KeyTooltip = content.tooltip;
				}
			}
		}

		public void Draw(Rect position, GUIContent content, int controlID)
		{
			bool flag = false;
			Draw(position, content, controlID, flag);
		}

		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			Event current = Event.current;
			if (current.type != EventType.repaint)
			{
				return;
			}
			Apply();
			bool flag = position.Contains(current.mousePosition) && GUIClip.enabled;
			bool isHover = flag && (GUIUtility.hotControl == controlID || GUIUtility.hotControl == 0);
			if (flag)
			{
				GUIUtility.mouseUsed = true;
			}
			bool flag2 = controlID == GUIUtility.hotControl && GUI.enabled && position.Contains(current.mousePosition);
			bool flag3 = GUIUtility.keyboardControl == controlID && GUI.enabled && showKeyboardFocus;
			Internal_Draw(m_Ptr, position, content.text, content.image, isHover, flag2, on, flag3);
			if (content.tooltip != null && content.tooltip != string.Empty && !flag2)
			{
				if (flag || flag2 || GUIUtility.hotControl == controlID)
				{
					GUI.s_EditorTooltip = (GUI.s_MouseTooltip = content.tooltip);
					Vector2 guiPoint = new Vector2(position.x, position.y);
					Vector2 vector = GUIUtility.GUIToScreenPoint(guiPoint);
					GUI.s_ToolTipRect = new Rect(vector.x, vector.y, position.width, position.height);
				}
				if (flag3)
				{
					GUI.s_EditorTooltip = (GUI.s_KeyTooltip = content.tooltip);
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawCursor(IntPtr target, Rect position, string text, Texture image, int pos, Color cursorColor);

		public void DrawCursor(Rect position, GUIContent content, int controlID, int Character)
		{
			Event current = Event.current;
			if (current.type == EventType.repaint)
			{
				Apply();
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				Internal_DrawCursor(m_Ptr, position, content.text, content.image, Character, cursorColor);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawWithTextSelection(IntPtr target, Rect position, string text, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, int firstPos, int lastPos, Color cursorColor, Color selectionColor);

		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			Event current = Event.current;
			if (current.type == EventType.repaint)
			{
				Apply();
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				Internal_DrawWithTextSelection(m_Ptr, position, content.text, content.image, position.Contains(current.mousePosition), controlID == GUIUtility.hotControl, false, controlID == GUIUtility.keyboardControl && showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, cursorColor, GUI.skin.settings.selectionColor);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDefaultFont(Font font);

		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Apply();
			Vector2 ret;
			Internal_GetCursorPixelPosition(m_Ptr, position, content.text, content.image, cursorStringIndex, out ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetCursorPixelPosition(IntPtr target, Rect position, string text, Texture image, int cursorStringIndex, out Vector2 ret);

		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			Apply();
			return Internal_GetCursorStringIndex(m_Ptr, position, content.text, content.image, cursorPixelPosition);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Internal_GetCursorStringIndex(IntPtr target, Rect position, string text, Texture image, Vector2 cursorPixelPosition);

		public Vector2 CalcSize(GUIContent content)
		{
			Apply();
			Vector2 ret;
			Internal_CalcSize(m_Ptr, content.text, content.image, out ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_CalcSize(IntPtr target, string text, Texture image, out Vector2 ret);

		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			Vector2 result = new Vector2((m_FixedWidth == 0f) ? Mathf.Ceil(contentSize.x + (float)m_Padding.left + (float)m_Padding.right) : m_FixedWidth, (m_FixedHeight == 0f) ? Mathf.Ceil(contentSize.y + (float)m_Padding.top + (float)m_Padding.bottom) : m_FixedHeight);
			return result;
		}

		public float CalcHeight(GUIContent content, float width)
		{
			Apply();
			return Internal_CalcHeight(m_Ptr, content.text, content.image, width);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_CalcHeight(IntPtr target, string text, Texture image, float width);

		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Apply();
			Internal_CalcMinMaxWidth(m_Ptr, content.text, content.image, out minWidth, out maxWidth);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcMinMaxWidth(IntPtr target, string text, Texture image, out float minWidth, out float maxWidth);

		public override string ToString()
		{
			return string.Format("GUIStyle '{0}'", m_Name);
		}

		public static implicit operator GUIStyle(string str)
		{
			if (GUISkin.current == null)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				return GUISkin.error;
			}
			return GUISkin.current.GetStyle(str);
		}
	}
}
