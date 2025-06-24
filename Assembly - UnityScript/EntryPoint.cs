using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class EntryPoint : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class Start_0024106 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal ParticleEmitter _0024pe_0024540;

			internal EntryPoint _0024self_541;

			public _0024(EntryPoint self_)
			{
				_0024self_541 = self_;
			}

			public override bool MoveNext()
			{
				switch (_state)
				{
				default:
					return Yield(2, new WaitForSeconds(15f));
				case 2:
					_0024pe_0024540 = (ParticleEmitter)_0024self_541.GetComponent(typeof(ParticleEmitter));
					_0024pe_0024540.emit = true;
					Yield(1, null);
					break;
				case 1:
					break;
				}
				bool result = default(bool);
				return result;
			}
		}

		internal EntryPoint _0024self_542;

		public Start_0024106(EntryPoint self_)
		{
			_0024self_542 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self_542);
		}
	}

	public IEnumerator Start()
	{
		return new Start_0024106(this).GetEnumerator();
	}

	public void Main()
	{
	}
}
