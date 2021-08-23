using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public float time = 1.5f;
	public float height = 100;
	public AudioClip source;

	private RectTransform _transform;


	void Start(){
		_transform = GetComponent<RectTransform>();
	}

	public void startGame(){
		GetComponent<RectTransform>().parent.BroadcastMessage("moveOff", time, SendMessageOptions.DontRequireReceiver);
		Camera.main.GetComponent<GarageCamera>().onGameStart(time-0.5f);
		GetComponent<AudioSource>().Play ();
		Destroy(GameObject.FindGameObjectWithTag("Options"));

	}



	public void moveOff(float timer){
		StartCoroutine(movingOffScreen(timer));

	}
	
	private IEnumerator movingOffScreen(float totalTime){
		float time = 0;

		while(time < totalTime){
			_transform.anchoredPosition += new Vector2(0,height * Time.deltaTime / totalTime);
			time += Time.deltaTime;
			yield return 0;
		}
	}
	

}
