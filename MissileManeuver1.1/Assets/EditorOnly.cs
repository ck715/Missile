using UnityEngine;
using System.Collections;

public class EditorOnly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
}
