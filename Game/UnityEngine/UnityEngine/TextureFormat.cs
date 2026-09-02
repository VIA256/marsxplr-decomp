using System;

namespace UnityEngine
{
	public enum TextureFormat
	{
		Alpha8 = 1,
		RGB24 = 3,
		ARGB32 = 5,
		DXT1 = 10,
		DXT5 = 12,
		PVRTC_RGB2 = 30,
		PVRTC_RGBA2 = 31,
		PVRTC_RGB4 = 32,
		PVRTC_RGBA4 = 33,
		[Obsolete("Use PVRTC_RGB2")]
		PVRTC_2BPP_RGB = 30,
		[Obsolete("Use PVRTC_RGBA2")]
		PVRTC_2BPP_RGBA = 31,
		[Obsolete("Use PVRTC_RGB4")]
		PVRTC_4BPP_RGB = 32,
		[Obsolete("Use PVRTC_RGBA4")]
		PVRTC_4BPP_RGBA = 33
	}
}
