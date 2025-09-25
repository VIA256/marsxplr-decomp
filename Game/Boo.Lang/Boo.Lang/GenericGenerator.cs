using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	public abstract class GenericGenerator<T> : IEnumerable<T>, IEnumerable
	{
		public abstract IEnumerator<T> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return string.Format("generator({0})", typeof(T));
		}
	}
}
