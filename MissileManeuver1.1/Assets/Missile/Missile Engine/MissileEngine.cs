using UnityEngine;
using System.Collections;

public class MissileEngine : MonoBehaviour {

	public float acceleration = 1f;
	public float powerSteering = 0.9f;

	protected Missile missile;

	// Use this for initialization
	void Start () {
		setMissile(GameObject.FindGameObjectWithTag("Player").GetComponent<Missile>());
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
		Material mat = missile.body.transform.GetChild(0).GetComponent<MeshRenderer>().material;
		transform.GetChild (0).GetComponent<MeshRenderer> ().material = mat;

	}
}
