using Boo.Lang;

namespace UnityEngine
{
	public struct ContactPoint
	{
		private Vector3 m_Point;

		private Vector3 m_Normal;

		private Collider m_ThisCollider;

		private Collider m_OtherCollider;

		public Vector3 point
		{
			get
			{
				return m_Point;
			}
		}

		public Vector3 normal
		{
			get
			{
				return m_Normal;
			}
		}

		[DuckTyped]
		public Collider thisCollider
		{
			get
			{
				return m_ThisCollider;
			}
		}

		[DuckTyped]
		public Collider otherCollider
		{
			get
			{
				return m_OtherCollider;
			}
		}
	}
}
