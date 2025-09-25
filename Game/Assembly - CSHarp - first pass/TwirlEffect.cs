using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Twirl")]
public class TwirlEffect : ImageEffectBase
{
	//public Vector2 radius = radius;
	public Vector2 radius = new Vector2(0.3F, 0.3F);

	public float angle = 50f;

	//public Vector2 center = center;
	public Vector2 center = new Vector2(0.5F, 0.5F);

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, angle, center, radius);
	}
}
