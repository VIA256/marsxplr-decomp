using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct Mathf
	{
		public const float PI = (float)Math.PI;

		public const float Infinity = float.PositiveInfinity;

		public const float NegativeInfinity = float.NegativeInfinity;

		public const float Deg2Rad = (float)Math.PI / 180f;

		public const float Rad2Deg = 57.29578f;

		public const float Epsilon = float.Epsilon;

		private byte _0024PRIVATE_0024;

		public static float Sin(float f)
		{
			return (float)Math.Sin(f);
		}

		public static float Cos(float f)
		{
			return (float)Math.Cos(f);
		}

		public static float Tan(float f)
		{
			return (float)Math.Tan(f);
		}

		public static float Asin(float f)
		{
			return (float)Math.Asin(f);
		}

		public static float Acos(float f)
		{
			return (float)Math.Acos(f);
		}

		public static float Atan(float f)
		{
			return (float)Math.Atan(f);
		}

		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2(y, x);
		}

		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt(f);
		}

		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		public static float Min(float a, float b)
		{
			return (!(a < b)) ? b : a;
		}

		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		public static float Max(float a, float b)
		{
			return (!(a > b)) ? b : a;
		}

		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		public static float Pow(float f, float p)
		{
			return (float)Math.Pow(f, p);
		}

		public static float Exp(float power)
		{
			return (float)Math.Exp(power);
		}

		public static float Log(float f, float p)
		{
			return (float)Math.Log(f, p);
		}

		public static float Log(float f)
		{
			return (float)Math.Log(f);
		}

		public static float Log10(float f)
		{
			return (float)Math.Log10(f);
		}

		public static float Ceil(float f)
		{
			return (float)Math.Ceiling(f);
		}

		public static float Floor(float f)
		{
			return (float)Math.Floor(f);
		}

		public static float Round(float f)
		{
			return (float)Math.Round(f);
		}

		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling(f);
		}

		public static int FloorToInt(float f)
		{
			return (int)Math.Floor(f);
		}

		public static int RoundToInt(float f)
		{
			return (int)Math.Round(f);
		}

		public static float Sign(float f)
		{
			return (!(f >= 0f)) ? (-1f) : 1f;
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		public static float Clamp01(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Clamp01(t);
		}

		public static float LerpAngle(float a, float b, float t)
		{
			float num = Repeat(b - a, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return a + num * Clamp01(t);
		}

		public static float SmoothStep(float from, float to, float t)
		{
			t = Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = false;
			if (value < 0f)
			{
				flag = true;
			}
			float num = Abs(value);
			if (num > absmax)
			{
				return (!flag) ? num : (0f - num);
			}
			float num2 = Pow(num / absmax, gamma) * absmax;
			return (!flag) ? num2 : (0f - num2);
		}

		public static bool Approximately(float a, float b)
		{
			return Abs(a - b) < float.Epsilon;
		}

		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			smoothTime = Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float value = current - target;
			float num4 = target;
			float num5 = maxSpeed * smoothTime;
			value = Clamp(value, 0f - num5, num5);
			target = current - value;
			float num6 = (currentVelocity + num * value) * deltaTime;
			currentVelocity = (currentVelocity - num * num6) * num3;
			float num7 = target + (value + num6) * num3;
			if (num4 - current > 0f == num7 > num4)
			{
				num7 = num4;
				currentVelocity = (num7 - num4) / deltaTime;
			}
			return num7;
		}

		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			target = current + DeltaAngle(current, target);
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float Repeat(float t, float length)
		{
			return t - Floor(t / length) * length;
		}

		public static float PingPong(float t, float length)
		{
			t = Repeat(t, length * 2f);
			return length - Abs(t - length);
		}

		public static float InverseLerp(float from, float to, float value)
		{
			if (from < to)
			{
				if (value < from)
				{
					return 0f;
				}
				if (value > to)
				{
					return 1f;
				}
				value -= from;
				value /= to - from;
				return value;
			}
			if (from > to)
			{
				if (value < to)
				{
					return 1f;
				}
				if (value > from)
				{
					return 0f;
				}
				return 1f - (value - to) / (from - to);
			}
			return 0f;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ClosestPowerOfTwo(int value);

		public static float DeltaAngle(float current, float target)
		{
			float num = Repeat(target - current, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);
	}
}
