using System;
using System.Collections;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	[Serializable]
	public class List : IList, ICollection, IEnumerable
	{
		private class ComparerImpl : IComparer
		{
			private Comparer _comparer;

			public ComparerImpl(Comparer comparer)
			{
				_comparer = comparer;
			}

			public int Compare(object lhs, object rhs)
			{
				return _comparer(lhs, rhs);
			}
		}

		private class ReversedListEnumerator : IEnumerator, IEnumerable
		{
			private object[] _items;

			private int _index;

			private int _count;

			public object Current
			{
				get
				{
					return _items[_index];
				}
			}

			public ReversedListEnumerator(object[] items, int count)
			{
				_items = items;
				_index = count;
				_count = count;
			}

			public void Reset()
			{
				_index = _count;
			}

			public bool MoveNext()
			{
				return --_index >= 0;
			}

			public IEnumerator GetEnumerator()
			{
				return this;
			}
		}

		private class ListEnumerator : IEnumerator
		{
			private List _list;

			private object[] _items;

			private int _count;

			private int _index;

			private object _current;

			public object Current
			{
				get
				{
					return _current;
				}
			}

			public ListEnumerator(List list)
			{
				_list = list;
				_items = list._items;
				_count = list._count;
				_index = 0;
			}

			public void Reset()
			{
				_index = 0;
			}

			public bool MoveNext()
			{
				if (_count != _list._count || _items != _list._items)
				{
					throw new InvalidOperationException(ResourceManager.GetString("ListWasModified"));
				}
				if (_index < _count)
				{
					_current = _items[_index];
					_index++;
					return true;
				}
				return false;
			}
		}

		private const int DefaultCapacity = 16;

		protected object[] _items;

		protected int _count;

		public int Count
		{
			get
			{
				return _count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public object SyncRoot
		{
			get
			{
				return _items;
			}
		}

		public object this[int index]
		{
			get
			{
				return _items[CheckIndex(NormalizeIndex(index))];
			}
			set
			{
				_items[CheckIndex(NormalizeIndex(index))] = value;
			}
		}

		public IEnumerable Reversed
		{
			get
			{
				return new ReversedListEnumerator(_items, _count);
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public List()
			: this(16)
		{
		}

		public List(IEnumerable enumerable)
			: this()
		{
			Extend(enumerable);
		}

		public List(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity");
			}
			_items = new object[initialCapacity];
			_count = 0;
		}

		public List(object[] items, bool takeOwnership)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (takeOwnership)
			{
				_items = items;
			}
			else
			{
				_items = (object[])items.Clone();
			}
			_count = items.Length;
		}

		public static List operator *(List lhs, int count)
		{
			return lhs.Multiply(count);
		}

		public static List operator *(int count, List rhs)
		{
			return rhs.Multiply(count);
		}

		public static List operator +(List lhs, IEnumerable rhs)
		{
			List list = new List(lhs.Count);
			list.Extend(lhs);
			list.Extend(rhs);
			return list;
		}

		public static string operator %(string format, List rhs)
		{
			return string.Format(format, rhs.ToArray());
		}

		public List Multiply(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			object[] array = new object[_count * count];
			for (int i = 0; i < count; i++)
			{
				Array.Copy(_items, 0, array, i * _count, _count);
			}
			return new List(array, true);
		}

		public IEnumerator GetEnumerator()
		{
			return new ListEnumerator(this);
		}

		public void CopyTo(Array target, int index)
		{
			Array.Copy(_items, 0, target, index, _count);
		}

		public List Push(object item)
		{
			return Add(item);
		}

		public List Add(object item)
		{
			EnsureCapacity(_count + 1);
			_items[_count] = item;
			_count++;
			return this;
		}

		public List AddUnique(object item)
		{
			if (!Contains(item))
			{
				Add(item);
			}
			return this;
		}

		public List Extend(IEnumerable enumerable)
		{
			foreach (object item in enumerable)
			{
				Add(item);
			}
			return this;
		}

		public List ExtendUnique(IEnumerable enumerable)
		{
			foreach (object item in enumerable)
			{
				AddUnique(item);
			}
			return this;
		}

		public List Collect(Predicate condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			List list = new List();
			InnerCollect(list, condition);
			return list;
		}

		public List Collect(List target, Predicate condition)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			InnerCollect(target, condition);
			return target;
		}

		public Array ToArray(Type targetType)
		{
			Array array = Array.CreateInstance(targetType, _count);
			CopyTo(array, 0);
			return array;
		}

		public object[] ToArray(object[] array)
		{
			CopyTo(array, 0);
			return array;
		}

		public object[] ToArray()
		{
			return (object[])ToArray(typeof(object));
		}

		public List Sort()
		{
			Array.Sort(_items, 0, _count, BooComparer.Default);
			return this;
		}

		public List Sort(IComparer comparer)
		{
			Array.Sort(_items, 0, _count, comparer);
			return this;
		}

		public List Sort(Comparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return Sort(new ComparerImpl(comparer));
		}

		public override string ToString()
		{
			return "[" + Join(", ") + "]";
		}

		public string Join(string separator)
		{
			return Builtins.join(this, separator);
		}

		public override int GetHashCode()
		{
			int num = _count;
			for (int i = 0; i < _count; i++)
			{
				object obj = _items[i];
				if (obj != null)
				{
					num ^= obj.GetHashCode();
				}
			}
			return num;
		}

		public override bool Equals(object other)
		{
			if (other == this)
			{
				return true;
			}
			List list = other as List;
			if (list == null)
			{
				return false;
			}
			if (_count != list.Count)
			{
				return false;
			}
			for (int i = 0; i < _count; i++)
			{
				if (!RuntimeServices.EqualityOperator(_items[i], list[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Clear()
		{
			for (int i = 0; i < _count; i++)
			{
				_items[i] = null;
			}
			_count = 0;
		}

		public List GetRange(int begin)
		{
			return InnerGetRange(AdjustIndex(NormalizeIndex(begin)), _count);
		}

		public List GetRange(int begin, int end)
		{
			return InnerGetRange(AdjustIndex(NormalizeIndex(begin)), AdjustIndex(NormalizeIndex(end)));
		}

		public bool Contains(object item)
		{
			return -1 != IndexOf(item);
		}

		public bool Contains(Predicate condition)
		{
			return -1 != IndexOf(condition);
		}

		public bool ContainsReference(object item)
		{
			return -1 != IndexOfReference(item);
		}

		public object Find(Predicate condition)
		{
			int num = IndexOf(condition);
			if (-1 != num)
			{
				return _items[num];
			}
			return null;
		}

		public int IndexOf(Predicate condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			for (int i = 0; i < _count; i++)
			{
				if (condition(_items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public int IndexOfReference(object item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (_items[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		public int IndexOf(object item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (RuntimeServices.EqualityOperator(_items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		public List Insert(int index, object item)
		{
			int num = NormalizeIndex(index);
			EnsureCapacity(Math.Max(_count, num) + 1);
			if (num < _count)
			{
				Array.Copy(_items, num, _items, num + 1, _count - num);
			}
			_items[num] = item;
			_count++;
			return this;
		}

		public object Pop()
		{
			return Pop(-1);
		}

		public object Pop(int index)
		{
			int num = CheckIndex(NormalizeIndex(index));
			object result = _items[num];
			InnerRemoveAt(num);
			return result;
		}

		public List PopRange(int begin)
		{
			int num = AdjustIndex(NormalizeIndex(begin));
			List result = InnerGetRange(num, AdjustIndex(NormalizeIndex(_count)));
			for (int i = num; i < _count; i++)
			{
				_items[i] = null;
			}
			_count = num;
			return result;
		}

		public List RemoveAll(Predicate match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < _count; i++)
			{
				if (match(_items[i]))
				{
					InnerRemoveAt(i--);
				}
			}
			return this;
		}

		public List Remove(object item)
		{
			InnerRemove(item);
			return this;
		}

		public List RemoveAt(int index)
		{
			InnerRemoveAt(CheckIndex(NormalizeIndex(index)));
			return this;
		}

		void IList.Insert(int index, object item)
		{
			Insert(index, item);
		}

		void IList.Remove(object item)
		{
			InnerRemove(item);
		}

		void IList.RemoveAt(int index)
		{
			InnerRemoveAt(CheckIndex(NormalizeIndex(index)));
		}

		int IList.Add(object item)
		{
			Add(item);
			return _count - 1;
		}

		private void EnsureCapacity(int minCapacity)
		{
			if (minCapacity > _items.Length)
			{
				object[] array = NewArray(minCapacity);
				Array.Copy(_items, 0, array, 0, _count);
				_items = array;
			}
		}

		private object[] NewArray(int minCapacity)
		{
			int val = Math.Max(1, _items.Length) * 2;
			return new object[Math.Max(val, minCapacity)];
		}

		private void InnerRemoveAt(int index)
		{
			_count--;
			_items[index] = null;
			if (index != _count)
			{
				Array.Copy(_items, index + 1, _items, index, _count - index);
			}
		}

		private void InnerRemove(object item)
		{
			int num = IndexOf(item);
			if (num != -1)
			{
				InnerRemoveAt(num);
			}
		}

		private void InnerCollect(List target, Predicate condition)
		{
			for (int i = 0; i < _count; i++)
			{
				object item = _items[i];
				if (condition(item))
				{
					target.Add(item);
				}
			}
		}

		private List InnerGetRange(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				object[] array = new object[num];
				Array.Copy(_items, begin, array, 0, num);
				return new List(array, true);
			}
			return new List();
		}

		private int AdjustIndex(int index)
		{
			if (index > _count)
			{
				return _count;
			}
			if (index < 0)
			{
				return 0;
			}
			return index;
		}

		private int CheckIndex(int index)
		{
			if (index >= _count)
			{
				throw new IndexOutOfRangeException();
			}
			return index;
		}

		private int NormalizeIndex(int index)
		{
			if (index < 0)
			{
				index += _count;
			}
			return index;
		}
	}
}
