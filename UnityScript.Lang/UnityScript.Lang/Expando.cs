using System;
using System.Collections.Generic;

namespace UnityScript.Lang
{
	[Serializable]
	public class Expando
	{
		protected WeakReference _target;

		protected Dictionary<object, object> _attributes;

		public object Target
		{
			get
			{
				return _target.Target;
			}
		}

		public object this[object key]
		{
			get
			{
				object value = null;
				_attributes.TryGetValue(key, out value);
				return value;
			}
			set
			{
				if (value == null)
				{
					_attributes.Remove(key);
				}
				else
				{
					_attributes[key] = value;
				}
			}
		}

		public Expando(object target)
		{
			_attributes = new Dictionary<object, object>();
			_target = new WeakReference(target);
		}
	}
}
