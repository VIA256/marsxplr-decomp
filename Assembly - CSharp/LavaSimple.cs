using UnityEngine;

[ExecuteInEditMode]
public class LavaSimple : MonoBehaviour
{
	private void Update()
	{
		if ((bool)base.renderer)
		{
			Material sharedMaterial = base.renderer.sharedMaterial;
			if ((bool)sharedMaterial)
			{
				Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
				float num = sharedMaterial.GetFloat("_WaveScale");
				float num2 = Time.time / 40f;
				Vector3 pos = new Vector3(num2 * vector.x, num2 * vector.y, 0f);
				Vector3 vector2 = new Vector3(1f / num, 1f / num, 1f);
				Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.identity, vector2);
				sharedMaterial.SetMatrix("_WaveMatrix", matrix);
				pos = new Vector3(num2 * vector.z, num2 * vector.w, 0f);
				matrix = Matrix4x4.TRS(pos, Quaternion.identity, vector2 * 0.45f);
				sharedMaterial.SetMatrix("_WaveMatrix2", matrix);
			}
		}
	}
}
