using System;

namespace UnityEngine
{
	public struct Rect
	{
		private float m_XMin;

		private float m_YMin;

		private float m_Width;

		private float m_Height;

		public float x
		{
			get
			{
				return m_XMin;
			}
			set
			{
				m_XMin = value;
			}
		}

		public float y
		{
			get
			{
				return m_YMin;
			}
			set
			{
				m_YMin = value;
			}
		}

		public float width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		public float height
		{
			get
			{
				return m_Height;
			}
			set
			{
				m_Height = value;
			}
		}

		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return m_XMin;
			}
		}

		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return m_XMin + m_Width;
			}
		}

		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return m_YMin;
			}
		}

		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return m_YMin + m_Height;
			}
		}

		public float xMin
		{
			get
			{
				return m_XMin;
			}
			set
			{
				float num = xMax;
				m_XMin = value;
				m_Width = num - m_XMin;
			}
		}

		public float yMin
		{
			get
			{
				return m_YMin;
			}
			set
			{
				float num = yMax;
				m_YMin = value;
				m_Height = num - m_YMin;
			}
		}

		public float xMax
		{
			get
			{
				return m_Width + m_XMin;
			}
			set
			{
				m_Width = value - m_XMin;
			}
		}

		public float yMax
		{
			get
			{
				return m_Height + m_YMin;
			}
			set
			{
				m_Height = value - m_YMin;
			}
		}

		public Rect(float left, float top, float width, float height)
		{
			m_XMin = left;
			m_YMin = top;
			m_Width = width;
			m_Height = height;
		}

		public Rect(Rect source)
		{
			m_XMin = source.m_XMin;
			m_YMin = source.m_YMin;
			m_Width = source.m_Width;
			m_Height = source.m_Height;
		}

		public static Rect MinMaxRect(float left, float top, float right, float bottom)
		{
			return new Rect(left, top, right - left, bottom - top);
		}

		public override string ToString()
		{
			return string.Format("(left:{0:F2}, top:{1:F2}, width:{2:F2}, height:{3:F2})", x, y, width, height);
		}

		public bool Contains(Vector2 point)
		{
			return point.x >= xMin && point.x < xMax && point.y >= yMin && point.y < yMax;
		}

		public bool Contains(Vector3 point)
		{
			return point.x >= xMin && point.x < xMax && point.y >= yMin && point.y < yMax;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (width.GetHashCode() << 2) ^ (y.GetHashCode() >> 2) ^ (height.GetHashCode() >> 1);
		}

		public override bool Equals(object other)
		{
			if (!(other is Rect))
			{
				return false;
			}
			Rect rect = (Rect)other;
			return x.Equals(rect.x) && y.Equals(rect.y) && width.Equals(rect.width) && height.Equals(rect.height);
		}

		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
		}

		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}
	}
}
