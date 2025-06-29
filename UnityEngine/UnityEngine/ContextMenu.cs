using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class ContextMenu : Attribute
	{
		private string m_ItemName;

		public string menuItem
		{
			get
			{
				return m_ItemName;
			}
		}

		public ContextMenu(string name)
		{
			m_ItemName = name;
		}
	}
}
