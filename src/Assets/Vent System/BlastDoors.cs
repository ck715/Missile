using UnityEngine;
using System.Collections;

public class BlastDoors : MonoBehaviour {

	private bool open = true;
	private bool closed = false;
	private float duration = 1;
	private float startTime = 0;

	private float gap = 4;
	public float distance = 20;

	// shut in y direction
	public GameObject door1, door2;

	// shut in x direction
	public GameObject door3, door4;

	public bool closeOnStart = false;

	public static bool enableCloseOnStart = true;
	public float editorCheck = 0;

	private bool close1 = true;
	private System.Random rand;
	public bool closeAll = false;

	// Use this for initialization
	void Start () {
		if(rand == null)
			rand = new System.Random();

		if(!closeAll)
		close1 = rand.Next() % 2 == 0;

		editorCheck = gap;

		if(closeOnStart && enableCloseOnStart){
		//	distance = 35;
			slowShut();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!open && !closed){
			float amt = 1/duration * Missile.instance.speed / distance * Time.deltaTime;

			if(editorCheck - amt < 0){
				amt = editorCheck;

				closed = true;
			}

			editorCheck -= amt;

			if(close1 || closeAll){
				// shut in y direction
				door1.transform.position -=  transform.rotation * new Vector3(0,1,0) * amt;
				door2.transform.position += transform.rotation * new Vector3(0,1,0) * amt;
			}
			if(!close1 || closeAll){
				// shut in x direction
				door3.transform.position += transform.rotation * new Vector3(1,0,0) * amt;
				door4.transform.position -= transform.rotation * new Vector3(1,0,0) * amt;
			}
		}
	}

	public void quickShut(){
		if(closed)
			return;

		startTime = Time.time;
		duration = 0.2f;
		open = false;
	}

	public void slowShut(){
		if(closed)
			return;

		startTime = Time.time;
		duration = 1.25f;
		open = false;
	}
}
