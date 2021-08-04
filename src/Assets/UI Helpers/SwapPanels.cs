using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapPanels : MonoBehaviour {

	public RectTransform[] panels;
	private ScrollRect scroll;

	// Use this for initialization
	void Start () {
		scroll = GetComponent<ScrollRect>();

//		if(scroll)
//			Debug.Log("SwapPanels: Found scroll rect");

		for(int i = 1; i < panels.Length; i++){
			panels[i].gameObject.SetActive(false);
		}
	}

	public void setActive(int enabled){
		for(int i = 0; i < panels.Length; i++){
			if(i != enabled)
				panels[i].gameObject.SetActive(false);
			else
				panels[i].gameObject.SetActive(true);
		}

		if(scroll)
			scroll.content = panels[enabled];
	}
}
