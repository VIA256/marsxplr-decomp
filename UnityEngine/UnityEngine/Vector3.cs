using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct Vector3
	{
		public const float kEpsilon = 1E-05f;

		public float x;

		public float y;

		public float z;

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
				case 2:
					return z;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		public Vector3 normalized
		{
			get
			{
				return Normalize(this);
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(x * x + y * y + z * z);
			}
		}

		public float sqrMagnitude
		{
			get
			{
				return x * x + y * y + z * z;
			}
		}

		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}

		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}

		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}

		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			z = 0f;
		}

		public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Vector3 Slerp(Vector3 from, Vector3 to, float t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Internal_OrthoNormalize2(ref normal, ref tangent);
		}

		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Internal_OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Vector3 RotateTowards(Vector3 from, Vector3 to, float maxRadiansDelta, float maxMagnitudeDelta);

		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public void Scale(Vector3 scale)
		{
			x *= scale.x;
			y *= scale.y;
			z *= scale.z;
		}

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return x.Equals(vector.x) && y.Equals(vector.y) && z.Equals(vector.z);
		}

		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		public static Vector3 Normalize(Vector3 value)
		{
			float num = Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return zero;
		}

		public void Normalize()
		{
			float num = Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = zero;
			}
		}

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
		}

		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Dot(onNormal, onNormal);
			if (num < float.Epsilon)
			{
				return zero;
			}
			return onNormal * Dot(vector, onNormal) / num;
		}

		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return fromThat - Project(fromThat, excludeThis);
		}

		public static float Angle(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f));
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(0f - a.x, 0f - a.y, 0f - a.z);
		}

		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}
	}
}
