using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Boo.Lang.Runtime
{
	public class PropertyDispatcherFactory : AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CGetCandidateExtensions_003Ed__0 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IEnumerator, IDisposable
		{
			private MethodInfo _003C_003E2__current;

			private int _003C_003E1__state;

			public PropertyDispatcherFactory _003C_003E4__this;

			public SetOrGet gos;

			public SetOrGet _003C_003E3__gos;

			public PropertyInfo _003Cp_003E5__1;

			public MethodInfo _003Cm_003E5__2;

			public IEnumerator<PropertyInfo> _003C_003E7__wrap3;

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
				_003CGetCandidateExtensions_003Ed__0 _003CGetCandidateExtensions_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003CGetCandidateExtensions_003Ed__0(0)
				{
					_003C_003E4__this = _003C_003E4__this
				} : this);
				_003CGetCandidateExtensions_003Ed__.gos = _003C_003E3__gos;
				return _003CGetCandidateExtensions_003Ed__;
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
						_003C_003E7__wrap3 = _003C_003E4__this.GetExtensions<PropertyInfo>(MemberTypes.Property).GetEnumerator();
						_003C_003E1__state = 1;
						goto IL_0094;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_0094;
						}
						IL_0094:
						while (_003C_003E7__wrap3.MoveNext())
						{
							_003Cp_003E5__1 = _003C_003E7__wrap3.Current;
							_003Cm_003E5__2 = Accessor(_003Cp_003E5__1, gos);
							if (_003Cm_003E5__2 != null)
							{
								_003C_003E2__current = _003Cm_003E5__2;
								_003C_003E1__state = 2;
								return true;
							}
						}
						_003C_003E1__state = -1;
						if (_003C_003E7__wrap3 != null)
						{
							_003C_003E7__wrap3.Dispose();
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
						if (_003C_003E7__wrap3 != null)
						{
							_003C_003E7__wrap3.Dispose();
						}
					}
				}
			}

			[DebuggerHidden]
			public _003CGetCandidateExtensions_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		public PropertyDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, name, arguments)
		{
		}

		public Dispatcher CreateSetter()
		{
			return Create(SetOrGet.Set);
		}

		public Dispatcher CreateGetter()
		{
			return Create(SetOrGet.Get);
		}

		private Dispatcher Create(SetOrGet gos)
		{
			MemberInfo[] member = _type.GetMember(_name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding);
			if (member.Length == 0)
			{
				return FindExtension(GetCandidateExtensions(gos));
			}
			if (member.Length > 1)
			{
				throw new AmbiguousMatchException(Builtins.join(member, ", "));
			}
			return EmitDispatcherFor(member[0], gos);
		}

		private Dispatcher FindExtension(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = ResolveExtension(candidates);
			if (candidateMethod != null)
			{
				return EmitExtensionDispatcher(candidateMethod);
			}
			throw MissingField();
		}

		private IEnumerable<MethodInfo> GetCandidateExtensions(SetOrGet gos)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003CGetCandidateExtensions_003Ed__0 _003CGetCandidateExtensions_003Ed__ = new _003CGetCandidateExtensions_003Ed__0(-2);
			_003CGetCandidateExtensions_003Ed__._003C_003E4__this = this;
			_003CGetCandidateExtensions_003Ed__._003C_003E3__gos = gos;
			return _003CGetCandidateExtensions_003Ed__;
		}

		private static MethodInfo Accessor(PropertyInfo p, SetOrGet gos)
		{
			if (gos != SetOrGet.Get)
			{
				return p.GetSetMethod(true);
			}
			return p.GetGetMethod(true);
		}

		private Dispatcher EmitDispatcherFor(MemberInfo info, SetOrGet gos)
		{
			MemberTypes memberType = info.MemberType;
			if (memberType == MemberTypes.Property)
			{
				return EmitPropertyDispatcher((PropertyInfo)info, gos);
			}
			return EmitFieldDispatcher((FieldInfo)info, gos);
		}

		private Dispatcher EmitFieldDispatcher(FieldInfo field, SetOrGet gos)
		{
			if (SetOrGet.Get != gos)
			{
				return new SetFieldEmitter(field, GetArgumentTypes()[0]).Emit();
			}
			return new GetFieldEmitter(field).Emit();
		}

		private Dispatcher EmitPropertyDispatcher(PropertyInfo property, SetOrGet gos)
		{
			Type[] argumentTypes = GetArgumentTypes();
			MethodInfo methodInfo = Accessor(property, gos);
			if (methodInfo == null)
			{
				throw MissingField();
			}
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(argumentTypes, new MethodInfo[1] { methodInfo });
			if (candidateMethod == null)
			{
				throw MissingField();
			}
			if (SetOrGet.Get == gos)
			{
				return new MethodDispatcherEmitter(_type, candidateMethod, argumentTypes).Emit();
			}
			return new SetPropertyEmitter(_type, candidateMethod, argumentTypes).Emit();
		}
	}
}
