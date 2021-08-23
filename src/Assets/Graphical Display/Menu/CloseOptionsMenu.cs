using UnityEngine;
using System.Collections;

public class CloseOptionsMenu : MonoBehaviour {

	public Canvas canvas = null;
	public AudioClip source;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void returnToGame(){

		if(canvas != null)
			Destroy(canvas.gameObject);
			
		else
			Application.LoadLevel("Game Level");

	}
}
