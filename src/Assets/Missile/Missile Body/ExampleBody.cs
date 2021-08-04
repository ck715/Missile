using UnityEngine;
using System.Collections;

public class ExampleBody : MissileBody {

	// save base stats so that you can return to them
	protected float tempFriction;
	protected float tempTurnDecel;

	// amount to change friction and turn deceleration
	public float unFriction = 0.25f;
	public float turnAccel = 0.25f;

	// negative value indicates acceleration instead of deceleration
	public float minTurnDecel = -0.5f;

	// last time the missile turned
	protected float lastTurnTime = 0;

	// delay after turning when friction and turnDecel are reset
	public float delayAccelTime = 0.2f;

	// Awake is called before Start and does not promise other components have been initialized
	void Awake () {
		tempFriction = friction;
		tempTurnDecel = turnDecel;
	}
	
	// LateUpdate is called once per frame after Update
	void LateUpdate () {
		if(Time.time - lastTurnTime > delayAccelTime){
			friction = Mathf.Min(friction + unFriction * Time.deltaTime, tempFriction);
			turnDecel = Mathf.Min(turnDecel + unFriction * Time.deltaTime, tempTurnDecel);
		}
	}

	public override void onTurn(ref Vector2 direction){
		// use this for events that happen when turning
		friction -= unFriction * Time.deltaTime;
		turnDecel -= turnAccel * Time.deltaTime;
		turnDecel = Mathf.Max(turnDecel, minTurnDecel);
	}
}
