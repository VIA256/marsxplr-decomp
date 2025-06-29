using System;
using System.Collections.Generic;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	public class ExtensionRegistry
	{
		private List<MemberInfo> _extensions = new List<MemberInfo>();

		public IEnumerable<MemberInfo> Extensions
		{
			get
			{
				lock (this)
				{
					return _extensions;
				}
			}
		}

		public void Register(Type type)
		{
			lock (this)
			{
				_extensions = AddExtensionMembers(CopyExtensions(), type);
			}
		}

		public void UnRegister(Type type)
		{
			lock (this)
			{
				List<MemberInfo> list = CopyExtensions();
				list.RemoveAll((MemberInfo member) => member.DeclaringType == type);
				_extensions = list;
			}
		}

		private static List<MemberInfo> AddExtensionMembers(List<MemberInfo> extensions, Type type)
		{
			MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public);
			foreach (MemberInfo memberInfo in members)
			{
				if (Attribute.IsDefined(memberInfo, typeof(ExtensionAttribute)) && !extensions.Contains(memberInfo))
				{
					extensions.Add(memberInfo);
				}
			}
			return extensions;
		}

		private List<MemberInfo> CopyExtensions()
		{
			return new List<MemberInfo>(_extensions);
		}
	}
}
