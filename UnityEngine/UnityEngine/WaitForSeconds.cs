namespace UnityEngine
{
	public class WaitForSeconds : YieldInstruction
	{
		internal float m_Seconds;

		public WaitForSeconds(float seconds)
		{
			m_Seconds = seconds;
		}
	}
}
