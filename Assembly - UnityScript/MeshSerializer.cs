using System;
using System.IO;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class MeshSerializer : MonoBehaviour
{
	public static Mesh ReadMesh(byte[] bytes)
	{
		if (bytes == null || bytes.Length < 5)
		{
			MonoBehaviour.print("Invalid mesh file!");
			return null;
		}
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
		ushort num = binaryReader.ReadUInt16();
		ushort num2 = binaryReader.ReadUInt16();
		byte b = binaryReader.ReadByte();
		if ((int)num < 0 || (int)num > 64000)
		{
			MonoBehaviour.print("Invalid vertex count in the mesh data!");
			return null;
		}
		if ((int)num2 < 0 || (int)num2 > 64000)
		{
			MonoBehaviour.print("Invalid triangle count in the mesh data!");
			return null;
		}
		if ((int)b < 1 || ((int)b & 1) == 0 || (int)b > 15)
		{
			MonoBehaviour.print("Invalid vertex format in the mesh data!");
			return null;
		}
		Mesh mesh = new Mesh();
		int num3 = default(int);
		Vector3[] array = new Vector3[(int)num];
		ReadVector3Array16bit(array, binaryReader);
		mesh.vertices = array;
		if (((int)b & 2) != 0)
		{
			Vector3[] array2 = new Vector3[(int)num];
			ReadVector3ArrayBytes(array2, binaryReader);
			mesh.normals = array2;
		}
		if (((int)b & 4) != 0)
		{
			Vector4[] array3 = new Vector4[(int)num];
			ReadVector4ArrayBytes(array3, binaryReader);
			mesh.tangents = array3;
		}
		if (((int)b & 8) != 0)
		{
			Vector2[] array4 = new Vector2[(int)num];
			ReadVector2Array16bit(array4, binaryReader);
			mesh.uv = array4;
		}
		checked
		{
			int[] array5 = new int[unchecked((int)num2) * 3];
			for (num3 = 0; num3 < unchecked((int)num2); num3++)
			{
				array5[RuntimeServices.NormalizeArrayIndex(array5, num3 * 3 + 0)] = binaryReader.ReadUInt16();
				array5[RuntimeServices.NormalizeArrayIndex(array5, num3 * 3 + 1)] = binaryReader.ReadUInt16();
				array5[RuntimeServices.NormalizeArrayIndex(array5, num3 * 3 + 2)] = binaryReader.ReadUInt16();
			}
			mesh.triangles = array5;
			binaryReader.Close();
			return mesh;
		}
	}

	public static void ReadVector3Array16bit(Vector3[] arr, BinaryReader buf)
	{
		int length = arr.Length;
		if (length != 0)
		{
			Vector3 vector = default(Vector3);
			Vector3 vector2 = default(Vector3);
			vector.x = buf.ReadSingle();
			vector2.x = buf.ReadSingle();
			vector.y = buf.ReadSingle();
			vector2.y = buf.ReadSingle();
			vector.z = buf.ReadSingle();
			vector2.z = buf.ReadSingle();
			for (int i = 0; i < length; i = checked(i + 1))
			{
				ushort num = buf.ReadUInt16();
				ushort num2 = buf.ReadUInt16();
				ushort num3 = buf.ReadUInt16();
				float x = (float)(int)num / 65535f * (vector2.x - vector.x) + vector.x;
				float y = (float)(int)num2 / 65535f * (vector2.y - vector.y) + vector.y;
				float z = (float)(int)num3 / 65535f * (vector2.z - vector.z) + vector.z;
				arr[RuntimeServices.NormalizeArrayIndex(arr, i)] = new Vector3(x, y, z);
			}
		}
	}

	public static void WriteVector3Array16bit(Vector3[] arr, BinaryWriter buf)
	{
		checked
		{
			if (arr.Length != 0)
			{
				Bounds bounds = new Bounds(arr[0], new Vector3(0.001f, 0.001f, 0.001f));
				int i = 0;
				for (int length = arr.Length; i < length; i++)
				{
					bounds.Encapsulate(arr[i]);
				}
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				buf.Write(min.x);
				buf.Write(max.x);
				buf.Write(min.y);
				buf.Write(max.y);
				buf.Write(min.z);
				buf.Write(max.z);
				int j = 0;
				for (int length2 = arr.Length; j < length2; j++)
				{
					float num = Mathf.Clamp((arr[j].x - min.x) / (max.x - min.x) * 65535f, 0f, 65535f);
					float num2 = Mathf.Clamp((arr[j].y - min.y) / (max.y - min.y) * 65535f, 0f, 65535f);
					float num3 = Mathf.Clamp((arr[j].z - min.z) / (max.z - min.z) * 65535f, 0f, 65535f);
					ushort value = (ushort)num;
					ushort value2 = (ushort)num2;
					ushort value3 = (ushort)num3;
					buf.Write(value);
					buf.Write(value2);
					buf.Write(value3);
				}
			}
		}
	}

	public static void ReadVector2Array16bit(Vector2[] arr, BinaryReader buf)
	{
		int length = arr.Length;
		if (length != 0)
		{
			Vector2 vector = default(Vector2);
			Vector2 vector2 = default(Vector2);
			vector.x = buf.ReadSingle();
			vector2.x = buf.ReadSingle();
			vector.y = buf.ReadSingle();
			vector2.y = buf.ReadSingle();
			for (int i = 0; i < length; i = checked(i + 1))
			{
				ushort num = buf.ReadUInt16();
				ushort num2 = buf.ReadUInt16();
				float x = (float)(int)num / 65535f * (vector2.x - vector.x) + vector.x;
				float y = (float)(int)num2 / 65535f * (vector2.y - vector.y) + vector.y;
				arr[RuntimeServices.NormalizeArrayIndex(arr, i)] = new Vector2(x, y);
			}
		}
	}

	public static void WriteVector2Array16bit(Vector2[] arr, BinaryWriter buf)
	{
		checked
		{
			if (arr.Length != 0)
			{
				Vector2 vector = arr[0] - new Vector2(0.001f, 0.001f);
				Vector2 vector2 = arr[0] + new Vector2(0.001f, 0.001f);
				int i = 0;
				for (int length = arr.Length; i < length; i++)
				{
					vector.x = Mathf.Min(vector.x, arr[i].x);
					vector.y = Mathf.Min(vector.y, arr[i].y);
					vector2.x = Mathf.Max(vector2.x, arr[i].x);
					vector2.y = Mathf.Max(vector2.y, arr[i].y);
				}
				buf.Write(vector.x);
				buf.Write(vector2.x);
				buf.Write(vector.y);
				buf.Write(vector2.y);
				int j = 0;
				for (int length2 = arr.Length; j < length2; j++)
				{
					float num = (arr[j].x - vector.x) / (vector2.x - vector.x) * 65535f;
					float num2 = (arr[j].y - vector.y) / (vector2.y - vector.y) * 65535f;
					ushort value = (ushort)num;
					ushort value2 = (ushort)num2;
					buf.Write(value);
					buf.Write(value2);
				}
			}
		}
	}

	public static void ReadVector3ArrayBytes(Vector3[] arr, BinaryReader buf)
	{
		int length = arr.Length;
		for (int i = 0; i < length; i = checked(i + 1))
		{
			byte b = buf.ReadByte();
			byte b2 = buf.ReadByte();
			byte b3 = buf.ReadByte();
			float x = ((float)(int)b - 128f) / 127f;
			float y = ((float)(int)b2 - 128f) / 127f;
			float z = ((float)(int)b3 - 128f) / 127f;
			arr[RuntimeServices.NormalizeArrayIndex(arr, i)] = new Vector3(x, y, z);
		}
	}

	public static void WriteVector3ArrayBytes(Vector3[] arr, BinaryWriter buf)
	{
		int i = 0;
		checked
		{
			for (int length = arr.Length; i < length; i++)
			{
				byte value = (byte)Mathf.Clamp(arr[i].x * 127f + 128f, 0f, 255f);
				byte value2 = (byte)Mathf.Clamp(arr[i].y * 127f + 128f, 0f, 255f);
				byte value3 = (byte)Mathf.Clamp(arr[i].z * 127f + 128f, 0f, 255f);
				buf.Write(value);
				buf.Write(value2);
				buf.Write(value3);
			}
		}
	}

	public static void ReadVector4ArrayBytes(Vector4[] arr, BinaryReader buf)
	{
		int length = arr.Length;
		for (int i = 0; i < length; i = checked(i + 1))
		{
			byte b = buf.ReadByte();
			byte b2 = buf.ReadByte();
			byte b3 = buf.ReadByte();
			byte b4 = buf.ReadByte();
			float x = ((float)(int)b - 128f) / 127f;
			float y = ((float)(int)b2 - 128f) / 127f;
			float z = ((float)(int)b3 - 128f) / 127f;
			float w = ((float)(int)b4 - 128f) / 127f;
			arr[RuntimeServices.NormalizeArrayIndex(arr, i)] = new Vector4(x, y, z, w);
		}
	}

	public static void WriteVector4ArrayBytes(Vector4[] arr, BinaryWriter buf)
	{
		int i = 0;
		checked
		{
			for (int length = arr.Length; i < length; i++)
			{
				byte value = (byte)Mathf.Clamp(arr[i].x * 127f + 128f, 0f, 255f);
				byte value2 = (byte)Mathf.Clamp(arr[i].y * 127f + 128f, 0f, 255f);
				byte value3 = (byte)Mathf.Clamp(arr[i].z * 127f + 128f, 0f, 255f);
				byte value4 = (byte)Mathf.Clamp(arr[i].w * 127f + 128f, 0f, 255f);
				buf.Write(value);
				buf.Write(value2);
				buf.Write(value3);
				buf.Write(value4);
			}
		}
	}

	public static byte[] WriteMesh(Mesh mesh, bool saveTangents)
	{
		if (!mesh)
		{
			MonoBehaviour.print("No mesh given!");
			return null;
		}
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		Vector4[] tangents = mesh.tangents;
		Vector2[] uv = mesh.uv;
		int[] triangles = mesh.triangles;
		byte b = (byte)1;
		checked
		{
			if (normals.Length > 0)
			{
				b = (byte)(unchecked((int)b) | 2);
			}
			if (saveTangents && tangents.Length > 0)
			{
				b = (byte)(unchecked((int)b) | 4);
			}
			if (uv.Length > 0)
			{
				b = (byte)(unchecked((int)b) | 8);
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			ushort value = (ushort)vertices.Length;
			ushort value2 = (ushort)unchecked(triangles.Length / 3);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write(b);
			WriteVector3Array16bit(vertices, binaryWriter);
			WriteVector3ArrayBytes(normals, binaryWriter);
			if (saveTangents)
			{
				WriteVector4ArrayBytes(tangents, binaryWriter);
			}
			WriteVector2Array16bit(uv, binaryWriter);
			int i = 0;
			int[] array = triangles;
			for (int length = array.Length; i < length; i++)
			{
				ushort value3 = (ushort)array[i];
				binaryWriter.Write(value3);
			}
			binaryWriter.Close();
			return memoryStream.ToArray();
		}
	}

	public static void WriteMeshToFileForWeb(Mesh mesh, string name, bool saveTangents)
	{
		byte[] array = WriteMesh(mesh, saveTangents);
		FileStream fileStream = new FileStream(name, FileMode.Create);
		fileStream.Write(array, 0, array.Length);
		fileStream.Close();
	}

	public static Mesh ReadMeshFromWWW(WWW download)
	{
		if (download.error != null)
		{
			MonoBehaviour.print("Error downloading mesh: " + download.error);
			return null;
		}
		if (!download.isDone)
		{
			MonoBehaviour.print("Download must be finished already");
			return null;
		}
		byte[] bytes = download.bytes;
		return ReadMesh(bytes);
	}

	public void Main()
	{
	}
}
