using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Boo.Lang.Runtime
{
	public abstract class AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CGetExtensions_003Ed__0<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable where T : MemberInfo
		{
			private T _003C_003E2__current;

			private int _003C_003E1__state;

			public AbstractDispatcherFactory _003C_003E4__this;

			public MemberTypes memberTypes;

			public MemberTypes _003C_003E3__memberTypes;

			public MemberInfo _003Cm_003E5__1;

			public IEnumerator<MemberInfo> _003C_003E7__wrap2;

			T IEnumerator<T>.Current
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
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				_003CGetExtensions_003Ed__0<T> _003CGetExtensions_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003CGetExtensions_003Ed__0<T>(0)
				{
					_003C_003E4__this = _003C_003E4__this
				} : this);
				_003CGetExtensions_003Ed__.memberTypes = _003C_003E3__memberTypes;
				return _003CGetExtensions_003Ed__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<T>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					switch (_003C_003E1__state)
					{
					case 0:
						_003C_003E1__state = -1;
						_003C_003E7__wrap2 = _003C_003E4__this.Extensions.GetEnumerator();
						_003C_003E1__state = 1;
						goto IL_00a8;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_00a8;
						}
						IL_00a8:
						while (_003C_003E7__wrap2.MoveNext())
						{
							_003Cm_003E5__1 = _003C_003E7__wrap2.Current;
							if (_003Cm_003E5__1.MemberType == memberTypes && !(_003Cm_003E5__1.Name != _003C_003E4__this._name))
							{
								_003C_003E2__current = (T)_003Cm_003E5__1;
								_003C_003E1__state = 2;
								return true;
							}
						}
						_003C_003E1__state = -1;
						if (_003C_003E7__wrap2 != null)
						{
							_003C_003E7__wrap2.Dispose();
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
						if (_003C_003E7__wrap2 != null)
						{
							_003C_003E7__wrap2.Dispose();
						}
					}
				}
			}

			[DebuggerHidden]
			public _003CGetExtensions_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		private readonly ExtensionRegistry _extensions;

		private readonly object _target;

		protected readonly Type _type;

		protected readonly string _name;

		private readonly object[] _arguments;

		protected IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return _extensions.Extensions;
			}
		}

		public AbstractDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
		{
			_extensions = extensions;
			_target = target;
			_type = type;
			_name = name;
			_arguments = arguments;
		}

		protected object[] GetExtensionArgs()
		{
			object[] array = new object[_arguments.Length + 1];
			array[0] = _target;
			Array.Copy(_arguments, 0, array, 1, _arguments.Length);
			return array;
		}

		protected Type[] GetArgumentTypes()
		{
			return MethodResolver.GetArgumentTypes(_arguments);
		}

		protected Type[] GetExtensionArgumentTypes()
		{
			return MethodResolver.GetArgumentTypes(GetExtensionArgs());
		}

		protected Dispatcher EmitExtensionDispatcher(CandidateMethod found)
		{
			return new ExtensionMethodDispatcherEmitter(found, GetArgumentTypes()).Emit();
		}

		protected CandidateMethod ResolveExtension(IEnumerable<MethodInfo> candidates)
		{
			MethodResolver methodResolver = new MethodResolver(GetExtensionArgumentTypes());
			return methodResolver.ResolveMethod(candidates);
		}

		protected IEnumerable<MethodInfo> GetExtensionMethods()
		{
			return GetExtensions<MethodInfo>(MemberTypes.Method);
		}

		protected IEnumerable<T> GetExtensions<T>(MemberTypes memberTypes) where T : MemberInfo
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003CGetExtensions_003Ed__0<T> _003CGetExtensions_003Ed__ = new _003CGetExtensions_003Ed__0<T>(-2);
			_003CGetExtensions_003Ed__._003C_003E4__this = this;
			_003CGetExtensions_003Ed__._003C_003E3__memberTypes = memberTypes;
			return _003CGetExtensions_003Ed__;
		}

		protected static CandidateMethod ResolveMethod(Type[] argumentTypes, IEnumerable<MethodInfo> candidates)
		{
			return new MethodResolver(argumentTypes).ResolveMethod(candidates);
		}

		protected MissingFieldException MissingField()
		{
			return new MissingFieldException(_type.FullName, _name);
		}
	}
}
