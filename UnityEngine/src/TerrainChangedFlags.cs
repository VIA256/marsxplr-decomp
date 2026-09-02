using System;

namespace UnityEngine
{
	[Flags]
	public enum TerrainChangedFlags
	{
		Heightmap = 1,
		DetailPrototypes = 2,
		DetailData = 4,
		SplatPrototypes = 8,
		SplatData = 0x10,
		HeightmapDelayedUpdate = 0x20,
		FlushImmediately = 0x40
	}
}
