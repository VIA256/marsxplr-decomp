using System;
using System.Collections;
using System.Runtime.Serialization;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	[Serializable]
	[EnumeratorItemType(typeof(DictionaryEntry))]
	public class Hash : Hashtable
	{
		public Hash()
			: base(BooHashCodeProvider.Default)
		{
		}

		public Hash(IDictionary other)
			: this()
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (DictionaryEntry item in other)
			{
				Add(item.Key, item.Value);
			}
		}

		public Hash(IEnumerable enumerable)
			: this()
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			foreach (Array item in enumerable)
			{
				Add(item.GetValue(0), item.GetValue(1));
			}
		}

		public Hash(bool caseInsensitive)
			: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		public Hash(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public override object Clone()
		{
			return new Hash(this);
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (GetType() != obj.GetType())
			{
				return false;
			}
			Hash hash = (Hash)obj;
			if (Count != hash.Count)
			{
				return false;
			}
			foreach (DictionaryEntry item in hash)
			{
				if (!ContainsKey(item.Key))
				{
					return false;
				}
				if (!RuntimeServices.EqualityOperator(item.Value, this[item.Key]))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = 0;
			IDictionaryEnumerator dictionaryEnumerator = GetEnumerator();
			try
			{
				while (dictionaryEnumerator.MoveNext())
				{
					object current = dictionaryEnumerator.Current;
					num ^= GetHash(current);
				}
				return num;
			}
			finally
			{
				IDisposable disposable = dictionaryEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
