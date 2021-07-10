using UnityEngine;
using System.Collections;

public class LevelStartUI : MonoBehaviour {

	public float displayTime = 1;
	public float shrinkTime = 0.25f;

	public float numSize = 0.5f;
	public float iconSize = 0.1f;
	public float iconOffset = 0.05f;

	public Texture2D number1, number2, number3;
	public Texture2D shopIcon;
	public Texture2D garageIcon;
	public Texture2D menuIcon;

	private Texture2D number;

	private Rect numRect, shopRect, garageRect, menuRect;
	private GUIStyle buttonStyle;

	// Use this for initialization
	void Start () {
		numRect = new Rect(numSize/2 * Screen.width, Screen.height/2 - numSize * Screen.width / 2, numSize * Screen.width, numSize * Screen.width);
		//shopRect = new Rect(iconOffset * Screen.width, iconOffset * Screen.height, iconSize * Screen.width, iconSize * Screen.width);
		//garageRect = new Rect(Screen.width - (iconOffset + iconSize) * Screen.width, iconOffset * Screen.height, iconSize * Screen.width, iconSize * Screen.width);
		//menuRect = new Rect(Screen.width - (iconOffset + iconSize) * Screen.width, Screen.height - iconOffset * Screen.height - iconSize * Screen.width, iconSize * Screen.width, iconSize * Screen.width);

		StartCoroutine(displayNumbers());

		GameManager.Paused = true;
	}

	void OnGUI(){
		if(buttonStyle == null)
			buttonStyle = GUI.skin.label;

		GUI.DrawTexture(numRect, number);

//		if(GUI.Button(shopRect, shopIcon, buttonStyle))
//			Debug.Log("Go to Shop");

//		if(GUI.Button(garageRect, garageIcon, buttonStyle))
//			Debug.Log("Go to Garage");

//		if(GUI.Button(menuRect, menuIcon, buttonStyle))
//			Application.LoadLevel("PlayerMenu");
	}

	private IEnumerator  displayNumbers(){
		number = number1;
		yield return new WaitForSeconds(1);
		number = number2;
		yield return new WaitForSeconds(1);
		number = number3;
		yield return new WaitForSeconds(1);
		GameManager.Paused = false;
		Destroy(this);
	}
}
