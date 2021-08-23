using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShopScoreText : MonoBehaviour {

	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" + PlayerPrefs.GetInt("Points", 0);
	}
}
