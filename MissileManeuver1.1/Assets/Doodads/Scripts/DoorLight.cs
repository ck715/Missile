using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour {

	private Renderer renderer;
	private Light light;
	private Material mat;

	public Color alertColor = new Color(1,0.1f,0.1f);

	private const float fadeInScale = 2;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		mat = renderer.material;

		light = GetComponent<Light>();

		StartCoroutine(fadeIn());
	}
	
	private IEnumerator fadeIn(){
		Color color = mat.color;
		color.a = 0;
		mat.color = color;

		while(mat.color.a < 1){
			color = mat.color;
			color.a += Time.deltaTime * Missile.instance.speed / fadeInScale;
			mat.color = color;
			light.color = color;
			light.intensity = color.a / 3;

			yield return 0;
		}

		yield return 0;
	}

	public void quickShut(){
		GetComponent<Renderer>().material.color = alertColor;
	}
}
