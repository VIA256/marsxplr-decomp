using UnityEngine;

public class WaterWaveScroll : MonoBehaviour
{
	private string oldWaterMode = "";

	private void Update()
	{
		if (!base.renderer)
		{
			return;
		}
		Material material = base.renderer.material;
		if (!material)
		{
			return;
		}
		Vector4 vector = material.GetVector("WaveSpeed");
		float num = material.GetFloat("_WaveScale");
		float num2 = Time.time / 40f;
		Vector3 pos = new Vector3(num2 * vector.x, num2 * vector.y, 0f);
		Vector3 vector2 = new Vector3(1f / num, 1f / num, 1f);
		Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.identity, vector2);
		material.SetMatrix("_WaveMatrix", matrix);
		pos = new Vector3(num2 * vector.z, num2 * vector.w, 0f);
		matrix = Matrix4x4.TRS(pos, Quaternion.identity, vector2 * 0.45f);
		material.SetMatrix("_WaveMatrix2", matrix);
		string text = material.GetTag("WATERMODE", false);
		if (text != oldWaterMode)
		{
			Component[] componentsInChildren = GetComponentsInChildren(typeof(Camera));
			Component[] array = componentsInChildren;
			int num3 = array.Length;
			for (int i = 0; i < num3; i++)
			{
				Camera camera = (Camera)array[i];
				camera.SendMessage("ExternalSetMode", text, SendMessageOptions.DontRequireReceiver);
			}
			oldWaterMode = text;
		}
	}
}
