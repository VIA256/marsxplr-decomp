using System;
using System.Runtime.CompilerServices;
using Boo.Lang;

namespace UnityEngine
{
	public class Material : Object
	{
		public extern Shader shader
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color color
		{
			get
			{
				return GetColor("_Color");
			}
			set
			{
				SetColor("_Color", value);
			}
		}

		[DuckTyped]
		public Texture mainTexture
		{
			get
			{
				return GetTexture("_MainTex");
			}
			set
			{
				SetTexture("_MainTex", value);
			}
		}

		public Vector2 mainTextureOffset
		{
			get
			{
				return GetTextureOffset("_MainTex");
			}
			set
			{
				SetTextureOffset("_MainTex", value);
			}
		}

		public Vector2 mainTextureScale
		{
			get
			{
				return GetTextureScale("_MainTex");
			}
			set
			{
				SetTextureScale("_MainTex", value);
			}
		}

		public extern int passCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int renderQueue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Material(string contents)
		{
			Internal_CreateWithString(this, contents);
		}

		public Material(Shader shader)
		{
			Internal_CreateWithShader(this, shader);
		}

		public Material(Material source)
		{
			Internal_CreateWithMaterial(this, source);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetColor(string propertyName, Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetColor(string propertyName);

		public void SetVector(string propertyName, Vector4 vector)
		{
			Color color = new Color(vector.x, vector.y, vector.z, vector.w);
			SetColor(propertyName, color);
		}

		public Vector4 GetVector(string propertyName)
		{
			Color color = GetColor(propertyName);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(string propertyName, Texture texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureOffset(Material mat, string name, out Vector2 output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureScale(Material mat, string name, out Vector2 output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexturePivot(Material mat, string name, out Vector2 output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureOffset(string propertyName, Vector2 offset);

		public Vector2 GetTextureOffset(string propertyName)
		{
			Vector2 output;
			Internal_GetTextureOffset(this, propertyName, out output);
			return output;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureScale(string propertyName, Vector2 scale);

		public Vector2 GetTextureScale(string propertyName)
		{
			Vector2 output;
			Internal_GetTextureScale(this, propertyName, out output);
			return output;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMatrix(string propertyName, Matrix4x4 matrix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Matrix4x4 GetMatrix(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(string propertyName, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProperty(string propertyName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetTag(string tag, bool searchFallbacks, string defaultValue);

		public string GetTag(string tag, bool searchFallbacks)
		{
			string defaultValue = "";
			return GetTag(tag, searchFallbacks, defaultValue);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Lerp(Material start, Material end, float t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPass(int pass);

		[Obsolete("Use the Material constructor instead.")]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithString(Material mono, string contents);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithShader(Material mono, Shader shader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithMaterial(Material mono, Material source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyPropertiesFromMaterial(Material mat);
	}
}
