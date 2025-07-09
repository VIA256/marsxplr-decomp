using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	internal class UnityDomainLoad
	{
		private static AppDomain gActiveDomain;

		public static void Main()
		{
			RegisterDomainAndLoadAssemblies();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RegisterDomainAndLoadAssemblies();

		public static void CreateDomainAndLoad(string domainLoadPath)
		{
			gActiveDomain = null;
			AppDomain appDomain = AppDomain.CreateDomain("UnityRuntimeDomain");
			if (appDomain != null)
			{
				appDomain.ExecuteAssembly(domainLoadPath);
				gActiveDomain = appDomain;
			}
			else
			{
				Console.WriteLine("FAILED TO Create Domain");
			}
		}

		public static void UnloadDomain()
		{
			if (gActiveDomain != null)
			{
				AppDomain.Unload(gActiveDomain);
				gActiveDomain = null;
			}
			else
			{
				Console.WriteLine("FAILED TO UNLOAD DOMAIN BECAUSE GLOBAL WAS CLEARED");
			}
		}
	}
}
