using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateJunction : MonoBehaviour {

	private static GameObject[] junctions;
	private static System.Random random;

	private bool triggered = false;

	// Use this for initialization
	void Start () {
		if(junctions == null){
			List<GameObject> list = new List<GameObject>();

			// junctions go here
			list.Add(Resources.Load("Prefabs/Junction/Junction Plus", typeof(GameObject)) as GameObject);

			junctions = list.ToArray();
		}

		if(random == null)
			random = new System.Random();
	}

	public void createJunction(){
		GameObject junction = Instantiate(getJunction()) as GameObject;
		junction.transform.parent = this.transform;
		junction.transform.position = this.transform.position;
		junction.transform.localPosition = Vector3.zero;
		junction.transform.rotation = this.transform.rotation;
	}

	private GameObject getJunction(){
		return junctions[0];
		//return junctions[random.Next(0, junctions.Length-1)];
	}

	// broadcast function
	void enterVent(){
		if(!triggered){
			createJunction();
			triggered = true;
		}
	}
}
