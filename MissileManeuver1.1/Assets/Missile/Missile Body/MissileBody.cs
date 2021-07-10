using UnityEngine;
using System.Collections;

public class MissileBody : MonoBehaviour {

	public float friction = 0.01f;
	public float powerSteering = 1;
	public float turnDecel = 0.1f;

	protected Missile missile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// use this for events that happen periodically
	}

	public virtual void onTurn(ref Vector2 direction){
		// use this for events that happen when turning
	}

	public void setMissile(Missile _missile){
		missile = _missile;
	}
}
