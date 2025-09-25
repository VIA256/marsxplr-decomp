using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Boo.Lang.Runtime
{
	public class MethodDispatcherFactory : AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CGetCandidates_003Ed__0 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IEnumerator, IDisposable
		{
			private MethodInfo _003C_003E2__current;

			private int _003C_003E1__state;

			public MethodDispatcherFactory _003C_003E4__this;

			public MethodInfo _003Cmethod_003E5__1;

			public MethodInfo[] _003C_003E7__wrap2;

			public int _003C_003E7__wrap3;

			MethodInfo IEnumerator<MethodInfo>.Current
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
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				return (Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003CGetCandidates_003Ed__0(0)
				{
					_003C_003E4__this = _003C_003E4__this
				} : this;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<MethodInfo>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					switch (_003C_003E1__state)
					{
					case 0:
						_003C_003E1__state = -1;
						_003C_003E1__state = 1;
						_003C_003E7__wrap2 = _003C_003E4__this._type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding);
						_003C_003E7__wrap3 = 0;
						goto IL_00ac;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_009e;
						}
						IL_00ac:
						if (_003C_003E7__wrap3 < _003C_003E7__wrap2.Length)
						{
							_003Cmethod_003E5__1 = _003C_003E7__wrap2[_003C_003E7__wrap3];
							if (!(_003C_003E4__this._name != _003Cmethod_003E5__1.Name))
							{
								_003C_003E2__current = _003Cmethod_003E5__1;
								_003C_003E1__state = 2;
								return true;
							}
							goto IL_009e;
						}
						_003C_003E1__state = -1;
						break;
						IL_009e:
						_003C_003E7__wrap3++;
						goto IL_00ac;
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
					_003C_003E1__state = -1;
					break;
				}
			}

			[DebuggerHidden]
			public _003CGetCandidates_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		public MethodDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string methodName, params object[] arguments)
			: base(extensions, target, type, methodName, arguments)
		{
		}

		public Dispatcher Create()
		{
			Type[] argumentTypes = GetArgumentTypes();
			CandidateMethod candidateMethod = ResolveMethod(argumentTypes);
			if (candidateMethod != null)
			{
				return EmitMethodDispatcher(candidateMethod, argumentTypes);
			}
			return ProduceExtensionDispatcher();
		}

		private Dispatcher ProduceExtensionDispatcher()
		{
			CandidateMethod candidateMethod = ResolveExtensionMethod();
			if (candidateMethod == null)
			{
				throw new MissingMethodException(_type.FullName, _name);
			}
			return EmitExtensionDispatcher(candidateMethod);
		}

		private CandidateMethod ResolveExtensionMethod()
		{
			return ResolveExtension(GetExtensionMethods());
		}

		private CandidateMethod ResolveMethod(Type[] argumentTypes)
		{
			return AbstractDispatcherFactory.ResolveMethod(argumentTypes, GetCandidates());
		}

		private IEnumerable<MethodInfo> GetCandidates()
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003CGetCandidates_003Ed__0 _003CGetCandidates_003Ed__ = new _003CGetCandidates_003Ed__0(-2);
			_003CGetCandidates_003Ed__._003C_003E4__this = this;
			return _003CGetCandidates_003Ed__;
		}

		private Dispatcher EmitMethodDispatcher(CandidateMethod found, Type[] argumentTypes)
		{
			return new MethodDispatcherEmitter(_type, found, argumentTypes).Emit();
		}
	}
}
