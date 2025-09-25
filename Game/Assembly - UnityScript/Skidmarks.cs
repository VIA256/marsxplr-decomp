using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class Skidmarks : MonoBehaviour
{
	private int maxMarks;

	public float markWidth;

	private float updateRate;

	private int indexShift;

	private int numMarks;

	private float updateTime;

	private bool newTrackFlag;

	private bool updateMeshFlag;

	private MeshFilter meshFilter;

	private Mesh mesh;

	public markSection[] skidmarks;

	public Skidmarks()
	{
		maxMarks = 256;
		markWidth = 0.225f;
		updateRate = 0.2f;
		indexShift = 0;
		numMarks = 0;
		updateTime = 0f;
		newTrackFlag = true;
		updateMeshFlag = true;
	}

	public void Start()
	{
		skidmarks = new markSection[maxMarks];
		for (int i = 0; i < maxMarks; i = checked(i + 1))
		{
			markSection[] array = skidmarks;
			array[RuntimeServices.NormalizeArrayIndex(array, i)] = new markSection();
		}
		meshFilter = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
		mesh = meshFilter.mesh;
		if (mesh == null)
		{
			mesh = new Mesh();
		}
	}

	public int AddSkidMark(Vector3 pos, Vector3 normal, float intensity, int lastIndex)
	{
		intensity = Mathf.Clamp01(intensity);
		int num = numMarks;
		if (lastIndex != -1 && !newTrackFlag)
		{
			num = lastIndex;
		}
		markSection[] array = skidmarks;
		markSection markSection2 = array[RuntimeServices.NormalizeArrayIndex(array, num % maxMarks)];
		markSection2.pos = pos + normal * 0.05f - transform.position;
		markSection2.normal = normal;
		markSection2.intensity = intensity;
		if (lastIndex == -1 || newTrackFlag)
		{
			markSection2.lastIndex = lastIndex;
		}
		if (markSection2.lastIndex != -1)
		{
			markSection[] array2 = skidmarks;
			markSection markSection3 = array2[RuntimeServices.NormalizeArrayIndex(array2, markSection2.lastIndex % maxMarks)];
			Vector3 lhs = markSection2.pos - markSection3.pos;
			Vector3 normalized = Vector3.Cross(lhs, normal).normalized;
			markSection2.posl = markSection2.pos + normalized * markWidth * 0.5f;
			markSection2.posr = markSection2.pos - normalized * markWidth * 0.5f;
			if (markSection3.lastIndex == -1)
			{
				markSection3.posl = markSection2.pos + normalized * markWidth * 0.5f;
				markSection3.posr = markSection2.pos - normalized * markWidth * 0.5f;
			}
		}
		checked
		{
			if (lastIndex == -1 || newTrackFlag)
			{
				numMarks++;
			}
			updateMeshFlag = true;
			return num;
		}
	}

	public void UpdateMesh()
	{
		int num = 0;
		checked
		{
			for (int i = 0; i < numMarks && i < maxMarks; i++)
			{
				markSection[] array = skidmarks;
				if (array[RuntimeServices.NormalizeArrayIndex(array, i)].lastIndex != -1)
				{
					markSection[] array2 = skidmarks;
					if (array2[RuntimeServices.NormalizeArrayIndex(array2, i)].lastIndex > numMarks - maxMarks)
					{
						num++;
					}
				}
			}
			Vector3[] array3 = new Vector3[num * 4];
			Vector3[] array4 = new Vector3[num * 4];
			Color[] array5 = new Color[num * 4];
			Vector2[] array6 = new Vector2[num * 4];
			int[] array7 = new int[num * 6];
			num = 0;
			for (int i = 0; i < numMarks && i < maxMarks; i++)
			{
				markSection[] array8 = skidmarks;
				if (array8[RuntimeServices.NormalizeArrayIndex(array8, i)].lastIndex != -1)
				{
					markSection[] array9 = skidmarks;
					if (array9[RuntimeServices.NormalizeArrayIndex(array9, i)].lastIndex > numMarks - maxMarks)
					{
						markSection[] array10 = skidmarks;
						markSection markSection2 = array10[RuntimeServices.NormalizeArrayIndex(array10, i)];
						markSection[] array11 = skidmarks;
						markSection markSection3 = array11[RuntimeServices.NormalizeArrayIndex(array11, unchecked(markSection2.lastIndex % maxMarks))];
						array3[RuntimeServices.NormalizeArrayIndex(array3, num * 4 + 0)] = markSection3.posl;
						array3[RuntimeServices.NormalizeArrayIndex(array3, num * 4 + 1)] = markSection3.posr;
						array3[RuntimeServices.NormalizeArrayIndex(array3, num * 4 + 2)] = markSection2.posl;
						array3[RuntimeServices.NormalizeArrayIndex(array3, num * 4 + 3)] = markSection2.posr;
						array4[RuntimeServices.NormalizeArrayIndex(array4, num * 4 + 0)] = markSection3.normal;
						array4[RuntimeServices.NormalizeArrayIndex(array4, num * 4 + 1)] = markSection3.normal;
						array4[RuntimeServices.NormalizeArrayIndex(array4, num * 4 + 2)] = markSection2.normal;
						array4[RuntimeServices.NormalizeArrayIndex(array4, num * 4 + 3)] = markSection2.normal;
						array5[RuntimeServices.NormalizeArrayIndex(array5, num * 4 + 0)] = new Color(1f, 1f, 1f, markSection3.intensity);
						array5[RuntimeServices.NormalizeArrayIndex(array5, num * 4 + 1)] = new Color(1f, 1f, 1f, markSection3.intensity);
						array5[RuntimeServices.NormalizeArrayIndex(array5, num * 4 + 2)] = new Color(1f, 1f, 1f, markSection2.intensity);
						array5[RuntimeServices.NormalizeArrayIndex(array5, num * 4 + 3)] = new Color(1f, 1f, 1f, markSection2.intensity);
						array6[RuntimeServices.NormalizeArrayIndex(array6, num * 4 + 0)] = new Vector2(0f, 0f);
						array6[RuntimeServices.NormalizeArrayIndex(array6, num * 4 + 1)] = new Vector2(1f, 0f);
						array6[RuntimeServices.NormalizeArrayIndex(array6, num * 4 + 2)] = new Vector2(0f, 0f);
						array6[RuntimeServices.NormalizeArrayIndex(array6, num * 4 + 3)] = new Vector2(1f, 0f);
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 0)] = num * 4 + 0;
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 1)] = num * 4 + 1;
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 2)] = num * 4 + 2;
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 3)] = num * 4 + 2;
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 4)] = num * 4 + 1;
						array7[RuntimeServices.NormalizeArrayIndex(array7, num * 6 + 5)] = num * 4 + 3;
						num++;
					}
				}
			}
			mesh.Clear();
			mesh.vertices = array3;
			mesh.normals = array4;
			mesh.triangles = array7;
			mesh.colors = array5;
			mesh.uv = array6;
			updateMeshFlag = false;
		}
	}

	public void Update()
	{
		if (updateMeshFlag)
		{
			UpdateMesh();
		}
	}

	public void FixedUpdate()
	{
		newTrackFlag = false;
		updateTime += Time.deltaTime;
		if (updateTime > updateRate)
		{
			newTrackFlag = true;
			updateTime -= updateRate;
		}
	}

	public void Main()
	{
		new RequireComponent(typeof(MeshFilter));
		new RequireComponent(typeof(MeshRenderer));
	}
}
