using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LaserStand : MonoBehaviour {

	private int movingPoints = 35;

	private float move_width = 5;
	private bool moveRight = false;
	public AudioSource audiolaser;
	private float position = 0;

	private float speed = 0;
	private float speedModifier = 0.3f;


	
	// Use this for initialization
	void Start () {
		position = Random.Range(-move_width, move_width);

		transform.position += transform.right * position;

		if(position < 0)
			moveRight = true;

		speed = Missile.instance.speed * speedModifier;

		if(Missile.instance.getPoints() > movingPoints)
			StartCoroutine(moving ());


	}
	
	private IEnumerator moving(){
		//Debug.Log("Speed: " + speed);
		yield return 0;

		while(true){
			if(moveRight){
				GetComponent<Rigidbody>().velocity = transform.right * speed;
				yield return new WaitForSeconds((move_width - position) / speed);
				position = move_width;
			}
			else{
				GetComponent<Rigidbody>().velocity = transform.right * -speed;
				yield return new WaitForSeconds((position+move_width) / speed);
				position = -move_width;
			}


			moveRight = !moveRight;
//			Debug.Log("LASERS!");
		}
	}


	
}
