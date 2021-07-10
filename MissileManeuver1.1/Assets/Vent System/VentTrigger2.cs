using UnityEngine;
using System.Collections;

public class VentTrigger2 : MonoBehaviour {

	private int laserPoints = 2;
	private float laserDistance = 24;

	private static GameObject laserLoad = null;

	// Use this for initialization
	void Start () {
		GetComponent<Collider>().isTrigger = true;

		if(Missile.instance.getPoints() > laserPoints){
			if(laserLoad == null)
				laserLoad = Resources.Load("Prefabs/Obstacles/Laser") as GameObject;

			GameObject laser = GameObject.Instantiate(laserLoad, transform.position + transform.forward * laserDistance, transform.rotation) as GameObject;
			laser.transform.parent = transform.parent;
			if(Mathf.RoundToInt(Random.Range(0,2)) == 1){
				laser.transform.rotation *= Quaternion.Euler(0,0,90);
			}
		}
	}
	
	// generate next vents
	void OnTriggerEnter(Collider collider){
		if(!collider.tag.Equals("Player"))
			return;

		collider.GetComponent<MissileControl>().correctTurning(transform.rotation);

		if(transform.parent.parent != null){
			GameObject parent = findParent(transform.parent).gameObject;

			transform.parent.parent = null;
			Destroy(parent);
		}
		
		transform.parent.gameObject.BroadcastMessage("enterVent");
	}


	void OnTriggerStay(Collider collider){
		if(!collider.tag.Equals("Player"))
			return;
		
		collider.GetComponent<MissileControl>().correctTurning(transform.rotation);
	}

	private Transform findParent(Transform parent){
		if(parent.parent == null)
			return parent;

		return findParent(parent.parent);
	}
}
