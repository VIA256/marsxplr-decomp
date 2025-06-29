using System;

namespace UnityEngine
{
	public struct Vector2
	{
		public const float kEpsilon = 1E-05f;

		public float x;

		public float y;

		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return x;
				case 1:
					return y;
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(x * x + y * y);
			}
		}

		public float sqrMagnitude
		{
			get
			{
				return x * x + y * y;
			}
		}

		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		public static Vector2 right
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1})", x, y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return x.Equals(vector.x) && y.Equals(vector.y);
		}

		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		public static float Distance(Vector2 a, Vector2 b)
		{
			return (a - b).magnitude;
		}

		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		public float SqrMagnitude()
		{
			return x * x + y * y;
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(0f - a.x, 0f - a.y);
		}

		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}
	}
}
