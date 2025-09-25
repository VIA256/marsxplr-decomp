using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class AddComponentMenu : Attribute
	{
		private string m_AddComponentMenu;

		public string componentMenu
		{
			get
			{
				return m_AddComponentMenu;
			}
		}

		public AddComponentMenu(string menuName)
		{
			m_AddComponentMenu = menuName;
		}
	}
}
