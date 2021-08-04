using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MenuOptions : MonoBehaviour {

	public string name = "lowerCamera";

	private Toggle toggle;

	// Use this for initialization
	void Start () {
		toggle = GetComponent<Toggle>();

		if(PlayerPrefs.GetInt(name, 1) == 1)
			toggle.isOn = true;
		else
			toggle.isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setLowerCamera(bool cam){
		if(toggle.isOn)
			PlayerPrefs.SetInt("lowerCamera", 1);
		else
			PlayerPrefs.SetInt("lowerCamera", 0);

		Debug.Log(PlayerPrefs.GetInt("lowerCamera",-1));
	}

	public void setInvertTouch(bool touch){
		if(toggle.isOn)
			PlayerPrefs.SetInt("invertTouch", 1);
		else
			PlayerPrefs.SetInt("invertTouch", 0);
	}

	public void setInverseVertical(bool vert){
		if(toggle.isOn)
			PlayerPrefs.SetInt("invertVertical", 1);
		else
			PlayerPrefs.SetInt("invertVertical", 0);
	}

	public void setName(bool vert){
		if(toggle.isOn)
			PlayerPrefs.SetInt(name, 1);
		else
			PlayerPrefs.SetInt(name, 0);
	}
}
