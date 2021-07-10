using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateVent : MonoBehaviour {
//	private static GameObject[] vents;
	private static GameObject vent;
	private static System.Random random;
	
	// Use this for initialization
	void Start () {
		if(vent == null){
			/*List<GameObject> list = new List<GameObject>();
			
			// junctions go here
			list.Add(Resources.Load("Prefabs/Vent/Vent", typeof(GameObject)) as GameObject);
			
			vents = list.ToArray();
			*/
			vent = Resources.Load("Prefabs/Vent/Vent", typeof(GameObject)) as GameObject;
		}
		
		/*if(random == null)
			random = new System.Random();*/

		createVent();
	}
	
	public void createVent(){
		GameObject vent_ = GameObject.Instantiate(vent, transform.position, transform.rotation) as GameObject;
		vent_.transform.parent = this.transform;
	}
	
	private GameObject getVent(){
		//return vents[random.Next(0, vents.Length-1)];
		return vent;
	}
}
