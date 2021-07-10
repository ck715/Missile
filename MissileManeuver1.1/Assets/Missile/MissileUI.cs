using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Missile))]
public class MissileUI : MonoBehaviour {

	public float laserDistance = 50;
	public float laserWidth = 0.15f;
	public float targettingRadius = 1;

	private float distance = 1;

	// time to warn player before collision
	public float warningTime = 1;
	public float distScaling = 0.5f;

	// store the line renderer for laser
	private LineRenderer _lineRenderer;
	private Material _laserMaterial;

	public GameObject _targetting;
	private Material _targettingMaterial;

	public Color laserColor = Color.green;
	public Color warningColor = Color.red;

	// save the missile component
	private Missile _missile;

	// save the transform component
	private Transform _transform;

	// draw text
	public static float points = 0;
	private Rect speedRect, pointsRect;
	private const int textWidth = 150;
	private const int textHeight = 20;


	private const float pointMultiplier = 0.01f;

	void Awake(){
		points = 0;
	}

	// Use this for initialization
	void Start () {
		// set parameters for the 
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.material = Resources.Load("Materials/Laser Material") as Material;
		_lineRenderer.SetWidth(laserWidth, laserWidth);
		_lineRenderer.castShadows = false;
		_lineRenderer.receiveShadows = false;

		_laserMaterial = _lineRenderer.material;
		_laserMaterial.color = laserColor;

		_targettingMaterial = _targetting.GetComponent<Renderer>().material;
		_targettingMaterial.renderQueue = 9000;

		_missile = GetComponent<Missile>();

		_transform = GetComponent<Transform>();

		speedRect = new Rect(0,0, textWidth, textHeight);
		pointsRect = new Rect(10,textHeight, textWidth, textHeight);

		StartCoroutine(projectorScaling());
	}

	void Update(){
		if(_missile.isAlive() && !GameManager.Paused)
			points += _missile.speed * _missile.speed * pointMultiplier * Time.deltaTime;
	}
	
	// LateUpdate is called once per frame after Update
	// This ensures that the line renderer is updated after the missile is rotated and before it is drawn with OnGUI()
	void LateUpdate () {
		distance = laserDistance;

		RaycastHit hit;
		// Check if there is a collidable object in front of the missile within range laserDistance
		if(Physics.SphereCast(_transform.position, targettingRadius, _transform.forward, out hit, laserDistance)){
			distance = hit.distance - 0.05f;

			// if object is within warning distance, change laser color
			if(distance < warningTime * _missile.speed){
				_laserMaterial.color = warningColor;
				_targettingMaterial.color = warningColor;
			}else{
				// if closest object is not within warning distance, reset laser color
				_laserMaterial.color = laserColor;
				_targettingMaterial.color = laserColor;
			}
		}
		else{
			// no object was detected, so change laser color back to normal
			_laserMaterial.color = laserColor;
			_targettingMaterial.color = laserColor;

			// set distance to max laser distance
			distance = laserDistance;
		}

		// update line points
		_lineRenderer.SetPosition(0, _transform.position);
		_lineRenderer.SetPosition(1, _transform.position + _transform.forward * distance);
	}

	void OnGUI(){
		GUI.Label(pointsRect, "Score: " + points);
		GUI.Label(speedRect, "Speed: " + _missile.speed);
	}

	private IEnumerator projectorScaling(){
		bool enlarge = true;

		float offset = 0;
		float rate = 0.5f;
		float maxOffset = 0.25f;
		Vector3 scale = _targetting.transform.localScale;

		while(true){
			if(enlarge){
				// add to offset if enlarging
				offset += rate * Time.deltaTime;

				// if offset is too large, reverse
				if(offset > maxOffset)
					enlarge = !enlarge;
			}
			else{
				// subtract from offset if shrinking
				offset -= rate * Time.deltaTime;

				// if offset is too small, reverse
				if(offset < -maxOffset)
					enlarge = !enlarge;
			}

			// scale the tracker according to offset
			float scaling = (1+offset);// + (0.25f - distance / laserDistance);

			// apply scaling to tracker
			_targetting.transform.localScale = scale * scaling;

			// move the tracker to the distance from missile
			_targetting.transform.localPosition = new Vector3(0,0,distance);


			yield return 0;
		}
	}
}
