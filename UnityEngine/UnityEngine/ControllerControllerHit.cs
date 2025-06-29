namespace UnityEngine
{
	public class ControllerControllerHit
	{
		private CharacterController m_Controller;

		private CharacterController m_Other;

		private bool m_Push;

		public CharacterController controller
		{
			get
			{
				return m_Controller;
			}
		}

		public CharacterController other
		{
			get
			{
				return m_Other;
			}
		}

		private bool push
		{
			get
			{
				return m_Push;
			}
			set
			{
				m_Push = value;
			}
		}
	}
}
