namespace UnityEngine
{
	public struct Particle
	{
		private Vector3 m_Position;

		private Vector3 m_Velocity;

		private float m_Size;

		private float m_Energy;

		private Color m_Color;

		public Vector3 position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public Vector3 velocity
		{
			get
			{
				return m_Velocity;
			}
			set
			{
				m_Velocity = value;
			}
		}

		public float energy
		{
			get
			{
				return m_Energy;
			}
			set
			{
				m_Energy = value;
			}
		}

		public float size
		{
			get
			{
				return m_Size;
			}
			set
			{
				m_Size = value;
			}
		}

		public Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}
	}
}
