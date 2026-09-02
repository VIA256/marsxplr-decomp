using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class Camera : Behaviour
	{
		public float fov
		{
			get
			{
				return fieldOfView;
			}
			set
			{
				fieldOfView = value;
			}
		}

		public float near
		{
			get
			{
				return nearClipPlane;
			}
			set
			{
				nearClipPlane = value;
			}
		}

		public float far
		{
			get
			{
				return farClipPlane;
			}
			set
			{
				farClipPlane = value;
			}
		}

		public extern float fieldOfView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float nearClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float farClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float orthographicSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool orthographic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool isOrthoGraphic
		{
			get
			{
				return orthographic;
			}
			set
			{
				orthographic = value;
			}
		}

		public extern float depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float aspect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Color backgroundColor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Rect rect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Rect pixelRect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture targetTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float pixelWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float pixelHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Matrix4x4 cameraToWorldMatrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Matrix4x4 worldToCameraMatrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Matrix4x4 projectionMatrix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 velocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern CameraClearFlags clearFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Camera main
		{
			get
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("MainCamera");
				GameObject[] array2 = array;
				int num = array2.Length;
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = array2[i];
					if (gameObject.camera != null && gameObject.camera.enabled)
					{
						return gameObject.camera;
					}
				}
				return null;
			}
		}

		public static extern Camera current
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Camera[] allCameras
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Camera mainCamera
		{
			get
			{
				return main;
			}
		}

		public extern float[] layerCullDistances
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern DepthTextureMode depthTextureMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetWorldToCameraMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetProjectionMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetAspect();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 WorldToScreenPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 WorldToViewportPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ViewportToWorldPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ScreenToWorldPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ScreenToViewportPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector3 ViewportToScreenPoint(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Ray ViewportPointToRay(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Ray ScreenPointToRay(Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetScreenWidth();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetScreenHeight();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DoClear();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetReplacementShader();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderDontRestore();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetupCurrent(Camera cur);

		public bool RenderToCubemap(Cubemap cubemap)
		{
			int faceMask = 63;
			return RenderToCubemap(cubemap, faceMask);
		}

		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return Internal_RenderToCubemapTexture(cubemap, faceMask);
		}

		public bool RenderToCubemap(RenderTexture cubemap)
		{
			int faceMask = 63;
			return RenderToCubemap(cubemap, faceMask);
		}

		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return Internal_RenderToCubemapRT(cubemap, faceMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapRT(RenderTexture cubemap, int faceMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapTexture(Cubemap cubemap, int faceMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyFrom(Camera other);
	}
}
