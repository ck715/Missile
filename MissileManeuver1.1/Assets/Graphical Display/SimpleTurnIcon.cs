using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleTurnIcon : MonoBehaviour {

	private Image img;
	private bool started = false;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();

		if(PlayerPrefs.GetInt("Simple Turn", 1) == 0)
			disable();
		else
			enable();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!started){
			if(PlayerPrefs.GetInt("Simple Turn", 1) == 0)
				disable();
			else
				enable();
		}
	}

	private void enable(){
		Color color = img.color;
		color.a = 1;

		img.color = color;
	}

	private void disable(){
		Color color = img.color;
		color.a = 0;
		
		img.color = color;
	}

	public void moveOff(){
		started = true;
		StartCoroutine(vanish());
	}

	private IEnumerator vanish(){
		yield return new WaitForSeconds(3);

		Color color = img.color;

		while(img.color.a > 0){
			color.a -= Time.deltaTime / 2f;
			img.color = color;

			yield return 0;
		}

		Destroy(gameObject);
	}
}
