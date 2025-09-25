using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	public class Builtins
	{
		public class duck
		{
		}

		internal class AssemblyExecutor : MarshalByRefObject
		{
			private string _filename;

			private string[] _arguments;

			private string _capturedOutput = "";

			public string CapturedOutput
			{
				get
				{
					return _capturedOutput;
				}
			}

			public AssemblyExecutor(string filename, string[] arguments)
			{
				_filename = filename;
				_arguments = arguments;
			}

			public void Execute()
			{
				StringWriter stringWriter = new StringWriter();
				TextWriter textWriter = Console.Out;
				try
				{
					Console.SetOut(stringWriter);
					Assembly.LoadFrom(_filename).EntryPoint.Invoke(null, new object[1] { _arguments });
				}
				finally
				{
					Console.SetOut(textWriter);
					_capturedOutput = stringWriter.ToString();
				}
			}
		}

		[EnumeratorItemType(typeof(object[]))]
		public class ZipEnumerator : IEnumerator, IEnumerable, IDisposable
		{
			private IEnumerator[] _enumerators;

			public object Current
			{
				get
				{
					object[] array = new object[_enumerators.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = _enumerators[i].Current;
					}
					return array;
				}
			}

			internal ZipEnumerator(params IEnumerator[] enumerators)
			{
				_enumerators = enumerators;
			}

			public void Dispose()
			{
				for (int i = 0; i < _enumerators.Length; i++)
				{
					IDisposable disposable = _enumerators[i] as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}

			public void Reset()
			{
				for (int i = 0; i < _enumerators.Length; i++)
				{
					_enumerators[i].Reset();
				}
			}

			public bool MoveNext()
			{
				for (int i = 0; i < _enumerators.Length; i++)
				{
					if (!_enumerators[i].MoveNext())
					{
						return false;
					}
				}
				return true;
			}

			public IEnumerator GetEnumerator()
			{
				return this;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cmap_003Ed__0 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			private object _003C_003E2__current;

			private int _003C_003E1__state;

			public object enumerable;

			public object _003C_003E3__enumerable;

			public ICallable function;

			public ICallable _003C_003E3__function;

			public object[] _003Cargs_003E5__1;

			public object _003Citem_003E5__2;

			public IEnumerator _003C_003E7__wrap3;

			public IDisposable _003C_003E7__wrap4;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				_003Cmap_003Ed__0 _003Cmap_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003Cmap_003Ed__0(0) : this);
				_003Cmap_003Ed__.enumerable = _003C_003E3__enumerable;
				_003Cmap_003Ed__.function = _003C_003E3__function;
				return _003Cmap_003Ed__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<object>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					switch (_003C_003E1__state)
					{
					case 0:
						_003C_003E1__state = -1;
						if (enumerable == null)
						{
							throw new ArgumentNullException("enumerable");
						}
						if (function == null)
						{
							throw new ArgumentNullException("function");
						}
						_003Cargs_003E5__1 = new object[1];
						_003C_003E7__wrap3 = iterator(enumerable).GetEnumerator();
						_003C_003E1__state = 1;
						goto IL_00be;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_00be;
						}
						IL_00be:
						if (_003C_003E7__wrap3.MoveNext())
						{
							_003Citem_003E5__2 = _003C_003E7__wrap3.Current;
							_003Cargs_003E5__1[0] = _003Citem_003E5__2;
							_003C_003E2__current = function.Call(_003Cargs_003E5__1);
							_003C_003E1__state = 2;
							return true;
						}
						_003C_003E1__state = -1;
						_003C_003E7__wrap4 = _003C_003E7__wrap3 as IDisposable;
						if (_003C_003E7__wrap4 != null)
						{
							_003C_003E7__wrap4.Dispose();
						}
						break;
					}
					return false;
				}
				catch
				{
					//try-fault
					((IDisposable)this).Dispose();
					throw;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
				switch (_003C_003E1__state)
				{
				case 1:
				case 2:
					try
					{
						break;
					}
					finally
					{
						_003C_003E1__state = -1;
						_003C_003E7__wrap4 = _003C_003E7__wrap3 as IDisposable;
						if (_003C_003E7__wrap4 != null)
						{
							_003C_003E7__wrap4.Dispose();
						}
					}
				}
			}

			[DebuggerHidden]
			public _003Cmap_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cenumerate_003Ed__7 : IEnumerable<object[]>, IEnumerable, IEnumerator<object[]>, IEnumerator, IDisposable
		{
			private object[] _003C_003E2__current;

			private int _003C_003E1__state;

			public object enumerable;

			public object _003C_003E3__enumerable;

			public int _003Ci_003E5__8;

			public object _003Citem_003E5__9;

			public IEnumerator _003C_003E7__wrapa;

			public IDisposable _003C_003E7__wrapb;

			object[] IEnumerator<object[]>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
			{
				_003Cenumerate_003Ed__7 _003Cenumerate_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003Cenumerate_003Ed__7(0) : this);
				_003Cenumerate_003Ed__.enumerable = _003C_003E3__enumerable;
				return _003Cenumerate_003Ed__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<object[]>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					switch (_003C_003E1__state)
					{
					case 0:
						_003C_003E1__state = -1;
						_003Ci_003E5__8 = 0;
						_003C_003E7__wrapa = iterator(enumerable).GetEnumerator();
						_003C_003E1__state = 1;
						goto IL_009e;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_009e;
						}
						IL_009e:
						if (_003C_003E7__wrapa.MoveNext())
						{
							_003Citem_003E5__9 = _003C_003E7__wrapa.Current;
							_003C_003E2__current = new object[2]
							{
								_003Ci_003E5__8++,
								_003Citem_003E5__9
							};
							_003C_003E1__state = 2;
							return true;
						}
						_003C_003E1__state = -1;
						_003C_003E7__wrapb = _003C_003E7__wrapa as IDisposable;
						if (_003C_003E7__wrapb != null)
						{
							_003C_003E7__wrapb.Dispose();
						}
						break;
					}
					return false;
				}
				catch
				{
					//try-fault
					((IDisposable)this).Dispose();
					throw;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
				switch (_003C_003E1__state)
				{
				case 1:
				case 2:
					try
					{
						break;
					}
					finally
					{
						_003C_003E1__state = -1;
						_003C_003E7__wrapb = _003C_003E7__wrapa as IDisposable;
						if (_003C_003E7__wrapb != null)
						{
							_003C_003E7__wrapb.Dispose();
						}
					}
				}
			}

			[DebuggerHidden]
			public _003Cenumerate_003Ed__7(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		[CompilerGenerated]
		private sealed class _003Ccat_003Ed__18 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			private object _003C_003E2__current;

			private int _003C_003E1__state;

			public object[] args;

			public object[] _003C_003E3__args;

			public object _003Ce_003E5__19;

			public object _003Citem_003E5__1a;

			public object[] _003C_003E7__wrap1b;

			public int _003C_003E7__wrap1c;

			public IEnumerator _003C_003E7__wrap1d;

			public IDisposable _003C_003E7__wrap1e;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				_003Ccat_003Ed__18 _003Ccat_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003Ccat_003Ed__18(0) : this);
				_003Ccat_003Ed__.args = _003C_003E3__args;
				return _003Ccat_003Ed__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<object>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					int num = _003C_003E1__state;
					if (num != 0)
					{
						if (num != 3)
						{
							goto IL_00fe;
						}
						_003C_003E1__state = 2;
						goto IL_009e;
					}
					_003C_003E1__state = -1;
					_003C_003E1__state = 1;
					_003C_003E7__wrap1b = args;
					_003C_003E7__wrap1c = 0;
					goto IL_00e4;
					IL_00fe:
					return false;
					IL_009e:
					if (_003C_003E7__wrap1d.MoveNext())
					{
						_003Citem_003E5__1a = _003C_003E7__wrap1d.Current;
						_003C_003E2__current = _003Citem_003E5__1a;
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003E1__state = 1;
					_003C_003E7__wrap1e = _003C_003E7__wrap1d as IDisposable;
					if (_003C_003E7__wrap1e != null)
					{
						_003C_003E7__wrap1e.Dispose();
					}
					_003C_003E7__wrap1c++;
					goto IL_00e4;
					IL_00e4:
					if (_003C_003E7__wrap1c < _003C_003E7__wrap1b.Length)
					{
						_003Ce_003E5__19 = _003C_003E7__wrap1b[_003C_003E7__wrap1c];
						_003C_003E7__wrap1d = iterator(_003Ce_003E5__19).GetEnumerator();
						_003C_003E1__state = 2;
						goto IL_009e;
					}
					_003C_003E1__state = -1;
					goto IL_00fe;
				}
				catch
				{
					//try-fault
					((IDisposable)this).Dispose();
					throw;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
				switch (_003C_003E1__state)
				{
				case 1:
				case 2:
				case 3:
					switch (_003C_003E1__state)
					{
					case 2:
					case 3:
						try
						{
						}
						finally
						{
							_003C_003E1__state = 1;
							_003C_003E7__wrap1e = _003C_003E7__wrap1d as IDisposable;
							if (_003C_003E7__wrap1e != null)
							{
								_003C_003E7__wrap1e.Dispose();
							}
						}
						break;
					}
					_003C_003E1__state = -1;
					break;
				}
			}

			[DebuggerHidden]
			public _003Ccat_003Ed__18(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		public static Version BooVersion
		{
			get
			{
				return new Version("0.8.2.2994");
			}
		}

		public static void print(object o)
		{
			Console.WriteLine(o);
		}

		public static string gets()
		{
			return Console.ReadLine();
		}

		public static string prompt(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}

		public static string join(IEnumerable enumerable, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = enumerable.GetEnumerator();
			using (enumerator as IDisposable)
			{
				if (enumerator.MoveNext())
				{
					stringBuilder.Append(enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public static string join(IEnumerable enumerable, char separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = enumerable.GetEnumerator();
			using (enumerator as IDisposable)
			{
				if (enumerator.MoveNext())
				{
					stringBuilder.Append(enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public static string join(IEnumerable enumerable)
		{
			return join(enumerable, ' ');
		}

		public static IEnumerable map(object enumerable, ICallable function)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003Cmap_003Ed__0 _003Cmap_003Ed__ = new _003Cmap_003Ed__0(-2);
			_003Cmap_003Ed__._003C_003E3__enumerable = enumerable;
			_003Cmap_003Ed__._003C_003E3__function = function;
			return _003Cmap_003Ed__;
		}

		public static object[] array(IEnumerable enumerable)
		{
			return new List(enumerable).ToArray();
		}

		public static Array array(Type elementType, ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			Array array = Array.CreateInstance(elementType, collection.Count);
			if (RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(elementType)))
			{
				int num = 0;
				foreach (object item in collection)
				{
					object value = RuntimeServices.CheckNumericPromotion(item).ToType(elementType, null);
					array.SetValue(value, num);
					num++;
				}
			}
			else
			{
				collection.CopyTo(array, 0);
			}
			return array;
		}

		public static Array array(Type elementType, IEnumerable enumerable)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			List list = null;
			if (RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(elementType)))
			{
				list = new List();
				foreach (object item2 in enumerable)
				{
					object item = RuntimeServices.CheckNumericPromotion(item2).ToType(elementType, null);
					list.Add(item);
				}
			}
			else
			{
				list = new List(enumerable);
			}
			return list.ToArray(elementType);
		}

		public static Array array(Type elementType, int length)
		{
			return matrix(elementType, length);
		}

		public static Array matrix(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return Array.CreateInstance(elementType, lengths);
		}

		public static IEnumerable iterator(object enumerable)
		{
			return RuntimeServices.GetEnumerable(enumerable);
		}

		public static string shellm(string filename, params string[] arguments)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = Path.GetDirectoryName(Path.GetFullPath(filename));
			AppDomain appDomain = AppDomain.CreateDomain("shellm", null, appDomainSetup);
			try
			{
				AssemblyExecutor assemblyExecutor = new AssemblyExecutor(filename, arguments);
				appDomain.DoCallBack(assemblyExecutor.Execute);
				return assemblyExecutor.CapturedOutput;
			}
			finally
			{
				AppDomain.Unload(appDomain);
			}
		}

		public static IEnumerable<object[]> enumerate(object enumerable)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003Cenumerate_003Ed__7 _003Cenumerate_003Ed__ = new _003Cenumerate_003Ed__7(-2);
			_003Cenumerate_003Ed__._003C_003E3__enumerable = enumerable;
			return _003Cenumerate_003Ed__;
		}

		public static IEnumerable<int> range(int max)
		{
			if (max < 0)
			{
				throw new ArgumentOutOfRangeException("max < 0");
			}
			return range(0, max);
		}

		public static IEnumerable<int> range(int begin, int end)
		{
			if (begin < end)
			{
				for (int i = begin; i < end; i++)
				{
					yield return i;
				}
			}
			else if (begin > end)
			{
				for (int i2 = begin; i2 > end; i2--)
				{
					yield return i2;
				}
			}
		}

		public static IEnumerable<int> range(int begin, int end, int step)
		{
			if (step == 0)
			{
				throw new ArgumentOutOfRangeException("step == 0");
			}
			if (step < 0)
			{
				if (begin < end)
				{
					throw new ArgumentOutOfRangeException("begin < end && step < 0");
				}
				for (int i = begin; i > end; i += step)
				{
					yield return i;
				}
			}
			else
			{
				if (begin > end)
				{
					throw new ArgumentOutOfRangeException("begin > end && step > 0");
				}
				for (int j = begin; j < end; j += step)
				{
					yield return j;
				}
			}
		}

		public static IEnumerable reversed(object enumerable)
		{
			return new List(iterator(enumerable)).Reversed;
		}

		public static ZipEnumerator zip(params object[] enumerables)
		{
			IEnumerator[] array = new IEnumerator[enumerables.Length];
			for (int i = 0; i < enumerables.Length; i++)
			{
				array[i] = GetEnumerator(enumerables[i]);
			}
			return new ZipEnumerator(array);
		}

		public static IEnumerable<object> cat(params object[] args)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003Ccat_003Ed__18 _003Ccat_003Ed__ = new _003Ccat_003Ed__18(-2);
			_003Ccat_003Ed__._003C_003E3__args = args;
			return _003Ccat_003Ed__;
		}

		private static IEnumerator GetEnumerator(object enumerable)
		{
			return RuntimeServices.GetEnumerable(enumerable).GetEnumerator();
		}
	}
}
