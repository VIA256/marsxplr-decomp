using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class RamoSphere : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class colorSet_0024107 : GenericGenerator<WaitForFixedUpdate>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForFixedUpdate>, IEnumerator
		{
			internal int _0024___temp381_0024554;

			internal Color _0024___temp382_0024555;

			internal bool _0024r556;

			internal RamoSphere _0024self_557;

			public _0024(bool r, RamoSphere self_)
			{
				_0024r556 = r;
				_0024self_557 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					return Yield(2, new WaitForFixedUpdate());
				case 2:
				{
					if (_0024r556)
					{
						_0024self_557.shield.renderer.material.color = _0024self_557.ramColor;
					}
					else
					{
						_0024self_557.shield.renderer.material.color = _0024self_557.tagColor;
					}
					int num = (_0024___temp381_0024554 = 0);
					Color color = (_0024___temp382_0024555 = _0024self_557.shield.renderer.material.color);
					float num2 = (_0024___temp382_0024555.a = _0024___temp381_0024554);
					Color color2 = (_0024self_557.shield.renderer.material.color = _0024___temp382_0024555);
					_0024self_557.ram = _0024r556;
					Yield(1, null);
					break;
				}
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal bool _0024r558;

		internal RamoSphere _0024self_559;

		public colorSet_0024107(bool r, RamoSphere self_)
		{
			_0024r558 = r;
			_0024self_559 = self_;
		}

		public override IEnumerator<WaitForFixedUpdate> GetEnumerator()
		{
			return new _0024(_0024r558, _0024self_559);
		}
	}

	public Transform shield;

	private float offset;

	public Color tagColor;

	public Color ramColor;

	public bool ram;

	public Vehicle vehicle;

	public RamoSphere()
	{
		ram = false;
	}

	public void Start()
	{
		shield.renderer.material.color = tagColor;
		vehicle = (Vehicle)transform.root.gameObject.GetComponent(typeof(Vehicle));
	}

	public void Update()
	{
		offset += Time.deltaTime * 0.1f;
		if (offset > 1f)
		{
			offset -= Mathf.Floor(offset);
		}
		shield.renderer.material.SetFloat("_Offset", offset);
		float a = Mathf.Lerp(shield.renderer.material.color.a, (!ram) ? tagColor.a : ramColor.a, Time.deltaTime * 3f);
		Color color = shield.renderer.material.color;
		float num = (color.a = a);
		Color color2 = (shield.renderer.material.color = color);
		shield.rotation = Quaternion.identity;
	}

	public IEnumerator colorSet(bool r)
	{
		return new colorSet_0024107(r, this).GetEnumerator();
	}

	public void OnCollisionEnter(Collision collision)
	{
		float num = collision.relativeVelocity.magnitude * Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity.normalized));
		if (num > 3f)
		{
			float a = ((!ram) ? tagColor.a : ramColor.a) + num * 0.1f;
			Color color = shield.renderer.material.color;
			float num2 = (color.a = a);
			Color color2 = (shield.renderer.material.color = color);
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer != 14)
		{
			if (other.name == "ORB(Clone)" && shield.renderer.material.color.a < 3f)
			{
				int num = 3;
				Color color = shield.renderer.material.color;
				float num2 = (color.a = num);
				Color color2 = (shield.renderer.material.color = color);
			}
			if (vehicle.networkView.isMine && (bool)other.attachedRigidbody)
			{
				vehicle.OnRam(other.attachedRigidbody.gameObject);
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != 14 && other.name == "ORB(Clone)")
		{
			int num = 10;
			Color color = shield.renderer.material.color;
			float num2 = (color.a = num);
			Color color2 = (shield.renderer.material.color = color);
		}
	}

	public void OnLaserHit()
	{
		int num = 5;
		Color color = shield.renderer.material.color;
		float num2 = (color.a = num);
		Color color2 = (shield.renderer.material.color = color);
	}

	public void Main()
	{
	}
}
