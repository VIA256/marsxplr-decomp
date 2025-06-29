namespace UnityEngine
{
	public struct Color32
	{
		public byte red;

		public byte green;

		public byte blue;

		public byte alpha;

		private Color32(byte r, byte g, byte b, byte a)
		{
			red = r;
			green = g;
			blue = b;
			alpha = a;
		}

		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)(Mathf.Clamp01(c.r) * 255f), (byte)(Mathf.Clamp01(c.g) * 255f), (byte)(Mathf.Clamp01(c.b) * 255f), (byte)(Mathf.Clamp01(c.a) * 255f));
		}

		public static implicit operator Color(Color32 v)
		{
			return new Color((float)(int)v.red / 255f, (float)(int)v.green / 255f, (float)(int)v.blue / 255f, (float)(int)v.alpha / 255f);
		}
	}
}
