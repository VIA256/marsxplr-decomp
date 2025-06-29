namespace UnityEngine
{
	public struct SoftJointLimit
	{
		private float m_Limit;

		private float m_Bouncyness;

		private float m_Spring;

		private float m_Damper;

		public float limit
		{
			get
			{
				return m_Limit;
			}
			set
			{
				m_Limit = value;
			}
		}

		public float spring
		{
			get
			{
				return m_Spring;
			}
			set
			{
				m_Spring = value;
			}
		}

		public float damper
		{
			get
			{
				return m_Damper;
			}
			set
			{
				m_Damper = value;
			}
		}

		public float bouncyness
		{
			get
			{
				return m_Bouncyness;
			}
			set
			{
				m_Bouncyness = value;
			}
		}
	}
}
