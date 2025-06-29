namespace UnityEngine
{
	public struct NetworkMessageInfo
	{
		private double m_TimeStamp;

		private NetworkPlayer m_Sender;

		private NetworkViewID m_ViewID;

		public double timestamp
		{
			get
			{
				return m_TimeStamp;
			}
		}

		public NetworkPlayer sender
		{
			get
			{
				return m_Sender;
			}
		}

		public NetworkView networkView
		{
			get
			{
				return NetworkView.Find(m_ViewID);
			}
		}
	}
}
