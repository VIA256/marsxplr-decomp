using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class GUI
	{
		private class SliderState
		{
			public float dragStartPos;

			public float dragStartValue;

			public bool isDragging;
		}

		internal class ScrollViewState
		{
			public Rect position;

			public Rect visibleRect;

			public Vector2 scrollPosition;

			public bool apply;

			public bool hasScrollTo;

			public Rect scrollTo;

			internal void ScrollTo(Rect position)
			{
				Vector2 vector = new Vector2(position.xMin, position.yMin);
				if (!hasScrollTo)
				{
					hasScrollTo = true;
					scrollTo.xMin = vector.x;
					scrollTo.yMin = vector.y;
					vector = new Vector2(position.xMax, position.yMax);
					scrollTo.xMax = vector.x;
					scrollTo.yMax = vector.y;
					hasScrollTo = true;
					Rect rect = visibleRect;
					rect.x += scrollPosition.x;
					rect.y += scrollPosition.y;
					Vector2 vector2 = new Vector2(scrollTo.xMax, scrollTo.yMax);
					Vector2 vector3 = new Vector2(scrollTo.xMin, scrollTo.yMin);
					if (vector2.x > rect.xMax)
					{
						scrollPosition.x += vector2.x - rect.xMax;
					}
					if (vector3.x < rect.xMin)
					{
						scrollPosition.x -= rect.xMin - vector3.x;
					}
					if (vector2.y > rect.yMax)
					{
						scrollPosition.y += vector2.y - rect.yMax;
					}
					if (vector3.y < rect.yMin)
					{
						scrollPosition.y -= rect.yMin - vector3.y;
					}
					apply = true;
					hasScrollTo = false;
				}
				else
				{
					scrollTo.x = Mathf.Min(vector.x, scrollTo.x);
					scrollTo.y = Mathf.Min(vector.y, scrollTo.y);
					vector = new Vector2(position.xMax, position.yMax);
					scrollTo.xMax = Mathf.Max(vector.x, scrollTo.xMax);
					scrollTo.yMax = Mathf.Max(vector.y, scrollTo.yMax);
				}
			}
		}

		internal class _WindowList
		{
			internal static _WindowList instance = new _WindowList();

			internal static Hashtable s_EditorWindows;

			internal Hashtable windows = new Hashtable();

			internal _Window Get(int id)
			{
				_Window window = (_Window)windows[id];
				if (window == null)
				{
					Debug.LogError("can't find window with ID " + id);
				}
				return window;
			}
		}

		internal class _Window : IComparable
		{
			internal static _Window current;

			internal Rect rect;

			internal int depth;

			internal float opacity;

			internal GUIStyle style;

			internal GUIContent title = new GUIContent();

			internal int id;

			internal bool used;

			internal WindowFunction func;

			internal bool moved;

			internal bool forceRect;

			internal Color color;

			internal Color backgroundColor;

			internal Color contentColor;

			internal GUISkin skin;

			internal Matrix4x4 matrix;

			internal int hashCode;

			internal bool enabled;

			internal _Window(int id)
			{
				this.id = id;
				hashCode = ("Window" + id).GetHashCode();
				depth = _WindowList.instance.windows.Count;
			}

			internal void Do()
			{
				GUIUtility.GetControlID(hashCode, FocusType.Passive);
				current = this;
				GUIClip.Push(rect);
				GUIStyle.showKeyboardFocus = focusedWindow == id;
				try
				{
					func(id);
				}
				finally
				{
					GUIStyle.showKeyboardFocus = true;
					GUIClip.Pop();
					current = null;
				}
			}

			internal void SetupGUIValues()
			{
				GUI.color = color;
				GUI.backgroundColor = backgroundColor;
				GUI.contentColor = contentColor;
				GUI.matrix = matrix;
				GUI.skin = skin;
				GUI.enabled = enabled;
			}

			public int CompareTo(object obj)
			{
				return depth - ((_Window)obj).depth;
			}
		}

		private class WindowDragState
		{
			public Vector2 dragStartPos = Vector2.zero;

			//FUCK public Rect dragStartRect = dragStartRect;
			public Rect dragStartRect; //I HAVE A BAD FEELING ABOUT THIS
		}

		public delegate void WindowFunction(int id);

		private static float scrollStepSize = 10f;

		private static DateTime nextScrollStepTime = DateTime.Now;

		private static int scrollControlID;

		private static int firstScrollWait = 250;

		private static int scrollWait = 30;

		private static int scrollThroughSide = 0;

		private static GUISkin s_Skin;

		private static bool s_Changed = false;

		private static Color s_Color = Color.white;

		private static Color s_BackgroundColor = Color.white;

		private static Color s_ContentColor = Color.white;

		private static bool s_Enabled = true;

		internal static string s_KeyTooltip = "";

		internal static string s_EditorTooltip = "";

		internal static string s_MouseTooltip = "";

		internal static Rect s_ToolTipRect;

		private static Material s_GUIBlendMaterial;

		private static Material s_GUIBlitMaterial;

		private static int boxHash = "Box".GetHashCode();

		private static int buttonHash = "Button".GetHashCode();

		private static int repeatButtonHash = "repeatButton".GetHashCode();

		private static int toggleHash = "Toggle".GetHashCode();

		private static int buttonGridHash = "ButtonGrid".GetHashCode();

		private static int sliderHash = "Slider".GetHashCode();

		private static int beginGroupHash = "BeginGroup".GetHashCode();

		private static int scrollviewHash = "scrollView".GetHashCode();

		private static Stack s_ScrollViewStates = new Stack();

		internal static int focusedWindow = -1;

		private static bool s_LayersChanged = false;

		private static _WindowList s_GameWindowList;

		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				if (!value)
				{
					value = GUIUtility.GetDefaultSkin();
				}
				s_Skin = value;
				value.MakeCurrent();
			}
		}

		public static Color color
		{
			get
			{
				return s_Color;
			}
			set
			{
				s_Color = value;
				UpdateColors();
			}
		}

		public static Color backgroundColor
		{
			get
			{
				return s_BackgroundColor;
			}
			set
			{
				s_BackgroundColor = value;
				UpdateColors();
			}
		}

		public static Color contentColor
		{
			get
			{
				return s_ContentColor;
			}
			set
			{
				s_ContentColor = value;
				UpdateColors();
			}
		}

		public static bool changed
		{
			get
			{
				return s_Changed;
			}
			set
			{
				s_Changed = value;
			}
		}

		public static bool enabled
		{
			get
			{
				return s_Enabled;
			}
			set
			{
				s_Enabled = value;
				Internal_SetEnabled(value);
			}
		}

		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.matrix;
			}
			set
			{
				GUIClip.matrix = value;
			}
		}

		public static string tooltip
		{
			get
			{
				if (s_MouseTooltip != "")
				{
					return s_MouseTooltip;
				}
				if (s_KeyTooltip != "")
				{
					return s_KeyTooltip;
				}
				return s_EditorTooltip;
			}
			set
			{
				s_MouseTooltip = (s_KeyTooltip = (s_EditorTooltip = value));
			}
		}

		protected static string mouseTooltip
		{
			get
			{
				return s_MouseTooltip;
			}
		}

		protected static Rect tooltipRect
		{
			get
			{
				return s_ToolTipRect;
			}
			set
			{
				s_ToolTipRect = value;
			}
		}

		public static extern int depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private static Material blendMaterial
		{
			get
			{
				if (!s_GUIBlendMaterial)
				{
					if (Application.GetBuildUnityVersion() >= Application.GetNumericUnityVersion("2.5a1"))
					{
						s_GUIBlendMaterial = new Material("Shader \"__GUI_DRAWTEXTURE_BLEND\" {\n Properties { _MainTex (\"Texture\", Any) = \"white\" {} }\n SubShader { Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always Blend SrcAlpha OneMinusSrcAlpha\n BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord }\n Pass { SetTexture [_MainTex] { combine primary * texture }\n SetTexture [_GUIClipTexture] { combine previous, previous * texture alpha } \n} }\nSubShader { Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always\n BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord }\n Pass { ColorMask A SetTexture [_MainTex] { combine primary * texture } } \n Pass { ColorMask A Blend DstAlpha Zero SetTexture [_GUIClipTexture] { combine texture, texture alpha } } \n Pass { ColorMask RGB Blend DstAlpha OneMinusDstAlpha SetTexture [_MainTex] { combine primary * texture } } \n}\n}");
					}
					else
					{
						s_GUIBlendMaterial = new Material("Shader \"GUIBlend\" { properties { _MainTex (\"\", Any) = \"\" } SubShader { BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord } Pass { Blend SrcAlpha OneMinusSrcAlpha Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always  SetTexture [_MainTex] { combine texture * primary } } } }");
					}
					s_GUIBlendMaterial.hideFlags = HideFlags.HideAndDontSave;
					s_GUIBlendMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
					InitializeGUIClipTexture();
				}
				return s_GUIBlendMaterial;
			}
		}

		private static Material blitMaterial
		{
			get
			{
				if (!s_GUIBlitMaterial)
				{
					if (Application.GetBuildUnityVersion() >= Application.GetNumericUnityVersion("2.5a1"))
					{
						s_GUIBlitMaterial = new Material("Shader \"__GUI_DRAWTEXTURE_CLIP\" {\n Properties { _MainTex (\"Texture\", Any) = \"white\" {} }\n SubShader { Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always Blend SrcAlpha OneMinusSrcAlpha\n BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord }\n Pass { SetTexture [_MainTex] { combine primary * texture, primary }\n SetTexture [_GUIClipTexture] { combine previous, previous * texture alpha } \n} }\nSubShader { Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always\n BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord }\n Pass { ColorMask A  SetTexture [_GUIClipTexture] { combine texture, texture alpha } } \n Pass { ColorMask RGB Blend DstAlpha OneMinusDstAlpha SetTexture [_MainTex] { combine primary * texture } } \n}\n}");
					}
					else
					{
						s_GUIBlitMaterial = new Material("Shader \"GUIBlit\" { properties { _MainTex (\"\", Any) = \"\" } SubShader { BindChannels { Bind \"vertex\", vertex Bind \"color\", color Bind \"TexCoord\", texcoord } Pass { Blend SrcAlpha OneMinusSrcAlpha Lighting Off Cull Off ZWrite Off Fog { Mode Off } ZTest Always SetTexture [_MainTex] { combine texture * primary, primary } } } }");
					}
					s_GUIBlitMaterial.hideFlags = HideFlags.HideAndDontSave;
					s_GUIBlitMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
					InitializeGUIClipTexture();
				}
				return s_GUIBlitMaterial;
			}
		}

		private static extern bool usePageScrollbars
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static void ResetSettings()
		{
			s_Color = Color.white;
			s_BackgroundColor = Color.white;
			s_ContentColor = Color.white;
			enabled = true;
			s_KeyTooltip = "";
			s_MouseTooltip = "";
			UpdateColors();
		}

		private static void UpdateColors()
		{
			Internal_UpdateColors(s_Color, s_BackgroundColor, s_ContentColor);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_UpdateColors(Color col, Color backgroundCol, Color contentCol);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetEnabled(bool enab);

		public static void Label(Rect position, string text)
		{
			Label(position, GUIContent.Temp(text), s_Skin.label);
		}

		public static void Label(Rect position, Texture image)
		{
			Label(position, GUIContent.Temp(image), s_Skin.label);
		}

		public static void Label(Rect position, GUIContent content)
		{
			Label(position, content, s_Skin.label);
		}

		public static void Label(Rect position, string text, GUIStyle style)
		{
			Label(position, GUIContent.Temp(text), style);
		}

		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			Label(position, GUIContent.Temp(image), style);
		}

		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.repaint)
			{
				style.Draw(position, content, false, false, false, false);
				if (position.Contains(Event.current.mousePosition) && content.tooltip != null && content.tooltip != string.Empty)
				{
					s_EditorTooltip = (s_MouseTooltip = content.tooltip);
					Vector2 guiPoint = new Vector2(position.x, position.y);
					Vector2 vector = GUIUtility.GUIToScreenPoint(guiPoint);
					s_ToolTipRect = new Rect(vector.x, vector.y, position.width, position.height);
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeGUIClipTexture();

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			float imageAspect = 0f;
			DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			float imageAspect = 0f;
			bool alphaBlend = true;
			DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		public static void DrawTexture(Rect position, Texture image)
		{
			float imageAspect = 0f;
			bool alphaBlend = true;
			ScaleMode scaleMode = ScaleMode.StretchToFill;
			DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
		{
			if (Event.current.type != EventType.repaint)
			{
				return;
			}
			if (imageAspect == 0f)
			{
				imageAspect = (float)image.width / (float)image.height;
			}
			Material mat = ((!alphaBlend) ? blitMaterial : blendMaterial);
			float num = position.width / position.height;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
			{
				Rect screenRect5 = position;
				Rect sourceRect5 = new Rect(0f, 0f, 1f, 1f);
				Graphics.DrawTexture(screenRect5, image, sourceRect5, 0, 0, 0, 0, color, mat);
				break;
			}
			case ScaleMode.ScaleAndCrop:
				if (num > imageAspect)
				{
					float num4 = imageAspect / num;
					Rect screenRect3 = position;
					Rect sourceRect3 = new Rect(0f, (1f - num4) * 0.5f, 1f, num4);
					Graphics.DrawTexture(screenRect3, image, sourceRect3, 0, 0, 0, 0, color, mat);
				}
				else
				{
					float num5 = num / imageAspect;
					Rect screenRect4 = position;
					Rect sourceRect4 = new Rect(0.5f - num5 * 0.5f, 0f, num5, 1f);
					Graphics.DrawTexture(screenRect4, image, sourceRect4, 0, 0, 0, 0, color, mat);
				}
				break;
			case ScaleMode.ScaleToFit:
				if (num > imageAspect)
				{
					float num2 = imageAspect / num;
					Rect screenRect = new Rect(position.xMin + position.width * (1f - num2) * 0.5f, position.yMin, num2 * position.width, position.height);
					Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(screenRect, image, sourceRect, 0, 0, 0, 0, color, mat);
				}
				else
				{
					float num3 = num / imageAspect;
					Rect screenRect2 = new Rect(position.xMin, position.yMin + position.height * (1f - num3) * 0.5f, position.width, num3 * position.height);
					Rect sourceRect2 = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(screenRect2, image, sourceRect2, 0, 0, 0, 0, color, mat);
				}
				break;
			}
		}

		public static void Box(Rect position, string text)
		{
			Box(position, GUIContent.Temp(text), s_Skin.box);
		}

		public static void Box(Rect position, Texture image)
		{
			Box(position, GUIContent.Temp(image), s_Skin.box);
		}

		public static void Box(Rect position, GUIContent content)
		{
			Box(position, content, s_Skin.box);
		}

		public static void Box(Rect position, string text, GUIStyle style)
		{
			Box(position, GUIContent.Temp(text), style);
		}

		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			Box(position, GUIContent.Temp(image), style);
		}

		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(boxHash, FocusType.Passive);
			if (Event.current.type == EventType.repaint)
			{
				style.Draw(position, content, controlID);
			}
		}

		public static bool Button(Rect position, string text)
		{
			return Button(position, GUIContent.Temp(text), s_Skin.button);
		}

		public static bool Button(Rect position, Texture image)
		{
			return Button(position, GUIContent.Temp(image), s_Skin.button);
		}

		public static bool Button(Rect position, GUIContent content)
		{
			return Button(position, content, s_Skin.button);
		}

		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return Button(position, GUIContent.Temp(text), style);
		}

		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return Button(position, GUIContent.Temp(image), style);
		}

		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(buttonHash, FocusType.Native, position);
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.mouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			case EventType.mouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.repaint:
				style.Draw(position, content, controlID);
				break;
			}
			return false;
		}

		public static bool RepeatButton(Rect position, string text)
		{
			return DoRepeatButton(position, GUIContent.Temp(text), s_Skin.button, FocusType.Native);
		}

		public static bool RepeatButton(Rect position, Texture image)
		{
			return DoRepeatButton(position, GUIContent.Temp(image), s_Skin.button, FocusType.Native);
		}

		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return DoRepeatButton(position, content, s_Skin.button, FocusType.Native);
		}

		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Native);
		}

		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Native);
		}

		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return DoRepeatButton(position, content, style, FocusType.Native);
		}

		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(repeatButtonHash, focusType, position);
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.mouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			case EventType.repaint:
				style.Draw(position, content, controlID);
				return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
			default:
				return false;
			}
		}

		public static string TextField(Rect position, string text)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, -1, skin.textField);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, skin.textField);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, -1, style);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, maxLength, style);
			return gUIContent.text;
		}

		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return PasswordField(position, password, maskChar, -1, skin.textField);
		}

		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return PasswordField(position, password, maskChar, maxLength, skin.textField);
		}

		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return PasswordField(position, password, maskChar, -1, style);
		}

		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			string t = PasswordFieldGetStrToShow(password, maskChar);
			GUIContent gUIContent = GUIContent.Temp(t);
			bool flag = changed;
			changed = false;
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, style);
			t = ((!changed) ? password : gUIContent.text);
			changed |= flag;
			return t;
		}

		public static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type != EventType.repaint && Event.current.type != EventType.mouseDown) ? password : "".PadRight(password.Length, maskChar);
		}

		public static string TextArea(Rect position, string text)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, -1, skin.textArea);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, maxLength, skin.textArea);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, -1, style);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, style);
			return gUIContent.text;
		}

		private static string TextArea(Rect position, GUIContent content, int maxLength, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(content.text, content.image);
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, style);
			return gUIContent.text;
		}

		public static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			GUIUtility.CheckOnGUI();
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.content.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.ClampPos();
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.mouseDown:
				if (position.Contains(current.mousePosition))
				{
					GUIUtility.hotControl = id;
					if (GUIUtility.keyboardControl != id)
					{
						GUIUtility.keyboardControl = id;
					}
					textEditor.MoveCursorToPosition(Event.current.mousePosition);
					if (Event.current.clickCount == 2 && skin.settings.doubleClickSelectsWord)
					{
						textEditor.SelectCurrentWord();
						textEditor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						textEditor.MouseDragSelectsWholeWords(true);
					}
					if (Event.current.clickCount == 3 && skin.settings.tripleClickSelectsLine)
					{
						textEditor.SelectCurrentParagraph();
						textEditor.MouseDragSelectsWholeWords(true);
						textEditor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			case EventType.mouseDrag:
				if (GUIUtility.hotControl == id)
				{
					if (current.shift)
					{
						textEditor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						textEditor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == id)
				{
					textEditor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			case EventType.keyDown:
			{
				if (GUIUtility.keyboardControl != id)
				{
					return;
				}
				if (textEditor.HandleKeyEvent(current))
				{
					current.Use();
					flag = true;
					content.text = textEditor.content.text;
					break;
				}
				if (current.keyCode == KeyCode.Tab || current.character == '\t')
				{
					return;
				}
				char character = current.character;
				if (character == '\n' && !multiline && !current.alt)
				{
					return;
				}
				Font font = style.font;
				if (!font)
				{
					font = skin.font;
				}
				if (font.HasCharacter(character) || character == '\n')
				{
					textEditor.Insert(character);
					flag = true;
				}
				else if (character == '\0')
				{
					current.Use();
				}
				break;
			}
			case EventType.repaint:
				if (GUIUtility.keyboardControl != id)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					textEditor.DrawCursor(content.text);
				}
				break;
			}
			if (flag)
			{
				changed = true;
				content.text = textEditor.content.text;
				if (maxLength >= 0 && content.text.Length > maxLength)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		public static void SetNextControlName(string name)
		{
			GUIUtility.SetNextKeyboardFocusName(name);
		}

		public static string GetNameOfFocusedControl()
		{
			return GUIUtility.GetNameOfFocusedControl();
		}

		public static void FocusControl(string name)
		{
			GUIUtility.MoveKeyboardFocus(name);
		}

		public static bool Toggle(Rect position, bool value, string text)
		{
			return Toggle(position, value, GUIContent.Temp(text), s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return Toggle(position, value, GUIContent.Temp(image), s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return Toggle(position, value, content, s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return Toggle(position, value, GUIContent.Temp(text), style);
		}

		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return Toggle(position, value, GUIContent.Temp(image), style);
		}

		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			return DoToggle(position, GUIUtility.GetControlID(toggleHash, FocusType.Native, position), value, content, style);
		}

		protected static bool DoToggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			switch (Event.current.GetTypeForControl(id))
			{
			case EventType.mouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = 0;
					Event.current.Use();
				}
				break;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == id)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					if (position.Contains(Event.current.mousePosition))
					{
						s_Changed = true;
						return !value;
					}
				}
				break;
			case EventType.mouseDrag:
				if (GUIUtility.hotControl == id)
				{
					Event.current.Use();
				}
				break;
			case EventType.repaint:
				style.Draw(position, content, id, value);
				break;
			}
			return value;
		}

		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return Toolbar(position, selected, GUIContent.Temp(texts), s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return Toolbar(position, selected, GUIContent.Temp(images), s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, GUIContent[] content)
		{
			return Toolbar(position, selected, content, s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			GUIStyle firstStyle;
			GUIStyle midStyle;
			GUIStyle lastStyle;
			FindStyles(ref style, out firstStyle, out midStyle, out lastStyle, "left", "mid", "right");
			return DoButtonGrid(position, selected, contents, contents.Length, style, firstStyle, midStyle, lastStyle);
		}

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return SelectionGrid(position, selected, content, xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			if (style == null)
			{
				style = s_Skin.button;
			}
			return DoButtonGrid(position, selected, contents, xCount, style, style, style, style);
		}

		private static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			if (style == null)
			{
				style = skin.button;
			}
			string name = style.name;
			midStyle = skin.FindStyle(name + mid);
			if (midStyle == null)
			{
				midStyle = style;
			}
			firstStyle = skin.FindStyle(name + first);
			if (firstStyle == null)
			{
				firstStyle = midStyle;
			}
			lastStyle = skin.FindStyle(name + last);
			if (lastStyle == null)
			{
				lastStyle = midStyle;
			}
		}

		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			if (xCount < 2)
			{
				return 0;
			}
			if (xCount == 2)
			{
				return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
			}
			int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
			return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
		}

		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			if (num == 0)
			{
				return selected;
			}
			int controlID = GUIUtility.GetControlID(buttonGridHash, FocusType.Native, position);
			int num2 = num / xCount;
			if (num % xCount != 0)
			{
				num2++;
			}
			float num3 = CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
			float num4 = Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1);
			float elemWidth = (position.width - num3) / (float)xCount;
			float elemHeight = (position.height - num4) / (float)num2;
			if (style.fixedWidth != 0f)
			{
				elemWidth = style.fixedWidth;
			}
			if (style.fixedHeight != 0f)
			{
				elemHeight = style.fixedHeight;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.mouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					if (GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1)
					{
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.mouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection2 = GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					changed = true;
					return buttonGridMouseSelection2;
				}
				break;
			case EventType.repaint:
			{
				GUIStyle gUIStyle = null;
				GUIClip.Push(position);
				position = new Rect(0f, 0f, position.width, position.height);
				Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
				int buttonGridMouseSelection = GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
				bool flag = position.Contains(Event.current.mousePosition);
				GUIUtility.mouseUsed |= flag;
				for (int i = 0; i < num; i++)
				{
					GUIStyle gUIStyle2 = null;
					gUIStyle2 = ((i == 0) ? firstStyle : midStyle);
					if (i == num - 1)
					{
						gUIStyle2 = lastStyle;
					}
					if (num == 1)
					{
						gUIStyle2 = style;
					}
					if (i != selected)
					{
						gUIStyle2.Draw(array[i], contents[i], i == buttonGridMouseSelection && (GUIClip.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && enabled, false, false);
					}
					else
					{
						gUIStyle = gUIStyle2;
					}
				}
				if (selected < num && selected > -1)
				{
					gUIStyle.Draw(array[selected], contents[selected], selected == buttonGridMouseSelection && (GUIClip.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl || (selected == buttonGridMouseSelection && GUIUtility.hotControl == 0), true, false);
				}
				GUIClip.Pop();
				break;
			}
			}
			return selected;
		}

		private static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders)
		{
			int num = 0;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle gUIStyle = style;
			Rect[] array = new Rect[count];
			if (count > 1)
			{
				gUIStyle = firstStyle;
			}
			for (int i = 0; i < count; i++)
			{
				if (!addBorders)
				{
					array[i] = new Rect(num3, num4, elemWidth, elemHeight);
				}
				else
				{
					/*ref Rect reference = ref array[i];
					RectOffset margin = gUIStyle.margin;
					Rect rect = new Rect(num3, num4, elemWidth, elemHeight);
					reference = margin.Add(rect);*/
					RectOffset margin = gUIStyle.margin;
					Rect rect = new Rect(num3, num4, elemWidth, elemHeight);
					array[i] = margin.Add(rect);
				}
				array[i].width = Mathf.Round(array[i].xMax) - Mathf.Round(array[i].x);
				array[i].x = Mathf.Round(array[i].x);
				num3 = Mathf.Round(num3);
				GUIStyle gUIStyle2 = midStyle;
				if (i == count - 2)
				{
					gUIStyle2 = lastStyle;
				}
				num3 += elemWidth + (float)Mathf.Max(gUIStyle.margin.right, gUIStyle2.margin.left);
				num2++;
				if (num2 >= xCount)
				{
					num++;
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
				}
			}
			return array;
		}

		private static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest)
		{
			for (int i = 0; i < buttonRects.Length; i++)
			{
				if (buttonRects[i].Contains(mousePos))
				{
					return i;
				}
			}
			if (!findNearest)
			{
				return -1;
			}
			float num = 10000000f;
			int result = -1;
			for (int j = 0; j < buttonRects.Length; j++)
			{
				Rect rect = buttonRects[j];
				Vector2 vector = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
				float sqrMagnitude = (mousePos - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = j;
					num = sqrMagnitude;
				}
			}
			return result;
		}

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return Slider(position, value, 0f, leftValue, rightValue, skin.horizontalSlider, skin.horizontalSliderThumb, true, GUIUtility.GetControlID(sliderHash, FocusType.Native, position));
		}

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, GUIUtility.GetControlID(sliderHash, FocusType.Native, position));
		}

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return Slider(position, value, 0f, topValue, bottomValue, skin.verticalSlider, skin.verticalSliderThumb, false, GUIUtility.GetControlID(sliderHash, FocusType.Native, position));
		}

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, GUIUtility.GetControlID(sliderHash, FocusType.Native, position));
		}

		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			GUIUtility.CheckOnGUI();
			float num = Mathf.Min(start, end);
			float num2 = Mathf.Max(start, end);
			float num3 = value;
			value = Mathf.Clamp(value, num, num2 - size);
			float num4 = ((!(start > end)) ? 1 : (-1));
			SliderState sliderState = (SliderState)GUIUtility.GetStateObject(typeof(SliderState), id);
			if (slider == null || thumb == null)
			{
				return num3;
			}
			float num6;
			Rect position2;
			float num7;
			if (horiz)
			{
				float num5 = ((thumb.fixedWidth == 0f) ? ((float)thumb.padding.horizontal) : thumb.fixedWidth);
				num6 = (position.width - (float)slider.padding.horizontal - num5) / (end - start);
				position2 = ((!(start < end)) ? new Rect((value + size - start) * num6 + position.x + (float)slider.padding.left, position.y, size * (0f - num6) + num5, position.height) : new Rect((value - start) * num6 + position.x + (float)slider.padding.left, position.y + (float)slider.padding.top, size * num6 + num5, position.height - (float)slider.padding.vertical));
				num7 = Event.current.mousePosition.x - position.x;
			}
			else
			{
				float num8 = ((thumb.fixedHeight == 0f) ? ((float)thumb.padding.vertical) : thumb.fixedHeight);
				num6 = (position.height - (float)slider.padding.vertical - num8) / (end - start);
				position2 = ((!(start < end)) ? new Rect(position.x + (float)slider.padding.left, (value + size - start) * num6 + position.y + (float)slider.padding.top, position.width - (float)slider.padding.horizontal, size * (0f - num6) + num8) : new Rect(position.x + (float)slider.padding.left, (value - start) * num6 + position.y + (float)slider.padding.top, position.width - (float)slider.padding.horizontal, size * num6 + num8));
				num7 = Event.current.mousePosition.y - position.y;
			}
			switch (Event.current.GetTypeForControl(id))
			{
			case EventType.mouseDown:
				if (!position.Contains(Event.current.mousePosition) || num - num2 == 0f)
				{
					return num3;
				}
				scrollThroughSide = 0;
				if (position2.Contains(Event.current.mousePosition))
				{
					sliderState.dragStartPos = num7;
					sliderState.dragStartValue = value;
					sliderState.isDragging = true;
					GUIUtility.hotControl = id;
					Event.current.Use();
					return num3;
				}
				if (size != 0f && usePageScrollbars)
				{
					num3 = (horiz ? ((!(num7 > position2.xMax - position.x)) ? (num3 - size * num4 * 0.9f) : (num3 + size * num4 * 0.9f)) : ((!(num7 > position2.yMax - position.y)) ? (num3 - size * num4 * 0.9f) : (num3 + size * num4 * 0.9f)));
					sliderState.isDragging = false;
					s_Changed = true;
					nextScrollStepTime = DateTime.Now.AddMilliseconds(firstScrollWait);
					float num9 = ((!horiz) ? Event.current.mousePosition.y : Event.current.mousePosition.x);
					float num10 = ((!horiz) ? position2.y : position2.x);
					scrollThroughSide = ((num9 > num10) ? 1 : (-1));
				}
				else
				{
					num3 = ((!horiz) ? ((num7 - position2.height * 0.5f) / num6 + start - size * 0.5f) : ((num7 - position2.width * 0.5f) / num6 + start - size * 0.5f));
					sliderState.dragStartPos = num7;
					sliderState.dragStartValue = num3;
					sliderState.isDragging = true;
					s_Changed = true;
				}
				GUIUtility.hotControl = id;
				num3 = Mathf.Clamp(num3, num, num2 - size);
				Event.current.Use();
				return num3;
			case EventType.mouseDrag:
			{
				if (GUIUtility.hotControl != id || !sliderState.isDragging)
				{
					return num3;
				}
				float num12 = num7 - sliderState.dragStartPos;
				num3 = sliderState.dragStartValue + num12 / num6;
				num3 = Mathf.Clamp(num3, num, num2 - size);
				s_Changed = true;
				Event.current.Use();
				break;
			}
			case EventType.mouseUp:
				if (GUIUtility.hotControl == id)
				{
					Event.current.Use();
					GUIUtility.hotControl = 0;
				}
				break;
			case EventType.repaint:
			{
				slider.Draw(position, GUIContent.none, id);
				thumb.Draw(position2, GUIContent.none, id);
				if (GUIUtility.hotControl != id || !position.Contains(Event.current.mousePosition) || num - num2 == 0f)
				{
					return num3;
				}
				if (position2.Contains(Event.current.mousePosition))
				{
					if (scrollThroughSide != 0)
					{
						GUIUtility.hotControl = 0;
					}
					return num3;
				}
				InternalRepaintEditorWindow();
				if (DateTime.Now < nextScrollStepTime)
				{
					return num3;
				}
				float num9 = ((!horiz) ? Event.current.mousePosition.y : Event.current.mousePosition.x);
				float num10 = ((!horiz) ? position2.y : position2.x);
				int num11 = ((num9 > num10) ? 1 : (-1));
				if (num11 != scrollThroughSide)
				{
					return num3;
				}
				if (size != 0f && usePageScrollbars)
				{
					num3 = (horiz ? ((!(num7 > position2.xMax - position.x)) ? (num3 - size * num4 * 0.9f) : (num3 + size * num4 * 0.9f)) : ((!(num7 > position2.yMax - position.y)) ? (num3 - size * num4 * 0.9f) : (num3 + size * num4 * 0.9f)));
					sliderState.isDragging = false;
					s_Changed = true;
				}
				num3 = Mathf.Clamp(num3, num, num2 - size);
				nextScrollStepTime = DateTime.Now.AddMilliseconds(scrollWait);
				break;
			}
			}
			return num3;
		}

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return Scroller(position, value, size, leftValue, rightValue, skin.horizontalScrollbar, skin.horizontalScrollbarThumb, skin.horizontalScrollbarLeftButton, skin.horizontalScrollbarRightButton, true);
		}

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return Scroller(position, value, size, leftValue, rightValue, style, skin.GetStyle(style.name + "thumb"), skin.GetStyle(style.name + "leftbutton"), skin.GetStyle(style.name + "rightbutton"), true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		private static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool result = false;
			if (DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
			{
				bool flag = scrollControlID != scrollerID;
				scrollControlID = scrollerID;
				if (flag)
				{
					s_Changed = true;
					result = true;
					nextScrollStepTime = DateTime.Now.AddMilliseconds(firstScrollWait);
				}
				else if (DateTime.Now >= nextScrollStepTime)
				{
					s_Changed = true;
					result = true;
					nextScrollStepTime = DateTime.Now.AddMilliseconds(scrollWait);
				}
				if (Event.current.type == EventType.repaint)
				{
					InternalRepaintEditorWindow();
				}
			}
			return result;
		}

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return Scroller(position, value, size, topValue, bottomValue, skin.verticalScrollbar, skin.verticalScrollbarThumb, skin.verticalScrollbarUpButton, skin.verticalScrollbarDownButton, false);
		}

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return Scroller(position, value, size, topValue, bottomValue, style, skin.GetStyle(style.name + "thumb"), skin.GetStyle(style.name + "upbutton"), skin.GetStyle(style.name + "downbutton"), false);
		}

		private static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(sliderHash, FocusType.Passive, position);
			Rect position2;
			Rect rect;
			Rect rect2;
			if (horiz)
			{
				position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
			bool flag = false;
			if (Event.current.type == EventType.mouseUp)
			{
				flag = true;
			}
			if (ScrollerRepeatButton(controlID, rect, leftButton))
			{
				value -= scrollStepSize * ((!(leftValue < rightValue)) ? (-1f) : 1f);
			}
			if (ScrollerRepeatButton(controlID, rect2, rightButton))
			{
				value += scrollStepSize * ((!(leftValue < rightValue)) ? (-1f) : 1f);
			}
			if (flag && Event.current.type == EventType.used)
			{
				scrollControlID = 0;
			}
			value = ((!(leftValue < rightValue)) ? Mathf.Clamp(value, rightValue, leftValue - size) : Mathf.Clamp(value, leftValue, rightValue - size));
			return value;
		}

		public static void BeginGroup(Rect position)
		{
			BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		public static void BeginGroup(Rect position, string text)
		{
			BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		public static void BeginGroup(Rect position, Texture image)
		{
			BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		public static void BeginGroup(Rect position, GUIContent content)
		{
			BeginGroup(position, content, GUIStyle.none);
		}

		public static void BeginGroup(Rect position, GUIStyle style)
		{
			BeginGroup(position, GUIContent.none, style);
		}

		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			BeginGroup(position, GUIContent.Temp(text), style);
		}

		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			BeginGroup(position, GUIContent.Temp(image), style);
		}

		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(beginGroupHash, FocusType.Passive);
			if (content != GUIContent.none || style != GUIStyle.none)
			{
				EventType type = Event.current.type;
				if (type == EventType.repaint)
				{
					style.Draw(position, content, controlID);
				}
				else if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.mouseUsed = true;
				}
			}
			GUIClip.Push(position);
		}

		public static void EndGroup()
		{
			GUIClip.Pop();
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return BeginScrollView(position, scrollPosition, viewRect, false, false, skin.horizontalScrollbar, skin.verticalScrollbar, skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, skin.horizontalScrollbar, skin.verticalScrollbar, skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, null);
		}

		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(scrollviewHash, FocusType.Passive);
			ScrollViewState scrollViewState = (ScrollViewState)GUIUtility.GetStateObject(typeof(ScrollViewState), controlID);
			if (scrollViewState.apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = viewRect;
			scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
			scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
			s_ScrollViewStates.Push(scrollViewState);
			Rect rect = new Rect(position);
			switch (Event.current.type)
			{
			case EventType.layout:
				GUIUtility.GetControlID(sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				break;
			default:
			{
				bool flag = alwaysShowVertical;
				bool flag2 = alwaysShowHorizontal;
				if (flag2 || viewRect.width > rect.width)
				{
					rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
					flag2 = true;
				}
				if (flag || viewRect.height > rect.height)
				{
					rect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
					flag = true;
					if (!flag2 && viewRect.width > rect.width)
					{
						rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
				}
				if (Event.current.type == EventType.repaint && background != GUIStyle.none)
				{
					background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
				}
				if (flag2 && horizontalScrollbar != GUIStyle.none)
				{
					Rect position2 = new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, rect.width, horizontalScrollbar.fixedHeight);
					scrollPosition.x = HorizontalScrollbar(position2, scrollPosition.x, rect.width, 0f, viewRect.width, horizontalScrollbar);
				}
				else
				{
					GUIUtility.GetControlID(sliderHash, FocusType.Passive);
					GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
					GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
					scrollPosition.x = 0f;
				}
				if (flag)
				{
					Rect position3 = new Rect(rect.xMax + (float)verticalScrollbar.margin.left, rect.y, verticalScrollbar.fixedWidth, rect.height);
					scrollPosition.y = VerticalScrollbar(position3, scrollPosition.y, rect.height, 0f, viewRect.height, verticalScrollbar);
					break;
				}
				scrollPosition.y = 0f;
				GUIUtility.GetControlID(sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(repeatButtonHash, FocusType.Passive);
				break;
			}
			case EventType.used:
				break;
			}
			Rect screenRect = rect;
			Vector2 scrollOffset = new Vector2(Mathf.Round(0f - scrollPosition.x - viewRect.x), Mathf.Round(0f - scrollPosition.y - viewRect.y));
			GUIClip.Push(screenRect, scrollOffset);
			return scrollPosition;
		}

		public static void EndScrollView()
		{
			ScrollViewState scrollViewState = (ScrollViewState)s_ScrollViewStates.Peek();
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
			s_ScrollViewStates.Pop();
			if (Event.current.type == EventType.scrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition += Event.current.delta * 20f;
				scrollViewState.apply = true;
				Event.current.Use();
			}
		}

		internal static ScrollViewState GetTopScrollView()
		{
			if (s_ScrollViewStates.Count != 0)
			{
				return (ScrollViewState)s_ScrollViewStates.Peek();
			}
			return null;
		}

		public static void ScrollTo(Rect position)
		{
			ScrollViewState topScrollView = GetTopScrollView();
			topScrollView.ScrollTo(GUIClip.Unclip(position));
		}

		private static ArrayList GetSortedWindowList()
		{
			ArrayList arrayList = new ArrayList(_WindowList.instance.windows.Values);
			arrayList.Sort();
			return arrayList;
		}

		public static Rect Window(int id, Rect position, WindowFunction func, string text)
		{
			return DoWindow(id, position, func, GUIContent.Temp(text), skin.window, true);
		}

		public static Rect Window(int id, Rect position, WindowFunction func, Texture image)
		{
			return DoWindow(id, position, func, GUIContent.Temp(image), skin.window, true);
		}

		public static Rect Window(int id, Rect position, WindowFunction func, GUIContent content)
		{
			return DoWindow(id, position, func, content, skin.window, true);
		}

		public static Rect Window(int id, Rect position, WindowFunction func, string text, GUIStyle style)
		{
			return DoWindow(id, position, func, GUIContent.Temp(text), style, true);
		}

		public static Rect Window(int id, Rect position, WindowFunction func, Texture image, GUIStyle style)
		{
			return DoWindow(id, position, func, GUIContent.Temp(image), style, true);
		}

		public static Rect Window(int id, Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style)
		{
			return DoWindow(id, clientRect, func, title, style, true);
		}

		internal static Rect DoWindow(int id, Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style, bool forceRectOnLayout)
		{
			GUIUtility.CheckOnGUI();
			_Window window = (_Window)_WindowList.instance.windows[id];
			if (window == null)
			{
				_Window window2 = new _Window(id);
				window = window2;
				object obj = window2;
				_WindowList.instance.windows[id] = window2;
				s_LayersChanged = true;
			}
			if (!window.moved)
			{
				window.rect = clientRect;
			}
			window.moved = false;
			window.opacity = 1f;
			window.style = style;
			window.title.text = title.text;
			window.title.image = title.image;
			window.title.tooltip = title.tooltip;
			window.func = func;
			window.used = true;
			window.enabled = enabled;
			window.color = color;
			window.backgroundColor = backgroundColor;
			window.matrix = matrix;
			window.skin = skin;
			window.contentColor = contentColor;
			window.forceRect = forceRectOnLayout;
			return window.rect;
		}

		public static void DragWindow(Rect position)
		{
			GUIUtility.CheckOnGUI();
			if (_Window.current == null)
			{
				Debug.LogError("Dragwindow can only be called within a window callback");
				return;
			}
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			_Window current = _Window.current;
			Event current2 = Event.current;
			if (current == null)
			{
				return;
			}
			WindowDragState windowDragState = (WindowDragState)GUIUtility.GetStateObject(typeof(WindowDragState), controlID + 100);
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.mouseDown:
				if (position.Contains(current2.mousePosition) && GUIUtility.hotControl == 0)
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
					Matrix4x4 matrix4x = _Window.current.matrix;
					Vector2 s_AbsoluteMousePosition = GUIClip.s_AbsoluteMousePosition;
					Vector2 vector2 = new Vector2(current.rect.x, current.rect.y);
					windowDragState.dragStartPos = s_AbsoluteMousePosition - (Vector2)matrix4x.MultiplyPoint(vector2);
					windowDragState.dragStartRect = current.rect;
				}
				break;
			case EventType.mouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
				}
				break;
			case EventType.mouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Vector2 vector = _Window.current.matrix.inverse.MultiplyPoint(GUIClip.s_AbsoluteMousePosition - windowDragState.dragStartPos);
					current.rect = new Rect(vector.x, vector.y, windowDragState.dragStartRect.width, windowDragState.dragStartRect.height);
					current.moved = true;
					Event.current.Use();
				}
				break;
			case EventType.mouseMove:
				break;
			}
		}

		public static void DragWindow()
		{
			Rect position = new Rect(0f, 0f, 10000f, 10000f);
			DragWindow(position);
		}

		public static void BringWindowToFront(int windowID)
		{
			GUIUtility.CheckOnGUI();
			_Window window = _WindowList.instance.Get(windowID);
			if (window != null)
			{
				window.depth = -1;
				s_LayersChanged = true;
			}
		}

		public static void BringWindowToBack(int windowID)
		{
			GUIUtility.CheckOnGUI();
			_Window window = _WindowList.instance.Get(windowID);
			if (window != null)
			{
				window.depth = 10000;
				s_LayersChanged = true;
			}
		}

		public static void FocusWindow(int windowID)
		{
			GUIUtility.CheckOnGUI();
			focusedWindow = windowID;
		}

		public static void UnfocusWindow()
		{
			GUIUtility.CheckOnGUI();
			focusedWindow = -1;
		}

		private static _Window FindWindowUnderMouse()
		{
			Event current = Event.current;
			foreach (_Window sortedWindow in GetSortedWindowList())
			{
				matrix = sortedWindow.matrix;
				if (sortedWindow.rect.Contains(current.mousePosition))
				{
					return sortedWindow;
				}
			}
			return null;
		}

		protected static void BeginWindows(Event e, int skinMode, IDList idlist, int editorWindowInstanceID)
		{
			GUILayoutUtility.LayoutCache current = GUILayoutUtility.current;
			if (editorWindowInstanceID == 0)
			{
				if (s_GameWindowList == null)
				{
					s_GameWindowList = new _WindowList();
				}
				_WindowList.instance = s_GameWindowList;
			}
			else
			{
				if (_WindowList.s_EditorWindows == null)
				{
					_WindowList.s_EditorWindows = new Hashtable();
				}
				_WindowList.instance = (_WindowList)_WindowList.s_EditorWindows[editorWindowInstanceID];
				if (_WindowList.instance == null)
				{
					_WindowList windowList = (_WindowList.instance = new _WindowList());
					object obj = windowList;
					_WindowList.s_EditorWindows[editorWindowInstanceID] = windowList;
				}
			}
			_Window window = null;
			Matrix4x4 matrix4x = matrix;
			Event current2 = Event.current;
			switch (current2.type)
			{
			case EventType.layout:
				foreach (_Window value in _WindowList.instance.windows.Values)
				{
					value.used = false;
				}
				break;
			case EventType.dragUpdated:
			case EventType.dragPerform:
			case EventType.DragExited:
				window = FindWindowUnderMouse();
				break;
			case EventType.mouseUp:
			case EventType.mouseDrag:
				window = ((GUIUtility.hotControl != 0) ? ((_Window)_WindowList.instance.windows[focusedWindow]) : FindWindowUnderMouse());
				break;
			case EventType.mouseDown:
			{
				focusedWindow = -1;
				bool flag = false;
				foreach (_Window sortedWindow in GetSortedWindowList())
				{
					matrix = sortedWindow.matrix;
					if (sortedWindow.rect.Contains(current2.mousePosition))
					{
						focusedWindow = sortedWindow.id;
						window = sortedWindow;
						((_Window)_WindowList.instance.windows[sortedWindow.id]).depth = -1;
						flag = true;
						break;
					}
				}
				if (!flag && !s_LayersChanged)
				{
					break;
				}
				int num = 0;
				foreach (_Window sortedWindow2 in GetSortedWindowList())
				{
					sortedWindow2.depth = num;
					num++;
				}
				s_LayersChanged = false;
				break;
			}
			case EventType.repaint:
				return;
			default:
				window = (_Window)_WindowList.instance.windows[focusedWindow];
				break;
			}
			if (window != null)
			{
				window.SetupGUIValues();
				IDList s_CurrentList = GUIUtility.s_CurrentList;
				GUIUtility.SelectIDList(idlist, window.id, true, Event.current.type == EventType.layout);
				window.Do();
				GUIUtility.SelectIDList(s_CurrentList, 0, false, Event.current.type == EventType.layout);
				GUIUtility.SetDidGUIWindowsEatLastEvent(true);
			}
			matrix = matrix4x;
			GUILayoutUtility.current = current;
		}

		internal static void EndWindows(IDList idlist)
		{
			GUILayoutUtility.LayoutCache current = GUILayoutUtility.current;
			Event current2 = Event.current;
			ResetSettings();
			switch (current2.type)
			{
			case EventType.layout:
			{
				Hashtable hashtable = new Hashtable();
				IDList s_CurrentList = GUIUtility.s_CurrentList;
				foreach (_Window value in _WindowList.instance.windows.Values)
				{
					if (value.used)
					{
						if (value.forceRect)
						{
							GUILayoutOption[] options = new GUILayoutOption[2]
							{
								GUILayout.Width(value.rect.width),
								GUILayout.Height(value.rect.height)
							};
							GUILayoutUtility.BeginWindow(value.id, value.style, options);
						}
						else
						{
							GUILayoutUtility.BeginWindow(value.id, value.style, null);
						}
						value.SetupGUIValues();
						GUIUtility.SelectIDList(idlist, value.id, true, Event.current.type == EventType.layout);
						value.Do();
						GUILayoutUtility.Layout();
						hashtable[value.id] = value;
					}
				}
				GUIUtility.SelectIDList(s_CurrentList, 0, false, Event.current.type == EventType.layout);
				_WindowList.instance.windows = hashtable;
				break;
			}
			case EventType.repaint:
			{
				ArrayList sortedWindowList = GetSortedWindowList();
				sortedWindowList.Reverse();
				IDList s_CurrentList = GUIUtility.s_CurrentList;
				foreach (_Window item in sortedWindowList)
				{
					item.SetupGUIValues();
					if (item.style != GUIStyle.none)
					{
						item.style.Draw(item.rect, item.title, item.rect.Contains(Event.current.mousePosition), false, focusedWindow == item.id, false);
					}
					if (item.rect.Contains(Event.current.mousePosition))
					{
						GUIUtility.mouseUsed = true;
					}
					GUIUtility.SelectIDList(idlist, item.id, true, Event.current.type == EventType.layout);
					item.Do();
				}
				GUIUtility.SelectIDList(s_CurrentList, 0, false, Event.current.type == EventType.layout);
				break;
			}
			}
			GUILayoutUtility.current = current;
		}

		protected static void DoEndWindows(IDList idlist)
		{
			EndWindows(idlist);
		}
	}
}
