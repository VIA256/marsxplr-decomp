using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Boo.Lang.Runtime
{
	public class TextReaderEnumerator
	{
		[CompilerGenerated]
		private sealed class _003Clines_003Ed__0 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			private string _003C_003E2__current;

			private int _003C_003E1__state;

			public TextReader reader;

			public TextReader _003C_003E3__reader;

			public string _003Cline_003E5__1;

			public TextReader _003C_003E7__wrap2;

			string IEnumerator<string>.Current
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
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				_003Clines_003Ed__0 _003Clines_003Ed__ = ((Interlocked.CompareExchange(ref _003C_003E1__state, 0, -2) != -2) ? new _003Clines_003Ed__0(0) : this);
				_003Clines_003Ed__.reader = _003C_003E3__reader;
				return _003Clines_003Ed__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<string>)this).GetEnumerator();
			}

			private bool MoveNext()
			{
				try
				{
					switch (_003C_003E1__state)
					{
					case 0:
						_003C_003E1__state = -1;
						_003C_003E7__wrap2 = reader;
						_003C_003E1__state = 1;
						goto IL_0055;
					case 2:
						{
							_003C_003E1__state = 1;
							goto IL_0055;
						}
						IL_0055:
						if ((_003Cline_003E5__1 = reader.ReadLine()) != null)
						{
							_003C_003E2__current = _003Cline_003E5__1;
							_003C_003E1__state = 2;
							return true;
						}
						_003C_003E1__state = -1;
						if (_003C_003E7__wrap2 != null)
						{
							((IDisposable)_003C_003E7__wrap2).Dispose();
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
							((IDisposable)_003C_003E7__wrap2).Dispose();
						}
					}
				}
			}

			[DebuggerHidden]
			public _003Clines_003Ed__0(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}
		}

		public static IEnumerable<string> lines(TextReader reader)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			_003Clines_003Ed__0 _003Clines_003Ed__ = new _003Clines_003Ed__0(-2);
			_003Clines_003Ed__._003C_003E3__reader = reader;
			return _003Clines_003Ed__;
		}
	}
}
