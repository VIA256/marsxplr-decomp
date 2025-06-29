using System;
using System.Collections;
using System.Runtime.InteropServices;
using Boo.Lang;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class Collision
	{
		private Vector3 m_RelativeVelocity;

		private Rigidbody m_Rigidbody;

		private Collider m_Collider;

		private ContactPoint[] m_Contacts;

		public Vector3 relativeVelocity
		{
			get
			{
				return m_RelativeVelocity;
			}
		}

		public Rigidbody rigidbody
		{
			get
			{
				return m_Rigidbody;
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

		public Transform transform
		{
			get
			{
				return (!(rigidbody != null)) ? collider.transform : rigidbody.transform;
			}
		}

		[DuckTyped]
		public GameObject gameObject
		{
			get
			{
				return (!(m_Rigidbody != null)) ? m_Collider.gameObject : m_Rigidbody.gameObject;
			}
		}

		public ContactPoint[] contacts
		{
			get
			{
				return m_Contacts;
			}
		}

		public Vector3 impactForceSum
		{
			get
			{
				return relativeVelocity;
			}
		}

		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead")]
		[DuckTyped]
		public Component other
		{
			get
			{
				return (!(m_Rigidbody != null)) ? ((Component)m_Collider) : ((Component)m_Rigidbody);
			}
		}

		public virtual IEnumerator GetEnumerator()
		{
			return contacts.GetEnumerator();
		}
	}
}
