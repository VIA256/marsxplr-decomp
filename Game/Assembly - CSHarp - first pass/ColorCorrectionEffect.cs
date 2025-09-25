using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Correction")]
public class ColorCorrectionEffect : ImageEffectBase
{
	public Texture textureRamp;

	public float rampOffsetR;

	public float rampOffsetG;

	public float rampOffsetB;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", textureRamp);
		Material obj = base.material;
		Vector4 vector = new Vector4(rampOffsetR, rampOffsetG, rampOffsetB, 0f);
		obj.SetVector("_RampOffset", vector);
		Graphics.Blit(source, destination, base.material);
	}
}
