using UnityEngine;
using System.Collections;
using CustomMenu;

public class PlayerMenu : MonoBehaviour {

	public float xOffset, yOffset, size, width, height;

	// Use this for initialization
	void Start () {
		Rect position = new Rect(xOffset, yOffset, size, (float)Screen.width/Screen.height * size);

		MenuCheckBox lowerCamera = new MenuCheckBox("Textures/Menu/CheckBox", position, PlayerPrefs.GetInt("lowerCamera", 0) == 1);
		lowerCamera.setCheckedCommands(disableLowerCamera);
		lowerCamera.setUncheckedCommands(enableLowerCamera);
		position.y += yOffset + (float)Screen.width/Screen.height * size;

		MenuCheckBox inverseTouch = new MenuCheckBox("Textures/Menu/CheckBox", position, PlayerPrefs.GetInt("invertTouch", 0) == 1);
		inverseTouch.setCheckedCommands(invertTouch);
		inverseTouch.setUncheckedCommands(revertTouch);
		position.y += yOffset + (float)Screen.width/Screen.height * size;

		MenuCheckBox inverseVertical = new MenuCheckBox("Textures/Menu/CheckBox", position, PlayerPrefs.GetInt("invertVertical", 0) == 1);
		inverseVertical.setCheckedCommands(invertVertical);
		inverseVertical.setUncheckedCommands(revertVertical);

		//MenuItem background = new MenuItem("Textures/Menu/Options", new Rect(0,0,1,1));

		Menu menu = new Menu(new MenuColumn("Textures/Menu/Options", new Rect(0,0,1,1), lowerCamera, inverseTouch, inverseVertical));
		MenuManager.addMenu(menu);
	}

	void OnGUI(){
		if(GUI.Button(new Rect(0,0,xOffset * Screen.width, yOffset * Screen.height), "Continue")){
			MenuManager.closeMenu();
			Application.LoadLevel("Game Level");
		}
	}

	// lower camera toggle
	private void enableLowerCamera(){
		PlayerPrefs.SetInt("lowerCamera", 1);
	}

	private void disableLowerCamera(){
		PlayerPrefs.SetInt("lowerCamera", 0);
	}

	// invert touch toggle
	private void invertTouch(){
		PlayerPrefs.SetInt("invertTouch", 1);
	}

	private void revertTouch(){
		PlayerPrefs.SetInt("invertTouch", 0);
	}

	// invert vertical toggle
	private void invertVertical(){
		PlayerPrefs.SetInt("invertVertical", 1);
	}

	private void revertVertical(){
		PlayerPrefs.SetInt("invertVertical", 0);
	}
}
