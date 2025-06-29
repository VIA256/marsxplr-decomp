using System;

namespace UnityEngine
{
	public struct Color
	{
		public float r;

		public float g;

		public float b;

		public float a;

		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		public static Color blue
		{
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		public static Color yellow
		{
			get
			{
				return new Color(1f, 47f / 51f, 0.015686275f, 1f);
			}
		}

		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		public static Color magenta
		{
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		public static Color gray
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		public static Color clear
		{
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		public float grayscale
		{
			get
			{
				return 0.299f * r + 0.587f * g + 0.114f * b;
			}
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return r;
				case 1:
					return g;
				case 2:
					return b;
				case 3:
					return a;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					r = value;
					break;
				case 1:
					g = value;
					break;
				case 2:
					b = value;
					break;
				case 3:
					a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			a = 1f;
		}

		public override string ToString()
		{
			return string.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", r, g, b, a);
		}

		public override int GetHashCode()
		{
			return ((Vector4)this).GetHashCode();
		}

		public override bool Equals(object other)
		{
			if (!(other is Color))
			{
				return false;
			}
			Color color = (Color)other;
			return r.Equals(color.r) && g.Equals(color.g) && b.Equals(color.b) && a.Equals(color.a);
		}

		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		public static bool operator ==(Color lhs, Color rhs)
		{
			return (Vector4)lhs == (Vector4)rhs;
		}

		public static bool operator !=(Color lhs, Color rhs)
		{
			return (Vector4)lhs != (Vector4)rhs;
		}

		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}
	}
}
