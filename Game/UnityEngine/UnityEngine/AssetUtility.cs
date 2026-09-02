using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class AssetUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object CreateEngineObject(string className);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CreateAsset(Object theAsset, string assetName, string extension);

		public static void CreateAsset(Object theAsset, string assetName)
		{
			string extension = "asset";
			CreateAsset(theAsset, assetName, extension);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddAssetToSameFile(Object newAsset, Object sameAssetFile);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SaveAsset(Object newAsset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object LoadWithPathAndFileID(string path, int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetObjectCountAtPath(string path);
	}
}
