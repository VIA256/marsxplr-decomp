using UnityEngine;

[AddComponentMenu("Image Effects/Blur (island)")]
[ExecuteInEditMode]
public class BlurEffectIsland : MonoBehaviour
{
	public int iterations = 3;

	public float blurSpread = 0.6f;

	private static string blurMatString = "Shader \"BlurConeTap\" {\n\tSubShader {\n\t\tPass {\n\t\t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\t\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant alpha}\n\t\t\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t}\n\t}\n\tFallback off\n}";

	private static Material m_Material;

	protected static Material material
	{
		get
		{
			if (m_Material == null)
			{
				m_Material = new Material(blurMatString);
				m_Material.hideFlags = HideFlags.HideAndDontSave;
				m_Material.shader.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material;
		}
	}

	protected void OnDisable()
	{
		if ((bool)m_Material)
		{
			Object.DestroyImmediate(m_Material.shader);
			Object.DestroyImmediate(m_Material);
		}
	}

	public bool IsSupported()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			return false;
		}
		if (!material.shader.isSupported)
		{
			return false;
		}
		return true;
	}

	protected void Start()
	{
		if (!IsSupported())
		{
			base.enabled = false;
		}
	}

	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		RenderTexture.active = dest;
		source.SetGlobalShaderProperty("__RenderTex");
		float offsetX = (0.5f + (float)iteration * blurSpread) / (float)source.width;
		float offsetY = (0.5f + (float)iteration * blurSpread) / (float)source.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			Render4TapQuad(dest, offsetX, offsetY);
		}
		GL.PopMatrix();
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		RenderTexture.active = dest;
		source.SetGlobalShaderProperty("__RenderTex");
		float offsetX = 1f / (float)source.width;
		float offsetY = 1f / (float)source.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			Render4TapQuad(dest, offsetX, offsetY);
		}
		GL.PopMatrix();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		DownSample4x(source, temporary);
		bool flag = true;
		for (int i = 0; i < iterations; i++)
		{
			if (flag)
			{
				FourTapCone(temporary, temporary2, i);
			}
			else
			{
				FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		if (flag)
		{
			ImageEffects.Blit(temporary, destination);
		}
		else
		{
			ImageEffects.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	private static void Render4TapQuad(RenderTexture dest, float offsetX, float offsetY)
	{
		GL.Begin(7);
		Vector2 vector = Vector2.zero;
		if (dest != null)
		{
			vector = dest.GetTexelOffset() * 0.75f;
		}
		Set4TexCoords(vector.x, vector.y, offsetX, offsetY);
		GL.Vertex3(0f, 0f, 0.1f);
		Set4TexCoords(1f + vector.x, vector.y, offsetX, offsetY);
		GL.Vertex3(1f, 0f, 0.1f);
		Set4TexCoords(1f + vector.x, 1f + vector.y, offsetX, offsetY);
		GL.Vertex3(1f, 1f, 0.1f);
		Set4TexCoords(vector.x, 1f + vector.y, offsetX, offsetY);
		GL.Vertex3(0f, 1f, 0.1f);
		GL.End();
	}

	private static void Set4TexCoords(float x, float y, float offsetX, float offsetY)
	{
		GL.MultiTexCoord2(0, x - offsetX, y - offsetY);
		GL.MultiTexCoord2(1, x + offsetX, y - offsetY);
		GL.MultiTexCoord2(2, x + offsetX, y + offsetY);
		GL.MultiTexCoord2(3, x - offsetX, y + offsetY);
	}
}
