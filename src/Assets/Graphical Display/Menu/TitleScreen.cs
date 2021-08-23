using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TitleScreen : MonoBehaviour {

	public float holdTime = 2;
	public float fadeTime = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine(fadeInOut());
	}
	
	private IEnumerator fadeInOut(){
		Image img = GetComponent<Image>();
		Color color = img.color;
		color.a = 0;

		img.color = color;

		yield return 0;

		while(color.a < 1){
			color.a += Time.deltaTime / fadeTime;
			img.color = color;

			yield return 0;
		}

		yield return new WaitForSeconds(holdTime);

		while(color.a > 0){
			color.a -= Time.deltaTime / fadeTime;
			img.color = color;
			
			yield return 0;
		}

		Application.LoadLevel("Garage");
	}
}
