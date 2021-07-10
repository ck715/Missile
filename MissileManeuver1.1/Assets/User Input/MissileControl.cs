using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Missile))]
public class MissileControl : MonoBehaviour {

	private Missile _missile;

	private Vector2 startAccel;

	// cutoff for turning if no mouse/tap
	private const float turnCutOff = 0.25f;
	private const float accelCutOff = 0.2f;
	private const float accelScale = 5f;

	private enum inputType { touch, tilt, conventional };

	private bool turning = false;
	private float simpleCutoff = 0.5f;

	private Vector3[] mouseDrag;
	private const int mouseDragFrames = 60;
	private float mouseDragTurn = 12.2f;

	private Coroutine turningRoutine = null;

	// Use this for initialization
	void Start () {
		_missile = GetComponent<Missile>();

		startAccel = new Vector2(-Input.acceleration.x, Input.acceleration.y);

		mouseDrag = new Vector3[mouseDragFrames];
		for(int i = 0; i < mouseDrag.Length; i++){
			mouseDrag[i] = Vector3.zero;
		}

		mouseDragTurn = Screen.height / 45f;

		// test
		//PlayerPrefs.SetInt("Simple Turn", 1);

		// default settings
		if(PlayerPrefs.GetInt("Simple Turn", -1) == -1){
			// simple turning on mobile, otherwise use normal turning
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WP8Player)
				PlayerPrefs.SetInt("Simple Turn", 1);
			else
				PlayerPrefs.SetInt("Simple Turn", -1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Paused)
			return;

		Vector2 accelerometer = new Vector2(-Input.acceleration.x, Input.acceleration.y) - startAccel;

		Vector2 turn = Vector2.zero;
		inputType input = inputType.conventional;



		if(PlayerPrefs.GetInt("Simple Turn", 1) == 1){
			if(!Input.GetMouseButton(0)){
				for(int i = 0; i < mouseDrag.Length; i++){
					mouseDrag[i] = Vector3.zero;
				}
			}
			else{
				for(int i = 1; i < mouseDrag.Length; i++){
					mouseDrag[i] = mouseDrag[i-1];
				}
				mouseDrag[0] = Vector3.zero;
			}
		}

		// if left mouse button is down
		if(Input.GetMouseButton(0)){
			// set mousePos center of screen to 0,0,0
			Vector3 mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height/2, 0);
			input = inputType.touch;

			if(PlayerPrefs.GetInt("Simple Turn", 1) == 1){
				mouseDrag[0] = mousePos;

				Vector3 difference = Vector3.zero;
				for(int i = 1; i < mouseDrag.Length; i++){
					if(mouseDrag[i] != Vector3.zero){
						Vector3 d = mousePos - mouseDrag[i];
						if(d.magnitude > difference.magnitude)
							difference = d;
					}
				}
				//difference.Scale(new Vector3(Screen.width / 2, Screen.height/2, 0));
				if(difference.magnitude > mouseDragTurn)
					turn = difference;
			}
			else{
				turn = new Vector2(mousePos.x, -mousePos.y);
				startAccel = new Vector2(-Input.acceleration.x, Input.acceleration.y);
			}
		}

		// if mobile and not touching screen, use accelerometer
		else if(accelerometer.magnitude > accelCutOff && PlayerPrefs.GetInt("Simple Turn", 1) != 1){
			turn = accelScale * accelerometer;

			input = inputType.tilt;
		}

		// if no accelerometer, use Xbox controller, WSAD, or arrow keys
		else{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = -1* Input.GetAxis("Vertical");

			if(Mathf.Abs(horizontal) > turnCutOff || Mathf.Abs(vertical) > turnCutOff){
				turn = new Vector2(horizontal, vertical);
				input = inputType.conventional;
			}
		}
		

		if(turn != Vector2.zero){
			if(PlayerPrefs.GetInt("invertTouch", 0) == 1 && input == inputType.touch){
				turn.x *= -1;
				turn.y *= -1;
			}

			if(PlayerPrefs.GetInt("invertVertical", 0) == 1)
				turn.y *= -1;

			if(PlayerPrefs.GetInt("Simple Turn", 1) == 1){
				if(!turning)
					turningRoutine = StartCoroutine(simpleTurn(turn));
			}else
				// normal turn
				_missile.turn(turn);
		}

		if(PlayerPrefs.GetInt("Simple Turn", 1) == 1){
			Vector2 drift = -accelScale * accelerometer;
			if(PlayerPrefs.GetInt("InvertVertical", 0) == 1)
				drift.y *= -1;
			if(PlayerPrefs.GetInt("invertTouch", 0) == 1){
				drift.y *= -1;
				drift.x *= -1;
			}

			_missile.drift(drift);
			//_missile.drift(new Vector2(1,0));
		}
	}

	public void correctTurning(Quaternion rotation){
		if(PlayerPrefs.GetInt("Simple Turn", 1) == 1){
			StopCoroutine("simpleTurn");
			transform.rotation = rotation;
			turning = false;
		}
	}

	private IEnumerator simpleTurn(Vector2 angle){
		if(Mathf.Abs(angle.x) > simpleCutoff){
			if(Mathf.Abs(angle.x) > Mathf.Abs(angle.y)){
				angle.y = 0;

				if(angle.x < 0)
					angle.x = -1;
				else
					angle.x = 1;
			}
			else{
				angle.x = 0;

				if(angle.y < 0)
					angle.y = -1;
				else
					angle.y = 1;
			}
		}
		else if(Mathf.Abs(angle.y) > simpleCutoff){
			angle.x = 0;

			if(angle.y < 0)
				angle.y = -1;
			else
				angle.y = 1;
		}
		else
			turning = true;

		if(!turning){
			turning = true;

			float a = 90;

			while(a >0){
				if(Time.deltaTime * _missile.modifiedTurnRate()  > a)
					angle *= a;

				a -= Time.deltaTime * _missile.modifiedTurnRate();
				_missile.turn (angle);

				yield return 0;
			}

		}
		turning = false;

		yield return 0;
	}
}
