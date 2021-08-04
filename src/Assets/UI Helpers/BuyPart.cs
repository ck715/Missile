using UnityEngine;
using System.Collections;

public class BuyPart : MonoBehaviour {

	public string partName;
	public AudioSource source;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buyPart(){
		if(!hasPart()){
			Debug.Log("Buy part: " + partName);
			PlayerPrefs.SetInt(partName, 1);
		}
		else
			Debug.Log("Has part: " + partName);
	}

	public bool hasPart(){
		return PlayerPrefs.GetInt(partName, 0) == 1;
		//return true;
	}
}
