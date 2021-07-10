using UnityEngine;
using System.Collections;

public class RotatingLight : MonoBehaviour {

	protected Transform _transform;

	public float rotationSpeed = 180;

	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		_transform.rotation *= Quaternion.Euler(new Vector3(0,rotationSpeed * Time.deltaTime,0));
	}
}
