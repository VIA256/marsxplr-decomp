using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur")]
public class BlurEffect : MonoBehaviour
{
	public int iterations = 3;

	public float blurSpread = 0.6f;

	private static string blurMatString = "Shader \"BlurConeTap\" {\n\tProperties { _MainTex (\"\", any) = \"\" {} }\n\tSubShader {\n\t\tPass {\n\t\t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\t\tSetTexture [_MainTex] {constantColor (0,0,0,0.25) combine texture * constant alpha}\n\t\t\tSetTexture [_MainTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t\tSetTexture [_MainTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t\tSetTexture [_MainTex] {constantColor (0,0,0,0.25) combine texture * constant + previous}\n\t\t}\n\t}\n\tFallback off\n}";

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

	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
		else if (!material.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * blurSpread;
		Material mat = material;
		//error CS8024
		/*Vector2[] array = new Vector2[4];
		ref Vector2 reference = ref array[0];
		Vector2 vector = new Vector2(0f - num, 0f - num);
		reference = vector;
		ref Vector2 reference2 = ref array[1];
		Vector2 vector2 = new Vector2(0f - num, num);
		reference2 = vector2;
		ref Vector2 reference3 = ref array[2];
		Vector2 vector3 = new Vector2(num, num);
		reference3 = vector3;
		ref Vector2 reference4 = ref array[3];
		Vector2 vector4 = new Vector2(num, 0f - num);
		reference4 = vector4;*/
		Vector2[] array = new Vector2[4] {
			new Vector2(0f - num, 0f - num),
			new Vector2(0f - num, num),
			new Vector2(num, num),
			new Vector2(num, 0f - num)
		};
		Graphics.BlitMultiTap(source, dest, mat, array);
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Material mat = material;
		//error CS8024
		/*Vector2[] array = new Vector2[4];
		ref Vector2 reference = ref array[0];
		Vector2 vector = new Vector2(0f - num, 0f - num);
		reference = vector;
		ref Vector2 reference2 = ref array[1];
		Vector2 vector2 = new Vector2(0f - num, num);
		reference2 = vector2;
		ref Vector2 reference3 = ref array[2];
		Vector2 vector3 = new Vector2(num, num);
		reference3 = vector3;
		ref Vector2 reference4 = ref array[3];
		Vector2 vector4 = new Vector2(num, 0f - num);
		reference4 = vector4;*/
		Vector2[] array = new Vector2[4] {
			new Vector2(0f - num, 0f - num),
			new Vector2(0f - num, num),
			new Vector2(num, num),
			new Vector2(num, 0f - num)
		};
		Graphics.BlitMultiTap(source, dest, mat, array);
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
}
