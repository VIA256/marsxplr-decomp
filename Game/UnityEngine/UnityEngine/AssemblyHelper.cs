using System.Collections;
using System.Reflection;

namespace UnityEngine
{
	public class AssemblyHelper
	{
		private static void AddReferencedAssembliesRecurse(Assembly assembly, ArrayList list)
		{
			if (assembly != null && list.IndexOf(assembly) == -1)
			{
				list.Add(assembly);
				AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
				for (int i = 0; i < referencedAssemblies.Length; i++)
				{
					AddReferencedAssembliesRecurse(Assembly.Load(referencedAssemblies[i]), list);
				}
			}
		}

		private static string[] GetReferencedAssemblies(Assembly assembly)
		{
			ArrayList arrayList = new ArrayList();
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				AddReferencedAssembliesRecurse(Assembly.Load(referencedAssemblies[i]), arrayList);
			}
			string[] array = new string[arrayList.Count];
			for (int j = 0; j < array.Length; j++)
			{
				Assembly assembly2 = (Assembly)arrayList[j];
				array[j] = assembly2.GetName().Name;
			}
			return array;
		}
	}
}
