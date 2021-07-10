using UnityEngine;
using System.Collections;

public class NikinikiEngine : MissileEngine {

	public float maxSpeedUp = 0.4f;
	public float minSpeedUp = 0.2f;

	public float minChangeTime = 1;
	public float maxChangeTime = 2;

	// Use this for initialization
	void Start () {
		setMissile(GameObject.FindGameObjectWithTag("Player").GetComponent<Missile>());
		StartCoroutine(speedModifier());
	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator speedModifier(){
		float wait = 1;

		yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));

		while(true){
			float speed = Random.Range (minSpeedUp, maxSpeedUp) * missile.speed;

			do{
				wait -= Time.deltaTime;
				missile.speed += Time.deltaTime * speed;
				yield return 0;
			} while(wait > 0);

			float stayTime = Random.Range(minChangeTime, maxChangeTime);
			yield return new WaitForSeconds(stayTime);

			do{
				wait += Time.deltaTime;
				missile.speed -= Time.deltaTime * speed;
			}while(wait < 1);
		}
	}
}
