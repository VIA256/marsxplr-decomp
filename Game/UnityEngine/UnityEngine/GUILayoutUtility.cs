using System;
using System.Collections;

namespace UnityEngine
{
	public class GUILayoutUtility
	{
		internal class LayoutCache
		{
			public GUILayoutGroup topLevel = new GUILayoutGroup();

			public Stack layoutGroups = new Stack();

			public GUILayoutGroup windows = new GUILayoutGroup();

			internal LayoutCache()
			{
				layoutGroups.Push(topLevel);
			}
		}

		private static Hashtable storedLayouts = new Hashtable();

		private static Hashtable storedWindows = new Hashtable();

		internal static LayoutCache current = new LayoutCache();

		private static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		private static GUIStyle s_SpaceStyle;

		internal static GUIStyle spaceStyle
		{
			get
			{
				s_SpaceStyle.stretchWidth = false;
				return s_SpaceStyle;
			}
		}

		static GUILayoutUtility()
		{
			kDummyRect = kDummyRect;
			s_SpaceStyle = new GUIStyle();
		}

		internal static LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Hashtable hashtable = ((!isWindow) ? storedLayouts : storedWindows);
			LayoutCache layoutCache = (LayoutCache)hashtable[instanceID];
			if (layoutCache == null)
			{
				layoutCache = new LayoutCache();
				hashtable[instanceID] = layoutCache;
			}
			current.topLevel = layoutCache.topLevel;
			current.layoutGroups = layoutCache.layoutGroups;
			current.windows = layoutCache.windows;
			return layoutCache;
		}

		internal static void Begin(int instanceID)
		{
			LayoutCache layoutCache = SelectIDList(instanceID, false);
			if (Event.current.type == EventType.layout)
			{
				GUILayoutGroup topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				current.topLevel = topLevel;
				current.layoutGroups.Clear();
				current.layoutGroups.Push(current.topLevel);
				topLevel = (layoutCache.windows = new GUILayoutGroup());
				current.windows = topLevel;
			}
			else
			{
				current.topLevel = layoutCache.topLevel;
				current.layoutGroups = layoutCache.layoutGroups;
				current.windows = layoutCache.windows;
			}
		}

		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			LayoutCache layoutCache = SelectIDList(windowID, true);
			if (Event.current.type == EventType.layout)
			{
				GUILayoutGroup topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				current.topLevel = topLevel;
				current.topLevel.style = style;
				current.topLevel.windowID = windowID;
				if (options != null)
				{
					current.topLevel.ApplyOptions(options);
				}
				current.layoutGroups.Clear();
				current.layoutGroups.Push(current.topLevel);
				topLevel = (layoutCache.windows = new GUILayoutGroup());
				current.windows = topLevel;
			}
			else
			{
				current.topLevel = layoutCache.topLevel;
				current.layoutGroups = layoutCache.layoutGroups;
				current.windows = layoutCache.windows;
			}
		}

		internal static void End()
		{
			if (Event.current.type == EventType.layout)
			{
				Layout();
			}
		}

		public static void BeginGroup(string GroupName)
		{
		}

		public static void EndGroup(string groupName)
		{
		}

		internal static void Layout()
		{
			if (current.topLevel.windowID == -1)
			{
				current.topLevel.CalcWidth();
				current.topLevel.SetHorizontal(0f, Mathf.Min(Screen.width, current.topLevel.maxWidth));
				current.topLevel.CalcHeight();
				current.topLevel.SetVertical(0f, Mathf.Min(Screen.height, current.topLevel.maxHeight));
				LayoutFreeGroup(current.windows);
			}
			else
			{
				LayoutSingleGroup(current.topLevel);
				LayoutFreeGroup(current.windows);
			}
		}

		public static void LayoutFromEditorWindow()
		{
			current.topLevel.CalcWidth();
			current.topLevel.SetHorizontal(0f, Screen.width);
			current.topLevel.CalcHeight();
			current.topLevel.SetVertical(0f, Screen.height);
			LayoutFreeGroup(current.windows);
		}

		public static float LayoutFromInspector(float width)
		{
			if (current.topLevel != null && current.topLevel.windowID == -1)
			{
				current.topLevel.CalcWidth();
				current.topLevel.SetHorizontal(0f, width);
				current.topLevel.CalcHeight();
				current.topLevel.SetVertical(0f, Mathf.Min(Screen.height, current.topLevel.maxHeight));
				float minHeight = current.topLevel.minHeight;
				LayoutFreeGroup(current.windows);
				return minHeight;
			}
			if (current.topLevel != null)
			{
				LayoutSingleGroup(current.topLevel);
			}
			return 0f;
		}

		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutGroup entry in toplevel.entries)
			{
				LayoutSingleGroup(entry);
			}
			toplevel.ResetCursor();
		}

		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			if (!i.isWindow)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
				return;
			}
			GUI._Window window = GUI._WindowList.instance.Get(i.windowID);
			i.CalcWidth();
			i.SetHorizontal(window.rect.x, Mathf.Clamp(window.rect.width, i.minWidth, i.maxWidth));
			i.CalcHeight();
			i.SetVertical(window.rect.y, Mathf.Clamp(window.rect.height, i.minHeight, i.maxHeight));
			if (window.rect != i.rect)
			{
				window.rect = i.rect;
				window.moved = true;
			}
		}

		public static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup gUILayoutGroup;
			if (type == EventType.layout || type == EventType.used)
			{
				gUILayoutGroup = (GUILayoutGroup)Activator.CreateInstance(LayoutType);
				gUILayoutGroup.style = style;
				if (options != null)
				{
					gUILayoutGroup.ApplyOptions(options);
				}
				current.topLevel.Add(gUILayoutGroup);
			}
			else
			{
				gUILayoutGroup = current.topLevel.GetNext() as GUILayoutGroup;
				if (gUILayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				gUILayoutGroup.ResetCursor();
			}
			current.layoutGroups.Push(gUILayoutGroup);
			current.topLevel = gUILayoutGroup;
			return gUILayoutGroup;
		}

		public static void EndLayoutGroup()
		{
			EventType type = Event.current.type;
			current.layoutGroups.Pop();
			current.topLevel = (GUILayoutGroup)current.layoutGroups.Peek();
		}

		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup gUILayoutGroup;
			if (type == EventType.layout || type == EventType.used)
			{
				gUILayoutGroup = (GUILayoutGroup)Activator.CreateInstance(LayoutType);
				gUILayoutGroup.style = style;
				current.windows.Add(gUILayoutGroup);
			}
			else
			{
				gUILayoutGroup = current.windows.GetNext() as GUILayoutGroup;
				if (gUILayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				gUILayoutGroup.ResetCursor();
			}
			current.layoutGroups.Push(gUILayoutGroup);
			current.topLevel = gUILayoutGroup;
			return gUILayoutGroup;
		}

		protected static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type LayoutType)
		{
			return BeginLayoutArea(style, LayoutType);
		}

		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return DoGetRect(content, style, null);
		}

		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoGetRect(content, style, options);
		}

		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			switch (Event.current.type)
			{
			case EventType.layout:
				if (style.isHeightDependantOnWidth)
				{
					current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 vector = style.CalcSize(content);
					current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				return kDummyRect;
			case EventType.used:
				return kDummyRect;
			default:
				return current.topLevel.GetNext().rect;
			}
		}

		public static Rect GetRect(float width, float height)
		{
			return DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return DoGetRect(width, width, height, height, style, null);
		}

		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoGetRect(width, width, height, height, style, options);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			switch (Event.current.type)
			{
			case EventType.layout:
				current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				return kDummyRect;
			case EventType.used:
				return kDummyRect;
			default:
				return current.topLevel.GetNext().rect;
			}
		}

		public static Rect GetLastRect()
		{
			switch (Event.current.type)
			{
			case EventType.layout:
				return kDummyRect;
			case EventType.used:
				return kDummyRect;
			default:
				return current.topLevel.GetLast();
			}
		}

		public static Rect GetAspectRect(float aspect)
		{
			return DoGetAspectRect(aspect, GUIStyle.none, null);
		}

		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return DoGetAspectRect(aspect, style, null);
		}

		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		private static Rect DoGetAspectRect(float aspect, GUIStyle style, GUILayoutOption[] options)
		{
			switch (Event.current.type)
			{
			case EventType.layout:
				current.topLevel.Add(new GUIAspectSizer(aspect, options));
				return kDummyRect;
			case EventType.used:
				return kDummyRect;
			default:
				return current.topLevel.GetNext().rect;
			}
		}
	}
}
