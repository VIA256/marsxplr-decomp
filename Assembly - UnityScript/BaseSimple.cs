using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class BaseSimple : MonoBehaviour {
	private Material mat;
	public bool upMode;

	public void Start(){
		mat = (Material)RuntimeServices.Coerce(RuntimeServices.GetProperty(GetComponent(typeof(MeshRenderer)), "material"), typeof(Material));
		mat.mainTextureScale.x = 1;
		mat.mainTextureScale.y = 0.1f;
	}

	public void Update(){
		transform.localScale = Vector3.one * Mathf.Max(0.5f, Mathf.Min(10, Vector3.Distance(transform.position, Camera.main.transform.position) / 10f));
		
		transform.localEulerAngles.y += Time.deltaTime * 10;
		if(transform.localEulerAngles.y > 360){
			transform.localEulerAngles.y -= 360;
		}
		
		mat.mainTextureOffset.x += Time.deltaTime * 0.5f;
		if(mat.mainTextureOffset.x > 1){
			mat.mainTextureOffset.x--;
		}
		
		if(upMode){
			mat.mainTextureOffset.y += Time.deltaTime * 0.1f;
			if(mat.mainTextureOffset.y < 0.4f){
				upMode = true;
			}
		}
	}

	public void Main(){}
}
