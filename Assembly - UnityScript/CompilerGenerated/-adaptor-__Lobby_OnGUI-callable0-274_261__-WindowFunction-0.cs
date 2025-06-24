using System;
using UnityEngine;

namespace CompilerGenerated
{
	[Serializable]
	internal sealed class _0024adaptor_0024__Lobby_OnGUI_0024callable0_0024274_261___0024WindowFunction_00240
	{
		protected __Lobby_OnGUI_0024callable0_0024274_261__ _0024from;

		public _0024adaptor_0024__Lobby_OnGUI_0024callable0_0024274_261___0024WindowFunction_00240(__Lobby_OnGUI_0024callable0_0024274_261__ from)
		{
			_0024from = from;
		}

		public void Invoke(int capacity)
		{
			_0024from();
		}

		public static GUI.WindowFunction Adapt(__Lobby_OnGUI_0024callable0_0024274_261__ from)
		{
			return new _0024adaptor_0024__Lobby_OnGUI_0024callable0_0024274_261___0024WindowFunction_00240(from).Invoke;
		}
	}
}
