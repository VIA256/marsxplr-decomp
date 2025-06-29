using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class IDList
	{
		internal class NamedControl
		{
			internal int windowID;

			internal int controlID;

			internal int scriptID;

			internal NamedControl(int controlID)
			{
				scriptID = GUIUtility.GetCurrentScriptInstanceID();
				if (GUI._Window.current != null)
				{
					windowID = GUI._Window.current.id;
				}
				else
				{
					windowID = -1;
				}
				this.controlID = controlID;
			}

			internal void MoveKeyboardFocusToThisControl()
			{
				GUIUtility.keyboardControl = controlID;
				GUI.FocusWindow(windowID);
				GUIUtility.SetKeyboardScriptInstanceID(scriptID);
			}
		}

		internal IntPtr m_GuiState;

		internal ArrayList keyboardFocusIDs = new ArrayList();

		private static Hashtable s_StateCache = new Hashtable();

		internal static Hashtable s_KeyboardFocusNames = new Hashtable();

		internal static string s_NextKeyboardFocusName = null;

		private IDList(IntPtr ptr)
		{
			m_GuiState = ptr;
		}

		internal int GetNext(int hint, FocusType focusType)
		{
			return GetNext(hint, focusType, GUIUtility.dummyRect);
		}

		internal int GetNext(int hint, FocusType focusType, Rect position)
		{
			int num = 0;
			if (Event.current.type != EventType.used)
			{
				num = CalculateNextFromHintList(m_GuiState, hint);
				if (s_NextKeyboardFocusName != null)
				{
					s_KeyboardFocusNames[s_NextKeyboardFocusName] = new NamedControl(num);
					s_NextKeyboardFocusName = null;
				}
				if (Event.current.type != EventType.layout && Event.current.rawType == EventType.keyDown && GUI.enabled)
				{
					if (GUIUtility.DecodeKeyboardControl(focusType))
					{
						keyboardFocusIDs.Add(num);
						switch (GUIUtility.s_TabControlSearchMode)
						{
						case GUIUtility.TabControlSearchMode.LookingForPrevious:
							if (GUIUtility.s_FirstKeyControl == 0)
							{
								GUIUtility.s_FirstKeyControl = num;
							}
							if (num != GUIUtility.keyboardControl)
							{
								GUIUtility.s_PreviousKeyControl = num;
							}
							else
							{
								GUIUtility.s_TabControlSearchMode = GUIUtility.TabControlSearchMode.LookingForNext;
							}
							break;
						case GUIUtility.TabControlSearchMode.LookingForNext:
							GUIUtility.s_NextKeyControl = num;
							GUIUtility.s_TabControlSearchMode = GUIUtility.TabControlSearchMode.Found;
							break;
						case GUIUtility.TabControlSearchMode.Found:
							GUIUtility.s_LastKeyControl = num;
							break;
						}
					}
					if (position != GUIUtility.dummyRect)
					{
						if (GUIUtility.keyboardControl == num)
						{
							GUIUtility.activeScrollView = GUI.GetTopScrollView();
						}
						if (GUIUtility.keyboardRects.ContainsKey(num))
						{
							GUIUtility.keyboardRects[num] = position;
						}
						else
						{
							GUIUtility.keyboardRects.Add(num, position);
						}
					}
				}
				return num;
			}
			return -1;
		}

		internal void Reset(bool clearFocusList)
		{
			if (this == null)
			{
				Debug.LogError("this null");
			}
			if (keyboardFocusIDs == null)
			{
				Debug.LogError("keyfocus null");
			}
			ResetIdx(m_GuiState);
			if (clearFocusList)
			{
				keyboardFocusIDs.Clear();
			}
		}

		internal object GetStateObject(Type t, int controlID)
		{
			object obj = s_StateCache[controlID];
			if (obj == null || obj.GetType() != t)
			{
				obj = Activator.CreateInstance(t);
				s_StateCache[controlID] = obj;
			}
			return obj;
		}

		internal object QueryStateObject(Type t, int controlID)
		{
			object obj = s_StateCache[controlID];
			if (t.IsInstanceOfType(obj))
			{
				return obj;
			}
			return null;
		}

		internal void SkipToControlID(int hint)
		{
			Internal_SkipToControlID(m_GuiState, hint);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SkipToControlID(IntPtr lst, int hint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CalculateNextFromHintList(IntPtr lst, int hint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetIdx(IntPtr lst);
	}
}
