using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Vortex")]
public class VortexEffect : ImageEffectBase
{
	//public Vector2 radius = radius;
	public Vector2 radius = new Vector2(0.4F, 0.4F);

	public float angle = 50f;

	//public Vector2 center = center;
	public Vector2 center = new Vector2(0.5F, 0.5F);

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, angle, center, radius);
	}
}
