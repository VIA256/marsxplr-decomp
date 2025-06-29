namespace UnityEngine
{
	public class ScriptableShaderPass : ScriptableObject
	{
		private Material m_Material;

		private Renderer m_Renderer;

		public Renderer renderer
		{
			get
			{
				return m_Renderer;
			}
		}
	}
}
