using UnityEngine;
using System.Collections;

public class CreateOptionsButton : MonoBehaviour {

	private GameObject optionsCanvas = null;
	public AudioClip source;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickOn(){
		GetComponent<AudioSource>().Play ();
		if(optionsCanvas)
			Destroy(optionsCanvas);
		else
			optionsCanvas = Instantiate(Resources.Load ("Menu/Options Canvas")) as GameObject;
	}
}
