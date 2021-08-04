using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour {

	public StartButton startButton;
	public MissileEngine engine;
	public MissileBody body;
	private AudioSource source;
	public AudioSource sb;
	private AudioSource[] allAudioSources;

	// begin properties
	public float acceleration {
		get { return engine.acceleration - (body.friction * speed); }
	}
	public float turnSpeed {
		get { return 90 * body.powerSteering * engine.powerSteering; }
	}
	public float turnSlowPercent {
		get { return body.turnDecel; } // the f at the end specifies that this is a float and not a double
	}

	// begin variables
	public float speed = 0;
	public float maxSpeed = 500;
	
	public float deathTime = 1;

	public float minSpeedScaling = 8f;

	private bool isDead = false;
	
	public GameObject rocketFlame;
	public GameObject rocketExplosion;



	// particle system from rocketFlame;
	private ParticleSystem _flameSystem;

	// keep track of missile's physics components
	private Rigidbody _rigidbody; // rigidbody controls collisions and velocity
	private Transform _transform; // transform keeps track of position, rotation, and scale

	// Is missile turning?
	private bool isTurning = false;

	private Vector3 rotation = Vector3.zero;

	public static Missile instance;

	protected float points = 0;
	private const float pointMultiplier = 0.01f;

	private Text pointText;



	void Awake(){
		instance = this;
		points = 0;
		pointText = GameObject.FindGameObjectWithTag("Points UI").GetComponent<Text>();

		updateModel();
	}

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
		_transform = GetComponent<Transform>();

		//PlayerPrefs.SetString("Engine Equipped", "NikiNiki Engine");

		// get particle systems;
		_flameSystem = rocketFlame.GetComponent<ParticleSystem>();
		if(!GameManager.Paused)
			_flameSystem.Play();

		// prevent screen from going to sleep
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		body.setMissile(this);
		engine.setMissile(this);

		points = 0;
	}
	
	// Update is called once per frame
	void Update () {


		if(GameManager.Paused){
		//	_flameSystem.Pause();
			return;
		}
		//else
		//	_flameSystem.Play();

		if(isDead){
			speed = 0;
		}

		// Copied from MissileUI
		if(isAlive() && !GameManager.Paused){

			points += speed * speed * pointMultiplier * Time.deltaTime;
			pointText.text = "Points: " + (int)points;
		}

		if(!isTurning)
			// smoothly increase the speed by acceleration over the course of a second
			speed += acceleration * Time.deltaTime;

		if(speed > maxSpeed)
			speed = maxSpeed;

		_rigidbody.velocity = speed * _transform.forward;

		// reset isTurning for next frame
		isTurning = false;

		//_transform.eulerAngles = rotation;
	}

	public void OnCollisionEnter(Collision collision){
		// don't explode if missile hit a trigger

		if(!collision.collider.isTrigger){
//			Debug.Log("Collision with: " + collision.gameObject.name);

			isDead = true;


			PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + (int)points);
			if(PlayerPrefs.GetInt("High Score", 0) < (int)points)
				PlayerPrefs.SetInt("High Score", (int)points);

			PlayerPrefs.SetInt("Score",(int)points);

			// open High Score menu
			Instantiate(Resources.Load ("Menu/High Score Canvas"));

			// start a coroutine to handle the explosion
			// coroutines allow wait commands which makes effects over time much easier
			StartCoroutine(explode());

			allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
			foreach( AudioSource audioS in allAudioSources) {
				audioS.Stop();
			}

			StartCoroutine(crash());
		}
	}

	// play crash sound and dim volume over time
	private IEnumerator crash(){
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();

		yield return new WaitForSeconds(0.2f);

		while(audio.volume > 0){
			audio.volume -= Time.deltaTime * 2;
			yield return 0;
		}
	}


	// turn is called by MissileControl
	public void turn(Vector2 direction){
		if(GameManager.Paused || isDead)
			return;

		if(direction.magnitude >= 1)
			direction.Normalize();

		body.onTurn(ref direction);
		engine.onTurn(ref direction);

		direction = direction * modifiedTurnRate();

		//rotation.x += direction.y * Time.deltaTime;
		//rotation.y += direction.x * Time.deltaTime;

		//_transform.Rotate(Vector3.up * direction.x * Time.deltaTime);
		//_transform.Rotate(Vector3.right * direction.y * Time.deltaTime);

		//Vector3 rot = _transform.eulerAngles;
		//rot.z = 0;
		//_transform.eulerAngles = rot;

		_transform.Rotate(new Vector3(direction.y, direction.x,0) * Time.deltaTime);
		//_transform.rotation = _transform.rotation * Quaternion.Euler(new Vector3(0, direction.x,0) * Time.deltaTime);
/*		Vector3 rot = _transform.rotation.eulerAngles;
		rot.z = 0;
		_transform.rotation = Quaternion.Euler(rot);*/

		// slow down while turning
		speed -= (turnSlowPercent * Time.deltaTime) * speed;

		// missile is turning
		isTurning = true;
	}

	public void drift(Vector2 direction){
		if(direction.magnitude > 1)
			direction.Normalize();

		/*Vector3 drifting = (_transform.up + _transform.right) *modifiedTurnRate() / 1000f;
		drifting.Scale(new Vector3(direction.x, direction.y));

		GetComponent<Rigidbody>().MovePosition(_transform.position + drifting.x * _transform.right + drifting.y * _transform.up);
		*/

		direction *= modifiedTurnRate() / 1000f;
		
		GetComponent<Rigidbody>().MovePosition(_transform.position + direction.x * _transform.right + direction.y * _transform.up);
	}

	void LateUpdate(){
		if(isDead)
			return;

		// increase min speed by 1/8 percent per point in score
		speed = Mathf.Max (speed, 1+(MissileUI.points + minSpeedScaling) / minSpeedScaling);
	}

	public int getPoints(){
		return (int)points;
	}

	// explosion effect.  IEnumerator makes this a coroutine which requires a special method call to start
	private IEnumerator explode(){
		// stop flame particle system
		//_flameSystem.Stop();

		// stop the missile
		speed = 0;
		//acceleration = 0;

		// create explosion if the explosion effect is set (not null)
		if(rocketExplosion){
			Instantiate(rocketExplosion, _transform.position, _transform.rotation);
		}
		
		// destroy missile and unparent the camera so that it is not deleted as well
		gameObject.BroadcastMessage("StopCamera");
		//Camera.main.transform.parent = null;
		Destroy(GetComponent<MeshRenderer>());
		
		// wait to show death
		yield return new WaitForSeconds(deathTime);

		// restart level
		//Application.LoadLevel("Garage");
	}

	public bool isAlive(){
		return !isDead;
	}

	public float modifiedTurnRate(){
		return turnSpeed * speed / 10f;
	}

	public void updateModel(){

		if(engine)
			Destroy(engine.gameObject);
		if(body)
			Destroy(body.gameObject);

		// load missile parts
		GameObject _body = Instantiate(Resources.Load("Prefabs/Bodies/" + PlayerPrefs.GetString("Body Equipped", "Impaler")), transform.position, transform.rotation) as GameObject;
		if(_body){
			_body.transform.parent = transform;
			body = _body.GetComponent<MissileBody>();
		}

		GameObject _engine = Instantiate(Resources.Load("Prefabs/Engines/" + PlayerPrefs.GetString("Engine Equipped", "Novice Engine")), transform.position, transform.rotation) as GameObject;
		if(_engine){

			_engine.transform.parent = transform;
			engine = _engine.GetComponent<MissileEngine>();
			engine.setMissile(this);
		}
		

	}

	public static void equipParts(){
		instance.updateModel();
	
	}
}
