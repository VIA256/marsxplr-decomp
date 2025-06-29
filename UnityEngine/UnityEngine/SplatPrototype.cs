using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class SplatPrototype
	{
		private Texture2D m_Texture;

		//FUCKprivate Vector2 m_TileSize = m_TileSize;
		private Vector2 m_TileSize;

		public Texture2D texture
		{
			get
			{
				return m_Texture;
			}
			set
			{
				m_Texture = value;
			}
		}

		public Vector2 tileSize
		{
			get
			{
				return m_TileSize;
			}
			set
			{
				m_TileSize = value;
			}
		}
	}
}
