using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetText : MonoBehaviour {

	public string pref = "Points";

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "" + PlayerPrefs.GetInt(pref, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
