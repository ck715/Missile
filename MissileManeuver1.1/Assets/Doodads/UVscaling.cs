using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class UVscaling : MonoBehaviour {

	public float xScale = 1;
	public float yScale = 1;

	/*
	void Awake(){
		gameObject.isStatic = false;
	}

	// Use this for initialization
	void Start () {
		gameObject.isStatic = false;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector2[] array = mesh.uv;

		for(int i = 0; i < array.Length; i++){
			array[i].x *= xScale;
			array[i].y *= yScale;
		}

		mesh.uv = array;
	}
	*/
}
