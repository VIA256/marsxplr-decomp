using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct Quaternion
	{
		public const float kEpsilon = 1E-06f;

		public float x;

		public float y;

		public float z;

		public float w;

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
				case 3:
					return w;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
				case 3:
					w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		public Vector3 eulerAngles
		{
			get
			{
				return Internal_ToEulerRad(this) * 57.29578f;
			}
			set
			{
				this = Internal_FromEulerRad(value * ((float)Math.PI / 180f));
			}
		}

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion AngleAxis(float angle, Vector3 axis);

		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection);

		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = FromToRotation(fromDirection, toDirection);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion LookRotation(Vector3 forward, Vector3 upwards);

		public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			return LookRotation(forward, up);
		}

		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			SetLookRotation(view, up);
		}

		public void SetLookRotation(Vector3 view, Vector3 up)
		{
			this = LookRotation(view, up);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion Slerp(Quaternion from, Quaternion to, float t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion Lerp(Quaternion a, Quaternion b, float t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Quaternion Inverse(Quaternion rotation);

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", x, y, z, w);
		}

		public static float Angle(Quaternion a, Quaternion b)
		{
			float f = Dot(a, b);
			return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * 57.29578f;
		}

		public static Quaternion Euler(float x, float y, float z)
		{
			Vector3 vector = new Vector3(x, y, z);
			return Internal_FromEulerRad(vector * ((float)Math.PI / 180f));
		}

		public static Quaternion Euler(Vector3 euler)
		{
			return Internal_FromEulerRad(euler * ((float)Math.PI / 180f));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 Internal_ToEulerRad(Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion Internal_FromEulerRad(Vector3 euler);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle);

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			Vector3 euler = new Vector3(x, y, z);
			return Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public void SetEulerRotation(float x, float y, float z)
		{
			Vector3 euler = new Vector3(x, y, z);
			this = Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instad of degrees")]
		public Vector3 ToEuler()
		{
			return Internal_ToEulerRad(this);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			Vector3 euler = new Vector3(x, y, z);
			return Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instad of degrees")]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public void SetEulerAngles(float x, float y, float z)
		{
			Vector3 eulerRotation = new Vector3(x, y, z);
			SetEulerRotation(eulerRotation);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instad of degrees")]
		public void SetEulerAngles(Vector3 euler)
		{
			this = EulerRotation(euler);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instad of degrees")]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Internal_ToEulerRad(rotation);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instad of degrees")]
		public Vector3 ToEulerAngles()
		{
			return Internal_ToEulerRad(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instad of degrees")]
		public static extern Quaternion AxisAngle(Vector3 axis, float angle);

		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instad of degrees")]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = AxisAngle(axis, angle);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
		}

		public override bool Equals(object other)
		{
			if (!(other is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)other;
			return x.Equals(quaternion.x) && y.Equals(quaternion.y) && z.Equals(quaternion.z) && w.Equals(quaternion.w);
		}

		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result = default(Vector3);
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Dot(lhs, rhs) > 0.999999f;
		}

		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return Dot(lhs, rhs) <= 0.999999f;
		}
	}
}
