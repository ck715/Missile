using UnityEngine;
using System.Collections;

public class PointsText : MonoBehaviour {

	public float time = 1.5f;
	public float height = 100;

	private RectTransform _transform;
	
	void Start(){
		_transform = GetComponent<RectTransform>();
		((RectTransform)(_transform.parent)).anchoredPosition -= new Vector2(height,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveOff(float timer){
		StartCoroutine(movingOffScreen(timer));
	}
	
	private IEnumerator movingOffScreen(float totalTime){
		float time = 0;
		
		while(time < totalTime){
			((RectTransform)(_transform.parent)).anchoredPosition += new Vector2(height * Time.deltaTime / totalTime,0);
			time += Time.deltaTime;
			yield return 0;
		}
	}
}
