using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class WaterLightmapFog : MonoBehaviour
{
	public float fogDensity;

	public Color fogColor;

	public Color baseColor;

	public float baseMultBlurPixels;

	public float blurOverDrive;

	public float depthAmbient;

	public Vector3 terrainSize;

	public Collider terrainCollider;

	public Texture2D texture;

	public WaterLightmapFog()
	{
		fogDensity = 0f;
		baseMultBlurPixels = 0f;
		blurOverDrive = 0f;
		depthAmbient = 1.5f;
	}

	[ContextMenu("Apply Fog")]
	public void ApplyFog()
	{
		Texture2D texture2D = new Texture2D(texture.width, texture.height);
		float num = 0f;
		float num2 = 0f;
		checked
		{
			for (; num < (float)texture.width; num += 1f)
			{
				for (num2 = 0f; num2 < (float)texture.height; num2 += 1f)
				{
					Vector3 vector = new Vector3(UnityBuiltins.parseFloat(num / (float)texture.width) * terrainSize.x, 400f, UnityBuiltins.parseFloat(num2 / (float)texture.height) * terrainSize.y);
					RaycastHit hitInfo = default(RaycastHit);
					if (terrainCollider.Raycast(new Ray(vector, Vector3.up * -500f), out hitInfo, 500f))
					{
						float num3 = 35.35f - hitInfo.point.y;
						if (num == 256f)
						{
							MonoBehaviour.print(vector);
						}
						if (num3 > 0f)
						{
							Color pixel = texture.GetPixel((int)num, (int)num2);
							Color color = Color.Lerp(pixel, Color.gray, depthAmbient * num3 * fogDensity);
							Vector3 vector2 = new Vector3(Mathf.Pow(fogColor.r, num3 * fogDensity), Mathf.Pow(fogColor.g, num3 * fogDensity), Mathf.Pow(fogColor.b, num3 * fogDensity));
							texture.SetPixel((int)num, (int)num2, new Color(color.r * vector2.x * pixel.a, color.g * vector2.y * pixel.a, color.b * vector2.z * pixel.a, color.a));
							texture2D.SetPixel((int)num, (int)num2, new Color(baseColor.r, baseColor.g, baseColor.b, 1f));
						}
						else
						{
							texture2D.SetPixel((int)num, (int)num2, Color.white);
						}
					}
				}
			}
			for (num = 0f; num < (float)texture.width; num += 1f)
			{
				for (num2 = 0f; num2 < (float)texture.height; num2 += 1f)
				{
					Color color = texture.GetPixel((int)num, (int)num2);
					float num4 = default(float);
					float num5;
					if (baseMultBlurPixels > 0f)
					{
						num4 = 1f / (4f * baseMultBlurPixels) * (1f + blurOverDrive);
						num5 = baseMultBlurPixels;
					}
					else
					{
						num4 = 1f;
						num5 = baseMultBlurPixels;
					}
					Color pixel2 = texture2D.GetPixel((int)Mathf.Clamp(num, 0f, texture.width - 1), (int)Mathf.Clamp(num2, 0f, texture.width - 1));
					color = Color.Lerp(color, new Color(color.r * pixel2.r, color.g * pixel2.g, color.b * pixel2.b, color.a), num4);
					while (num5 > 0f)
					{
						pixel2 = texture2D.GetPixel((int)Mathf.Clamp(num + num5, 0f, texture.width - 1), (int)Mathf.Clamp(num2, 0f, texture.width - 1));
						color = Color.Lerp(color, new Color(color.r * pixel2.r, color.g * pixel2.g, color.b * pixel2.b, color.a), num4);
						pixel2 = texture2D.GetPixel((int)Mathf.Clamp(num - num5, 0f, texture.width - 1), (int)Mathf.Clamp(num2, 0f, texture.width - 1));
						color = Color.Lerp(color, new Color(color.r * pixel2.r, color.g * pixel2.g, color.b * pixel2.b, color.a), num4);
						pixel2 = texture2D.GetPixel((int)Mathf.Clamp(num, 0f, texture.width - 1), (int)Mathf.Clamp(num2 + num5, 0f, texture.width - 1));
						color = Color.Lerp(color, new Color(color.r * pixel2.r, color.g * pixel2.g, color.b * pixel2.b, color.a), num4);
						pixel2 = texture2D.GetPixel((int)Mathf.Clamp(num, 0f, texture.width - 1), (int)Mathf.Clamp(num2 - num5, 0f, texture.width - 1));
						color = Color.Lerp(color, new Color(color.r * pixel2.r, color.g * pixel2.g, color.b * pixel2.b, color.a), num4);
						num5 -= 1f;
					}
					texture.SetPixel((int)num, (int)num2, color);
				}
			}
			texture.Apply();
			UnityEngine.Object.DestroyImmediate(texture2D);
		}
	}

	public void Main()
	{
	}
}
