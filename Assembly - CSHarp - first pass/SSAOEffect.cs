using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/SSAO")]
public class SSAOEffect : MonoBehaviour
{
	public enum SSAOSamples
	{
		Low,
		Medium,
		High
	}

	public float m_Radius = 0.4f;

	public SSAOSamples m_SampleCount = SSAOSamples.Medium;

	public float m_OcclusionIntensity = 1.5f;

	public int m_Blur = 2;

	public int m_Downsampling = 2;

	public float m_OcclusionAttenuation = 1f;

	public float m_MinZ = 0.01f;

	public Shader m_SSAOShader;

	private Material m_SSAOMaterial;

	public Texture2D m_RandomTexture;

	private bool m_Supported;

	private bool m_IsOpenGL;

	private static Material CreateMaterial(Shader shader)
	{
		if (!shader)
		{
			return null;
		}
		Material material = new Material(shader);
		material.hideFlags = HideFlags.HideAndDontSave;
		return material;
	}

	private static void DestroyMaterial(Material mat)
	{
		if ((bool)mat)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	private void OnDisable()
	{
		DestroyMaterial(m_SSAOMaterial);
	}

	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			m_Supported = false;
			base.enabled = false;
			return;
		}
		CreateMaterials();
		if (!m_SSAOMaterial || m_SSAOMaterial.passCount != 5)
		{
			m_Supported = false;
			base.enabled = false;
		}
		else
		{
			base.camera.depthTextureMode = DepthTextureMode.DepthNormals;
			m_Supported = true;
			m_IsOpenGL = SystemInfo.graphicsDeviceVersion.StartsWith("OpenGL");
		}
	}

	private void CreateMaterials()
	{
		if (!m_SSAOMaterial && m_SSAOShader.isSupported)
		{
			m_SSAOMaterial = CreateMaterial(m_SSAOShader);
			m_SSAOMaterial.SetTexture("_RandomTexture", m_RandomTexture);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!m_Supported || !m_SSAOShader.isSupported)
		{
			base.enabled = false;
			return;
		}
		CreateMaterials();
		m_Downsampling = Mathf.Clamp(m_Downsampling, 1, 6);
		m_Radius = Mathf.Clamp(m_Radius, 0.05f, 1f);
		m_MinZ = Mathf.Clamp(m_MinZ, 1E-05f, 0.5f);
		m_OcclusionIntensity = Mathf.Clamp(m_OcclusionIntensity, 0.5f, 4f);
		m_OcclusionAttenuation = Mathf.Clamp(m_OcclusionAttenuation, 0.2f, 2f);
		m_Blur = Mathf.Clamp(m_Blur, 0, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / m_Downsampling, source.height / m_Downsampling, 0);
		float fieldOfView = base.camera.fieldOfView;
		float farClipPlane = base.camera.farClipPlane;
		float num = Mathf.Tan(fieldOfView * ((float)Math.PI / 180f) * 0.5f) * farClipPlane;
		float x = num * base.camera.aspect;
		Material sSAOMaterial = m_SSAOMaterial;
		Vector3 vector = new Vector3(x, num, farClipPlane);
		sSAOMaterial.SetVector("_FarCorner", vector);
		int num2;
		int num3;
		if ((bool)m_RandomTexture)
		{
			num2 = m_RandomTexture.width;
			num3 = m_RandomTexture.height;
		}
		else
		{
			num2 = 1;
			num3 = 1;
		}
		Material sSAOMaterial2 = m_SSAOMaterial;
		Vector3 vector2 = new Vector3((float)renderTexture.width / (float)num2, (float)renderTexture.height / (float)num3, 0f);
		sSAOMaterial2.SetVector("_NoiseScale", vector2);
		Material sSAOMaterial3 = m_SSAOMaterial;
		Vector4 vector3 = new Vector4(m_Radius, m_MinZ, 1f / m_OcclusionAttenuation, m_OcclusionIntensity);
		sSAOMaterial3.SetVector("_Params", vector3);
		bool flag = m_Blur > 0;
		Graphics.Blit((!flag) ? source : null, renderTexture, m_SSAOMaterial, (int)m_SampleCount);
		if (flag)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			Material sSAOMaterial4 = m_SSAOMaterial;
			Vector4 vector5;
			if (m_IsOpenGL)
			{
				Vector4 vector4 = new Vector4(m_Blur, 0f, 1f / (float)m_Downsampling, 0f);
				vector5 = vector4;
			}
			else
			{
				Vector4 vector6 = new Vector4((float)m_Blur / (float)source.width, 0f, 0f, 0f);
				vector5 = vector6;
			}
			sSAOMaterial4.SetVector("_TexelOffsetScale", vector5);
			m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(null, temporary, m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			Material sSAOMaterial5 = m_SSAOMaterial;
			Vector4 vector8;
			if (m_IsOpenGL)
			{
				Vector4 vector7 = new Vector4(0f, m_Blur, 1f, 0f);
				vector8 = vector7;
			}
			else
			{
				Vector4 vector9 = new Vector4(0f, (float)m_Blur / (float)source.height, 0f, 0f);
				vector8 = vector9;
			}
			sSAOMaterial5.SetVector("_TexelOffsetScale", vector8);
			m_SSAOMaterial.SetTexture("_SSAO", temporary);
			Graphics.Blit(source, temporary2, m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary);
			renderTexture = temporary2;
		}
		m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
		Graphics.Blit(source, destination, m_SSAOMaterial, 4);
		RenderTexture.ReleaseTemporary(renderTexture);
	}
}
