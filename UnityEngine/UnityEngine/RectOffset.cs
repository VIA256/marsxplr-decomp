using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RectOffset
	{
		[SerializeField]
		internal int m_Left;

		[SerializeField]
		internal int m_Right;

		[SerializeField]
		internal int m_Top;

		[SerializeField]
		internal int m_Bottom;

		[NonSerialized]
		internal GUIStyle owner;

		public int left
		{
			get
			{
				return m_Left;
			}
			set
			{
				m_Left = value;
				if (owner != null)
				{
					owner.Apply();
				}
			}
		}

		public int right
		{
			get
			{
				return m_Right;
			}
			set
			{
				m_Right = value;
				if (owner != null)
				{
					owner.Apply();
				}
			}
		}

		public int top
		{
			get
			{
				return m_Top;
			}
			set
			{
				m_Top = value;
				if (owner != null)
				{
					owner.Apply();
				}
			}
		}

		public int bottom
		{
			get
			{
				return m_Bottom;
			}
			set
			{
				m_Bottom = value;
				if (owner != null)
				{
					owner.Apply();
				}
			}
		}

		public int horizontal
		{
			get
			{
				return m_Left + m_Right;
			}
		}

		public int vertical
		{
			get
			{
				return m_Top + m_Bottom;
			}
		}

		public Rect Add(Rect rect)
		{
			return new Rect(rect.x - (float)m_Left, rect.y - (float)top, rect.width + (float)m_Left + (float)m_Right, rect.height + (float)m_Top + (float)m_Bottom);
		}

		public Rect Remove(Rect rect)
		{
			return new Rect(rect.x + (float)m_Left, rect.y + (float)m_Top, rect.width - (float)m_Left - (float)m_Right, rect.height - (float)m_Top - (float)m_Bottom);
		}

		internal void CopyFrom(RectOffset other)
		{
			m_Left = other.m_Left;
			m_Right = other.m_Right;
			m_Top = other.m_Top;
			m_Bottom = other.m_Bottom;
			if (owner != null)
			{
				owner.Apply();
			}
		}

		public override string ToString()
		{
			return string.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", m_Left, m_Right, m_Top, m_Bottom);
		}
	}
}
