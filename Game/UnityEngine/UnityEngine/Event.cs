using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class Event
	{
		[Flags]
		internal enum Modifiers
		{
			Shift = 1,
			Control = 2,
			Alt = 4,
			Command = 8,
			Numeric = 0x10,
			CapsLock = 0x20,
			FunctionKey = 0x40
		}

		private EventType m_Type;

		private Vector2 m_MousePosition;

		private Vector2 m_Delta;

		private Ray m_MouseRay;

		private Ray m_LastMouseRay;

		private int m_Button;

		private Modifiers m_Modifiers;

		private float m_Pressure;

		private int m_ClickCount;

		private IntPtr m_Camera;

		private char m_Character;

		private short m_KeyCode;

		private IntPtr m_CommandName;

		private static Event m_Current;

		public EventType rawType
		{
			get
			{
				return m_Type;
			}
		}

		public EventType type
		{
			get
			{
				if (current == this)
				{
					if (!GUI.enabled)
					{
						if (m_Type == EventType.repaint || m_Type == EventType.layout || m_Type == EventType.used)
						{
							return m_Type;
						}
						return EventType.ignore;
					}
					if (GUIClip.enabled)
					{
						return m_Type;
					}
					if (m_Type == EventType.mouseDown || m_Type == EventType.mouseUp)
					{
						return EventType.ignore;
					}
					return m_Type;
				}
				return m_Type;
			}
			set
			{
				m_Type = value;
			}
		}

		public Vector2 mousePosition
		{
			get
			{
				return m_MousePosition;
			}
			set
			{
				m_MousePosition = value;
			}
		}

		public Vector2 delta
		{
			get
			{
				return m_Delta;
			}
			set
			{
				m_Delta = value;
			}
		}

		public Ray mouseRay
		{
			get
			{
				return m_MouseRay;
			}
			set
			{
				m_MouseRay = value;
			}
		}

		public int button
		{
			get
			{
				return m_Button;
			}
			set
			{
				m_Button = value;
			}
		}

		internal Modifiers modifiers
		{
			get
			{
				return m_Modifiers;
			}
			set
			{
				m_Modifiers = value;
			}
		}

		public float pressure
		{
			get
			{
				return m_Pressure;
			}
			set
			{
				m_Pressure = value;
			}
		}

		public int clickCount
		{
			get
			{
				return m_ClickCount;
			}
			set
			{
				m_ClickCount = value;
			}
		}

		public IntPtr camera
		{
			get
			{
				return m_Camera;
			}
			set
			{
				m_Camera = value;
			}
		}

		public char character
		{
			get
			{
				return m_Character;
			}
			set
			{
				m_Character = value;
			}
		}

		public string commandName
		{
			get
			{
				return ConvertCStringToString(m_CommandName);
			}
			set
			{
				m_CommandName = ConvertStringToCString(m_CommandName, value);
			}
		}

		public KeyCode keyCode
		{
			get
			{
				return (KeyCode)m_KeyCode;
			}
			set
			{
				m_KeyCode = (short)value;
			}
		}

		public bool shift
		{
			get
			{
				return (m_Modifiers & Modifiers.Shift) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.Shift;
				}
				else
				{
					m_Modifiers |= Modifiers.Shift;
				}
			}
		}

		public bool control
		{
			get
			{
				return (m_Modifiers & Modifiers.Control) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.Control;
				}
				else
				{
					m_Modifiers |= Modifiers.Control;
				}
			}
		}

		public bool alt
		{
			get
			{
				return (m_Modifiers & Modifiers.Alt) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.Alt;
				}
				else
				{
					m_Modifiers |= Modifiers.Alt;
				}
			}
		}

		public bool command
		{
			get
			{
				return (m_Modifiers & Modifiers.Command) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.Command;
				}
				else
				{
					m_Modifiers |= Modifiers.Command;
				}
			}
		}

		public bool capsLock
		{
			get
			{
				return (m_Modifiers & Modifiers.CapsLock) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.CapsLock;
				}
				else
				{
					m_Modifiers |= Modifiers.CapsLock;
				}
			}
		}

		public bool numeric
		{
			get
			{
				return (m_Modifiers & Modifiers.Numeric) != 0;
			}
			set
			{
				if (!value)
				{
					m_Modifiers &= ~Modifiers.Shift;
				}
				else
				{
					m_Modifiers |= Modifiers.Shift;
				}
			}
		}

		public bool functionKey
		{
			get
			{
				return (m_Modifiers & Modifiers.FunctionKey) != 0;
			}
		}

		public static Event current
		{
			get
			{
				return m_Current;
			}
			set
			{
				m_Current = value;
			}
		}

		public bool isKey
		{
			get
			{
				EventType eventType = type;
				return eventType == EventType.keyDown || eventType == EventType.keyUp;
			}
		}

		public bool isMouse
		{
			get
			{
				EventType eventType = type;
				return eventType == EventType.mouseMove || eventType == EventType.mouseDown || eventType == EventType.mouseUp || eventType == EventType.mouseDrag;
			}
		}

		public Event()
		{
		}

		public Event(Event other)
		{
			CopyInternal(other, this);
		}

		public EventType GetTypeForControl(int controlID)
		{
			if (GUIUtility.hotControl == 0)
			{
				return type;
			}
			switch (m_Type)
			{
			case EventType.mouseDown:
			case EventType.mouseUp:
			case EventType.mouseMove:
			case EventType.mouseDrag:
				if (!GUI.enabled)
				{
					return EventType.ignore;
				}
				if (GUIClip.enabled || GUIUtility.hotControl == controlID)
				{
					return m_Type;
				}
				return EventType.ignore;
			case EventType.keyDown:
			case EventType.keyUp:
				if (!GUI.enabled)
				{
					return EventType.ignore;
				}
				if (GUIClip.enabled || GUIUtility.hotControl == controlID || GUIUtility.keyboardControl == controlID)
				{
					return m_Type;
				}
				return EventType.ignore;
			case EventType.scrollWheel:
				if (!GUI.enabled)
				{
					return EventType.ignore;
				}
				if (GUIClip.enabled || GUIUtility.hotControl == controlID || GUIUtility.keyboardControl == controlID)
				{
					return m_Type;
				}
				return EventType.ignore;
			default:
				return m_Type;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyInternal(object src, object dst);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ConvertCStringToString(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ConvertStringToCString(IntPtr old, string value);

		public void Use()
		{
			type = EventType.used;
		}

		public static Event KeyboardEvent(string key)
		{
			Event obj = new Event();
			obj.type = EventType.keyDown;
			if (string.IsNullOrEmpty(key))
			{
				return obj;
			}
			int num = 0;
			bool flag = false;
			do
			{
				flag = true;
				if (num >= key.Length)
				{
					flag = false;
					break;
				}
				switch (key[num])
				{
				case '&':
					obj.modifiers |= Modifiers.Alt;
					num++;
					break;
				case '^':
					obj.modifiers |= Modifiers.Control;
					num++;
					break;
				case '%':
					obj.modifiers |= Modifiers.Command;
					num++;
					break;
				case '#':
					obj.modifiers |= Modifiers.Shift;
					num++;
					break;
				default:
					flag = false;
					break;
				}
			}
			while (flag);
			string text = key.Substring(num, key.Length - num).ToLower();
			string text2 = text;
			if (text2 == null)
			{
				goto IL_08c9;
			}
			text2 = string.IsInterned(text2);
			if ((object)text2 == "[0]")
			{
				obj.character = '0';
				obj.keyCode = KeyCode.Keypad0;
			}
			else if ((object)text2 == "[1]")
			{
				obj.character = '1';
				obj.keyCode = KeyCode.Keypad1;
			}
			else if ((object)text2 == "[2]")
			{
				obj.character = '2';
				obj.keyCode = KeyCode.Keypad2;
			}
			else if ((object)text2 == "[3]")
			{
				obj.character = '3';
				obj.keyCode = KeyCode.Keypad3;
			}
			else if ((object)text2 == "[4]")
			{
				obj.character = '4';
				obj.keyCode = KeyCode.Keypad4;
			}
			else if ((object)text2 == "[5]")
			{
				obj.character = '5';
				obj.keyCode = KeyCode.Keypad5;
			}
			else if ((object)text2 == "[6]")
			{
				obj.character = '6';
				obj.keyCode = KeyCode.Keypad6;
			}
			else if ((object)text2 == "[7]")
			{
				obj.character = '7';
				obj.keyCode = KeyCode.Keypad7;
			}
			else if ((object)text2 == "[8]")
			{
				obj.character = '8';
				obj.keyCode = KeyCode.Keypad8;
			}
			else if ((object)text2 == "[9]")
			{
				obj.character = '9';
				obj.keyCode = KeyCode.Keypad9;
			}
			else if ((object)text2 == "[.]")
			{
				obj.character = '.';
				obj.keyCode = KeyCode.KeypadPeriod;
			}
			else if ((object)text2 == "[/]")
			{
				obj.character = '/';
				obj.keyCode = KeyCode.KeypadDivide;
			}
			else if ((object)text2 == "[-]")
			{
				obj.character = '-';
				obj.keyCode = KeyCode.KeypadMinus;
			}
			else if ((object)text2 == "[+]")
			{
				obj.character = '+';
				obj.keyCode = KeyCode.KeypadPlus;
			}
			else if ((object)text2 == "[=]")
			{
				obj.character = '=';
				obj.keyCode = KeyCode.KeypadEquals;
			}
			else if ((object)text2 == "[equals]")
			{
				obj.character = '=';
				obj.keyCode = KeyCode.KeypadEquals;
			}
			else if ((object)text2 == "[enter]")
			{
				obj.character = '\n';
				obj.keyCode = KeyCode.KeypadEnter;
			}
			else if ((object)text2 == "up")
			{
				obj.keyCode = KeyCode.UpArrow;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "down")
			{
				obj.keyCode = KeyCode.DownArrow;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "left")
			{
				obj.keyCode = KeyCode.LeftArrow;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "right")
			{
				obj.keyCode = KeyCode.RightArrow;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "insert")
			{
				obj.keyCode = KeyCode.Insert;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "home")
			{
				obj.keyCode = KeyCode.Home;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "end")
			{
				obj.keyCode = KeyCode.End;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "pgup")
			{
				obj.keyCode = KeyCode.PageDown;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "page up")
			{
				obj.keyCode = KeyCode.PageUp;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "pgdown")
			{
				obj.keyCode = KeyCode.PageUp;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "page down")
			{
				obj.keyCode = KeyCode.PageDown;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "backspace")
			{
				obj.keyCode = KeyCode.Backspace;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "delete")
			{
				obj.keyCode = KeyCode.Delete;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "tab")
			{
				obj.keyCode = KeyCode.Tab;
			}
			else if ((object)text2 == "f1")
			{
				obj.keyCode = KeyCode.F1;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f2")
			{
				obj.keyCode = KeyCode.F2;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f3")
			{
				obj.keyCode = KeyCode.F3;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f4")
			{
				obj.keyCode = KeyCode.F4;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f5")
			{
				obj.keyCode = KeyCode.F5;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f6")
			{
				obj.keyCode = KeyCode.F6;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f7")
			{
				obj.keyCode = KeyCode.F7;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f8")
			{
				obj.keyCode = KeyCode.F8;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f9")
			{
				obj.keyCode = KeyCode.F9;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f10")
			{
				obj.keyCode = KeyCode.F10;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f11")
			{
				obj.keyCode = KeyCode.F11;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f12")
			{
				obj.keyCode = KeyCode.F12;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f13")
			{
				obj.keyCode = KeyCode.F13;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f14")
			{
				obj.keyCode = KeyCode.F14;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "f15")
			{
				obj.keyCode = KeyCode.F15;
				obj.modifiers |= Modifiers.FunctionKey;
			}
			else if ((object)text2 == "[esc]")
			{
				obj.keyCode = KeyCode.Escape;
			}
			else if ((object)text2 == "return")
			{
				obj.character = '\n';
				obj.keyCode = KeyCode.Return;
				obj.modifiers &= ~Modifiers.FunctionKey;
			}
			else
			{
				if ((object)text2 != "space")
				{
					goto IL_08c9;
				}
				obj.keyCode = KeyCode.Space;
				obj.character = ' ';
				obj.modifiers &= ~Modifiers.FunctionKey;
			}
			goto IL_0947;
			IL_0947:
			return obj;
			IL_08c9:
			if (text.Length != 1)
			{
				try
				{
					obj.keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), text, true);
				}
				catch (ArgumentException)
				{
					Debug.LogError(string.Format("Unable to find key name that matches '{0}'", text));
				}
			}
			else
			{
				obj.character = text.ToLower()[0];
				obj.keyCode = (KeyCode)obj.character;
				if (obj.modifiers != 0)
				{
					obj.character = '\0';
				}
			}
			goto IL_0947;
		}

		public override int GetHashCode()
		{
			int num = 1;
			if (isKey)
			{
				num = (ushort)keyCode;
			}
			if (isMouse)
			{
				num = mousePosition.GetHashCode();
			}
			return (num * 37) | (int)modifiers;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			Event obj2 = (Event)obj;
			if (type != obj2.type || modifiers != obj2.modifiers)
			{
				return false;
			}
			if (isKey)
			{
				return keyCode == obj2.keyCode && modifiers == obj2.modifiers;
			}
			if (isMouse)
			{
				return mousePosition == obj2.mousePosition;
			}
			return false;
		}

		public override string ToString()
		{
			if (isKey)
			{
				if (character == '\0')
				{
					return string.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", type, modifiers, keyCode);
				}
				return string.Format(string.Concat("Event:", type, "   Character:", (int)character, "   Modifiers:", modifiers, "   KeyCode:", keyCode));
			}
			if (isMouse)
			{
				return string.Format("Event: {0}   Position: {1} Modifiers: {2}", type, mousePosition, modifiers);
			}
			return "" + type;
		}
	}
}
