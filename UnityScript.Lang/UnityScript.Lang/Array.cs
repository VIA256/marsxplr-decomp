using System;
using System.Collections;
using Boo.Lang;
using Boo.Lang.Runtime;

namespace UnityScript.Lang
{
	[Serializable]
	public class Array : CollectionBase, ICoercible
	{
		public int length
		{
			get
			{
				return Count;
			}
			set
			{
				EnsureCapacity(value);
				if (value < Count)
				{
					InnerList.RemoveRange(value, checked(InnerList.Count - value));
				}
			}
		}

		public object this[int index]
		{
			get
			{
				return InnerList[index];
			}
			set
			{
				EnsureCapacity(checked(index + 1));
				InnerList[index] = value;
			}
		}

		public Array()
		{
		}

		public Array(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentException("Expected: (capacity >= 0)", "capacity");
			}
			EnsureCapacity(capacity);
		}

		public Array(IEnumerable collection)
		{
			if (collection is string)
			{
				Add(collection);
			}
			else
			{
				AddRange(collection);
			}
		}

		public Array(params object[] items)
		{
			if (items.Length == 1 && items[0] is IEnumerable)
			{
				AddRange((IEnumerable)items[0]);
			}
			else
			{
				AddRange(items);
			}
		}

		//FUCK
		/*
		public static implicit operator Array(IEnumerable e)
		{
			return new Array(e);
		}
		 */
		public static implicit operator Array(Enum e)
		{
			return new Array(e);
		}

		public static implicit operator Array(System.Array a)
		{
			return new Array(a as IEnumerable);
		}

		public void clear()
		{
			Clear();
		}

		public virtual object Coerce(Type toType)
		{
			if (toType.IsArray)
			{
				return ToBuiltin(toType.GetElementType());
			}
			return this;
		}

		public System.Array ToBuiltin(Type type)
		{
			return InnerList.ToArray(type);
		}

		public void Add(object value, params object[] items)
		{
			AddImpl(value, items);
		}

		public void Add(object value)
		{
			InnerList.Add(value);
		}

		public int Push(object value, params object[] items)
		{
			return AddImpl(value, items);
		}

		public int push(object value, params object[] items)
		{
			return AddImpl(value, items);
		}

		public int push(object value)
		{
			InnerList.Add(value);
			return InnerList.Count;
		}

		public int Push(object value)
		{
			return push(value);
		}

		public int Unshift(object value, params object[] items)
		{
			return UnshiftImpl(value, items);
		}

		public int unshift(object value, params object[] items)
		{
			return UnshiftImpl(value, items);
		}

		public void Splice(int index, int howmany, params object[] items)
		{
			SpliceImpl(index, howmany, items);
		}

		public void splice(int index, int howmany, params object[] items)
		{
			SpliceImpl(index, howmany, items);
		}

		public Array Concat(ICollection value, params object[] items)
		{
			return ConcatImpl(value, items);
		}

		public Array concat(ICollection value, params object[] items)
		{
			return ConcatImpl(value, items);
		}

		public object Pop()
		{
			int index = checked(InnerList.Count - 1);
			object result = InnerList[index];
			InnerList.RemoveAt(index);
			return result;
		}

		public object pop()
		{
			return Pop();
		}

		public object Shift()
		{
			int index = 0;
			object result = InnerList[index];
			InnerList.RemoveAt(0);
			return result;
		}

		public object shift()
		{
			return Shift();
		}

		public override string ToString()
		{
			return Join(",");
		}

		public string toString()
		{
			return ToString();
		}

		public Array Slice(int start, int end)
		{
			return new Array(InnerList.GetRange(start, checked(end - start)));
		}

		public Array Slice(int start)
		{
			return Slice(start, checked(InnerList.Count - start));
		}

		public Array slice(int start, int end)
		{
			return Slice(start, end);
		}

		public Array slice(int start)
		{
			return Slice(start);
		}

		public Array Reverse()
		{
			InnerList.Reverse();
			return this;
		}

		public Array reverse()
		{
			Reverse();
			return this;
		}

		public Array Sort()
		{
			InnerList.Sort();
			return this;
		}

		public Array sort()
		{
			Sort();
			return this;
		}

		public string Join(string seperator)
		{
			return Builtins.join(InnerList, seperator);
		}

		public string join(string seperator)
		{
			return Builtins.join(InnerList, seperator);
		}

		public void Remove(object obj)
		{
			InnerList.Remove(obj);
		}

		public void remove(object obj)
		{
			Remove(obj);
		}

		public void AddRange(IEnumerable collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (object item in collection)
			{
				InnerList.Add(item);
			}
		}

		private int AddImpl(object value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			InnerList.Add(value);
			foreach (object item in items)
			{
				InnerList.Add(item);
			}
			return InnerList.Count;
		}

		private int UnshiftImpl(object value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			InnerList.InsertRange(0, (ICollection)items);
			InnerList.Insert(0, value);
			return InnerList.Count;
		}

		private void SpliceImpl(int index, int howmany, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (howmany != 0)
			{
				InnerList.RemoveRange(index, howmany);
			}
			InnerList.InsertRange(index, (ICollection)items);
		}

		private Array ConcatImpl(ICollection value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			Array array = new Array(InnerList);
			array.InnerList.AddRange(value);
			foreach (ICollection item in items)
			{
				array.InnerList.AddRange(item);
			}
			return array;
		}

		private void EnsureCapacity(int capacity)
		{
			if (capacity >= Count)
			{
				int num = 0;
				int num2 = checked(capacity - Count);
				if (num2 < 0)
				{
					throw new ArgumentOutOfRangeException("max");
				}
				while (num < num2)
				{
					int num3 = num;
					num++;
					InnerList.Add(null);
				}
			}
		}
	}
}
