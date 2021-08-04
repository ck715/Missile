using UnityEngine;
using System.Collections;

public class Intellicam : MonoBehaviour {

	protected Missile missile;
	protected Transform _transform;
	
	public float edgeAvoid = 1;

	public float followDistance = 2;
	public float followHeight = 0.5f;
	public float minHeight = 0;
	public float lookAhead = 2;
	protected float maxOffset;

	protected Vector3 position = Vector3.zero;

	// Use this for initialization
	void Start () {
		missile = GameObject.FindGameObjectWithTag("Player").GetComponent<Missile>();
		_transform = transform;

		maxOffset = Mathf.Sqrt(followHeight*followHeight + followDistance*followDistance);

		position = Vector3.zero - followDistance * _transform.forward + followHeight * _transform.up;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		RaycastHit hit;
		// raycast left/right
		if(Physics.Raycast(missile.transform.position, _transform.right, out hit, edgeAvoid)){
			onProximity(hit);
		}
		else if(Physics.Raycast(missile.transform.position, -_transform.right, out hit, edgeAvoid)){
			onProximity(hit);
		}

		// raycast up/down
		if(Physics.Raycast(missile.transform.position, _transform.up, out hit, edgeAvoid)){
			onProximity(hit);
		}
		else if(Physics.Raycast(missile.transform.position, -_transform.up, out hit, edgeAvoid)){
			onProximity(hit);
		}

		position.z = -followDistance;

		if(PlayerPrefs.GetInt("lowerCamera", 0) == 0)
			position.y = Mathf.Max(position.y, minHeight);

		position = position.normalized * maxOffset;
		//position.z = -followDistance;

		_transform.position = missile.transform.position + missile.transform.rotation * position;
		//_transform.rotation = missile.transform.rotation;
		_transform.LookAt(missile.transform.position + missile.transform.forward * lookAhead, missile.transform.up);
	}

	private void onProximity(RaycastHit hit){
		if(hit.collider.gameObject.tag.Equals("Vent"))
			position -= (hit.point - missile.transform.position) * (edgeAvoid - hit.distance) * Time.deltaTime;
	}
}
