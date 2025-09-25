using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRequest : AsyncOperation
	{
		private AssetBundle m_AssetBundle;

		private string m_Path;

		private Type m_Type;

		public Object asset
		{
			get
			{
				return m_AssetBundle.Load(m_Path, m_Type);
			}
		}
	}
}
