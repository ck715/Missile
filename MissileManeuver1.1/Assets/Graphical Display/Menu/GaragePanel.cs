using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class GaragePanel : MonoBehaviour {

	private RectTransform _transform;
	public float width = 100;
	//public float totalTime = 3;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveOff(float time){
		StartCoroutine(movingOffScreen(time));
	}

	private IEnumerator movingOffScreen(float totalTime){
		float time = 0;

		while(time < totalTime){
			_transform.anchoredPosition += new Vector2(width * Time.deltaTime / totalTime, 0);
			time += Time.deltaTime;
			yield return 0;
		}
	}
}
