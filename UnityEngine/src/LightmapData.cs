using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class LightmapData
	{
		private Texture2D m_Lightmap;

		public Texture2D lightmap
		{
			get
			{
				return m_Lightmap;
			}
			set
			{
				m_Lightmap = value;
			}
		}
	}
}
