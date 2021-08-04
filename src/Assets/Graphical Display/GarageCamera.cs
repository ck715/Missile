using UnityEngine;
using System.Collections;

public class GarageCamera : MonoBehaviour {

	public float distance = 10;
	public float forwardPosition = 2;
	public GameObject orbit;

	public float slow = 10;
	private float xSpeed = 0;
	private float ySpeed = 0;

	private float[] lastPos;
	private bool mouseDown = false;

	public Vector3 rotation;

	private Vector2 startAccel = Vector2.zero;
	public float maxSpeed = 1;
	public float controllerCutoff = 0.1f;

	public float openWidth = 0.7f;

	private bool countdown = false;
	private bool gameStarted = false;

	public float edgeAvoid = 2;
	public float minHeight = 2;
	public float maxRotation = 5f;

	public float lastAvoidTime = 0;
	public float avoidTime = 0.5f;

	// Use this for initialization
	void Start () {
		lastPos = new float[2];
		transform.LookAt(orbit.transform.position);
		rotation = transform.rotation.eulerAngles;

		startAccel = new Vector2(-Input.acceleration.x, Input.acceleration.y);

		// remove Smart Camera option
		PlayerPrefs.SetInt("lowerCamera", 0);

		// lower frame rate for mobile
	//	if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
	//		Application.targetFrameRate = 40;
	}

	// Update is called once per frame
	void LateUpdate () {
		// non-countdown garage Camera
		if(!countdown){
			garageCamera();
		}

		// Game Camera
		if(gameStarted){
			gameCamera();
		}

		// place camera
		rotation.y = (rotation.y + xSpeed * Time.deltaTime) % 360;
		rotation.x = (rotation.x + ySpeed * Time.deltaTime) % 360;

		rotation.z = 0;

		if(float.IsNaN(rotation.x)){
			Debug.Log("Camera Rotation NaN");

			rotation.x = 0;
			rotation.y = 0;
			rotation.z = 0;
		}

		xSpeed = Mathf.Lerp(xSpeed, 0, slow * Time.deltaTime);
		ySpeed = Mathf.Lerp(ySpeed, 0, slow * Time.deltaTime);

		//transform.LookAt(orbit.transform, orbit.transform.up);
		if(!gameStarted){
			transform.rotation = Quaternion.Euler(rotation) * orbit.transform.rotation;
			transform.position = orbit.transform.position + orbit.transform.forward * forwardPosition - transform.forward * distance;
		}
		else
			transform.localRotation = Quaternion.Euler(rotation);

	}

	private void garageCamera(){
		bool moving = false;

		Vector2 accelerometer = new Vector2(-Input.acceleration.x, Input.acceleration.y) - startAccel;
		
		// if left mouse button is down
		if(Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width * openWidth){
			// set mousePos center of screen to 0,0,0
			Vector3 mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height/2, 0);
			mousePos.y *= -1;
			
			
			
			if(Input.GetMouseButtonDown(0))
				mouseDown = true;
			else if(Input.GetMouseButtonUp(1))
				mouseDown = false;
			else{
				// if not just pressing the mouse button
				if(!mouseDown){
					mouseDown = true;
				}
				else{
					xSpeed = (mousePos.x - lastPos[0]) / Time.deltaTime;
					ySpeed = (mousePos.y - lastPos[1]) / Time.deltaTime;
				}
			}
			
			lastPos[0] = mousePos.x;
			lastPos[1] = mousePos.y;
			
			moving = true;
		}
		else{
			lastPos[0] = 0;
			lastPos[1] = 0;
			
			mouseDown = false;
		}
		
		// if no accelerometer, use Xbox controller, WSAD, or arrow keys
		float horizontal = Input.GetAxis("Horizontal 2");
		float vertical = -1* Input.GetAxis("Vertical 2");
		
		if(Mathf.Abs(horizontal) > controllerCutoff || Mathf.Abs(vertical) > controllerCutoff){
			xSpeed = horizontal * maxSpeed;
			ySpeed = vertical * maxSpeed;
			
			moving = true;
		}
		
		// if mobile and not touching screen, use accelerometer
		if(!moving && accelerometer.magnitude > controllerCutoff){
			xSpeed = accelerometer.x * maxSpeed;
			ySpeed = accelerometer.y * maxSpeed;
		}
		else
			startAccel = new Vector2(-Input.acceleration.x, Input.acceleration.y);
	}

	private void gameCamera(){
		RaycastHit hit;
		// raycast left/right
		if(Physics.Raycast(orbit.transform.position, transform.right, out hit, edgeAvoid)){
			onProximity(hit);
		}
		else if(Physics.Raycast(orbit.transform.position, -transform.right, out hit, edgeAvoid)){
			onProximity(hit);
		}
		
		// raycast up/down
		if(Physics.Raycast(orbit.transform.position, transform.up, out hit, edgeAvoid)){
			onProximity(hit);
		}
		else if(Physics.Raycast(orbit.transform.position, -transform.up, out hit, edgeAvoid)){
			onProximity(hit);
		}

		// move back to default position
		if(Time.time - lastAvoidTime > avoidTime){
			rotation.x = Mathf.Lerp(rotation.x, 4.6f, Time.deltaTime);
			rotation.y = Mathf.Lerp(rotation.y, 0, Time.deltaTime);
		}
		
		rotation.x = Mathf.Clamp(rotation.x, -maxRotation, maxRotation);
		rotation.y = Mathf.Clamp(rotation.y, -maxRotation, maxRotation);

		if(PlayerPrefs.GetInt("lowerCamera", 0) == 0)
			rotation.x = Mathf.Max(rotation.x, minHeight);
	}


	private void onProximity(RaycastHit hit){
		if(hit.collider.gameObject.tag.Equals("Vent")){
			Vector3 rot = (hit.point - orbit.transform.position) * (edgeAvoid - hit.distance);

			float x = rot.x;
			rot.x = -rot.y;
			rot.y = x;

			rotation -= rot;
			//rotation.x -= ((hit.point - orbit.transform.position)*transform.right).y * (edgeAvoid - hit.distance);
			//position -= (hit.point - missile.transform.position) * (edgeAvoid - hit.distance) * Time.deltaTime;
			lastAvoidTime = Time.time;
		}
	}

	public void onGameStart(float timer){
		StartCoroutine(startIEnum(timer));
		StartCoroutine(dimGarageSound(timer));
	}

	private IEnumerator dimGarageSound(float timer){
		AudioSource audio = GetComponent<AudioSource>();

		while(audio.volume > 0){
			audio.volume -= Time.deltaTime / timer;
			yield return 0;
		}

		yield return 0;
		audio.Stop ();
	}

	private IEnumerator startIEnum(float timer){
		float totalTime = timer;
		Light visibility = transform.GetChild(0).GetComponent<Light>();

		float _vis = visibility.range;

		float _distance = distance;
		float _forward = forwardPosition;
		float _rotx = rotation.x;
		float _roty = rotation.y;

		countdown = true;

		while(timer > 0){
			// change distance
			distance = Mathf.Lerp (_distance, 2+forwardPosition, 1 - timer / totalTime);
			forwardPosition = Mathf.Lerp (_forward, 6, 1 - timer / totalTime);

			visibility.range = Mathf.Lerp(_vis, 35, 1 - timer / totalTime);

			//y
			rotation.x = Mathf.Lerp (_rotx, 4.6f,1 - timer / totalTime);

			//x
			if(rotation.y > 180)
				rotation.y = Mathf.Lerp (_roty, 360, 1 - timer / totalTime);
			else
				rotation.y = Mathf.Lerp (_roty, 0, 1 - timer / totalTime);

			timer -= Time.deltaTime;
			yield return 0;
		}

		rotation.y = 0;

		gameStarted = true;
		transform.parent = orbit.transform;

		yield return new WaitForSeconds(1.5f);

		Missile missile = orbit.GetComponent<Missile>();
		missile.maxSpeed = 40;
		missile.speed = 5;

		orbit.GetComponent<MissileControl>().enabled = true;
		//Application.LoadLevel("Game Level");
	}

	void StopCamera(){
		gameStarted = false;
//		Debug.Log("Stop Camera");

	}
}