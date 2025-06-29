using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Boo.Lang.Runtime
{
	internal class SliceDispatcherFactory : AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CGetters_003Ed__0 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IEnumerator, IDisposable
		{
			private MethodInfo _003C_003E2__current;

			private int _003C_003E1__state;

			public MemberInfo[] candidates;

			public MemberInfo[] _003C_003E3__candidates;

			public MemberInfo _003Cinfo_003E5__1;

			public PropertyInfo _003Cp_003E5__2;

			public MethodInfo _003Cgetter_003E5__3;

			public SliceDispatcherFactory _003C_003E4__this;

			public MemberInfo[] _003C_003E7__wrap4;

			public int _003C_003E7__wrap5;

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
				_003CGetters_003Ed__0 _003CGetters_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003CGetters_003Ed__0(0)
				{
					_003C_003E4__this = _003C_003E4__this
				} : this);
				_003CGetters_003Ed__.candidates = _003C_003E3__candidates;
				return _003CGetters_003Ed__;
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
						_003C_003E7__wrap4 = candidates;
						_003C_003E7__wrap5 = 0;
						goto IL_00b3;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_00a5;
						}
						IL_00b3:
						if (_003C_003E7__wrap5 < _003C_003E7__wrap4.Length)
						{
							_003Cinfo_003E5__1 = _003C_003E7__wrap4[_003C_003E7__wrap5];
							_003Cp_003E5__2 = _003Cinfo_003E5__1 as PropertyInfo;
							if (_003Cp_003E5__2 != null)
							{
								_003Cgetter_003E5__3 = _003Cp_003E5__2.GetGetMethod(true);
								if (_003Cgetter_003E5__3 != null)
								{
									_003C_003E2__current = _003Cgetter_003E5__3;
									_003C_003E1__state = 2;
									return true;
								}
							}
							goto IL_00a5;
						}
						_003C_003E1__state = -1;
						break;
						IL_00a5:
						_003C_003E7__wrap5++;
						goto IL_00b3;
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
			public _003CGetters_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		public SliceDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, (name == "") ? RuntimeServices.GetDefaultMemberName(type) : name, arguments)
		{
		}

		public Dispatcher CreateGetter()
		{
			MemberInfo[] array = ResolveMember();
			if (array.Length == 1)
			{
				return CreateGetter(array[0]);
			}
			return EmitMethodDispatcher(Getters(array));
		}

		private IEnumerable<MethodInfo> Getters(MemberInfo[] candidates)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003CGetters_003Ed__0 _003CGetters_003Ed__ = new _003CGetters_003Ed__0(-2);
			_003CGetters_003Ed__._003C_003E4__this = this;
			_003CGetters_003Ed__._003C_003E3__candidates = candidates;
			return _003CGetters_003Ed__;
		}

		private Dispatcher CreateGetter(MemberInfo member)
		{
			switch (member.MemberType)
			{
			case MemberTypes.Field:
			{
				FieldInfo field = (FieldInfo)member;
				return (object o, object[] arguments) => RuntimeServices.GetSlice(field.GetValue(o), "", arguments);
			}
			case MemberTypes.Property:
			{
				MethodInfo getter = ((PropertyInfo)member).GetGetMethod(true);
				if (getter == null)
				{
					throw MissingField();
				}
				if (getter.GetParameters().Length > 0)
				{
					return EmitMethodDispatcher(getter);
				}
				return (object o, object[] arguments) => RuntimeServices.GetSlice(getter.Invoke(o, null), "", arguments);
			}
			default:
				throw MissingField();
			}
		}

		private Dispatcher EmitMethodDispatcher(MethodInfo candidate)
		{
			return EmitMethodDispatcher(new MethodInfo[1] { candidate });
		}

		private Dispatcher EmitMethodDispatcher(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(GetArgumentTypes(), candidates);
			if (candidateMethod == null)
			{
				throw MissingField();
			}
			return new MethodDispatcherEmitter(_type, candidateMethod, GetArgumentTypes()).Emit();
		}

		private MemberInfo[] ResolveMember()
		{
			MemberInfo[] member = _type.GetMember(_name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding);
			if (member.Length == 0)
			{
				throw MissingField();
			}
			return member;
		}

		public Dispatcher CreateSetter()
		{
			MemberInfo[] array = ResolveMember();
			if (array.Length > 1)
			{
				throw new AmbiguousMatchException(Builtins.join(array, ", "));
			}
			return CreateSetter(array[0]);
		}

		private Dispatcher CreateSetter(MemberInfo member)
		{
			switch (member.MemberType)
			{
			case MemberTypes.Field:
			{
				FieldInfo field = (FieldInfo)member;
				return (object o, object[] arguments) => RuntimeServices.SetSlice(field.GetValue(o), "", arguments);
			}
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (propertyInfo.GetIndexParameters().Length > 0)
				{
					MethodInfo setMethod = propertyInfo.GetSetMethod(true);
					if (setMethod == null)
					{
						throw MissingField();
					}
					return EmitMethodDispatcher(setMethod);
				}
				return (object o, object[] arguments) => RuntimeServices.SetSlice(RuntimeServices.GetProperty(o, _name), "", arguments);
			}
			default:
				throw MissingField();
			}
		}
	}
}
