namespace UnityEngine
{
	public class GUILayout
	{
		private class LayoutedWindow
		{
			private GUI.WindowFunction func;

			private Rect screenRect;

			private GUILayoutOption[] options;

			internal LayoutedWindow(GUI.WindowFunction f, Rect _screenRect, GUIContent _content, GUILayoutOption[] _options)
			{
				func = f;
				screenRect = _screenRect;
				options = _options;
			}

			public void DoWindow(int windowID)
			{
				GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
				GUI._Window window = GUI._WindowList.instance.Get(windowID);
				EventType type = Event.current.type;
				if (type == EventType.layout)
				{
					topLevel.resetCoords = true;
					topLevel.rect = screenRect;
					if (options != null)
					{
						topLevel.ApplyOptions(options);
					}
					topLevel.isWindow = true;
					topLevel.windowID = windowID;
					topLevel.style = window.style;
				}
				else
				{
					topLevel.ResetCursor();
				}
				func(windowID);
			}
		}

		public static void Label(Texture image, params GUILayoutOption[] options)
		{
			DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
		}

		public static void Label(string text, params GUILayoutOption[] options)
		{
			DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		public static void Label(GUIContent content, params GUILayoutOption[] options)
		{
			DoLabel(content, GUI.skin.label, options);
		}

		public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			DoLabel(GUIContent.Temp(image), style, options);
		}

		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			DoLabel(GUIContent.Temp(text), style, options);
		}

		public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			DoLabel(content, style, options);
		}

		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		public static void Box(Texture image, params GUILayoutOption[] options)
		{
			DoBox(GUIContent.Temp(image), GUI.skin.box, options);
		}

		public static void Box(string text, params GUILayoutOption[] options)
		{
			DoBox(GUIContent.Temp(text), GUI.skin.box, options);
		}

		public static void Box(GUIContent content, params GUILayoutOption[] options)
		{
			DoBox(content, GUI.skin.box, options);
		}

		public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			DoBox(GUIContent.Temp(image), style, options);
		}

		public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			DoBox(GUIContent.Temp(text), style, options);
		}

		public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			DoBox(content, style, options);
		}

		private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		public static bool Button(Texture image, params GUILayoutOption[] options)
		{
			return DoButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		public static bool Button(GUIContent content, params GUILayoutOption[] options)
		{
			return DoButton(content, GUI.skin.button, options);
		}

		public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoButton(GUIContent.Temp(image), style, options);
		}

		public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoButton(GUIContent.Temp(text), style, options);
		}

		public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoButton(content, style, options);
		}

		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
		{
			return DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		public static bool RepeatButton(string text, params GUILayoutOption[] options)
		{
			return DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
		{
			return DoRepeatButton(content, GUI.skin.button, options);
		}

		public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoRepeatButton(GUIContent.Temp(image), style, options);
		}

		public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoRepeatButton(GUIContent.Temp(text), style, options);
		}

		public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoRepeatButton(content, style, options);
		}

		private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoTextField(text, -1, false, style, options);
		}

		public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoTextField(text, maxLength, true, style, options);
		}

		public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
		{
			return PasswordField(password, maskChar, -1, GUI.skin.textField, options);
		}

		public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
		{
			return PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
		}

		public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
		{
			return PasswordField(password, maskChar, -1, style, options);
		}

		public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIContent content = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
			return GUI.PasswordField(GUILayoutUtility.GetRect(content, GUI.skin.textField, options), password, maskChar, maxLength, style);
		}

		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
		{
			return DoTextField(text, maxLength, true, GUI.skin.textArea, options);
		}

		public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoTextField(text, -1, true, style, options);
		}

		public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoTextField(text, maxLength, true, style, options);
		}

		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			Rect rect = GUILayoutUtility.GetRect(gUIContent, style, options);
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard, rect);
			GUI.DoTextField(rect, controlID, gUIContent, multiline, maxLength, style);
			return gUIContent.text;
		}

		public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
		{
			return DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
		}

		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
		{
			return DoToggle(value, content, GUI.skin.toggle, options);
		}

		public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoToggle(value, GUIContent.Temp(image), style, options);
		}

		public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoToggle(value, GUIContent.Temp(text), style, options);
		}

		public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoToggle(value, content, style, options);
		}

		protected static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
		{
			return Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
		}

		public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
		{
			return Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
		}

		public static int Toolbar(int selected, GUIContent[] content, params GUILayoutOption[] options)
		{
			return Toolbar(selected, content, GUI.skin.button, options);
		}

		public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
		{
			return Toolbar(selected, GUIContent.Temp(texts), style, options);
		}

		public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
		{
			return Toolbar(selected, GUIContent.Temp(images), style, options);
		}

		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
		{
			Vector2 vector = style.CalcSize(contents[0]);
			vector.x *= contents.Length;
			return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, style);
		}

		public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
		{
			return SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
		}

		public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
		{
			return SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
		}

		public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
		{
			return SelectionGrid(selected, content, xCount, GUI.skin.button, options);
		}

		public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
		}

		public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
		}

		public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
		}

		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
		}

		public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
		}

		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
		}

		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
		{
			return VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
		}

		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
		}

		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, Height(pixels));
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, Width(pixels));
			}
		}

		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			GUILayoutOption gUILayoutOption = ((!GUILayoutUtility.current.topLevel.isVertical) ? ExpandWidth(true) : ExpandHeight(true));
			gUILayoutOption.value = 10000;
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, gUILayoutOption);
		}

		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			BeginHorizontal(GUIStyle.none, options);
		}

		public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			gUILayoutGroup.isVertical = false;
			if (style != GUIStyle.none)
			{
				GUI.Box(gUILayoutGroup.rect, GUIContent.none, style);
			}
		}

		public static void EndHorizontal()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndHorizontal");
			GUILayoutUtility.EndLayoutGroup();
		}

		public static void BeginVertical(params GUILayoutOption[] options)
		{
			BeginVertical(GUIStyle.none, options);
		}

		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			gUILayoutGroup.isVertical = true;
			if (style != GUIStyle.none)
			{
				GUI.Box(gUILayoutGroup.rect, GUIContent.none, style);
			}
		}

		public static void EndVertical()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndVertical");
			GUILayoutUtility.EndLayoutGroup();
		}

		public static void BeginArea(Rect screenRect)
		{
			BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		public static void BeginArea(Rect screenRect, string text)
		{
			BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
		}

		public static void BeginArea(Rect screenRect, Texture image)
		{
			BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
		}

		public static void BeginArea(Rect screenRect, GUIContent content)
		{
			BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		public static void BeginArea(Rect screenRect, GUIStyle style)
		{
			BeginArea(screenRect, GUIContent.none, style);
		}

		public static void BeginArea(Rect screenRect, string text, GUIStyle style)
		{
			BeginArea(screenRect, GUIContent.Temp(text), style);
		}

		public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
		{
			BeginArea(screenRect, GUIContent.Temp(image), style);
		}

		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			if (Event.current.type == EventType.layout)
			{
				gUILayoutGroup.resetCoords = true;
				gUILayoutGroup.minWidth = (gUILayoutGroup.maxWidth = screenRect.width);
				gUILayoutGroup.minHeight = (gUILayoutGroup.maxHeight = screenRect.height);
				gUILayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, gUILayoutGroup.rect.xMax, gUILayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(gUILayoutGroup.rect, content, style);
		}

		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type != EventType.used)
			{
				GUILayoutUtility.current.layoutGroups.Pop();
				GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				GUI.EndGroup();
			}
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			return BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			return BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return BeginScrollView(scrollPosition, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
		{
			string name = style.name;
			GUIStyle gUIStyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
			if (gUIStyle == null)
			{
				gUIStyle = GUI.skin.verticalScrollbar;
			}
			GUIStyle gUIStyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
			if (gUIStyle2 == null)
			{
				gUIStyle2 = GUI.skin.horizontalScrollbar;
			}
			return BeginScrollView(scrollPosition, false, false, gUIStyle2, gUIStyle, style);
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUIScrollGroup gUIScrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
			EventType type = Event.current.type;
			if (type == EventType.layout)
			{
				gUIScrollGroup.resetCoords = true;
				gUIScrollGroup.isVertical = true;
				gUIScrollGroup.stretchWidth = 1;
				gUIScrollGroup.stretchHeight = 1;
				gUIScrollGroup.verticalScrollbar = verticalScrollbar;
				gUIScrollGroup.horizontalScrollbar = horizontalScrollbar;
				gUIScrollGroup.needsVerticalScrollbar = alwaysShowVertical;
				gUIScrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
				gUIScrollGroup.ApplyOptions(options);
			}
			Rect rect = gUIScrollGroup.rect;
			Rect viewRect = new Rect(0f, 0f, gUIScrollGroup.clientWidth, gUIScrollGroup.clientHeight);
			return GUI.BeginScrollView(rect, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		public static void EndScrollView()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndScrollView");
			GUILayoutUtility.EndLayoutGroup();
			GUI.EndScrollView();
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, content, GUI.skin.window, options);
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
		}

		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoWindow(id, screenRect, func, content, style, options);
		}

		private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			LayoutedWindow layoutedWindow = new LayoutedWindow(func, screenRect, content, options);
			return GUI.Window(id, screenRect, layoutedWindow.DoWindow, content, style);
		}

		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		public static GUILayoutOption MinWidth(float minWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
		}

		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		public static GUILayoutOption MinHeight(float minHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
		}

		public static GUILayoutOption MaxHeight(float maxHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
		}

		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, expand ? 1 : 0);
		}

		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, expand ? 1 : 0);
		}
	}
}
