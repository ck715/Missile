using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JunctionManager : MonoBehaviour {

	public GameObject[] passages;
	public int maxOpen = 2;
	public int minOpen = 1;

	// only have one random number generator to avoid getting the same results each time
	private static System.Random rand;

	// Use this for initialization
	void Start () {
		if(rand == null)
			rand = new System.Random();

		shutPassages();
	}

	private void shutPassages(){
		int numShut = rand.Next(passages.Length - maxOpen, passages.Length - minOpen+1);
		List<int> shutting = new List<int>();

		// shut random vents but always leave one open
		for(int i = 0; i < numShut; i++){
			int shut = 0;

			// don't close a vent already closing
			do{
				shut = rand.Next(passages.Length);
			}while(shutting.Contains(shut));

			shutting.Add(shut);

//			Debug.Log(shut);

			passages[shut].BroadcastMessage("quickShut");
		}

//		Debug.Log("--");
	}
}
