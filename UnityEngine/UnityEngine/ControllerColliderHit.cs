using Boo.Lang;

namespace UnityEngine
{
	public class ControllerColliderHit
	{
		private CharacterController m_Controller;

		private Collider m_Collider;

		private Vector3 m_Point;

		private Vector3 m_Normal;

		private Vector3 m_MoveDirection;

		private float m_MoveLength;

		private bool m_Push;

		public CharacterController controller
		{
			get
			{
				return m_Controller;
			}
		}

		[DuckTyped]
		public Collider collider
		{
			get
			{
				return m_Collider;
			}
		}

		public Rigidbody rigidbody
		{
			get
			{
				return m_Collider.attachedRigidbody;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return m_Collider.gameObject;
			}
		}

		public Transform transform
		{
			get
			{
				return m_Collider.transform;
			}
		}

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

		public Vector3 moveDirection
		{
			get
			{
				return m_MoveDirection;
			}
		}

		public float moveLength
		{
			get
			{
				return m_MoveLength;
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
